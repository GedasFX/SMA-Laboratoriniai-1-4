using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Pvz1
{
    internal class SplineInterpolation
    {
        private readonly Form1 _form1;
        private readonly CheckBox _cUseSpline;

        public static readonly Point[] TemperatureData = {
            new Point(1, 28.1676), new Point(2, 28.2474), new Point(3, 28.8201), new Point(4, 29.2252),
            new Point(5, 28.9777), new Point(6, 28.2100), new Point(7, 28.1201), new Point(8, 27.9829),
            new Point(9, 27.6778), new Point(10, 27.8201), new Point(11, 27.8100), new Point(12, 27.7676)
        };

        public SplineInterpolation(Form1 form1, CheckBox cUseSpline)
        {
            _form1 = form1;
            _cUseSpline = cUseSpline;
        }

        public void Run()
        {
            var dat = Interpolation.Interpolate(TemperatureData);
            if (_cUseSpline.Checked)
                _form1.DrawGraph(Interpolate, "F(x)", 1, 11.96875, 0.03125);
            else
                _form1.DrawGraph(x => Interpolation.Fstar(x, dat), "F*(x)", 1, 11.96875, 0.03125);
        }

        private static double Interpolate(double x)
        {
            var i = (int)x - 1;
            var tot = 0.0;
            var intPts = new[] { TemperatureData[i].X, TemperatureData[i + 1].X };
            for (int j = 0; j < 2; j++)
            {
                var h = Hermite(x, j, intPts);
                tot += h.Item1 * TemperatureData[i + j].Y + h.Item2 * Akima(x, i, TemperatureData);
            }

            return tot;
        }

        private static double Akima(double x, int i, IReadOnlyList<Point> p)
        {
            if (i == 0) i = 1;
            if (i == p.Count - 1) i = p.Count - 2;
            return (2 * x - p[i].X - p[i + 1].X) / ((p[i - 1].X - p[i].X) * (p[i - 1].X - p[i + 1].X)) * p[i - 1].Y +
                   (2 * x - p[i - 1].X - p[i + 1].X) / ((p[i].X - p[i - 1].X) * (p[i].X - p[i + 1].X)) * p[i].Y +
                   (2 * x - p[i - 1].X - p[i].X) / ((p[i + 1].X - p[i - 1].X) * (p[i + 1].X - p[i].X)) * p[i + 1].Y;
        }

        // Returs the U and V composition
        private static (double, double) Hermite(double x, int j, double[] arr)
        {
            var lagrange = Lagrange(x, j, arr);
            var dLagrange = LagrangeDerivative(j, arr);
            return (U(x, j, lagrange, dLagrange, arr), V(x, j, lagrange, arr));
        }

        private static double LagrangeDerivative(int j, IReadOnlyList<double> arr)
        {
            var res = 0.0;
            for (var k = 0; k < arr.Count; k++)
            {
                if (k == j)
                    continue;
                res += 1.0 / (arr[j] - arr[k]);
            }

            return res;
        }

        private static double Lagrange(double x, int j, IReadOnlyList<double> arr)
        {
            var res = 1.0;
            for (int k = 0; k < arr.Count; k++)
            {
                if (k == j)
                    continue;
                res *= (x - arr[k]) / (arr[j] - arr[k]);
            }

            return res;
        }

        // Returns the U part
        private static double U(double x, int j, double lagrange, double dLagrange, IReadOnlyList<double> arr) =>
            (1 - 2 * dLagrange * (x - arr[j])) * Math.Pow(lagrange, 2);
        private static double V(double x, int j, double lagrange, IReadOnlyList<double> arr) =>
            (x - arr[j]) * Math.Pow(lagrange, 2);
    }
}