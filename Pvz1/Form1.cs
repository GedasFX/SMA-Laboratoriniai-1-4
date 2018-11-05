using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Pvz1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            // Graph size declaration
            _canvasF = new GraphDimensions(-4, 4, -6, 6);
            _canvasG = new GraphDimensions(-10, 10, -6, 6);
            _canvasH = new GraphDimensions(0, 10, -60, 60);
            InitializeComponent();
        }

        // ---------------------------------------------- PUSIAUKIRTOS METODAS ----------------------------------------------

        private readonly GraphDimensions _canvasF, _canvasG, _canvasH; // Canvas dimensions
        private const int MaxIteration = 1000; // Maximum iteration number, in case result is undefinable.
        private const int ThreadSleepTime = 50; // Lower the time, faster the execution.
        private const double GraphStep = 0.05, ScanStep = 0.2;
        private const double Precision = 1e-12;

        private const double PolyStartX = -3.33, PolyEndX = 3.83;

        private GraphDimensions _currentCanvas;
        private Series _x1X2, _xMid; // naudojama atvaizduoti f-jai, šaknų rėžiams ir vidiniams taškams. Legend

        /// <summary>
        /// Function f(x)
        /// </summary>
        /// <param name="x">Argument of the function</param>
        /// <returns>Evaluated function result for a given x value</returns>
        private static double F(double x)
        {
            return 1.03 * Math.Pow(x, 5) - 2.91 * Math.Pow(x, 4) - 1.44 * Math.Pow(x, 3) + 5.56 * Math.Pow(x, 2) - 0.36 * x - 1.13;
        }

        /// <summary>
        /// Function g(x)
        /// Can be evaluated only in range [-10; 10]
        /// </summary>
        /// <param name="x">Argument of the function</param>
        /// <returns>Evaluated function result for a given x value</returns>
        private static double G(double x)
        {
            return x * Math.Pow(Math.Cos(x), 2) - x * x / 4;
        }

        private static double H(double c)
        {
            var power = Math.Pow(Math.E, -4 * c);
            return 100 * power + 7.35 / c * (power - 1) - 49;
        }

        /// <summary>
        /// Clears previous data and prepares canvas with given dimensions
        /// </summary>
        private void PrepareCanvas(GraphDimensions dimensions)
        {
            ClearForm();
            _currentCanvas = dimensions;
            PreparareForm((float)dimensions.MinX, (float)dimensions.MaxX, (float)dimensions.MinY, (float)dimensions.MaxY);
        }

        /// <summary>
        /// Draws the function on the canvas
        /// </summary>
        /// <param name="name">Name of the series</param>
        /// <param name="start">Starting point of graphing</param>
        /// <param name="end">End point of graphing</param>
        /// <param name="step">Step. The smaller value, the more accurate the function becomes</param>
        /// <param name="function">Function to graph</param>
        private void AddSeries(string name, double start, double end, double step, Func<double, double> function)
        {
            var graph = chartGraph.Series.Add(name);
            graph.ChartType = SeriesChartType.Line;

            var currentPos = start;
            while (currentPos <= end)
            {
                graph.Points.AddXY(currentPos, function(currentPos));
                currentPos += step;
            }
            graph.BorderWidth = 3;
        }

        /// <summary>
        /// Adds a legend to the graph
        /// </summary>
        private void AddLegend()
        {
            _x1X2 = chartGraph.Series.Add("x1, x2");
            _x1X2.MarkerStyle = MarkerStyle.Circle;
            _x1X2.ChartType = SeriesChartType.Point;
            _x1X2.ChartType = SeriesChartType.Line;
            _x1X2.MarkerSize = 8;

            _xMid = chartGraph.Series.Add("XMid");
            _xMid.MarkerStyle = MarkerStyle.Circle;
            _xMid.MarkerSize = 8;
        }

        // Mygtukas "Pusiaukirtos metodas" - ieškoma šaknies, ir vizualizuojamas paieškos procesas
        private void BtnDivision_Click(object sender, EventArgs e)
        {
            GraphFunction(DivisionMethod);
        }

        private void BtnScan_Click(object sender, EventArgs e)
        {
            GraphFunction(ScanMethod);
        }

        private void BtnStrings_Click(object sender, EventArgs e)
        {
            GraphFunction(ChordsMethod);
        }

        private void GraphFunction(Action<Func<double, double>, double, double> method)
        {
            // Selects the canvas dimensions based on radio buttons
            PrepareCanvas(radioF.Checked ? _canvasF : radioG.Checked ? _canvasG : _canvasH);

            // Display the legend
            AddLegend();

            // Draw the functions on the canvas
            if (radioF.Checked)
            {
                AddSeries("F(x)", PolyStartX, PolyEndX, GraphStep, F);
                Task.Factory.StartNew(() => FindRoots(method, F, PolyStartX, PolyEndX));
            }
            else if (radioG.Checked)
            {
                AddSeries("G(x)", -10, 10, GraphStep, G);
                Task.Factory.StartNew(() => FindRoots(method, G, -10, 10));
            }
            else
            {
                AddSeries("H(x)", 0, 10, 0.01, H);
                Task.Factory.StartNew(() => FindRoots(method, H, 0.01, 10));
            }
        }

        /// <summary>
        /// Finds roots of a given function within a gived range
        /// </summary>
        /// <param name="scanMethod"></param>
        /// <param name="function"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void FindRoots(Action<Func<double, double>, double, double> scanMethod, Func<double, double> function, double start, double end)
        {
            for (var i = start; i < end; i += 0.2)
            {
                Thread.Sleep(ThreadSleepTime);

                var isoStart = i; // Isolation start and end to search the root for
                var isoEnd = i + ScanStep;

                MovePoints(isoStart, _currentCanvas.MinX - 1, isoEnd, function); // Scanning animation

                if (Math.Sign(function(isoStart)) == Math.Sign(function(isoEnd)))
                    continue;

                // Root discsovered. Zone in on it
                scanMethod(function, isoStart, isoEnd);

                ClearPoints();
            }
        }

        /// <summary>
        /// Finds the root of function via division method.
        /// </summary>
        /// <param name="function">Function to analyze</param>
        /// <param name="isoStart">Starting point</param>
        /// <param name="isoEnd">End point</param>
        private void DivisionMethod(Func<double, double> function, double isoStart, double isoEnd)
        {
            var start = isoStart;
            var end = isoEnd;
            var iteration = 0;
            double isoMid; // izoliacijos intervalo pradžia ir galas, vidurio taškas
            do
            {
                isoMid = (isoStart + isoEnd) / 2;
                Thread.Sleep(ThreadSleepTime);

                if (Math.Sign(function(isoStart)) != Math.Sign(function(isoMid)))
                {
                    isoEnd = isoMid;
                }
                else
                {
                    isoStart = isoMid;
                }

                iteration = iteration + 1;

                MovePoints(isoStart, isoMid, isoEnd, function);

            } while (Math.Abs(function(isoMid)) > Precision && iteration <= MaxIteration);
            PrintResult("Division", iteration, isoMid, start, end);
        }

        private void ScanMethod(Func<double, double> function, double isoStart, double isoEnd)
        {
            var step = ScanStep / 10;
            var current = isoStart;
            var value = function(current);
            var nextValue = function(current + step);
            var iteration = 0;
            while (Math.Abs(value) > Precision && iteration < MaxIteration && current <= isoEnd)
            {
                // Find next intersection
                while (Math.Sign(value) == Math.Sign(nextValue))
                {
                    Thread.Sleep(ThreadSleepTime);
                    MovePoints(current, current, current, function);
                    value = nextValue;
                    current += step;
                    nextValue = function(current + step);
                    iteration++;
                }

                step /= 10;
                nextValue = function(current + step);
                iteration++;
            }

            PrintResult("Scan", iteration, current, isoStart, isoEnd);
        }

        private void ChordsMethod(Func<double, double> function, double isoStart, double isoEnd)
        {
            var start = isoStart;
            var end = isoEnd;
            var iteration = 0;
            double isoMid; // izoliacijos intervalo pradžia ir galas, vidurio taškas
            do
            {
                Thread.Sleep(ThreadSleepTime);
                var xm = function(isoStart);
                var xn = function(isoEnd);
                var k = Math.Abs(xm / xn);
                isoMid = (isoStart + k * isoEnd) / (1 + k);
                Thread.Sleep(ThreadSleepTime);

                if (Math.Sign(function(isoStart)) == Math.Sign(function(isoMid)))
                {
                    isoStart = isoMid;
                }
                else
                {
                    isoEnd = isoMid;
                }

                iteration = iteration + 1;

                MovePoints(isoMid, isoMid, isoMid, function);

            } while (Math.Abs(function(isoMid)) > Precision && iteration <= MaxIteration);
            PrintResult("Chords", iteration, isoMid, start, end);
        }

        private void SimpleIterationsMethod(Func<double, double> function, double isoStart, double isoEnd)
        {
            var iteration = 0;
            var x = isoStart;

            while (Math.Abs(function(x)) > Precision && iteration <= MaxIteration)
            {
                Thread.Sleep(ThreadSleepTime);
                x += 1 / GetAlpha(function, x) * function(x);
                MovePoints(x, x, x, function);
                iteration++;
            }
            PrintResult("Iterations", iteration, x, isoStart, isoEnd);
        }

        private static double GetAlpha(Func<double, double> f, double x)
        {
            return -Derivative(f, x);
        }

        private static double Derivative(Func<double, double> f, double x)
        {
            return (f(x - 2 * Precision) - 8 * f(x - Precision) + 8 * f(x + Precision) - f(x + 2 * Precision)) / (12 * Precision);
        }

        private void PrintResult(string name, int iterationCnt, double x, double xs, double xe)
        {
            textOutputBox.AppendText($"{name,10} method: {iterationCnt,3} iterations. X = {(x < 0 ? "" : " ")}{x:0.000000000}. Start = {(xs < 0 ? "" : " ")}{xs:0.00}, End = {(xe < 0 ? "" : " ")}{xe:0.00}\n");
        }

        private void BtnIterations_Click(object sender, EventArgs e)
        {
            GraphFunction(SimpleIterationsMethod);
        }

        // ---------------------------------------------- KITI METODAI ----------------------------------------------

        private void MovePoints(double startPos, double midPos, double endPos, Func<double, double> func)
        {
            ClearPoints();

            _x1X2.Points.AddXY(startPos, func(startPos));
            _x1X2.Points.AddXY(endPos, func(endPos));
            _xMid.Points.AddXY(midPos, 0);
        }

        private void ClearPoints()
        {
            _x1X2.Points.Clear();
            _xMid.Points.Clear();
        }

        private void BtnOptimization_Click(object sender, EventArgs e)
        {
            new Optimization(this).Run();
        }

        // ---------------------------------------------------------------------------------------------------------------

        private void BtnGauss_Click(object sender, EventArgs e)
        {
            new GaussianElimination(this).Run();
        }

        private void ButBroiden_Click(object sender, EventArgs e)
        {
            new BroydenMethod(this).Run();
        }

        private void BtnInterpolation_Click(object sender, EventArgs e)
        {
            new Interpolation(this, radioLinear, radioCiobyscev).Run();
        }

        /// <summary>
        /// Uždaroma programa
        /// </summary>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Išvalomas grafikas ir consolė
        /// </summary>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            textOutputBox.Clear();
            ClearForm();
        }


        public void ClearForm()
        {
            chartGraph.Series.Clear();
        }

       
        public void OutputText(string text)
        {
            textOutputBox.AppendText(text);
        }

        public Chart GetChart()
        {
            return chartGraph;
        }
    }
}
