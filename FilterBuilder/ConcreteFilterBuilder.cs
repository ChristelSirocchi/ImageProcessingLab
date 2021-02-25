using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class ConcreteFilterBuilder : IBuilder
    {
        private List<IFilterCreator> filterCreatorList = new List<IFilterCreator>();        // list of filter creators
        private Dictionary<Transformation, List<IFilterCreator>> filterCreatorDictionary = new Dictionary<Transformation, List<IFilterCreator>>(); //dictionary of all filter creators
    

        public void Reset()     //reset list
        {
            this.filterCreatorList = new List<IFilterCreator>();
        }
        public void ResetAll()  //reset dictionary
        {
            this.filterCreatorDictionary = new Dictionary<Transformation, List<IFilterCreator>>();
        }

        public void BuildSpatialOpList()        //build list of spatial operation filter creators
        {
            this.Reset();
            filterCreatorList.Add(new RotateCreator());
            filterCreatorList.Add(new MirrorCreator());
            filterCreatorList.Add(new ResizeCreator());
            filterCreatorDictionary.Add(Transformation.spatial, filterCreatorList);
        }

        public void BuildConvList()             //build list of spatial filtering filter creators
        {
            this.Reset();
            filterCreatorList.Add(new AverageFilterCreator());
            filterCreatorList.Add(new GaussianFilterCreator());
            filterCreatorList.Add(new Laplacian1FilterCreator());
            filterCreatorList.Add(new Laplacian2FilterCreator());
            filterCreatorList.Add(new HighBoost1FilterCreator());
            filterCreatorList.Add(new HighBoost2FilterCreator());
            filterCreatorList.Add(new Embossing1FilterCreator());
            filterCreatorList.Add(new Embossing2FilterCreator());
            filterCreatorList.Add(new EmbossingIntenseFilterCreator());
            filterCreatorList.Add(new SobelFilterCreator());
            filterCreatorList.Add(new PrewittFilterCreator());
            filterCreatorDictionary.Add(Transformation.convolution, filterCreatorList);
        }

        public void BuildLookUpTableList()      //build list of intensity transformation filter creators
        {
            this.Reset();
            filterCreatorList.Add(new NegativeLUTCreator());
            filterCreatorList.Add(new ContrastStretchingLUTCreator());
            filterCreatorList.Add(new LogarithmicLUTCreator());
            filterCreatorList.Add(new EqualizedHistoLUTCreator());
            filterCreatorList.Add(new ThresholdingLUTCreator());
            filterCreatorList.Add(new SlicingLUTCreator());
            filterCreatorList.Add(new GammaLUTCreator());
            filterCreatorList.Add(new PiecewiseLUTCreator());
            filterCreatorDictionary.Add(Transformation.intensity, filterCreatorList);
        }


        public void BuildOrderStatList()        //build list of mathematical morphology filter creators
        {
            this.Reset();
            filterCreatorList.Add(new MedianFilterCreator());
            filterCreatorList.Add(new MinFilterCreator());
            filterCreatorList.Add(new MaxFilterCreator());
            filterCreatorList.Add(new MinMaxFilterCreator());
            filterCreatorList.Add(new MaxMinFilterCreator());
            filterCreatorDictionary.Add(Transformation.morphology, filterCreatorList);
        }

        public void BuildPaletteList()          //build list of false colour filter creators
        {
            this.Reset();
            filterCreatorList.Add(new Palette1Creator());
            filterCreatorList.Add(new Palette2Creator());
            filterCreatorList.Add(new Palette3Creator());
            filterCreatorList.Add(new Palette4Creator());
            filterCreatorList.Add(new Palette5Creator());
            filterCreatorList.Add(new CustomPaletteCreator()); 
            filterCreatorDictionary.Add(Transformation.colour, filterCreatorList);
        }

        public List<IFilterCreator> GetList()       //get list
        {
            List<IFilterCreator> result = this.filterCreatorList;
            this.Reset();

            return result;
        }

        public Dictionary<Transformation, List<IFilterCreator>> GetDictionary() //get dictionary
        {
            Dictionary<Transformation, List<IFilterCreator>> result = this.filterCreatorDictionary;
            this.ResetAll();

            return result;
        }


    }
}
