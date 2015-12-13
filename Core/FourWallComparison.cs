using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class FourWallComparison
    {
        Puzzle firstPuzzle;
        Puzzle secondPuzzle;
        Puzzle thirdPuzzle;
        Puzzle fourPuzzle;     
        List<Puzzle> _puzzles;      
        decimal bottomPuzzleResult;
        decimal upPuzzleResult = 1000;
        OneEdgeCompareMechanism oneEdgeMechanism;
        public FourWallComparison(List<Puzzle> puzzles)
        {
            _puzzles = puzzles;
            oneEdgeMechanism = new OneEdgeCompareMechanism(_puzzles);
        }

        public void CompareWalls()
        {
            FindFirstAndSecondPuzzle();
            decimal resultDown = FindThirdPuzzle("down");
            decimal resultUp = FindThirdPuzzle("up");

            if (resultUp < resultDown)
            {
                thirdPuzzle.row = 1;
                thirdPuzzle.column = -1;
            }
            else
            {
                thirdPuzzle.row = 1;
                thirdPuzzle.column = 1;
            }
            thirdPuzzle.puzzleChecked = true;

            FindFourthPuzzle();
        }

        private void FindFourthPuzzle()
        {
            decimal fourResult = 1000;
            decimal result;
            fourPuzzle = _puzzles.Where(x => !x.puzzleChecked).First();
            for (int i = 0; i < 4; i++)
            {
                result = thirdPuzzle.leftImageWall.MyPercentageDifference(fourPuzzle.rightImageWall);
                if (result < fourResult)
                {

                    fourResult = result;
                    fourPuzzle.column = thirdPuzzle.column;
                    fourPuzzle.row = 0;
                }
            }
        }

        private decimal FindThirdPuzzle(string way)
        {
            decimal result = 1000;
            foreach (Puzzle puzzle in _puzzles.Where(x => !x.puzzleChecked))
            {
                if (way.Equals("up"))
                {
                    result = secondPuzzle.upImageWall.MyPercentageDifference(puzzle.bottomImageWall);
                }
                else
                {
                    result = secondPuzzle.bottomImageWall.MyPercentageDifference(puzzle.upImageWall);
                }

                if (result < upPuzzleResult)
                {                   
                    upPuzzleResult = result;
                    thirdPuzzle = puzzle;
                }
            }
            return upPuzzleResult;

        }

        private void FindFirstAndSecondPuzzle()
        {
            decimal min = 1000;
            foreach (Puzzle puzzle in _puzzles)
            {
                for (int i = 0; i < 4; i++)
                {
                    decimal result = puzzle.rightImageWall.MyPercentageDifference(_puzzles[i].leftImageWall);
                    if (result < min)
                    {
                        min = result;
                        secondPuzzle = _puzzles[i];
                        firstPuzzle = puzzle;

                    }
                }
            }
            secondPuzzle.puzzleChecked = true;
            secondPuzzle.row += 1;
            secondPuzzle.column = 0;
            firstPuzzle.puzzleChecked = true;
            firstPuzzle.row = 0;
            firstPuzzle.column = 0;          
        }      
    }
}
