using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using XnaFan.ImageComparison;

namespace Core
{
    public static class ExtensionMethods
    {
        public static Image Resize(this Image image, int maxWidth = 0, int maxHeight = 0)
        {
            if (maxWidth == 0)
                maxWidth = image.Width;
            if (maxHeight == 0)
                maxHeight = image.Height;

            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }
       
        public static decimal MyPercentageDifference(this Image image, Image imageToCompare)
        {
            Bitmap bitToCompare = imageToCompare as Bitmap;          
            Bitmap baseBitmap = image as Bitmap;           

            decimal bluePixelsDiff = 0;
            decimal redPixelsDiff = 0;
            decimal greenPixelsDiff = 0;
            for (int i = 0; i < bitToCompare.Width; i++)
            {
                for (int j = 0; j < bitToCompare.Height; j++)
                {
                    Color colorBaseBit = baseBitmap.GetPixel(i, j);
                    Color colorBitToCompare = bitToCompare.GetPixel(i, j);
                    bluePixelsDiff += Math.Abs(colorBaseBit.B-colorBitToCompare.B);
                    redPixelsDiff += Math.Abs(colorBaseBit.R - colorBitToCompare.R);
                    greenPixelsDiff += Math.Abs(colorBaseBit.G - colorBitToCompare.G);
                }
            }
             decimal total = bitToCompare.Width * bitToCompare.Height;
            decimal d = (bluePixelsDiff + redPixelsDiff + greenPixelsDiff) / total;
            return d;
        }
    }
}
