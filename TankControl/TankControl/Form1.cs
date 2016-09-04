using System;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace TankControl
{
    public partial class Form1 : Form
    {
        private SerialPort _serialPort;
        private bool _isConnected;

        Thread _t;
        readonly ManualResetEvent _runThread = new ManualResetEvent(false);

        public Form1()
        {
            InitializeComponent();
        }
        

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (_isConnected)
            {
                _serialPort.Close();
                _runThread.Reset();
                buttonConnect.Text = "Connect";
                DisplayResults("Info: Disconnected Succesfully");
            }
            else
            {
                _serialPort = new SerialPort(textCom.Text, Convert.ToInt32(textBaud.Text));
                _serialPort.Open();
                _runThread.Set();
                DisplayResults("Info: Connected Succesfully");
                buttonConnect.Text = "Disconnect";
            }
            _isConnected = !_isConnected;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            var message = textMessage.Text;
            _serialPort.Write(message + '\n');
            DisplayResults("Info: Send Message Succesfully");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _t = new Thread(ReceiveThread);
            _t.Start();
        }

        private void ReceiveThread()
        {
            while (true)
            {
                _runThread.WaitOne(Timeout.Infinite);

                while (true)
                {
                    try
                    {
                        // receive data 
                        string msg = _serialPort.ReadLine();
                        DisplayResults(msg);
                    }
                    catch
                    {
                        DisplayResults("Failed");
//                        try
//                        {
//                            this.Invoke(this.m_DelegateStopPerfmormClick, new Object[] { });
//                        }
//                        catch { }

                        _runThread.Reset();

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
            textResults.Text += value + Environment.NewLine;
        }

    }
}
