using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Core
{
    public class BuildImage
    {
        PictureBox _resultPictureBox;
        PictureBox _basePictureBox;
        public BuildImage(PictureBox basePictureBox, PictureBox resultPictureBox)
        {
            this._resultPictureBox = resultPictureBox;
            this._basePictureBox = basePictureBox;
        }

        public void ConstructOrderedPicture(string[] paths)
        {           
            GenerateImages imageGenerator = new GenerateImages();
            List<Image> images = imageGenerator.ReceiveImagesFromPaths(paths);
            List<Puzzle> puzzles = imageGenerator.GeneratePuzzlesFromImages(images);
            PuzzlesSeparation separatePuzzles = new PuzzlesSeparation(puzzles);
            separatePuzzles.SettIdenticalPixelColorWall();
            if (puzzles.Count < 5 && puzzles.Count!=1)
            {
                FourWallComparison fourWall = new FourWallComparison(puzzles);
                fourWall.CompareWalls();
            }

            else if (puzzles.Count != 1)
            {
                FirstPuzzle firstPuzzle = new FirstPuzzle(puzzles);
                Puzzle firstFitPuzzle = firstPuzzle.GetFirstPuzzle();

                ThreeEdgeCompareMechanism connectImagesMainMethod = new ThreeEdgeCompareMechanism(puzzles);
                connectImagesMainMethod.CompareWalls(firstFitPuzzle);
                OneEdgeCompareMechanism connectImagesAdditionMethod = new OneEdgeCompareMechanism(puzzles);
                connectImagesAdditionMethod.CompareWalls();
            }
            DrawImages draw = new DrawImages();
            draw.DrawAllOrderedImages(_resultPictureBox, puzzles);

        }

        public bool ConstructUnorderedPicture(string[] paths)
        {
            if (paths.Count() == 0)
            {
                return false;
            }
            GenerateImages imageGenerator = new GenerateImages();
            List<Image> images = imageGenerator.ReceiveImagesFromPaths(paths);
            if (imageGenerator.CheckForImageSizeEquality(images) && imageGenerator.CheckForImageQuadraticEquality(images))
            {
                DrawImages draw = new DrawImages();
                draw.DrawAllUnorderedImages(_basePictureBox, images);
                return true;
            }
            return false;
        }
    }
}
