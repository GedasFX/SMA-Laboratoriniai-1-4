// ReSharper disable InconsistentNaming

using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace Pvz1
{
    internal class DifferentialEquations
    {
        private readonly Form1 _form;

        // Meaningful dt. Gives results accurate to within 1e-2
        // optimum 0.00005;
        private const double dt = 0.00005; // s

        private const double m = 0.2;   // kg
        private const double g = 9.8;   // m/(s^2)

        private const double v0 = 50;   // m/s
        private const double h0 = 100;  // m

        private const double k1 = 0.01;     // kg/m
        private const double k2 = 0.005;    // kg/m


        public DifferentialEquations(Form1 form1) => _form = form1;

        public void Run()
        {
            // Chart setup
            var chart = _form.GetChart();
            var seriesV = chart.Series.Add("v(t)");
            var seriesH = chart.Series.Add("h(t)");

            double hMaxX = 0, hMaxY = 0;

            seriesH.ChartType = SeriesChartType.Line;
            seriesV.ChartType = SeriesChartType.Line;
            
            // Initial variable conditions for test
            var v_cur = v0;
            var t = 0.0;

            var h = h0;
            var k = k1;

            // While distance is above ground, the ball has not hit the ground
            while (h > 0)
            {
                var v_prev = v_cur;
                v_cur = V(v_prev, k);
                h += v_cur * dt;
                t += dt;

                // Maximum point reached. Object started to fall
                if (Math.Sign(v_cur) != Math.Sign(v_prev))
                {
                    hMaxY = h;
                    hMaxX = t;
                    k = k2;
                }

                seriesV.Points.AddXY(t, v_cur);
                seriesH.Points.AddXY(t, h);
            }

            _form.OutputText($"Maximum: ({hMaxX:F}, {hMaxY:F}). Landing at {t:F}");
        }

        // Calculate velocity dt time after previous.
        private double V(double v_prev, double k)
        {
            return v_prev + dt * (-g - k * v_prev * Math.Abs(v_prev) / m);
        }
    }
}