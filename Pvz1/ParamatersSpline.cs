using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace Pvz1
{
    internal class ParamatersSpline
    {
        private const int InterpPoints = 100;
        private const int PointsBetweenInterps = 20;

        private Form1 form1;
        private readonly Point[] _borderData = ReadDataFile(@"..\..\Salis.txt");

        public ParamatersSpline(Form1 form1)
        {
            this.form1 = form1;
        }

        public void Run()
        {
            form1.PreparareForm(26.5f, 30.5f, 45, 49);
            // form1.DrawPoints(_borderData, "Border");

            var step = _borderData.Length / InterpPoints;

            var t = new double[InterpPoints];
            var tx = new Point[InterpPoints];
            var ty = new Point[InterpPoints];
            var dxdt = new double[InterpPoints];
            var dydt = new double[InterpPoints];
            for (int i = 0; i < InterpPoints; i++)
            {
                // Data transfer
                t[i] = step * i;
                tx[i] = new Point(step * i, _borderData[(int)t[i]].X);
                ty[i] = new Point(step * i, _borderData[(int)t[i]].Y);

                // Derivative calculation
                dxdt[i] = Akima(t[i], i, tx);
                dydt[i] = Akima(t[i], i, ty);
            }

            var series = form1.GetChart().Series.Add("Tin");
            series.ChartType = SeriesChartType.Line;
            for (int i = 0; i < InterpPoints - 1; i++)
            {
                for (int j = 0; j < PointsBetweenInterps; j++)
                {
                    // Step, (x in normal equation)
                    var t0 = t[i] + (t[i + 1] - t[i]) * j / PointsBetweenInterps;
                    var intPts = new[] { t[i], t[i + 1] };
                    var x0 = 0.0; var y0 = 0.0;
                    for (int k = 0; k < 2; k++)
                    {
                        var h = SplineInterpolation.Hermite(t0, k, intPts);
                        x0 += h.Item1 * tx[i + k].Y + h.Item2 * Akima(t[i + k], i + k, tx);
                        y0 += h.Item1 * ty[i + k].Y + h.Item2 * Akima(t[i + k], i + k, ty);
                    }

                    series.Points.AddXY(x0, y0);
                }
            }
            for (int j = 0; j < PointsBetweenInterps; j++)
            {
                var dist = 2.0;
                // Step, (x in normal equation)
                var t0 = dist * j / PointsBetweenInterps;
                var intPts = new[] { 0.0, dist };
                var h1 = SplineInterpolation.Hermite(t0, 0, intPts);
                var h2 = SplineInterpolation.Hermite(t0, 1, intPts);
                var x0 = h1.Item1 * tx[InterpPoints - 1].Y + h1.Item2 * Akima(t[InterpPoints - 1], InterpPoints - 1, tx)
                                                           + h2.Item1 * tx[0].Y + h2.Item2 * Akima(t[0], 0, tx);
                var y0 = h1.Item1 * ty[InterpPoints - 1].Y + h1.Item2 * Akima(t[InterpPoints - 1], InterpPoints - 1, ty)
                                                           + h2.Item1 * ty[0].Y + h2.Item2 * Akima(t[0], 0, ty);

                series.Points.AddXY(x0, y0);
            }
        }

        private double Akima(double x, int i, Point[] p)
        {
            if (i == 0)
                return SplineInterpolation.Akima(x, p.Length - 1, 0, 1, p);
            if (i == p.Length - 1)
                return SplineInterpolation.Akima(x, p.Length - 2, p.Length - 1, 0, p);
            return SplineInterpolation.Akima(x, i - 1, i, i + 1, p);
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