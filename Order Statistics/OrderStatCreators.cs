using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{ 
    class MedianFilterCreator : IFilterCreator      // Mathematical morphology filter creators
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new MedianFilter(parser.KernelSize);
        }
    }
    class MinFilterCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new MinFilter(parser.KernelSize);
        }
    }

    class MaxFilterCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new MaxFilter(parser.KernelSize);
        }
    }

    class MinMaxFilterCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new MinMaxFilter(parser.KernelSize);
        }
    }

    class MaxMinFilterCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new MaxMinFilter(parser.KernelSize);
        }
    }

}

