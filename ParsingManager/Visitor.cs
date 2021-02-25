using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessingWF
{
    public class Visitor : IVisitor
    {
        private bool parsing = true;

        public bool Parsing { get => parsing; }
        public void Visit(IParse parser)
        {
            try  // attemp parsing
            {
                bool p = parser.Parse();
                if (p == false) // parsing was not successful
                {
                    MessageBox.Show(parser.GetError());
                    parsing = false;
                }
                else  // parsing was successful
                {
                    parsing = true;
                }
                    
            }
            catch   // display error message if errors occurred
            {
                MessageBox.Show("parsing error");
                parsing = false;
            }
        }
    }
}
