using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGameLibrary
{
    public class Line
    {
        public Vector2 Start { get; private set; }
        public Vector2 End { get; private set; }

        public Line(Vector2 pt1, Vector2 pt2)
        {
            Start = pt1;
            End = pt2;
        }
    }
}
