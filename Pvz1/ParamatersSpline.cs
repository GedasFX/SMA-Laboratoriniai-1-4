using System;
using System.Globalization;
using System.IO;

namespace Pvz1
{
    internal class ParamatersSpline
    {
        private Form1 form1;

        public ParamatersSpline(Form1 form1) => this.form1 = form1;

        public void Run()
        {
            var borderData = ReadDataFile(@"..\..\Salis.txt");

            form1.DrawPoints(borderData, "ddd");
        }

        private static Point[] ReadDataFile(string path)
        {
            var txtOrig = File.ReadAllLines(path);
            var dX = txtOrig[0].Split('\t');
            var dY = txtOrig[1].Split('\t');
            var border = new Point[dY.Length];
            for (int i = 0; i < dX.Length; i++)
            {
                border[i] = new Point(double.Parse(dX[i], CultureInfo.GetCultureInfo("lt-LT")),
                    double.Parse(dY[i], CultureInfo.GetCultureInfo("lt-LT")));
            }

            return border;
        }
    }
}