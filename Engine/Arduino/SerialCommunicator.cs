using System.Collections.Concurrent;
using System.IO.Ports;
using System.Threading;

namespace Engine.Arduino
{
    public class SerialCommunicator
    {
        private readonly Thread _receiveThread;
        private readonly SerialPort _serialPort;

        public ConcurrentQueue<string> ReceivedData;

        public SerialCommunicator(string port, int baudrate)
        {
            ReceivedData = new ConcurrentQueue<string>();

            _serialPort = new SerialPort(port, baudrate);
            _serialPort.Open();

            _receiveThread = new Thread(StreamData);
            _receiveThread.Start();
        }

        public void Send(string message)
        {
            _serialPort.Write(message + '\n');
        }

        public void Close()
        {
            _receiveThread.Abort();
            _serialPort.Close();
        }

        public void StreamData()
        {
            while (true)
            {
                string data = _serialPort.ReadLine();
                ReceivedData.Enqueue(data);
            }
        }
    }
}
