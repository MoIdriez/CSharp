using System;
using Engine.Arduino;
using Engine.Controllers;

namespace ConsoleInterface
{
    public class ArduinoInterface
    {
        public void Run()
        {
            Console.WriteLine("== ARDUINO INTERFACE ==");
            var controller = new XboxController();

            Console.Write("Use default settings? (y/n) :");
            TankInterface tank;

            if (Console.ReadLine().ToUpper().Equals("Y"))
                tank = new TankInterface("COM3", 115200);
            else
            {
                Console.Write("PORT: ");
                var port = Console.ReadLine();

                Console.Write("BAUDRATE: ");
                var baudrate = int.Parse(Console.ReadLine());

                tank = new TankInterface(port, baudrate);
            }
            
            tank.AttachController(controller);
        }

    }
}
