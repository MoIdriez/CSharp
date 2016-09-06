using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatlabClient
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            MLApp.MLApp matlab = new MLApp.MLApp();

            System.Array pr = new double[4];
            pr.SetValue(11, 0);
            pr.SetValue(12, 1);
            pr.SetValue(13, 2);
            pr.SetValue(14, 3);

            System.Array pi = new double[4];
            pi.SetValue(1, 0);
            pi.SetValue(2, 1);
            pi.SetValue(3, 2);
            pi.SetValue(4, 3);

            matlab.PutFullMatrix("a", "global", pr, pi);

            System.Array prresult = new double[4];
            System.Array piresult = new double[4];

            matlab.GetFullMatrix("a", "global", ref prresult, ref piresult);

        }
    }
}
