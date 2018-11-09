using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace Pvz1
{
    internal class Optimization
    {
        private const int K = 2;
        private const int M = 14;
        private const int N = 14;

        private readonly Form1 _form1;
        private readonly List<Vector2> _coordinates;

        public Optimization(Form1 form1)
        {
            _form1 = form1;
            _coordinates = GenerateCoordtinates(M, 14);
        }

        public void Run()
        {
            _form1.ClearForm();
            _form1.PreparareForm(-30, 30, -30, 30);
            var chart = _form1.GetChart();

            var sM = chart.Series.Add("M");
            sM.ChartType = SeriesChartType.Point;

            foreach (var coordinate in _coordinates)
            {
                sM.Points.AddXY(coordinate.X, coordinate.Y);
            }

            var sN = chart.Series.Add("N");
            sN.ChartType = SeriesChartType.Point;
            //new Thread(() => {
            AddPoints(sM, sN);
            //}).Start();
        }

        private void AddPoints(Series seriesM, Series seriesN)
        {
            var rand = new Random();
            for (var i = 0; i < N; i++)
            {
                var point = new Vector2(rand.Next(-20, 20), rand.Next(-20, 20));
                //new Vector2(6, 8);
                var neighbours = CalculateClosestPoints(point, K);
                var average = CalculateAverageDistance(neighbours);

                var of = double.MaxValue;
                var f = TargetFunc(average, point, neighbours);
                seriesN.Points.AddXY(point.X, point.Y);
                while (of > f)
                {
                    of = f;
                    point = Vector2.Subtract(point, Vector2.Multiply(0.02f, Gradiant(average, point, neighbours)));
                    f = TargetFunc(average, point, neighbours);
                    seriesN.Points.AddXY(point.X, point.Y);
                    //Thread.Sleep(1);
                }

                //_form1.OutputText($"Done. {point} {f} {average} {Vector2.Distance(point, neighbours[0])} {Vector2.Distance(point, neighbours[1])} {Vector2.Distance(point, neighbours[1])}\n");
                seriesM.Points.AddXY(point.X, point.Y);
                _coordinates.Add(point);
                //seriesN.Points.Clear();
            }
        }

        private static List<Vector2> GenerateCoordtinates(int count, int seed)
        {
            var rand = new Random(seed);
            var res = new List<Vector2>();
            for (var i = 0; i < count; i++)
            {
                res.Add(new Vector2(rand.Next(-20, 20), rand.Next(-20, 20)));
            }

            return res;
        }

        private static double TargetFunc(float average, Vector2 vec, params Vector2[] coordinates)
        {
            return coordinates.Average(i =>
                Math.Abs(average - Math.Sqrt(Math.Pow(vec.X - i.X, 2) + Math.Pow(vec.Y - i.Y, 2))));
        }

        private static Vector2 Gradiant(float average, Vector2 point, Vector2[] coordinates)
        {
            const float precision = (float)1e-5;
            var x = (TargetFunc(average, new Vector2(point.X + precision, point.Y), coordinates)
                    - TargetFunc(average, new Vector2(point.X, point.Y), coordinates)) / precision;
            var y = (TargetFunc(average, new Vector2(point.X, point.Y + precision), coordinates)
                    - TargetFunc(average, new Vector2(point.X, point.Y), coordinates)) / precision;
            return new Vector2((float)x, (float)y);
        }

        private Vector2[] CalculateClosestPoints(Vector2 loc, int count)
        {
            var dat = new List<(Vector2, float)>();

            foreach (var coordinate in _coordinates)
            {
                var dist = Vector2.Distance(coordinate, loc);
                //(float)Math.Sqrt(Math.Pow(coordinate.X - loc.X, 2) + Math.Pow(coordinate.Y - loc.Y, 2));
                dat.Add((coordinate, dist));
            }

            return dat.OrderBy(d => d.Item2).Take(count).Select(d => d.Item1).ToArray();
        }

        private float CalculateAverageDistance(params Vector2[] coordinates)
        {
            var total = 0f;
            for (var i = 0; i < coordinates.Length; i++)
            {
                for (var j = i; j < coordinates.Length; j++)
                {
                    total += Vector2.Distance(coordinates[i], coordinates[j]);
                    //(float)Math.Sqrt(Math.Pow(coordinates[i].X - coordinates[j].X, 2) + Math.Pow(coordinates[i].Y - coordinates[j].Y, 2));
                }
            }

            return total / ((float)coordinates.Length * (coordinates.Length - 1) / 2);
        }
    }
}