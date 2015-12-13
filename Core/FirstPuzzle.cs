using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
   public class FirstPuzzle
    {
       List<Puzzle> _puzzles;
       public FirstPuzzle(List<Puzzle> puzzles)
       {
           _puzzles = puzzles;
       }

       public Puzzle GetFirstPuzzle()
       {
           decimal getMinLeft=1000;
           decimal getMinRight = 1000;
           decimal getMinUp = 1000;
           decimal getMinBottom = 1000;
           foreach (Puzzle puzzle in _puzzles)
           {
               for (int i = 0; i < _puzzles.Count; i++)
               {
                   decimal resultMinRight = puzzle.rightImageWall.MyPercentageDifference(_puzzles[i].leftImageWall);
                   if (resultMinRight < getMinRight) getMinRight = resultMinRight;

                   decimal resultMinLeft = puzzle.leftImageWall.MyPercentageDifference(_puzzles[i].rightImageWall);
                   if (resultMinLeft < getMinLeft) getMinLeft = resultMinLeft;

                   decimal resultMinBottom = puzzle.bottomImageWall.MyPercentageDifference(_puzzles[i].upImageWall);
                   if (resultMinBottom < getMinBottom) getMinBottom = resultMinBottom;

                   decimal resultMinUp = puzzle.upImageWall.MyPercentageDifference(_puzzles[i].bottomImageWall);
                   if (resultMinUp < getMinUp) getMinUp = resultMinUp;                
               }
               puzzle.min = getMinLeft + getMinRight + getMinUp + getMinBottom;
               getMinLeft = getMinRight = getMinUp = getMinBottom = 1000;
           }
           Puzzle firstPuzzle = GetMaxFitPuzzle();
           SetAllMinValuesToZero();         
           return firstPuzzle;
       }

       private Puzzle GetMaxFitPuzzle()
       {
           decimal min = _puzzles.Select(x => x.min).Where(z => z != 0).Min();
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
    }
}
