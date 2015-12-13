using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XnaFan.ImageComparison;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public class ThreeEdgeCompareMechanism
    {
        public enum movingWay { Up, Bottom, Left, Right };
        List<Puzzle> _puzzles;
        private object locks = new object(); 
        public ThreeEdgeCompareMechanism(List<Puzzle> puzzles)
        {
            _puzzles = puzzles;
        }

        public void CompareWalls(Puzzle puzzle)
        {
            puzzle.puzzleChecked = true;
                      
            Puzzle bottomPuzzle = puzzle.Clone() as Puzzle;
            Puzzle upPuzzle = puzzle.Clone() as Puzzle;
            Puzzle rightPuzzle = puzzle.Clone() as Puzzle;
            Puzzle leftPuzzle = puzzle.Clone() as Puzzle;
       
            RecognizeRow(bottomPuzzle, movingWay.Bottom);
            RecognizeRow(upPuzzle, movingWay.Up);
            RecognizeColumn(rightPuzzle, movingWay.Right);
            RecognizeColumn(leftPuzzle, movingWay.Left);         
        }

        private void RecognizeColumn(Puzzle puzzle, movingWay way)
        {
            while (puzzle != null)
            {
                Puzzle upPuzzle = puzzle.Clone() as Puzzle;
                Puzzle bottomPuzzle = puzzle.Clone() as Puzzle;
                while (bottomPuzzle != null)
                {
                    bottomPuzzle = FindBottomPuzzle(bottomPuzzle);

                }
                while (upPuzzle != null)
                {
                    upPuzzle = FindUpPuzzle(upPuzzle);
                }
                if (way == movingWay.Right)
                {
                    puzzle = FindRightPuzzle(puzzle);
                    if (puzzle != null)
                        puzzle = CheckForRowMaxPuzzles(puzzle, movingWay.Right);
                }
                else
                {
                    puzzle = FindLeftPuzzle(puzzle);
                    if (puzzle != null)
                        puzzle = CheckForRowMaxPuzzles(puzzle, movingWay.Left);
                }
            }
        }

        private void RecognizeRow(Puzzle puzzle,movingWay way)
        {
          Puzzle firstPuzzle  = puzzle.Clone() as Puzzle;
          while (firstPuzzle != null)
            {

                Puzzle rightPuzzle = firstPuzzle.Clone() as Puzzle;
                Puzzle leftPuzzle = firstPuzzle.Clone() as Puzzle;
                while (rightPuzzle != null)
                {
                    rightPuzzle = FindRightPuzzle(rightPuzzle);

                    if (rightPuzzle != null)
                    {                    
                        rightPuzzle = CheckForRowMaxPuzzles(rightPuzzle,movingWay.Right);
                    }
                   
                }
                while (leftPuzzle != null)
                {
                    leftPuzzle = FindLeftPuzzle(leftPuzzle);
                    if (leftPuzzle != null)
                    {
                        leftPuzzle = CheckForRowMaxPuzzles(leftPuzzle, movingWay.Left);
                    }
                }
                if (way == movingWay.Up)
                {
                    firstPuzzle = FindUpPuzzle(firstPuzzle);
                }
                else
                {
                    firstPuzzle = FindBottomPuzzle(firstPuzzle);
                }
            }
        }

        private Puzzle FindUpPuzzle(Puzzle upPuzzle)
        {
            if (_puzzles.Exists(x => x.column == upPuzzle.column - 1 && x.row == upPuzzle.row))
            {
                return _puzzles.Where((x => x.column == upPuzzle.column - 1 && x.row == upPuzzle.row)).First();
            }
            decimal percentageDifferencePuzzleInRow=0;
            List<Puzzle> allMuchedPuzzles = new List<Puzzle>();
            Puzzle right_leftMuchPuzzleWay1 = GoRight(upPuzzle);
            Puzzle up_bottomMuchPuzzleWay1 = GoUp(right_leftMuchPuzzleWay1);
            Puzzle resultWay1 = GoLeft(up_bottomMuchPuzzleWay1);      
            if (resultWay1 != null)
            {
                allMuchedPuzzles.Add(resultWay1);               
            }

            Puzzle resultWay2 = GoUp(upPuzzle);
            percentageDifferencePuzzleInRow = resultWay2.percentageDifferencePuzzleInRow;
            allMuchedPuzzles.Add(resultWay2);

            Puzzle left_rightMuchPuzzleWay3 = GoLeft(upPuzzle);
            Puzzle up_bottomMuchPuzzleWay3 = GoUp(left_rightMuchPuzzleWay3);
            Puzzle resultWay3 = GoRight(up_bottomMuchPuzzleWay3);           
            if (resultWay3 != null)
            {
                allMuchedPuzzles.Add(resultWay3);             
            }

            if (allMuchedPuzzles.Count > 2)
            {
                Puzzle muchedPuzzle = AdditionFilter(allMuchedPuzzles, upPuzzle);
                if (muchedPuzzle != null)
                {
                    SetPuzzleOrder(muchedPuzzle, upPuzzle, movingWay.Up);
                    muchedPuzzle.percentageDifferencePuzzleInRow = percentageDifferencePuzzleInRow;
                }
                return muchedPuzzle;
            }      
            return null;
        }
  
        private Puzzle FindBottomPuzzle(Puzzle bottomPuzzle)
        {
            if (_puzzles.Exists(x => x.column == bottomPuzzle.column + 1 && x.row == bottomPuzzle.row))
            {
                return _puzzles.Where((x => x.column == bottomPuzzle.column + 1 && x.row == bottomPuzzle.row)).First();
            }
            decimal percentageDifferencePuzzleInRow = 0;
            List<Puzzle> allMuchedPuzzles = new List<Puzzle>();
            Puzzle right_leftMuchPuzzleWay1 = GoRight(bottomPuzzle);
            Puzzle bottom_upMuchPuzzleWay1 = GoBottom(right_leftMuchPuzzleWay1);
            Puzzle resultWay1 = GoLeft(bottom_upMuchPuzzleWay1);       
            if (resultWay1 != null)
            {
                allMuchedPuzzles.Add(resultWay1);         
            }
            Puzzle resultWay2 = GoBottom(bottomPuzzle);
            percentageDifferencePuzzleInRow = resultWay2.percentageDifferencePuzzleInRow;
            allMuchedPuzzles.Add(resultWay2);

            Puzzle left_rightMuchPuzzleWay3 = GoLeft(bottomPuzzle);
            Puzzle bottom_upMuchPuzzleWay3 = GoBottom(left_rightMuchPuzzleWay3);
            Puzzle resultWay3 = GoRight(bottom_upMuchPuzzleWay3);       
            if (resultWay3 != null)
            {
                allMuchedPuzzles.Add(resultWay3);               
            }
        
            if (allMuchedPuzzles.Count >2)
            {
                Puzzle muchedPuzzle = AdditionFilter(allMuchedPuzzles, bottomPuzzle);
                if (muchedPuzzle != null)
                {
                    SetPuzzleOrder(muchedPuzzle, bottomPuzzle, movingWay.Bottom);
                    muchedPuzzle.percentageDifferencePuzzleInRow = percentageDifferencePuzzleInRow;
                }
                return muchedPuzzle;
            }
            return null;
        }       

        private Puzzle FindLeftPuzzle(Puzzle leftPuzzle)
        {
            decimal percentageDifferencePuzzleInRow = 0;
            List<Puzzle> allMuchedPuzzles = new List<Puzzle>();
            Puzzle bottom_upMuchPuzzleWay1 = GoBottom(leftPuzzle);
            Puzzle left_rightMuchPuzzleWay1 = GoLeft(bottom_upMuchPuzzleWay1);
            Puzzle resultWay1 = GoUp(left_rightMuchPuzzleWay1);      
            if (resultWay1 != null)
            {
                allMuchedPuzzles.Add(resultWay1);           
            }
            Puzzle resultWay2 = GoLeft(leftPuzzle);
            percentageDifferencePuzzleInRow = resultWay2.percentageDifferencePuzzleInRow;
            allMuchedPuzzles.Add(resultWay2);

            Puzzle up_bottomMuchPuzzleWay3 = GoUp(leftPuzzle);
            Puzzle left_rightMuchPuzzleWay3 = GoLeft(up_bottomMuchPuzzleWay3);
            Puzzle resultWay3 = GoBottom(left_rightMuchPuzzleWay3);           
            if (resultWay3 != null)
            {
                allMuchedPuzzles.Add(resultWay3);              
            }           
           
            if (allMuchedPuzzles.Count> 2)
            {
                Puzzle muchedPuzzle = AdditionFilter(allMuchedPuzzles, leftPuzzle);
                if (muchedPuzzle != null)
                {
                    SetPuzzleOrder(muchedPuzzle, leftPuzzle, movingWay.Left);
                    muchedPuzzle.percentageDifferencePuzzleInRow = percentageDifferencePuzzleInRow;
                }
                return muchedPuzzle;
            }
            return null;
        }
     
        private Puzzle FindRightPuzzle(Puzzle rightPuzzle)
        {
            decimal percentageDifferencePuzzleInRow = 0;
            List<Puzzle> allMuchedPuzzles = new List<Puzzle>();
            Puzzle bottom_upMuchPuzzleWay1 = GoBottom(rightPuzzle);
            Puzzle right_leftMuchPuzzleWay1 = GoRight(bottom_upMuchPuzzleWay1);

            Puzzle resultWay1 = GoUp(right_leftMuchPuzzleWay1);     
            if (resultWay1 != null)
            {
                allMuchedPuzzles.Add(resultWay1);              
            }
            Puzzle resultWay2 = GoRight(rightPuzzle);
            percentageDifferencePuzzleInRow = resultWay2.percentageDifferencePuzzleInRow;
            allMuchedPuzzles.Add(resultWay2);

            Puzzle up_bottomMuchPuzzleWay3 = GoUp(rightPuzzle);
            Puzzle right_leftMuchPuzzleWay3 = GoRight(up_bottomMuchPuzzleWay3);
            Puzzle resultWay3 = GoBottom(right_leftMuchPuzzleWay3);          
            if (resultWay3 != null)
            {
                allMuchedPuzzles.Add(resultWay3);               
            }
        
            if (allMuchedPuzzles.Count > 2)
            {             
                Puzzle muchedPuzzle = AdditionFilter(allMuchedPuzzles,rightPuzzle);
                if (muchedPuzzle != null)
                {
                    SetPuzzleOrder(muchedPuzzle, rightPuzzle, movingWay.Right);
                    muchedPuzzle.percentageDifferencePuzzleInRow = percentageDifferencePuzzleInRow;
                }
                return muchedPuzzle;
            }           
            return null;

        }

        private Puzzle AdditionFilter(List<Puzzle> puzzles, Puzzle basePuzzle)
        {
            int maxGroupCount = 0;
            Puzzle maxRepeatedItem;
            if (puzzles.Count != 0)
            {
                maxGroupCount = (from x in puzzles
                     group x by x into g
                     let count = g.Count()
                     orderby count descending
                     select (int)count)
                     .ToList()
                     .First();
            }

            if (maxGroupCount < 3)
            {
                return null;
            }
           
            maxRepeatedItem = puzzles.GroupBy(x => x)
                     .OrderByDescending(x => x.Count())
                     .First().Key;       
            return maxRepeatedItem;
        }


        private Puzzle GoRight(Puzzle puzzle)
        {
            for (int i = 0; i < _puzzles.Count; i++)
            {
                decimal result = puzzle.rightImageWall.MyPercentageDifference(_puzzles[i].leftImageWall);
                _puzzles[i].min = result;
            }
            Puzzle puzzleFind = GetMaxFitPuzzle();
            SetAllMinValuesToZero();

            return puzzleFind;
        }

        private Puzzle GoBottom(Puzzle puzzle)
        {
            for (int i = 0; i < _puzzles.Count; i++)
            {
                decimal result = puzzle.bottomImageWall.MyPercentageDifference(_puzzles[i].upImageWall);
                _puzzles[i].min = result;
            }
            Puzzle puzzleFind = GetMaxFitPuzzle();
            SetAllMinValuesToZero();

            return puzzleFind;
        }

        private Puzzle GoLeft(Puzzle puzzle)
        {
            for (int i = 0; i < _puzzles.Count; i++)
            {
                decimal result = puzzle.leftImageWall.MyPercentageDifference(_puzzles[i].rightImageWall);
                _puzzles[i].min = result;
            }
            Puzzle puzzleFind = GetMaxFitPuzzle();
            SetAllMinValuesToZero();

            return puzzleFind;
        }

        private Puzzle GoUp(Puzzle puzzle)
        {
            List<Puzzle> upPuzzlesMuch = new List<Puzzle>();
            for (int i = 0; i < _puzzles.Count; i++)
            {
                decimal result = puzzle.upImageWall.MyPercentageDifference(_puzzles[i].bottomImageWall);
                _puzzles[i].min = result;
            }
            Puzzle puzzleFind = GetMaxFitPuzzle();
            SetAllMinValuesToZero();

            return puzzleFind;
        }

        private Puzzle GetMaxFitPuzzle()
        {
            decimal min = _puzzles.Select(x => x.min).Where(z=>z!=0).Min();
            Puzzle maxFitPuzzle = _puzzles.Where(z => z.min == min).First();
            maxFitPuzzle.percentageDifferencePuzzleInRow = min;
            maxFitPuzzle.min = 0;
            return maxFitPuzzle;
        }

      
        private void SetAllMinValuesToZero()
        {
            foreach (Puzzle puzzle in _puzzles)
            {
                puzzle.min = 0;
            }
        }

        private Puzzle CheckForRowMaxPuzzles(Puzzle puzzle,movingWay way)
        {
            int maxLength = (int)Math.Sqrt(_puzzles.Count);
            if (Math.Abs(puzzle.row) >= maxLength)
            {
                List<Puzzle> puzzlesRow;    
                bool deleteNextRows = false;
                if (way == movingWay.Right)
                {
                    puzzlesRow = _puzzles.Where(x => x.column == puzzle.column && x.row > 0).OrderBy(x => x.row).ToList();
                    if (puzzlesRow.Count == 0) return null;
                }
                else
                {
                    puzzlesRow = _puzzles.Where(x => x.column == puzzle.column && x.row < 0).OrderByDescending(x => x.row).ToList();
                    if (puzzlesRow.Count == 0) return null;
                }
                decimal max = puzzlesRow.Select(x => x.percentageDifferencePuzzleInRow).Max();

                for (int i = 1; i < puzzlesRow.Count; i++)
                {
                    if (puzzlesRow[i].percentageDifferencePuzzleInRow == max | deleteNextRows)
                    {
                        puzzlesRow[i].row = 0;            
                        deleteNextRows = true;
                    }
                }
                return null;
            }
            return puzzle;
        }

        private void SetPuzzleOrder(Puzzle puzzle,Puzzle basePuzle, movingWay way)
        {
            switch (way)
            {
                case movingWay.Right:
                    basePuzle.puzzleChecked = true;
                    puzzle.row = basePuzle.row + 1;
                    puzzle.column = basePuzle.column;
                    break;
                case movingWay.Bottom:
                    basePuzle.puzzleChecked = true;
                    puzzle.row = basePuzle.row;
                    puzzle.column = basePuzle.column + 1;
                    break;
                case movingWay.Left:
                    basePuzle.puzzleChecked = true;
                    puzzle.row = basePuzle.row - 1;
                    puzzle.column = basePuzle.column;
                    break;
                case movingWay.Up:
                    basePuzle.puzzleChecked = true;
                    puzzle.row = basePuzle.row;
                    puzzle.column = basePuzle.column - 1;
                    break;
            }
        }
    }
}
