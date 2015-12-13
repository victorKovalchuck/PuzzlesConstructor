using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Core
{
    [Serializable]
    public class Puzzle:ICloneable
    {
        public Image leftImageWall;
        public Image rightImageWall;
        public Image upImageWall;
        public Image bottomImageWall;

        public decimal min;
        public decimal percentageDifferencePuzzleInRow;

        public Image MainImage;

        public bool leftImagePlaced = false;
        public bool rightImagePlaced = false;
        public bool upImagePlaced = false;
        public bool bottomImagePlaced = false;

        public bool leftImageWallHasIdenticalPixelColor = false;
        public bool rightImageWallHasIdenticalPixelColor = false;
        public bool upImageWallHasIdenticalPixelColor = false;
        public bool bottomImageWallHasIdenticalPixelColor = false;

        public int row;
        public int column;
        public bool puzzleChecked = false;
     
        public object Clone()
        {
            BinaryFormatter BF = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();

            BF.Serialize(memStream, this);
            memStream.Position = 0;

            return (Puzzle)(BF.Deserialize(memStream));
        }
    }
}
