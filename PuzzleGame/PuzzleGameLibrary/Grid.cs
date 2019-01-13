using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PuzzleGameLibrary.Interfaces;

namespace PuzzleGameLibrary
{
    public class Grid : IDrawable
    {
        public Vector2 GridSize { get; private set; }
        Vector2 cellCount;
        public int Interval { get; private set; }
        Color gridGolor;

        public Grid(IRenderer renderer, Vector2 Size, Vector2 _cellCount, Color _gridColor)
        {
            cellCount = _cellCount;
            gridGolor = _gridColor;
            GridSize = new Vector2((int)(Size.X * 3f / 4), Size.Y);
            Interval = GridSize.X / cellCount.X;

        }

        public void Draw(IRenderer renderer)
        {
            for (int x = 0; x <= GridSize.X; x += Interval)
            {
                renderer.DrawLine(new Line(new Vector2(x, 0), new Vector2(x, GridSize.Y)), gridGolor);
            }
            for (int y = 0; y <= GridSize.Y; y += Interval)
            {
                renderer.DrawLine(new Line(new Vector2(0, y), new Vector2(GridSize.X, y)), gridGolor);
            }
        }
    }
}
