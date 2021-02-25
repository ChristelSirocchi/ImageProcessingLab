using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class Average : ConvFilter
    {
        public Average(int kernelsize) // avreage filter
        {
            Offset = 0;
            Kernel = new double[kernelsize, kernelsize];

            for (int i = 0; i < kernelsize; i++)
            {
                for (int j = 0; j < kernelsize; j++)
                {
                    Kernel[i, j] = 1;
                }
            }
            Normalize();
        }
    }
    class Gaussian : ConvFilter         // gaussian filter
    {
        public Gaussian(int kernelsize, double sigma)
        {
            Offset = 0;
            Kernel = ConvMath.GetGaussianMatrix(kernelsize, sigma);
            Normalize();
        }
    }
}
