using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PuzzleGameLibrary.Interfaces;

namespace PuzzleGameLibrary
{
    public class Cell
    {
        //TODO

        public Vector2 Position { get; private set; }
        public Vector2 Size { get; private set; }

        public Cell()
        {

        }


        public void Draw(IRenderer renderer)
        {
            
        }
    }
}
