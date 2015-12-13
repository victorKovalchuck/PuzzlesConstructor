using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class OneEdgeCompareMechanism
    {
        List<Puzzle> _puzzles;
        List<Puzzle> _puzzleNotInUse;
        int minColumn;
        int maxColumn;
        int minRow;
        int maxRow;    
        enum puzzleEdge { CompareWithUp, CompareWithBottom, CompareWithLeft, CompareWithRight, None };
        GenerateImages generate = new GenerateImages();

        public OneEdgeCompareMechanism(List<Puzzle> puzzles)
        {             
             _puzzles = puzzles;
             _puzzleNotInUse = puzzles.Where(x => x.row == 0 && x.column == 0 && !x.puzzleChecked).ToList();               
        }

        public void CompareWalls()
        {
            minColumn = _puzzles.Select(x => x.column).Min();
            maxColumn = _puzzles.Select(x => x.column).Max();
            minRow = _puzzles.Select(x => x.row).Min();
            maxRow = _puzzles.Select(x => x.row).Max();

            for (int j = minColumn; j <= maxColumn; j++)
            {
                for (int i = minRow; i <= maxRow; i++)
                {
                    if (!_puzzles.Exists(x => x.row == i && x.column == j))
                    {
                        MoveWay(j, i);                       
                    }
                }
            }
        }
      
        private void CompareWithUpPuzzle(int column, int row)
        {
            Puzzle puzzleToCompare = _puzzles.Where(x => x.column == column-1 && x.row == row).First();
            foreach (Puzzle puzzle in _puzzleNotInUse)
            {
                decimal result = puzzleToCompare.bottomImageWall.MyPercentageDifference(puzzle.upImageWall);
                puzzle.min = result;
            }
            decimal minFound = _puzzleNotInUse.Select(x => x.min).Where(z => z != 0).Min();
            Puzzle puzzleFind = Rotate(puzzleToCompare, puzzleEdge.CompareWithUp, minFound);          
            puzzleFind.row = row;
            puzzleFind.column = column;
            SetAllMinValuesToZero();
        }

        private void CompareWithBottomPuzzle(int column, int row)
        {
            Puzzle puzzleToCompare = _puzzles.Where(x => x.column == column+1 && x.row == row).First();
            foreach (Puzzle puzzle in _puzzleNotInUse)
            {
                decimal result = puzzleToCompare.bottomImageWall.MyPercentageDifference(puzzle.upImageWall);
                puzzle.min = result;
            }
            decimal minFound = _puzzleNotInUse.Select(x => x.min).Where(z => z != 0).Min();
            Puzzle puzzleFind = Rotate(puzzleToCompare, puzzleEdge.CompareWithBottom, minFound);         
            puzzleFind.row = row;
            puzzleFind.column = column;
            SetAllMinValuesToZero();
        }

        private void CompareWithLeftPuzzle(int column, int row)
        {
            Puzzle puzzleToCompare = _puzzles.Where(x => x.column == column && x.row == row-1).First();
            foreach (Puzzle puzzle in _puzzleNotInUse)
            {
                decimal result = puzzleToCompare.rightImageWall.MyPercentageDifference(puzzle.leftImageWall);
                puzzle.min = result;
            }
            decimal minFound = _puzzleNotInUse.Select(x => x.min).Where(z => z != 0).Min();
            Puzzle puzzleFind = Rotate(puzzleToCompare, puzzleEdge.CompareWithLeft, minFound);          
            puzzleFind.row = row;
            puzzleFind.column = column;
            SetAllMinValuesToZero();
        }

        private void CompareWithRightPuzzle(int column, int row)
        {
            Puzzle puzzleToCompare = _puzzles.Where(x => x.column == column && x.row == row+1).First();
            foreach (Puzzle puzzle in _puzzleNotInUse)
            {
                decimal result = puzzleToCompare.leftImageWall.MyPercentageDifference(puzzle.rightImageWall);
                 puzzle.min = result;
            }
            decimal minFound = _puzzleNotInUse.Select(x => x.min).Where(z => z != 0).Min();
            Puzzle puzzleFind = Rotate(puzzleToCompare, puzzleEdge.CompareWithRight, minFound);       
            puzzleFind.row = row;
            puzzleFind.column = column;
            SetAllMinValuesToZero();
        }

        public Puzzle Rotate90(Puzzle puzzle, int rotateTime)
        {
            if (rotateTime == 1)
            {
                puzzle.MainImage.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                puzzle = generate.SetPuzzleWalls(puzzle.MainImage,puzzle);
            }
            else if (rotateTime == 2)
            {
                puzzle.MainImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                puzzle = generate.SetPuzzleWalls(puzzle.MainImage,puzzle);
            }
            else if (rotateTime == 3)
            {
                puzzle.MainImage.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);
                puzzle = generate.SetPuzzleWalls(puzzle.MainImage,puzzle);
            }
            return puzzle;

        }

        private Puzzle Rotate(Puzzle puzzleToCompare,puzzleEdge edge, decimal min)
        {
            decimal result = min;
            for (int i = 0; i < _puzzleNotInUse.Count; i++)
            {
                int rotateTime = 0;
                for (int j = 1; j <= 4; j++)
                {
                    _puzzleNotInUse[i] = Rotate90(_puzzleNotInUse[i], 1);
                    switch (edge)
                    {
                        case puzzleEdge.CompareWithRight:
                            result = puzzleToCompare.leftImageWall.MyPercentageDifference(_puzzleNotInUse[i].rightImageWall);
                            break;
                        case puzzleEdge.CompareWithLeft:
                            result = puzzleToCompare.rightImageWall.MyPercentageDifference(_puzzleNotInUse[i].leftImageWall);
                            break;
                        case puzzleEdge.CompareWithBottom:
                            result = puzzleToCompare.upImageWall.MyPercentageDifference(_puzzleNotInUse[i].bottomImageWall);
                            break;
                        case puzzleEdge.CompareWithUp:
                            result = puzzleToCompare.bottomImageWall.MyPercentageDifference(_puzzleNotInUse[i].upImageWall);
                            break;
                    }
                    if (result < min && !_puzzleNotInUse[i].puzzleChecked)
                    {
                        rotateTime=j;
                        _puzzleNotInUse[i].min = result;
                        min = result;
                    }
                }               
                Rotate90(_puzzleNotInUse[i], rotateTime);
            }

            Puzzle puzzleFind = GetMaxFitPuzzle();        
            return puzzleFind;          
        }

        private void SetAllMinValuesToZero()
        {
            foreach (Puzzle puzzle in _puzzleNotInUse)
            {
                puzzle.min = 0;
            }
        }


        private Puzzle GetMaxFitPuzzle()
        {
            decimal min = _puzzleNotInUse.Select(x => x.min).Where(z => z != 0).Min();
            Puzzle maxFitPuzzle = _puzzles.Where(z => z.min == min).First();
            maxFitPuzzle.puzzleChecked = true;
            maxFitPuzzle.min = 0;
            return maxFitPuzzle;
        }

        private void MoveWay(int column, int row)
        {
            if (_puzzles.Exists(x => x.row == row + 1 && x.column == column))
            {
                CompareWithRightPuzzle(column, row);
            }
            else if (_puzzles.Exists(x => x.row == row - 1 && x.column == column))
            {
                CompareWithLeftPuzzle(column, row);
            }
            else if (_puzzles.Exists(x => x.row == row && x.column == column - 1))
            {
                CompareWithUpPuzzle(column, row);
            }
            else if (_puzzles.Exists(x => x.row == row && x.column == column + 1))
            {
                CompareWithBottomPuzzle(column, row);
            }          
            
        }       
    }
}
