﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            //new MatlabServer().Run();
            //new ControllerInterface().Run();
            new ArduinoInterface().Run();
        }
    }
}
