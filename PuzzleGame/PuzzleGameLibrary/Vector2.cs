using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGameLibrary
{
    public class Vector2
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Vector2(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }
    }
}
