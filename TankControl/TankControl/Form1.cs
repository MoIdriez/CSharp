using System;
using System.Collections.Concurrent;
using System.IO.Ports;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Forms;
using Engine.Data;

namespace TankControl
{
    public partial class Form1 : Form
    {
        private SerialPort _serialPort;
        private bool _isConnected;
        private bool _isStreaming;

        Thread _serialThread;
        readonly ManualResetEvent _runSerialThread = new ManualResetEvent(false);

        Thread _streamThread;
        readonly ManualResetEvent _runStreamThread = new ManualResetEvent(false);

        private Thread _tcpThread;

        ConcurrentQueue<Sample> _samples = new ConcurrentQueue<Sample>();

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStream_Click(object sender, EventArgs e)
        {
            if (_isStreaming)
            {
                _runStreamThread.Reset();
                buttonStream.Text = "Stream Data";
                DisplayResults("Info: Stopped Streaming");
            }
            else
            {
                _runStreamThread.Set();
                buttonStream.Text = "Streaming Data ...";
                DisplayResults("Info: Streaming Data Succesfully");

            }
            _isStreaming = !_isStreaming;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (_isConnected)
            {
                _serialPort.Close();
                _runSerialThread.Reset();
                buttonConnect.Text = "Connect";
                DisplayResults("Info: Disconnected Succesfully");
            }
            else
            {
                _serialPort = new SerialPort(textCom.Text, Convert.ToInt32(textBaud.Text));
                _serialPort.Open();
                _runSerialThread.Set();
                DisplayResults("Info: Connected Succesfully");
                buttonConnect.Text = "Disconnect";
            }
            _isConnected = !_isConnected;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            var message = textMessage.Text;
            SendMessage(message);
            DisplayResults("Info: Send Message Succesfully");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _serialThread = new Thread(ReceiveThread);
            _serialThread.Start();

            _streamThread = new Thread(StreamThread);
            _streamThread.Start();

            _tcpThread = new Thread(AsynchronousSocketListener.StartListening);
            _tcpThread.Start();
        }

        private void SendMotorCommands(int leftMotor, int rightMotor)
        {
            var message = leftMotor + "-" + rightMotor;
            SendMessage(message);
        }

        private void SendMessage(string message)
        {
            _serialPort.Write(message + '\n');
        }

        private void ReceiveThread()
        {
            FileGenerator.Run();
            while (true)
            {
                _runSerialThread.WaitOne(Timeout.Infinite);

                while (true)
                {
                    try
                    {
                        // receive data 
                        string msg = _serialPort.ReadLine();
                        
                        if (msg.StartsWith("Data:"))
                        {
                            var sample = new Sample(msg.Replace("Data:", ""));
                            if (sample.Millis > 0)
                            {
                                _samples.Enqueue(sample);
                                DisplayResults(sample.ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DisplayResults(ex.ToString());
                        _runSerialThread.Reset();
                        break;
                    }

                }
            }
        }

        private void StreamThread()
        {
            while (true)
            {
                _runStreamThread.WaitOne(Timeout.Infinite);
                while (true)
                {
                    try
                    {
                        Sample sample;
                        if (_samples.TryDequeue(out sample))
                        {
                            var sampleString = sample.ToString();
                            FileGenerator.Data.Enqueue(sampleString);
                        }
                    }
                    catch (Exception ex)
                    {
                        DisplayResults(ex.ToString());
                        _runStreamThread.Reset();
                        break;
                    }
                }
            }
        }

        public void DisplayResults(string value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(DisplayResults), value);
                return;
            }
            textResults.AppendText(value + Environment.NewLine);
        }

        private void buttonUp_MouseDown(object sender, MouseEventArgs e)
        {
            SendMotorCommands(150, 150);
        }

        private void buttonUp_MouseUp(object sender, MouseEventArgs e)
        {
            SendMotorCommands(0, 0);
        }

        private void buttonLeft_MouseDown(object sender, MouseEventArgs e)
        {
            SendMotorCommands(0, 150);
        }

        private void buttonLeft_MouseUp(object sender, MouseEventArgs e)
        {
            SendMotorCommands(0, 0);
        }

        private void buttonRight_MouseDown(object sender, MouseEventArgs e)
        {
            SendMotorCommands(150, 0);
        }

        private void buttonRight_MouseUp(object sender, MouseEventArgs e)
        {
            SendMotorCommands(0, 0);
        }
    }
}
