using System;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace Pvz1
{
    internal class BroydenMethod
    {
        private readonly Form1 _form1;

        public BroydenMethod(Form1 form1)
        {
            _form1 = form1;
        }

        public void Run()
        {
            var xVec = Vector<double>.Build.DenseOfArray(new[] { -2.0, 1.0 });
            var bMtx = Matrix<double>.Build.DenseIdentity(2);

//            var r = MathNet.Numerics.RootFinding.Broyden.FindRoot(F, new[] { 2.0, 1, 3, 4 }, 1e-12, 1000);
//            _form1.OutputText($"{r[0]} {r[1]} {r[2]} {r[3]}");

            var it = 0;
            while (ErrFunc(xVec) > 1e-12)
            {
                var sVec = -bMtx.Inverse() * F(xVec);   // delta x  deltax = -A \ ff
                var x1 = xVec + sVec;                   // next x   x1 = x + deltax
                var yVec = F(x1) - F(xVec);             // delta y  ff1 - ff
                xVec = x1;
                bMtx = bMtx + (yVec - bMtx * sVec).ToColumnMatrix() * sVec.ToRowMatrix() * (sVec.ToRowMatrix() * sVec).ToRowMatrix().Inverse()[0,0];
                it++;
                //A = A + (ff1 - ff - A * deltax) * deltax' / (deltax' * deltax);
                //B = B + (y - Bs)s'/(s's)
            }
            _form1.OutputText($"{xVec[0]} {xVec[1]} {it}");
        }

        // Calculates the error value of the function
        double ErrFunc(Vector<double> x)
        {
            var opt = F(x);
            var ans = 0.0;
            for (int i = 0; i < x.Count; i++)
            {
                ans += Math.Pow(opt[i], 2);
            }

            return ans;
        }

        private Vector<double> F(Vector<double> x) => Vector<double>.Build.DenseOfArray(F(x.ToArray()));

        private static double[] F(double[] x)
        {
            var vec = new double[x.Length];

            switch (x.Length)
            {
                case 2:
                    vec[0] = x[0] * (x[1] + 2 * Math.Cos(x[0])) - 1;
                    vec[1] = Math.Pow(x[0], 4) + Math.Pow(x[1], 4) - 64;
                    break;
                case 4:
                    vec[0] = 3 * x[0] + 5 * x[1] + 3 * x[2] + x[3] - 8;
                    vec[1] = Math.Pow(x[0], 2) + 2 * x[1] * x[3] - 5;
                    vec[2] = -3 * Math.Pow(x[1], 2) - 3 * x[0] * x[1] + 2 * Math.Pow(x[3], 3) + 16;
                    vec[3] = 5 * x[0] - 15 * x[1] + 3 * x[3] + 22;
                    break;
                default:
                    throw new ArgumentException("Works with 2 or 4 arguments");
            }

            return vec;
        }
    }
}