using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PuzzleGameLibrary.Interfaces;
using System.Drawing;

namespace PuzzleGameLibrary
{
    [Serializable]
    public class Cell
    {
        public Vector2 Position { get; private set; }
        public int Size { get; private set; }
        public bool IsEmpty { get; private set; }

        public Cell(Vector2 pos, int cellSize, bool isEmpty = false)
        {
            Position = pos;
            Size = cellSize;
            IsEmpty = isEmpty;
        }


    }
}
