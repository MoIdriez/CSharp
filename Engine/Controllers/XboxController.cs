using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using SharpDX.XInput;


namespace Engine.Controllers
{
    public class XboxController
    {
        private Thread _t;
        private Controller _controller;

        public XboxController()
        {
            _controller = new Controller(UserIndex.One);
            if (!_controller.IsConnected)
                throw new Exception("Could not find game controller");
        }

        public void RunStatusChecker()
        {
            _t = new Thread(() => CheckStatus(this, _controller));
            _t.Start();
        }

        private static void CheckStatus(XboxController xboxController, Controller controller)
        {
            while (true)
            {
                var state = controller.GetState();

                if (state.PacketNumber <= xboxController.PacketNumber) continue;

                xboxController.LeftThumbX = state.Gamepad.LeftThumbX;
                xboxController.RightThumbX = state.Gamepad.RightThumbX;
                xboxController.LeftTrigger = Convert.ToInt32(state.Gamepad.LeftTrigger);
                xboxController.RightTrigger = Convert.ToInt32(state.Gamepad.RightTrigger);
                xboxController.PacketNumber = state.PacketNumber;
            }
        }

        public string GetControllerInformation()
        {
            var state = _controller.GetState();

            var result = $"Counter: {PacketNumber}";
            result +=$"X: {LeftThumbX} Y: {LeftThumbY}" + Environment.NewLine;
            result += $"X: {RightThumbX} Y: {RightThumbX}" + Environment.NewLine;
            result += $"X: {LeftTrigger} Y: {RightTrigger}" + Environment.NewLine;
            //Buttons = string.Format("A: {0} B: {1} X: {2} Y: {3}", state.Gamepad.Buttons.ToString(), state.Gamepad.LeftThumbY);
            //result += $"{state.Gamepad.Buttons}" + Environment.NewLine;
            return result;
        }

        ~XboxController()
        {
            _controller = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Properties

        private int _packetNumber;
        public int PacketNumber
        {
            get { return _packetNumber; }
            set
            {
                if (value == _packetNumber) return;
                _packetNumber = value;
                OnPropertyChanged();
            }

        }

        public int LeftThumbX { get; set; }
        public int LeftThumbY { get; set; }
        public int RightThumbX { get; set; }
        public int RightThumbY { get; set; }
        public int LeftTrigger { get; set; }
        public int RightTrigger { get; set; }
        #endregion


    }
}
