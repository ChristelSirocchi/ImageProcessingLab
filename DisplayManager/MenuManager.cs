using System;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    abstract class MenuManager
    {
        protected readonly Panel panel;     //Panel 
        protected readonly Timer timer;     //Timer
        protected bool isOpen = false;      //panel is open or closed

        public MenuManager(Timer timer, Panel panel) //constructor
        {
            this.timer = timer;
            this.panel = panel;
        }
        
        public abstract void Open();                
    }

    class PrimaryMenu : MenuManager
    {
        public PrimaryMenu(Timer timer, Panel panel) : base(timer, panel) { }       //constructor

        public override void Open()                                         
        {
            if (isOpen)                  //Timer stops if minimum size has been reached, panel is closed                           
            {
                panel.Height -= 20;
                if (panel.Height == 0)
                {
                    timer.Stop();
                    isOpen = false;
                }
            }
            else
            {
                panel.Height += 20;     //Timer stops if maximum size has been reached, panel is open    
                if (panel.Height == panel.MaximumSize.Height)
                {
                    timer.Stop();
                    isOpen = true;
                }
            }
        }
    }

    class SecondaryMenu : MenuManager
    {
        public SecondaryMenu(Timer timer, Panel panel) : base(timer, panel) { }       //constructor

        public override void Open()
        {
            if (isOpen)
            {
                panel.Width -= 20;
                if (panel.Width == 0)
                {
                    timer.Stop();
                    isOpen = false;
                }
            }
            else
            {
                panel.Width += 20;
                if (panel.Width == panel.MaximumSize.Width)
                {
                    timer.Stop();
                    isOpen = true;
                }
            }
        }
    }

}
