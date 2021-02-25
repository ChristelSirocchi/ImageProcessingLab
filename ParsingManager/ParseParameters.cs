using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessingWF
{
    class IntensityParser : IParse
    {
        private string error = "Please select an intensity value between 0 and 255"; // error message
        private readonly NumericUpDown numericUpDown;           
        public int Intensity { get => (int)numericUpDown.Value; }   // intensity
        public string GetError() { return error; }                  // get error message
        public IntensityParser(NumericUpDown numericUpDown)         // constructor
        {
            this.numericUpDown = numericUpDown;
        }
        public bool Parse()                                         // parse parameters
        {
            int n = (int)numericUpDown.Value;
            return (n >= 0 && n <= 255);
        }     
        public void Accept(IVisitor visitor)                        // accept visit
        {
            visitor.Visit(this);
        }
    }

    class RangeParser : IParse
    {
        private string error = "Please select intensity values between 0 and 255 and a lower-bound value less than the upper-bound value"; // error message
        private readonly NumericUpDown numericUpDown1;
        private readonly NumericUpDown numericUpDown2;

        public Range range { get => new Range((int)numericUpDown1.Value, (int)numericUpDown2.Value); }       // range
        public string GetError() { return error; }                   // get error message
        public RangeParser(NumericUpDown upper, NumericUpDown lower) // constructor
        {
            this.numericUpDown1 = upper;
            this.numericUpDown2 = lower;
        }
        public bool Parse()                                         // parse parameters
        {
            int l = (int)numericUpDown1.Value;

            int u = (int)numericUpDown2.Value;

            return (l >= 0 && l <= 255) & (u >= 0 && u <= 255) & (l < u);
        }      
        public void Accept(IVisitor visitor)                         // accept visit
        {
            visitor.Visit(this);
        }
    }

    class ConstantParser : IParse
    {
        private string error = "Please select a real value between 0 and 20"; // error message
        private readonly NumericUpDown numericUpDown;
        public float Constant { get => (float)numericUpDown.Value; }    // constant
        public string GetError() { return error; }                  // get error message
        public ConstantParser(NumericUpDown numericUpDown)
        {
            this.numericUpDown = numericUpDown;
        }
        public bool Parse()                                         // parse parameter
        {
            double d = (double)numericUpDown.Value;
            return (d >= 0 && d <= 20);
        }
        public void Accept(IVisitor visitor)                        // accept visit
        {
            visitor.Visit(this);
        }
    }

    class KernelSizeParser : IParse
    {
        private string error = "Please select a odd number between 3 and 15"; // error message
        private readonly NumericUpDown numericUpDown;
        public int KernelSize { get => (int)numericUpDown.Value; } // kernel size
        public string GetError() { return error; }              // get error message
        public KernelSizeParser(NumericUpDown numericUpDown)    //  constructor
        {
            this.numericUpDown = numericUpDown;
        }
        public bool Parse()                             // parse parameter
        {
            int kernel = (int)numericUpDown.Value;
            return new[] { 1, 3, 5, 7, 9, 11, 13, 15 }.Contains(kernel);
        }
        public void Accept(IVisitor visitor)            // accept visit
        {
            visitor.Visit(this);
        }
    }

}



