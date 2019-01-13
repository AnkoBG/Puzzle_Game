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
    internal class Figure : IDrawable
    {
        //TODO
        public Vector2 Position { get; private set; }
        public Vector2 Size { get; private set; }
        public Color figureColor;
        public Cell[,] cells;

        public Figure(Vector2 _position, Cell[,] _cells, Color color)
        {
            Position = _position;
            Size = new Vector2(_cells.GetLength(0), _cells.GetLength(1));
            cells = _cells;
            figureColor = color;
        }

        public void Draw(IRenderer renderer)
        {
            foreach (Cell cell in cells)
            {
                if(!cell.IsEmpty)
                    renderer.DrawRectangle(new Rectangle(Position + cell.Position * cell.Size, new Vector2(cell.Size, cell.Size)), figureColor, 10, true);
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

        public void Rotate(Vector2 mousePos)
        {
            if (IsInside(mousePos))
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
            
        }

        public void Flip(Vector2 mousePos)
        {
            if (IsInside(mousePos))
            {
                Cell[,] newCells = new Cell[Size.X, Size.Y];

                for (int i = 0; i < Size.X; i++)
                {
                    for (int j = 0; j < Size.Y; j++)
                    {
                        newCells[i, newCells.GetLength(0) - 1 - j] = new Cell(new Vector2(i, newCells.GetLength(0) - 1 - j), cells[i, j].Size, cells[i, j].IsEmpty);
                    }
                }
                cells = newCells;
            }
        }
    }
}
