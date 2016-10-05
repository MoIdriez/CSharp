using System.Collections.Concurrent;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace Engine.Matlab
{
    public class TcpServer
    {
        private bool _interrupt;
        private readonly TcpListener _server;
        public ConcurrentQueue<string> Data;
        public ConcurrentQueue<string> Messages;
        
        public TcpServer(string ip, int port)
        {
            _server = new TcpListener(IPAddress.Parse(ip), port);
            Messages = new ConcurrentQueue<string>();
            Data = new ConcurrentQueue<string>();
        }

        public void Run()
        {
            try
            {
                _server.Start();

                byte[] bytes = new byte[256];

                while (_interrupt == false)
                {
                    Messages.Enqueue("Waiting for connection...");

                    TcpClient client = _server.AcceptTcpClient();
                    Messages.Enqueue("Connected to a user.");

                    int i;
                    NetworkStream stream = client.GetStream();

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        var data = Encoding.ASCII.GetString(bytes, 0, i);
                        Messages.Enqueue("Received: " + data);

                        string msg = string.Empty;

                        if (data.Contains("hello"))
                        {
                            msg = data;
                        }
                        else
                        {
                            string line;
                            if (Data.TryDequeue(out line))
                            {
                                msg = line;
                            }
                        }

                        byte[] sendBytes = Encoding.ASCII.GetBytes(msg);

                        // Send back a response.
                        stream.Write(sendBytes, 0, sendBytes.Length);
                        Messages.Enqueue("Sent: " + msg);
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            } catch (SocketException ex)
            {
                Messages.Enqueue(ex.ToString());
            }
        }

        public void Stop()
        {
            _interrupt = true;
        }
    }
}
