using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Pvz1
{
    internal class Interpolation
    {
        private readonly Form1 _form1;
        private readonly RadioButton _radioLinear;
        private readonly RadioButton _radioCiobyscev;

        private const int PointCount = 30;

        public Interpolation(Form1 form1, RadioButton radioLinear, RadioButton radioCiobyscev)
        {
            _form1 = form1;
            _radioLinear = radioLinear;
            _radioCiobyscev = radioCiobyscev;
        }

        public void Run()
        {
            DrawGraph();
        }

        public double F(double x) => Math.Log(x, Math.E) / (Math.Sin(2 * x) + 2.5);

        public void DrawGraph()
        {
            var chart = _form1.GetChart();
            var series = chart.Series.Add("F(x)");
            series.ChartType = SeriesChartType.Line;
            var step = (10.0 - 2) / (PointCount - 1);
            for (var x = 2.0; x <= 10; x += step)
            {
                series.Points.AddXY(x, F(x));
            }
        }
    }
}