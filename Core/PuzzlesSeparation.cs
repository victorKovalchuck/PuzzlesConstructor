using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Core
{
    public class PuzzlesSeparation
    {
        const int imageWallColorSameness = 5;
        List<Puzzle> _puzzles;
        public PuzzlesSeparation(List<Puzzle> puzzles)
        {
            this._puzzles = puzzles;
        }
       

        public void SettIdenticalPixelColorWall()
        {
            foreach (Puzzle puzzle in _puzzles)
            {
                MarkWallWithIdenticalPixelColor(puzzle);
            }
         
        }

        private void MarkWallWithIdenticalPixelColor(Puzzle puzzle)
        {
            puzzle.bottomImageWallHasIdenticalPixelColor = ImageWallHasIdenticalPixelColor(puzzle.bottomImageWall);
            puzzle.upImageWallHasIdenticalPixelColor = ImageWallHasIdenticalPixelColor(puzzle.upImageWall);
            puzzle.leftImageWallHasIdenticalPixelColor = ImageWallHasIdenticalPixelColor(puzzle.leftImageWall);
            puzzle.rightImageWallHasIdenticalPixelColor = ImageWallHasIdenticalPixelColor(puzzle.rightImageWall);
        }

        private bool ImageWallHasIdenticalPixelColor(Image image)
        {
            Color color;
            using (Bitmap bitmap = new Bitmap(image))
            {
                color = bitmap.GetPixel(0, 0);

                if (color == null)
                {
                    return false;
                }

                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        Color colorPixel = bitmap.GetPixel(i, j);
                        if (color.R - colorPixel.R > imageWallColorSameness || color.R - colorPixel.R < -imageWallColorSameness ||
                            color.G - colorPixel.G > imageWallColorSameness || color.G - colorPixel.G < -imageWallColorSameness ||
                            color.B - colorPixel.B > imageWallColorSameness || color.B - colorPixel.B < -imageWallColorSameness)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
