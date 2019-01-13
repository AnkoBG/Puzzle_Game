using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PuzzleGameLibrary.Interfaces;
using System.Drawing;

namespace PuzzleGameLibrary
{
    internal class Cell
    {
        //TODO
        
        public Vector2 Position { get; private set; }
        public int Size { get; private set; }
        public Color Color { get; private set; }
        public bool IsEmpty { get; private set; }

        public Cell(Vector2 position, int cellSize, bool isEmpty = false)
        {
            Position = position;
            Size = cellSize;
            IsEmpty = isEmpty;
        }


    }
}
