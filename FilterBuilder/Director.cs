using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    public enum Transformation { spatial, intensity, convolution, morphology, colour };

    class Director
    {
        private IBuilder builder;       // builder

        public IBuilder Builder         
        {
            set { builder = value; }
        }
        public void BuildSpatialOpList()    // build spatial operation filter creators
        {
            this.builder.BuildSpatialOpList();
        }

        public void BuildConvList()         // build spatial filtering filter creators
        {
            this.builder.BuildConvList();
        }

        public void BuildOrderStatList()    // build mathematical morphology filter creators
        {
            this.builder.BuildOrderStatList();
        }

        public void BuildLookUpTableList()  // build intensity transformation filter creators
        {
            this.builder.BuildLookUpTableList();
        }

        public void BuildPaletteList()      // build false colour filter creators
        {
            this.builder.BuildPaletteList();
        }

        public void BuildAll()              // build all filter creators
        {
            this.builder.BuildSpatialOpList();
            this.builder.BuildLookUpTableList();
            this.builder.BuildConvList();
            this.builder.BuildOrderStatList();
            this.builder.BuildPaletteList();
        }
    }
}
