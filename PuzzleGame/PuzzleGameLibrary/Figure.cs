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
    public class Figure
    {
        //TODO
        public Vector2 Position { get; private set; }
        public Vector2 Size { get; private set; }
        public Color figureColor;
        bool[,] cells;

        public Figure(Vector2 _position,Vector2 _size, bool[,] _cells, Color color)
        {
            Position = _position;
            Size = _size;
            cells = _cells;
            figureColor = color;
        }

        public void Draw(IRenderer renderer)
        {
            foreach (bool cell in cells)
            {
                DrawCell(renderer);
            }
        }

        public void DrawCell(IRenderer renderer)
        {
            return;
        }

        public void Rotate(bool right)
        {
            bool[,] newCells = new bool[cells.GetLength(1), cells.GetLength(0)];
            Vector2 newSize = new Vector2(Size.Y, Size.X);
            
            if (right == true)
            {

                for (int i = 0; i < Size.X; i++)
                {
                    for (int j = 0; j < Size.Y; j++)
                    {
                        newCells[newCells.GetLength(0) - 1 - j, i] = cells[i, j];
                    }
                }
            }
            else
            {
                for (int i = 0; i < Size.X; i++)
                {
                    for (int j = 0; j < Size.Y; j++)
                    {
                        newCells[newCells.GetLength(1) - 1 - i, j] = cells[i, j];
                    }
                }
            }
            Size = newSize;
            cells = newCells;
        }
    }
}
