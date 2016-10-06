using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading;
using Engine.Controllers;
using Engine.Data;

namespace Engine.Arduino
{
    public class TankInterface
    {
        private XboxController _xboxController;
        private readonly Thread _sampleThread;
        private readonly SerialCommunicator _serialCommunicator;
        private readonly ConcurrentQueue<Sample> _samples;

        public TankInterface(string port, int baudrate)
        {
            _serialCommunicator = new SerialCommunicator(port, baudrate);

            _samples = new ConcurrentQueue<Sample>();

            _sampleThread = new Thread(GenerateSamples);
            _sampleThread.Start();
        }

        ~TankInterface()
        {
            _sampleThread.Abort();
            _serialCommunicator.Close();
        }

        public void AttachController(XboxController controller)
        {
            _xboxController = controller;
            _xboxController.PropertyChanged += ControllerPropertyChanged;
            _xboxController.RunStatusChecker();
        }

        public void SendMotorCommands(int leftMotor, int rightMotor)
        {
            var message = leftMotor + "-" + rightMotor;
            _serialCommunicator.Send(message);
        }

        private void ControllerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SendMotorCommands(_xboxController.LeftTrigger, _xboxController.RightTrigger);
        }

        private void GenerateSamples()
        {
            while (true)
            {
                string message;
                if (_serialCommunicator.ReceivedData.TryDequeue(out message))
                {
                    var sample = new Sample(message);
                    _samples.Enqueue(sample);
                }
            }

        }
    }
}
