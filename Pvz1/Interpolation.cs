using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics.LinearAlgebra;

namespace Pvz1
{
    public struct Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }
    }

    internal class Interpolation
    {
        private readonly Form1 _form1;
        private readonly CheckBox _useChebyshev;

        private const int PointCount = 16;

        public Interpolation(Form1 form1, CheckBox useChebyshev)
        {
            _form1 = form1;
            _useChebyshev = useChebyshev;

            _form1.GetChart().Series.Clear();
        }

        public void Run()
        {
            DrawGraph(_form1.GetChart(), F, "F(x)");
            var res = Interpolate(GenerateInterpolationPoints(PointCount, _useChebyshev.Checked));
            DrawGraph(_form1.GetChart(), d => Fstar(d, res), "F*(x)");
            DrawGraph(_form1.GetChart(), x => Fstar(x, res) - F(x), "G(x)");

            var r = "";
            for (var i = 0; i < res.Length; i++)
            {
                r += $"{res[i]:E3} * x^{i} + ";
            }

            r = r.Remove(r.Length - 3, 3);
            _form1.OutputText(r + '\n');
        }

        public static double F(double x) => Math.Log(x, Math.E) / (Math.Sin(2 * x) + 2.5);

        public static double Fstar(double x, double[] coeficients) => coeficients.Select((t, i) => Math.Pow(x, i) * t).Sum();

        public static void DrawGraph(Chart chart, Func<double, double> func, string name)
        {
            var series = chart.Series.Add(name);
            series.ChartType = SeriesChartType.Line;

            for (var x = 2.0; x <= 10; x += 0.01)
            {
                series.Points.AddXY(x, func(x));
            }
        }

        private Point[] GenerateInterpolationPoints(int count, bool chebyshev)
        {
            var points = new Point[count];

            if (chebyshev)
            {
                for (var i = 0; i < count; i++)
                {
                    var x = (10.0 - 2) / 2 * Math.Cos(Math.PI * (2 * i + 1) / (2 * count)) + (10.0 + 2) / 2;
                    points[i] = new Point { X = x, Y = F(x) };
                }
            }
            else
            {
                var step = (10.0 - 2) / (count - 1);
                var i = 0;
                for (var x = 2.0; i < count; x += step)
                {
                    points[i++] = new Point { X = x, Y = F(x) };
                }
            }

            return points;
        }

        public static double[] Interpolate(Point[] dataPoints)
        {
            var matrix = Matrix<double>.Build.Dense(dataPoints.Length, dataPoints.Length);
            var bVec = Vector<double>.Build.DenseOfEnumerable(dataPoints.Select(dp => dp.Y));

            for (var i = 0; i < dataPoints.Length; i++)
            {
                for (var j = 0; j < dataPoints.Length; j++)
                {
                    matrix[i, j] = Math.Pow(dataPoints[i].X, j);
                }
            }

            return matrix.LU().Solve(bVec).ToArray();
        }
    }
}