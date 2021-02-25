using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    static class ConvMath
    {
        public static double[,] GetGaussianMatrix(int size, double sigma) // calculate gaussian matrix given sigma and kernel size
        {
            double[,] matrix = new double[size, size];

            double exponent;

            int offset = (size - 1) / 2;

            double coefficient = 1.0 / (2.0 * Math.PI * Math.Pow(sigma, 2));

            for (int x = -offset; x <= offset; x++)
            {
                for (int y = -offset; y <= offset; y++)
                {
                    exponent = -(((x * x) + (y * y)) / (2 * sigma * sigma));

                    matrix[x + offset, y + offset] = coefficient * Math.Exp(exponent);
                }
            }
            return matrix;
        }

        public static double[,] GetSimmetricSquareMatrix(double[,] matrixIn, int size) // calculate simmetric matrix
        {
            double[,] matrixOut = new double[size, size];

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    matrixOut[y, x] = matrixIn[x, y];
                }

            }
            return matrixOut;

        }
    }
}
