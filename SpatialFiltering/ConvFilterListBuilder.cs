using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class ConvFilterBuilder : IBuilder
    {
        private List<IConvFilter> ConvFilterList = new List<IConvFilter>();
        private int min;
        private int max;
        private int[] histo;
        private int width;
        private int height;
        private int intensity;
        private int lower;
        private int upper;
        private bool choice;
        private double constant;
        private int kernelSize;

        public ConvFilterBuilder(ParseManager parser, ImageFile inputImage)
        {
            this.min = inputImage.ImageStats.min;
            this.max = inputImage.ImageStats.max;
            this.histo = inputImage.Histo;
            this.width = inputImage.Bmp.Width;
            this.height = inputImage.Bmp.Height;
            this.intensity = parser.Intensity;
            this.lower = parser.Lower;
            this.upper = parser.Upper;
            this.choice = parser.Choice;
            this.constant = parser.Constant;
            this.kernelSize = parser.KernelSize;


            this.Reset();
        }

        public void Reset()
        {
            this.ConvFilterList = new List<IConvFilter>();
        }

        public void BuildPart1()
        {
            this.ConvFilterList.Add(new Average(kernelSize));
        }
        public void BuildPart2()
        {
            this.ConvFilterList.Add(new Gaussian(kernelSize, constant));
        }
        public void BuildPart3()
        {
            this.ConvFilterList.Add(new Laplacian1(1));
        }
        public void BuildPart4()
        {
            this.ConvFilterList.Add(new Laplacian2(1));
        }
        public void BuildPart5()
        {
            this.ConvFilterList.Add(new Laplacian1(constant));
        }
        public void BuildPart6()
        {
            this.ConvFilterList.Add(new Laplacian2(constant));
        }
        public void BuildPart7()
        {
            this.ConvFilterList.Add(new Embossing1());
        }
        public void BuildPart8()
        {
            this.ConvFilterList.Add(new Embossing2());
        }
        public void BuildPart9()
        {
            this.ConvFilterList.Add(new EmbossingIntense());
        }
        public void BuildPart10()
        {
            this.ConvFilterList.Add(new CompositeFilter(new Sobel()));
        }
        public void BuildPart11()
        {
            this.ConvFilterList.Add(new CompositeFilter(new Prewitt()));
        }
        public void BuildPart12()
        {
        }

        public List<IConvFilter> GetProduct()
        {
            List<IConvFilter> result = this.ConvFilterList;
            this.Reset();

            return result;
        }
    }
}

