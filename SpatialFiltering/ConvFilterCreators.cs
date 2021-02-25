using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class AverageFilterCreator : IFilterCreator // convolution filter creators
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Average(parser.KernelSize);
        }
    }

    class GaussianFilterCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Gaussian(parser.KernelSize, parser.Constant);
        }
    }

    class Laplacian1FilterCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Laplacian1(1);
        }
    }

    class Laplacian2FilterCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Laplacian2(1);
        }
    }
    class HighBoost1FilterCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Laplacian1(parser.Constant);
        }
    }
    class HighBoost2FilterCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Laplacian2(parser.Constant);
        }
    }
    class Embossing1FilterCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Embossing1();
        }
    }

    class Embossing2FilterCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Embossing2();
        }
    }

    class EmbossingIntenseFilterCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new EmbossingIntense();
        }
    }

    class SobelFilterCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new CompositeFilter(new Sobel());
        }
    }

    class PrewittFilterCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new CompositeFilter(new Prewitt());
        }
    }
}
