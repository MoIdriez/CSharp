using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace MatlabInterface
{
    public static class FileGenerator
    {
        private static Thread _t;
        public static ManualResetEvent RunThread = new ManualResetEvent(false);
        public static ConcurrentQueue<string> Data = new ConcurrentQueue<string>();

        private static string _directory = @"D:\Unorganized\Shared";
        private static string _prefix = "tank_";
        private static int _counter;

        public static void Run()
        {
            Directory.CreateDirectory(_directory);
            _t = new Thread(GenerateFiles);
            _t.Start();
        }

        public static void Start() { RunThread.Set(); }
        public static void Stop() { RunThread.Reset(); }
        
        public static void GenerateFiles()
        {
            var temp = new List<string>();
            while (true)
            {
                try
                {
                    while (!Data.IsEmpty)
                    {
                        if (temp.Count < 100)
                        {
                            string line;
                            if (Data.TryDequeue(out line))
                                temp.Add(line.Replace('\r',' '));
                        }
                        else
                        {
                            var path = _directory + _prefix + _counter + ".txt";
                            if (!File.Exists(path))
                                File.WriteAllLines(path, temp);
                            _counter++;
                            temp = new List<string>();
                        }
                    }
                }
                catch
                {
                    RunThread.Reset();
                    throw;
                }
            }
            
        }
    }
}
