using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PuzzleGameLibrary.Interfaces;
using System.Collections;

namespace PuzzleGameLibrary
{
    [Serializable]
    public class Figure : IDrawable
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; private set; }
        public Color figureColor;
        public Cell[,] cells;
        public bool isStatic;

        public Figure(Vector2 _position, Cell[,] _cells, Color color, bool _isStatic = false)
        {
            Position = _position;
            Size = new Vector2(_cells.GetLength(0), _cells.GetLength(1));
            cells = _cells;
            figureColor = color;
            isStatic = _isStatic;
        }

        public void Draw(IRenderer renderer)
        {
            foreach (Cell cell in cells)
            {
                if (!cell.IsEmpty)
                {
                    renderer.DrawRectangle(new Rectangle(Position + cell.Position * cell.Size, new Vector2(cell.Size, cell.Size)), figureColor, 10, true);
                    if(isStatic)
                        renderer.DrawRectangle(new Rectangle(Position + cell.Position * cell.Size, new Vector2(cell.Size, cell.Size)), Color.Black, 3);
                }
            }
        }

        public bool IsInside(Vector2 mousePos)
        {
            foreach (Cell cell in cells)
            {
                if (!cell.IsEmpty)
                    if (mousePos.X >= Position.X + cell.Position.X * cell.Size
                        && mousePos.Y >= Position.Y + cell.Position.Y * cell.Size
                        && mousePos.X <= Position.X + cell.Position.X * cell.Size + cell.Size
                        && mousePos.Y <= Position.Y + cell.Position.Y * cell.Size + cell.Size)
                        return true;
            }
            return false;
        }

        public void Rotate()
        {
            Cell[,] newCells = new Cell[Size.Y, Size.X];
            Vector2 newSize = new Vector2(Size.Y, Size.X);
            
            for (int j = 0; j < Size.Y; j++)
            {
                for (int i = 0; i < Size.X; i++)
                {
                    newCells[j, newCells.GetLength(1) - 1 - i] = new Cell(new Vector2(j, newCells.GetLength(1) - 1 - i), cells[i, j].Size, cells[i, j].IsEmpty);
                }
            }

            Size = newSize;
            cells = newCells;  
        }

        public void Flip()
        {
            // Flips horizontally, I think it works now, but I've not tested it thoroughly
            if (Size.Y > 1)
            {
                Cell[,] newCells = new Cell[Size.X, Size.Y];

                for (int j = 0; j < Size.Y; j++)
                {
                    for (int i = 0; i < Size.X; i++)
                    {
                        newCells[newCells.GetLength(0) - 1 - i, j] = new Cell(new Vector2(newCells.GetLength(0) - 1 - i, j), cells[i, j].Size, cells[i, j].IsEmpty);
                    }
                }
                cells = newCells;
            }
        }

        public Figure SnapToGrid(Grid grid)
        {
            Position = Position - new Vector2(Position.X % grid.Interval, Position.Y % grid.Interval);
            return this;
        }

        public bool CanPlace(Grid grid, List<Figure> placedFigures)
        {
            bool outOfBounds = true;
            bool overlaps = false;
            Vector2 startPos = new Vector2(Position.X / grid.Interval, Position.Y / grid.Interval);

            if(startPos.X >= 0 && startPos.Y >= 0 && (startPos.X + Size.X) <= grid.cellCount.X && (startPos.Y + Size.Y) <= grid.cellCount.Y)
                outOfBounds = false;

            foreach(Figure fig in placedFigures)
                foreach(Cell placedCell in fig.cells)
                {
                    Vector2 placedCellPos = new Vector2(fig.Position.X + placedCell.Position.X * grid.Interval, fig.Position.Y + placedCell.Position.Y * grid.Interval);
                    foreach (Cell cell in this.cells)
                    {
                        Vector2 cellPos = new Vector2(Position.X + cell.Position.X * grid.Interval, Position.Y + cell.Position.Y * grid.Interval);
                        if (!placedCell.IsEmpty && !cell.IsEmpty)
                            if (cellPos.X == placedCellPos.X && cellPos.Y == placedCellPos.Y)
                                overlaps = true;
                    }
                }

            return (!outOfBounds && !overlaps);
        }
    }
}
