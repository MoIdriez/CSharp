using System;
using System.Threading;
using Engine.Matlab;

namespace ConsoleInterface
{
    public class MatlabServer
    {
        private Thread _serverThread;
        private Thread _messageThread;

        public void Run()
        {
            Console.WriteLine("== MATLAB SERVER ==");
            Console.Write("Use default settings? (y/n) :");

            TcpServer server;

            if (Console.ReadLine().ToUpper().Equals("Y"))
                server = new TcpServer("127.0.0.1", 8080);
            else
            {
                Console.Write("IP: ");
                var ip = Console.ReadLine();
                
                Console.Write("PORT: ");
                var port = int.Parse(Console.ReadLine());

                server = new TcpServer(ip, port);
            }


            _serverThread = new Thread(server.Run);
            _serverThread.Start();

            _messageThread = new Thread(() => DisplayMessages(server));
            _messageThread.Start();

            var counter = 0;
            while (true)
            {
                var msg = counter + "," + counter + "," + counter;// + Environment.NewLine;
                server.Data.Enqueue(msg);
                counter++;
                Thread.Sleep(1000);
            }
        }

        public static void DisplayMessages(TcpServer server)
        {
            while (true)
            {
                string message;
                if (server.Messages.TryDequeue(out message))
                    Console.WriteLine(message);
            }
        }
    }
}
