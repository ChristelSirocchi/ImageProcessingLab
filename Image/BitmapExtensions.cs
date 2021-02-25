using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;

namespace ImageProcessingWF
{
    public static class BitmapExtensions         //extension methods for type Bitmap
    {
        public static Bitmap OpenBmp(this Bitmap input)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG"; // image format

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        input = (Bitmap)Bitmap.FromFile(dlg.FileName);  // get Bitmap from file
                    
                        if (input.CheckIfGray() == false)               //check if image is RGB
                        {
                            DialogResult dr = MessageBox.Show("Image chosen is RGB. Convert to gray scale?", "Attention", MessageBoxButtons.YesNo);
                            switch (dr)
                            {
                                case DialogResult.Yes:
                                    {
                                        input.RGBtoGray();              //convert image to grayscale
                                    }
                                    break;
                                case DialogResult.No:
                                    break;
                            }
                        }
                    }
                    catch (ApplicationException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return input;
        }
        public static void SaveBitmap(this Bitmap output)
        {
            if (output != null)
            {
                try
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Title = "Specify a file name and file path";
                    sfd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";    //image formats
                    sfd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string fileExtension = Path.GetExtension(sfd.FileName).ToUpper();
                        ImageFormat imgFormat = ImageFormat.Png;

                        if (fileExtension == "BMP")
                        {
                            imgFormat = ImageFormat.Bmp;
                        }
                        else if (fileExtension == "JPG")
                        {
                            imgFormat = ImageFormat.Jpeg;
                        }

                        StreamWriter streamWriter = new StreamWriter(sfd.FileName, false);
                        output.Save(streamWriter.BaseStream, imgFormat);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static Bitmap CopyToSquareCanvas(this Bitmap input, int canvasWidthLenght)
        {
            float ratio = 1.0f;
            int maxSide = input.Width > input.Height ? input.Width : input.Height; // image max side

            ratio = (float)maxSide / (float)canvasWidthLenght;
             
            Bitmap output = (input.Width > input.Height ? 
                new Bitmap(canvasWidthLenght, (int)(input.Height / ratio)) : new Bitmap((int)(input.Width / ratio), canvasWidthLenght)); //generate Bitmap that fits the canvas

            using (Graphics graphicsResult = Graphics.FromImage(output))
            {
                graphicsResult.CompositingQuality = CompositingQuality.HighQuality;
                graphicsResult.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsResult.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphicsResult.DrawImage(input, new Rectangle(0, 0, output.Width, output.Height),  new Rectangle(0, 0,input.Width, input.Height), GraphicsUnit.Pixel);
                graphicsResult.Flush();
            }

            return output;
        }

        public static void RGBtoGray(this Bitmap bmp)
        {            
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);    //Lock bitmap bits to system memory
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;                                     //Scan for first line, returns pointer

            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;              //Declare array in which intensity values will be stored
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);                         //Copy intensity values in array

            for (int i = 0; i < rgbValues.Length; i += 3)                   //Calculate new intensity values
            {                                                                                      
                byte gray = (byte)MathF.CheckRange((int)(rgbValues[i] * .21 + rgbValues[i + 1] * .71 + rgbValues[i + 2] * .071));
                rgbValues[i] = rgbValues[i + 1] = rgbValues[i + 2] = gray;
            }
            Marshal.Copy(rgbValues, 0, ptr, bytes);                         //Copy changed intensity values back to the bitmap

            bmp.UnlockBits(bmpData);                                        //Unlock bits from system memory
        }

        public static bool CheckIfGray(this Bitmap bmp)                     
        {
            bool isgray = true;                                             //scan all image until it finds a RGB pixel

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color c = bmp.GetPixel(i, j);

                    isgray = c.R == c.G && c.B == c.R;

                    if (isgray == false)
                        break;
                }
            }
            return isgray;
        }

        /*GetPixel SetPixelMethod  (slow)
         
        public static Bitmap RGBtoGray(this Bitmap bmp)
        {
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color c = bmp.GetPixel(i, j);

                    //Apply conversion equation
                    byte gray = (byte)(.21 * c.R + .71 * c.G + .071 * c.B);

                    //Set the color of this pixel
                    bmp.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            return bmp;
        }
        */

    }
}
