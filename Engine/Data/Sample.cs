using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Data
{
    public class Sample
    {
        public Sample(string line)
        {
            var tokens = line.Split(',');
            var data = new List<double>();
            try
            {
                data = Array.ConvertAll(tokens, double.Parse).ToList();
            }
            // this is mostly because of serial gyberish 
            catch { }

            if (data.Count != 18) return;

            Line = line;
            Millis = data[0];
            Sonar = data[1];
            Quaternion = new Quaternion(data[2], data[3], data[4], data[5]);
            Euler = new Euler(data[6], data[7], data[8]);
            YPR = new YPR(data[9], data[10], data[11]);
            AccReal = new AccReal(data[12], data[13], data[14]);
            AccWorld = new AccWorld(data[15], data[16], data[17]);
        }

        public double Millis { get; }
        public double Sonar { get; }
        public Quaternion Quaternion { get; }
        public Euler Euler { get; }
        public YPR YPR { get; }
        public AccReal AccReal { get; }
        public AccWorld AccWorld { get; }
        private string Line { get; }

        public override string ToString()
        {
            return Line;
        }
    }

    public class Quaternion
    {
        public Quaternion(double w, double x, double y, double z)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }

        public double W { get; }
        public double X { get; }
        public double Y { get;}
        public double Z { get;}
    }
    public class Euler
    {
        public Euler(double alpha, double beta, double gamma)
        {
            Alpha = alpha;
            Beta = beta;
            Gamma = gamma;
        }

        public double Alpha { get;}
        public double Beta { get;}
        public double Gamma { get;}
    }
    public class YPR
    {
        public YPR(double roll, double pitch, double yaw)
        {
            Roll = roll;
            Pitch = pitch;
            Yaw = yaw;
        }

        public double Yaw { get;}
        public double Pitch { get;}
        public double Roll { get;}
    }
    public class AccReal
    {
        public AccReal(double z, double y, double x)
        {
            Z = z;
            Y = y;
            X = x;
        }

        public double X { get;}
        public double Y { get;}
        public double Z { get;}
    }
    public class AccWorld
    {
        public AccWorld(double z, double y, double x)
        {
            Z = z;
            Y = y;
            X = x;
        }

        public double X { get;}
        public double Y { get;}
        public double Z { get;}
    }
}
