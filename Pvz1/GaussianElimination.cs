using System;

namespace Pvz1
{
    internal class GaussianElimination
    {
        private readonly Form1 _form;

        // A[row][col]
        public readonly int[][] A =
        {
            new[] {  2,  5,  1,  2 },
            new[] { -2,  0,  3,  5 },
            new[] {  1,  0, -1,  1 },
            new[] {  0,  5,  4,  7 }
        };
        public readonly int[] B = { 14, 10, 4, 24 };

        public GaussianElimination(Form1 form)
        {
            _form = form;
        }

        public void Run()
        {

            var eMatrix = new[] {
            new double[] {  2,  5,  1,  2, 14 },
            new double[] { -2,  0,  3,  5, 10 },
            new double[] {  1,  0, -1,  1,  4 },
            new double[] {  0,  5,  4,  7, 24 }
        };

            PrintMatrix(eMatrix);
            for (int i = 0; i < eMatrix.Length - 1; i++)
            {
                FindAndSwapLeading(i, eMatrix);
                Nullify(i, eMatrix);
                _form.OutputText("===================\n");
                PrintMatrix(eMatrix);
            }
            _form.OutputText("===================\n");

            var res = Return(eMatrix);
            if (res != null)
            {
                for (int i = 0; i < res.Length; i++)
                {
                    _form.OutputText($"x{i + 1} = {(Math.Sign(res[i]) >= 0 ? " " : "")}{res[i]:00.000}, ");
                }
            }
            else
            {
                _form.OutputText("Invalid setup. No possible solution found.");
                return;
            }
            _form.OutputText("\n===================\n");
            for (int i = 0; i < res.Length; i++)
            {
                var val = 0.0;
                for (int j = 0; j < res.Length; j++)
                {
                    val += A[i][j] * res[j];
                    _form.OutputText($"{(Math.Sign(A[i][j]) >= 0 ? " " : "")}{A[i][j]:00.000} * ");
                    _form.OutputText($"{(Math.Sign(res[j]) >= 0 ? " " : "")}{res[j]:00.000} {(j < res.Length - 1 ? "+" : "")}");
                }
                _form.OutputText($"=  {val:00.000}\n");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="diagonalIndex">Index to work with</param>
        /// <param name="matrix">Matrix</param>
        private void Nullify(int diagonalIndex, double[][] matrix)
        {
            // Iterates through rows
            for (int j = diagonalIndex + 1; j < matrix.Length; j++)
            {
                var multiplier = -matrix[j][diagonalIndex] / matrix[diagonalIndex][diagonalIndex];
                // Iterates trough columns in row
                for (int k = 0; k < matrix[j].Length; k++)
                {
                    matrix[j][k] += multiplier * matrix[diagonalIndex][k];
                }
            }
        }

        private static void FindAndSwapLeading(int i, double[][] matrix)
        {
            var hv = double.MinValue; var hi = 0; // Highest value, index
            // Find hi, hv
            for (int j = i; j < matrix.Length; j++)
            {
                if (Math.Abs(matrix[j][i]) > hv)
                {
                    hv = Math.Abs(matrix[j][i]);
                    hi = j;
                }
            }

            var tmp = matrix[i];
            matrix[i] = matrix[hi];
            matrix[hi] = tmp;
        }

        private double[] Return(double[][] matrix)
        {
            var result = new double[matrix.Length];

            // Diagonal length
            var dLength = matrix.Length;
            for (int i = dLength - 1; i >= 0; i--)
            {
                result[i] = (matrix[i][dLength] - FindReturnResult(matrix, result, i)) / matrix[i][i];
                if (double.IsNaN(result[i]))
                {
                    // More than one result
                    _form.OutputText($"{i + 1}: More than one result found. Assign x{i + 1} = 0.\n");
                    result[i] = 0;
                }
                else if (double.IsInfinity(result[i]))
                {
                    // Result doesn't exist
                    return null;
                }
            }

            return result;
        }

        private static double FindReturnResult(double[][] matrix, double[] answers, int index)
        {
            var result = 0.0;
            for (int i = index; i < matrix.Length; i++)
            {
                result += matrix[index][i] * answers[i];
            }

            return result;
        }

        private void PrintMatrix(double[][] matrix)
        {
            foreach (var row in matrix)
            {
                for (int j = 0; j < row.Length - 1; j++)
                {
                    _form.OutputText($"{(Math.Sign(row[j]) >= 0 ? " " : "")}{row[j]:00.000} ");
                }
                _form.OutputText($"| {(Math.Sign(row[row.Length - 1]) >= 0 ? " " : "")}{row[row.Length - 1]:00.000}\n");
            }
        }
    }
}