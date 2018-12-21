using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGameLibrary
{
    public class Rectangle
    {
        public Vector2 Position { get; private set; }
        public Vector2 Size { get; private set; }

        public Rectangle(Vector2 pos, Vector2 size)
        {
            Position = pos;
            Size = size;
        }
    }
}
