using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessingWF
{
    public partial class MainForm : Form
    {
        private ImageFile inputImage, outputImage;
        private DisplayManager displayInput, displayOutput, displayGraph;
        private ParseManager parseParameters;
        private PrimaryMenu menuP1, menuP2, menuP3, menuP4, menuP5;
        private SecondaryMenu menuS1, menuS2, menuS3, menuS4, menuS5;
        private Director director;
        private ConcreteFilterBuilder builder;
        private int selectionIndex;
        private Bitmap inputBmp;
        public Transformation transformationIndex;
        Dictionary<Transformation, List<IFilterCreator>> filterCreator;
        Visitor visitor;

        public MainForm()
        {
            InitializeComponent();
            InitializeFeatures();
        }
        public void InitializeFeatures()
        {
            visitor = new Visitor();
            displayInput = new DisplayManager(this.pictureBox2, this.pictureBox4, this.textBox1, this.textBox2, this.textBox3, this.textBox4, this.groupBox1);
            displayOutput = new DisplayManager(this.pictureBox5, this.pictureBox3, this.textBox5, this.textBox6, this.textBox7, this.textBox8, this.groupBox2);
            displayGraph = new DisplayManager(this.chart1);
            parseParameters = new ParseManager(this.numericUpDown1, this.numericUpDown2, this.numericUpDown3, this.numericUpDown5, this.numericUpDown4, this.radioButton1, this.textBox9, this.dataGridView1, visitor);
            menuP1 = new PrimaryMenu(timer1, SpatialOperations);
            menuP2 = new PrimaryMenu(timer2, panel3);
            menuP3 = new PrimaryMenu(timer3, panel6);
            menuP4 = new PrimaryMenu(timer4, panel7);
            menuP5 = new PrimaryMenu(timer5, panel4);
            menuS1 = new SecondaryMenu(timer6, panel5);
            menuS2 = new SecondaryMenu(timer7, panel11);
            menuS3 = new SecondaryMenu(timer8, panel8);
            menuS4 = new SecondaryMenu(timer9, panel9);
            menuS5 = new SecondaryMenu(timer10, panel2);            
            director = new Director();
            builder = new ConcreteFilterBuilder();
            director.Builder = builder;
            director.BuildAll();
            filterCreator = builder.GetDictionary();
        }
        private void button6_Click(object sender, EventArgs e) // click buttom Upload Image
        {
            inputBmp = inputBmp.OpenBmp();

            if (inputBmp != null)
            {
                inputImage = new ImageFile(inputBmp);
                displayInput.DisplayImage(inputImage);
                panel1.Visible = true;
            }
            else
            {
                MessageBox.Show("No image was uploaded");
            }
        }       

        //----------------------------------Transformations--------------------------------------------

        //apply button
        private void ApplyPmt_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                IFilter filter = filterCreator[transformationIndex][selectionIndex].FactoryMethod(parseParameters, inputImage);

                if (visitor.Parsing == true)
                { 
                    Bitmap outputBmp = filter.ApplyFilter(inputImage.Bmp);

                    outputImage = new ImageFile(outputBmp);
                    displayOutput.DisplayImage(outputImage);
                    displayGraph.DisplayGraph(filter, transformationIndex);
                    panel10.Visible = false;
                    panel12.Visible = true;
                }
            }
            catch
            {
                MessageBox.Show("Error : transformation was not applied");
            }           
        }

        private void HideAllOptions()
        {
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox6.Visible = false;
            groupBox7.Visible = false;
            groupBox8.Visible = false;
            panel10.Visible = true;
        }

        private void button16_Click(object sender, EventArgs e)         //rotate image
        {
            transformationIndex = Transformation.spatial;
            selectionIndex = 0;
            HideAllOptions();
            timer1.Start();
        }
            private void button15_Click(object sender, EventArgs e)        //mirror image
        {
            selectionIndex = 1;
            transformationIndex = Transformation.spatial;
            HideAllOptions();
            timer1.Start();
        }                     
        private void button17_Click(object sender, EventArgs e)         //resize image
        {
            transformationIndex = Transformation.spatial;
            selectionIndex = 2;
            HideAllOptions();
            groupBox5.Visible = true;
            radioButton1.Text = "bilinear";
            radioButton2.Text = "bicubic";
            groupBox7.Visible = true;
            timer1.Start();
        }

        //INTENSITY TRASFORMATION       
        private void button18_Click(object sender, EventArgs e)
        {
            selectionIndex = 0;     //negative
            transformationIndex = Transformation.intensity;
            HideAllOptions();
            timer2.Start();
        }              
        private void button19_Click(object sender, EventArgs e)
        {
            selectionIndex = 1;     //contrast stretching
            transformationIndex = Transformation.intensity;
            HideAllOptions();
            timer2.Start();
        }        
        private void button22_Click(object sender, EventArgs e)
        {
            selectionIndex = 2;     //logarithmic
            transformationIndex = Transformation.intensity;
            HideAllOptions();
            timer2.Start();
        }        
        private void button21_Click(object sender, EventArgs e)
        {
            selectionIndex = 3;     //histogram Equalization
            transformationIndex = Transformation.intensity;
            HideAllOptions();
            timer2.Start();
        }
        private void button20_Click(object sender, EventArgs e)
        {
            selectionIndex = 4; //thresholding
            transformationIndex = Transformation.intensity;
            HideAllOptions();
            groupBox3.Visible = true;
            label10.Text = "threshold";
            timer2.Start();
        }
        private void button25_Click(object sender, EventArgs e)
        {
            selectionIndex = 5; //slicing
            transformationIndex = Transformation.intensity;
            HideAllOptions();
            groupBox3.Visible = true;
            groupBox4.Visible = true;
            groupBox5.Visible = true;
            label10.Text = "intensity values in range";
            timer2.Start();
        }
        private void button23_Click(object sender, EventArgs e)
        {
            selectionIndex = 6; //gamma correction
            transformationIndex = Transformation.intensity;
            HideAllOptions();
            groupBox7.Visible = true;
            timer2.Start();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            selectionIndex = 7; //piecewise
            transformationIndex = Transformation.intensity;
            HideAllOptions();
            timer2.Start();
            parseParameters.StartParsing(transformationIndex);
            groupBox5.Visible = true;
            radioButton1.Text = "linear";
            radioButton2.Text = "steps";
            groupBox8.Visible = true;
        }


        //Mathematical Morphology
        private void button38_Click(object sender, EventArgs e)
        {
            selectionIndex = 1; //erosion
            transformationIndex = Transformation.morphology;
            HideAllOptions();
            groupBox6.Visible = true;
            timer3.Start();
        }
        private void button37_Click(object sender, EventArgs e)
        {
            selectionIndex = 2; //dilation
            transformationIndex = Transformation.morphology;
            HideAllOptions();
            groupBox6.Visible = true;
            timer3.Start();
        }
        private void button36_Click(object sender, EventArgs e)
        {
            selectionIndex = 3; //opening
            transformationIndex = Transformation.morphology;
            HideAllOptions();
            groupBox6.Visible = true;
            timer3.Start();
        }
        private void button35_Click(object sender, EventArgs e)
        {
            selectionIndex = 4; //closing
            transformationIndex = Transformation.morphology;
            HideAllOptions();
            groupBox6.Visible = true;
            timer3.Start();
        }

        // SPATIAL FILTERING
        private void button26_Click(object sender, EventArgs e)
        {
            selectionIndex = 0; //average
            transformationIndex = Transformation.convolution;
            HideAllOptions();
            groupBox6.Visible = true;
            timer5.Start();
            timer6.Start();
        }
        private void button27_Click(object sender, EventArgs e)
        {
            selectionIndex = 1; //gaussian
            transformationIndex = Transformation.convolution;
            HideAllOptions();
            groupBox6.Visible = true;
            groupBox7.Visible = true;
            timer5.Start();
            timer6.Start();
        }
        private void button34_Click(object sender, EventArgs e)
        {
            selectionIndex = 0; //median -- orderstat
            transformationIndex = Transformation.morphology;
            HideAllOptions();
            groupBox6.Visible = true;
            timer5.Start();
            timer6.Start();
        }
        private void button52_Click(object sender, EventArgs e)
        {
            selectionIndex = 2;  //Laplacian 1
            transformationIndex = Transformation.convolution;
            HideAllOptions();
            timer5.Start();
            timer7.Start();
        }
        private void button53_Click(object sender, EventArgs e)
        {
            selectionIndex = 3;  //laplacian 2
            transformationIndex = Transformation.convolution;
            HideAllOptions();
            timer5.Start();
            timer7.Start();
        }
        private void button6_Click_1(object sender, EventArgs e)
        {
            selectionIndex = 4; //highboost Filtering Laplacian 1
            transformationIndex = Transformation.convolution;
            HideAllOptions();
            groupBox7.Visible = true;
            timer5.Start();
            timer10.Start();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            selectionIndex = 5; //highboost Filtering Laplacian 2
            transformationIndex = Transformation.convolution;
            HideAllOptions();
            groupBox7.Visible = true;
            timer5.Start();
            timer10.Start();
        }       
        private void button47_Click(object sender, EventArgs e)
        {
            selectionIndex = 6;     //Embossing1
            transformationIndex = Transformation.convolution;
            HideAllOptions();
            timer5.Start();
            timer9.Start();
        }
        private void button48_Click(object sender, EventArgs e)
        {
            selectionIndex = 7;     //Embossing2
            transformationIndex = Transformation.convolution;
            HideAllOptions();
            timer5.Start();
            timer9.Start();
        }
        private void button55_Click(object sender, EventArgs e)
        {
            selectionIndex = 8;     //EmbossingIntense
            transformationIndex = Transformation.convolution;
            HideAllOptions();
            timer5.Start();
            timer9.Start();
        }
        private void button46_Click(object sender, EventArgs e)
        {
            selectionIndex = 9;     //Sobel
            transformationIndex = Transformation.convolution;
            HideAllOptions();
            timer5.Start();
            timer8.Start();
        }
        private void button54_Click(object sender, EventArgs e)
        {
            selectionIndex = 10;    //Prewitt
            transformationIndex = Transformation.convolution;
            HideAllOptions();
            timer5.Start();
            timer8.Start();
        }
        // custom

        //---------------------APPLY COLOURS------------------------------//
        private void button40_Click(object sender, EventArgs e)
        {
            selectionIndex = 0;//False colour Palette 1
            transformationIndex = Transformation.colour;
            HideAllOptions();
            timer4.Start();
        }
        private void button42_Click(object sender, EventArgs e)
        {
            selectionIndex = 1;//False colour Palette 2
            transformationIndex = Transformation.colour;
            HideAllOptions();
            timer4.Start();
        }
        private void button41_Click(object sender, EventArgs e)
        {
            selectionIndex = 2;//False colour Palette 3
            transformationIndex = Transformation.colour;
            HideAllOptions();
            timer4.Start();
        }
        private void button39_Click(object sender, EventArgs e)
        {
            selectionIndex = 3;//False colour Palette 4
            transformationIndex = Transformation.colour;
            HideAllOptions();
            timer4.Start();
        }
        private void button43_Click(object sender, EventArgs e)
        {
            selectionIndex = 4;//False colour Palette 5
            transformationIndex = Transformation.colour;
            HideAllOptions();
            timer4.Start();
        }

        private void button44_Click(object sender, EventArgs e) //custom Colour
        {
            selectionIndex = 5;//False colour Custom Colour
            transformationIndex = Transformation.colour;
            HideAllOptions();
            timer4.Start();
            parseParameters.StartParsing(transformationIndex);
            groupBox8.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e) // press cancel
        {
            panel10.Visible = false;
        }
        private void SetAsOr_Btn_Click(object sender, EventArgs e)  // set output image as input
        {
            if (outputImage.IsGray == true)
            {
                inputImage = new ImageFile(outputImage.Bmp);
                displayInput.DisplayImage(inputImage);
            }
            else
                MessageBox.Show("False colour Images can't be set as original");
        }

        //--------------------------------------MENU CONTROLS-------------------------------------------------

        // control open Menu Spatial Operations
        private void button10_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            menuP1.Open();
        }
        // control open Menu Intensity Transformations
        private void button11_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }
        private void timer2_Tick_1(object sender, EventArgs e)
        {
            menuP2.Open();
        }
        // control open Menu Mathematical Morphology
        private void button13_Click(object sender, EventArgs e)
        {
            timer3.Start();
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            menuP3.Open();
        }
        // control open Menu False Colour 
        private void button14_Click(object sender, EventArgs e)
        {
            timer4.Start();
        }
        private void timer4_Tick(object sender, EventArgs e)
        {
            menuP4.Open();
        }
        // control open Menu Spatial Filtering
        private void button12_Click(object sender, EventArgs e)
        {
            timer5.Start();
        }
        private void timer5_Tick(object sender, EventArgs e)
        {
            menuP5.Open();
        }
        // control open Menu Spatial Filtering - Secondary Menu 1
        private void button33_Click(object sender, EventArgs e)
        {
            timer6.Start();
        }
        // save image
        private void SaveImage_Btn_Click(object sender, EventArgs e)
        {
            outputImage.Bmp.SaveBitmap();
        }

        private void button9_Click(object sender, EventArgs e) // add colour to custom
        {          
            parseParameters.ParsePoint();                
        }
        private void button31_Click(object sender, EventArgs e) // remove
        {
            parseParameters.ClearPoints();
        }
        private void timer6_Tick(object sender, EventArgs e)
        {
            menuS1.Open();
        }
        // control open Menu Spatial Filtering - Secondary Menu 2
        private void button32_Click(object sender, EventArgs e)
        {
            timer7.Start();
        }
        private void timer7_Tick(object sender, EventArgs e)
        {
            menuS2.Open();
        }
        // control open Menu Spatial Filtering - Secondary Menu 3      
        private void button45_Click(object sender, EventArgs e)
        {
            timer8.Start();
        }
        private void timer8_Tick(object sender, EventArgs e)
        {
            menuS3.Open();
        }
        // control open Menu Spatial Filtering - Secondary Menu 4
        private void button29_Click(object sender, EventArgs e)
        {
            timer9.Start();
        }
        private void timer9_Tick(object sender, EventArgs e)
        {
            menuS4.Open();
        }
        // control open Menu Spatial Filtering - Secondary Menu 5
        private void button30_Click(object sender, EventArgs e)
        {
            timer10.Start();
        }
        private void timer10_Tick(object sender, EventArgs e)
        {
            menuS5.Open();
        }

    }
}
