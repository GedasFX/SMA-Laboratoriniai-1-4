using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace Pvz1
{
    internal class Approximation
    {
        private readonly Form1 _form1;
        
        public Approximation(Form1 form1) => _form1 = form1;

        private const int LevelPoly = 6;

        public void Run()
        {
            // Build matices
            var aMtx = Matrix<double>.Build.Dense(12, LevelPoly);
            var bVec = Vector<double>.Build.DenseOfEnumerable(SplineInterpolation.TemperatureData.Select(d => d.Y));
            for (int i = 0; i < bVec.Count; i++)
            {
                for (int j = 0; j < LevelPoly; j++)
                {
                    aMtx[i, j] = Math.Pow(SplineInterpolation.TemperatureData[i].X, j);
                }
            }
            
            // Generats coeficient vector and draws the function
            _form1.DrawPoints(SplineInterpolation.TemperatureData, "Temperature data");
            _form1.DrawGraph(x => Interpolation.Fstar(x, aMtx.QR().Solve(bVec).ToArray()), "F(X)", 1, 11.96875, 0.03125);
        }
    }
}