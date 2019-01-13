using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PuzzleGameLibrary.Interfaces;
using System.Drawing;

namespace PuzzleGameLibrary
{
    public enum Key { R, F, Invalid }
    public enum MouseKey { Left, Right, Invalid }

    public class Scene
    {
        List<Figure> placedFigures = new List<Figure>(0);
        List<Figure> storedFigures = new List<Figure>(0);
        bool figureIsSelected = false;
        Figure selectedFigure = null;
        IRenderer renderer;
        Grid grid;

        public Scene(Grid _grid, IRenderer _renderer)
        {
            renderer = _renderer;
            grid = _grid;
        }

        public void LoadLevel(string levelPath)
        {
            //TODO
        }

        public void KeyEvent(Key key, Vector2 mousePos)
        {
            switch (key)
            {
                case Key.R:
                    foreach (Figure fig in storedFigures)
                    {
                        fig.Rotate(mousePos);
                    }
                    break;
                case Key.F:
                    foreach (Figure fig in storedFigures)
                    {
                        fig.Flip(mousePos);
                    }
                    break;
                default:
                    break;
            }
                

        }

        public void MouseKeyEvent(MouseKey key, Vector2 mousePos)
        {

            switch (key)
            {
                case MouseKey.Left:
                    if (!figureIsSelected)
                    {
                        selectedFigure = Select(mousePos);
                    }
                    else
                    {

                    }
                    break;
                case MouseKey.Right:
                    break;
                default:
                    break;
            }
        }

        public void Draw()
        {
            grid.Draw(renderer);

            foreach (Figure fig in placedFigures)
            {
                fig.Draw(renderer);
            }
        }

        Figure Select(Vector2 mousePos)
        {
            foreach(Figure fig in storedFigures)
            {
                if (fig.IsInside(mousePos))
                {
                    figureIsSelected = true;
                    return fig;
                }
            }
            return null;
        }
    }
}
