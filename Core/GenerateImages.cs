using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Core
{
    public class GenerateImages
    {
        public const int imageMarginSize = 1;       
        public List<Image> ReceiveImagesFromPaths(string[] imagesPath)
        {
            List<Image> images = new List<Image>();
            foreach (string imagePath in imagesPath)
            {
                Image image = Image.FromFile(imagePath);
                images.Add(image);
            }

            return images;
        }


        public bool CheckForImageSizeEquality(List<Image> images)
        {
            for (int i = 0; i < images.Count; i++)
            {
                if (images[0].Width != images[i].Width
                    || images[0].Height != images[i].Height ||
                    images[0].Width != images[i].Height)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CheckForImageQuadraticEquality(List<Image> images)
        {
            int result = (int)Math.Sqrt(images.Count());
            if (images.Count() != result * result)
            {
                return false; 
            }
            return true;
        }
        

        public List<Puzzle> GeneratePuzzlesFromImages(List<Image> images)
        {
            List<Puzzle> puzzles = new List<Puzzle>();
            foreach (Image image in images)
            {
                Puzzle puzzle = new Puzzle();
                puzzles.Add(SetPuzzleWalls(image,puzzle));
            }
            return puzzles;
        }

        public Puzzle SetPuzzleWalls(Image image,Puzzle puzzle)
        {           
            puzzle.leftImageWall = GetPartOfImage(image, 0, 0, imageMarginSize, image.Height);
            puzzle.rightImageWall = GetPartOfImage(image, image.Width - imageMarginSize, 0, imageMarginSize, image.Height);
            puzzle.upImageWall = GetPartOfImage(image, 0, 0, image.Width, imageMarginSize);
            puzzle.bottomImageWall = GetPartOfImage(image, 0, image.Height - imageMarginSize, image.Width, imageMarginSize);
            puzzle.MainImage = image;
            return puzzle;
        }


        private Bitmap GetPartOfImage(Image image,int x,int y,int width,int height)
        {
            Bitmap original = image.Clone() as Bitmap;
            Rectangle srcRect = new Rectangle(x, y, width, height);
            Bitmap cropped = (Bitmap)original.Clone(srcRect, original.PixelFormat);
            return cropped;
        }
    }
}
