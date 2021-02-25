using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data;

namespace ImageProcessingWF
{
    public class ParseManager // handles parsing of parameters selected by the user
    {
        private List<IParse> allParsers = new List<IParse>();
        private RadioButton choice;
        private TextBox text;
        private DataGridView dataGrid;
        private IntensityParser intensityParser;
        private RangeParser rangeParser;
        private ConstantParser constantParser;
        private KernelSizeParser kernelParser;
        private ParseDataTable customParser;
        private Visitor visitor;

        public int Intensity { get => GetIntensity(); }
        public Range Range { get => GetRange(); }              // intensity range - lower bound
        public float Constant { get => GetConstant(); }   // real constant
        public int KernelSize { get => GetKernel(); }  // kernel size
        public bool Choice { get => choice.Checked; }               // dual choice
        public DataTable Table { get => GetTable(); }               // data table 

        public ParseManager(NumericUpDown nUpDown1, NumericUpDown nUpDown2, NumericUpDown nUpDown3, NumericUpDown nUpDown4, NumericUpDown nUpDown5, RadioButton choice,
            TextBox text, DataGridView dataGrid, Visitor visitor)  // constructor
        {
            intensityParser = new IntensityParser(nUpDown1);
            rangeParser = new RangeParser(nUpDown2, nUpDown3);
            constantParser = new ConstantParser(nUpDown4);
            kernelParser = new KernelSizeParser(nUpDown5);
            this.visitor = visitor;
            this.choice = choice;
            this.dataGrid = dataGrid;
            this.text = text;
        }

        public int GetIntensity()
        {
            visitor.Visit(intensityParser);
            if (visitor.Parsing == true)
            {
                return intensityParser.Intensity;
            }
            else
            {
                return 0;
            }
        }

        public Range GetRange()
        {
            visitor.Visit(rangeParser);
            if (visitor.Parsing == true)
            {
                return rangeParser.range;
            }
            else
            {
                return new Range(0,0);
            }
        }

        public float GetConstant()
        {
            visitor.Visit(constantParser);
            if (visitor.Parsing == true)
            {
                return constantParser.Constant;
            }
            else
            {
                return 0;
            }
        }

        public int GetKernel()
        {
            visitor.Visit(kernelParser);
            if (visitor.Parsing == true)
            {
                return kernelParser.KernelSize;
            }
            else
            {
                return 0;
            }
        }

        public void StartParsing(Transformation index) // start parsing custom points
        {
            if (index == Transformation.intensity)
            {
                customParser = new ParseIntensityPoints(text, dataGrid);
            }
            else if (index == Transformation.colour)
            {
                customParser = new ParseColourPoints(text, dataGrid);                
            }
            customParser.InitializeDataTable();
        }

        public void ParsePoint()    // add point
        {
            customParser.AddPoint();
        }

        public void ClearPoints()   // clear points
        {
            customParser.ClearPoints();
        }

        public DataTable GetTable() // get table
        {
            visitor.Visit(customParser);

            return customParser.GetTable();

        }
    }
}
