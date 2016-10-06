using System;
using System.ComponentModel;
using System.Threading;
using Engine.Controllers;

namespace ConsoleInterface
{
    public class ControllerInterface
    {
        private int counter = 0;
        private XboxController _controller;

        public void Run()
        {
            Console.WriteLine("== XBOX CONTROLLER ==");

            _controller = new XboxController();
            _controller.PropertyChanged += ControllerPropertyChanged;

            _controller.RunStatusChecker();

//            while (true)
//            {
//                var msg = _controller.GetControllerInformation();
//                Console.WriteLine(msg);
//            }

                        Console.ReadLine();
        }

        public void ControllerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var msg = _controller.GetControllerInformation();
            Console.WriteLine(msg);
            Thread.Sleep(50);

        }
    }
}
