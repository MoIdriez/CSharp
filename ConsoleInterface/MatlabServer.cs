using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Engine.Matlab;

namespace ConsoleInterface
{
    public class MatlabServer
    {
        private Thread _serverThread;

        public void Run()
        {
            Console.WriteLine("== MATLAB SERVER ==");
            Console.Write("IP: ");
            var ip = Console.ReadLine();

            Console.Write("PORT: ");
            var port = int.Parse(Console.ReadLine());

            var server = new TcpServer(ip, port);

            _serverThread = new Thread(server.Run);
            _serverThread.Start();

            while (true)
            {
                string message;
                if (server.Messages.TryDequeue(out message))
                    Console.WriteLine(message);
            }
        }
    }
}
