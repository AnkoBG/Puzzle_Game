using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PuzzleGameLibrary.Interfaces;

namespace PuzzleGameLibrary
{
    [Serializable]
    public class Grid : IDrawable
    {
        public Vector2 GridSize { get; private set; }
        public Vector2 Position { get; private set; }
        public int Interval { get; private set; }
        public bool Right { get; private set; }
        public Vector2 cellCount;
        Color gridGolor;

        public Grid(IRenderer renderer, Vector2 Size, Vector2 _cellCount, Color _gridColor, bool right = false, int interval = 0)
        {

            cellCount = _cellCount;
            gridGolor = _gridColor;
            Right = right;
            if (!Right)
            {
                Position = new Vector2(0, 0);
                Interval = Math.Min((int)(Size.X * 2f / 3) / cellCount.X, (Size.Y - 40) / cellCount.Y);
                GridSize = new Vector2(Interval * cellCount.X, Interval * cellCount.Y);
            }
            else
            {
                Interval = interval;
                GridSize = new Vector2(Interval * cellCount.X, Interval * cellCount.Y);
                Position = new Vector2(Size.X - GridSize.X, 0);
            }
        }

        public void Draw(IRenderer renderer)
        {
            for (int x = 0; x <= GridSize.X; x += Interval)
            {
                renderer.DrawLine(new Line(new Vector2(Position.X + x, Position.Y), new Vector2(Position.X + x, Position.Y + GridSize.Y)), gridGolor);
            }
            for (int y = 0; y <= GridSize.Y; y += Interval)
            {
                renderer.DrawLine(new Line(new Vector2(Position.X, Position.Y + y), new Vector2(Position.X + GridSize.X, Position.Y + y)), gridGolor);
            }
        }

    }
}
