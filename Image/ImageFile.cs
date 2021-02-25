using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class ImageFile 
    {
        private static int count = 0;               //static attribute of the class, counts images istantiated

        private readonly Bitmap bmp = null;         //image Bitmap private attribute

        private readonly string imageName = null;   //image name private attribute

        private readonly bool isGray;

        private ImageStats imageStats;

        // properties to access private attributes   
        public Bitmap Bmp { get { return bmp; } }                               //image Bitmap
        public string ImageName { get { return imageName; } }                   //image name
        public ImageStats ImageStats { get { return new ImageStats(bmp); } }    //minimum, maximum, average, standard deviation values of intensity in image
        public bool IsGray { get { return bmp.CheckIfGray(); } }                //image is grayscale or RGB

        public ImageFile(string fileName)       //Constructor from file for input images
        {
            try
            {
                bmp = (Bitmap)Bitmap.FromFile(fileName);
                imageName = fileName;
            }
            catch (Exception)
            {
                throw new ApplicationException("Loading image Error");
            }
        }      
        public ImageFile(Bitmap bmpnew)         //Constructor from bitmap
        {
            bmp = bmpnew;
            imageName = "image" + count;
            count++;
        }
        public ImageFile()                      //Constructor zero-arguments
        {
            bmp = null;
            imageName = "image" + count;
            count++;
        }
        public static int GetCount() { return count; } //get number of images instatiated
 
    }
}
