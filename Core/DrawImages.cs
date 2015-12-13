using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Core
{
    public class DrawImages
    {
        public void DrawAllUnorderedImages(PictureBox picture, List<Image> images)
        {
            int imageCount = (int)Math.Sqrt(images.Count());
            int imageCounter = 0;
            Bitmap bit = new Bitmap(imageCount * images[0].Width, imageCount * images[0].Height);
            using (Graphics g = Graphics.FromImage(bit))
            {             
                for (int y = 0; y < imageCount; y++)
                {
                    for (int x = 0; x < imageCount; x++)
                    {
                        g.DrawImage(images[imageCounter], x * images[0].Width, y * images[0].Height,
                               images[0].Width, images[0].Height);
                        imageCounter++;
                       
                    }                   
                }
            }
            ResizeImage(picture, bit);            
           
        }     

        public void DrawAllOrderedImages(PictureBox picture,List<Puzzle> puzzles)
        {
            List<Puzzle> adgePuzzles = puzzles
                .Where(x => x.row == 0 && x.column == 0)
                .ToList();
            int minRow = puzzles.Select(x => x.row).Min();
            int maxRow = puzzles.Select(x => x.row).Max();

            int minColumn = puzzles.Select(x => x.column).Min();
            int maxColumn = puzzles.Select(x => x.column).Max();

            Bitmap bit = new Bitmap(Math.Abs(minRow - maxRow - 1) * puzzles[0].MainImage.Width,
                Math.Abs(minColumn - maxColumn - 1) * puzzles[0].MainImage.Height);
            using (Graphics g = Graphics.FromImage(bit))
            {
                int coordinateX = 0;
                int coordinateY = 0;
                for (int j = minColumn; j <= maxColumn; j++)
                {
                    for (int i = minRow; i <= maxRow; i++)
                    {
                        Puzzle puzzle = puzzles.Where(x => x.row == i && x.column == j).FirstOrDefault();
                        if (puzzle != null)
                        {                           
                            g.DrawImage(puzzle.MainImage, coordinateX * puzzle.MainImage.Width, coordinateY * puzzle.MainImage.Height,
                                puzzle.MainImage.Width, puzzle.MainImage.Height);
                            coordinateX++;
                            
                        }                      
                        else
                        {
                            coordinateX++;
                        }
                    }
                    coordinateY++;
                    coordinateX = 0;
                }
            }
            ResizeImage(picture, bit);
           
        } 

        private void ResizeImage(PictureBox picture, Bitmap bit)
        {            
            if (bit.Width > 500 || bit.Height > 500)
            {
                bit = bit.Resize(500, 500) as Bitmap;
            }
            picture.Size = new Size(bit.Width, bit.Height);
            picture.Image = bit;
        }
    }
}
