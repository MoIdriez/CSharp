using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.WebService;

namespace ConsoleInterface
{
    class WebServiceInterface
    {
        public void Run()
        {
            var uri = "http://localhost:8080";
            var host = new WebHost(uri);
            host.Run();
            Console.WriteLine($"Service available at {uri}");
            Console.WriteLine("Press <enter> to stop the service");
            Console.ReadLine();
            host.Stop();
        }
    }
}
