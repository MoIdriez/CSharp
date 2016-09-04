using System;

namespace TankControl.Data
{
    public class Sample
    {
        public Sample(string line)
        {
            var tokens = line.Split(',');
            var data = Array.ConvertAll(tokens, double.Parse);
            if (data.Length != 16)
                throw new Exception("Sample creation failed. Incorrect amount of tokens");

            Millis = data[0];
            Sonar = data[1];
            Quaternion = new Quaternion(data[0], data[1], data[2], data[3]);
            Euler = new Euler(data[4], data[5], data[6]);
            YPR = new YPR(data[7], data[8], data[9]);
            AccReal = new AccReal(data[10], data[11], data[12]);
            AccWorld = new AccWorld(data[13], data[14], data[15]);
        }

        public double Millis { get; }
        public double Sonar { get; }
        public Quaternion Quaternion { get; }
        public Euler Euler { get; }
        public YPR YPR { get; }
        public AccReal AccReal { get; }
        public AccWorld AccWorld { get; }
        

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
