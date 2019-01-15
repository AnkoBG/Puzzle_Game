using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using PuzzleGameLibrary.Interfaces;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

namespace PuzzleGameLibrary
{
    public enum Key { R, F, A, D, F5, Invalid }
    public enum MouseKey { Left, Right, Invalid }

    public class Scene
    {
        Level level;
        public bool gameWon = false;

        List<Figure> placedFigures = new List<Figure>(0);
        List<Figure> storedFigures = new List<Figure>(0);
        int storedFigsIndex = 0;

        Figure selectedFigure = null;
        Figure draggedFigure = null;
        Vector2 dragOffset = null;

        IRenderer renderer;

        Grid grid;
        Grid rightGrid;

        Rectangle background = new Rectangle(new Vector2(0, 0), new Vector2(2000, 2000));

        public Scene(Level _level, IRenderer _renderer)
        {
            renderer = _renderer;
            level = _level;
            grid = level.Grid;
            rightGrid = level.RightGrid;
            placedFigures = level.placedFigures;
            storedFigures = level.storedFigures;
            foreach(Figure fig in storedFigures)
            {
                fig.Position = rightGrid.Position;
            }
            selectedFigure = storedFigures[0];
        }

        public Scene(string levelPath, IRenderer _renderer)
        {
            renderer = _renderer;
            level = new Level(levelPath);
            grid = level.Grid;
            rightGrid = level.RightGrid;
            placedFigures = level.placedFigures;
            storedFigures = level.storedFigures;
            selectedFigure = storedFigures[0];
        }

        public void KeyEvent(Key key, Vector2 mousePos)
        {
            switch (key)
            {
                case Key.R:
                    if(selectedFigure != null)
                    {
                        selectedFigure.Rotate();
                        Draw();
                    }
                    break;
                case Key.F:
                    if (selectedFigure != null)
                    {
                        selectedFigure.Flip();
                        Draw();
                    }
                    break;
                case Key.A:
                    if (storedFigures.Count > 1)
                    {
                        if(storedFigsIndex > 0)
                            storedFigsIndex--;
                        else
                            storedFigsIndex = storedFigures.Count - 1;
                        if(draggedFigure == null)
                            selectedFigure = storedFigures[storedFigsIndex];
                        Draw();
                    }
                    break;
                case Key.D:
                    if (storedFigures.Count > 1)
                    {
                        if (storedFigsIndex < storedFigures.Count - 1)
                            storedFigsIndex++;
                        else
                            storedFigsIndex = 0;
                        if (draggedFigure == null)
                            selectedFigure = storedFigures[storedFigsIndex];
                        Draw();
                    }
                    break;
                case Key.F5:
                    //Meant for Continue function, currently overwrites level savefile
                    level.SerializeLevel();
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
                    if (draggedFigure == null)
                    {
                        if (mousePos.X >= rightGrid.Position.X && mousePos.Y <= rightGrid.GridSize.Y)
                        {
                            if(storedFigures.Count > 0)
                            {
                                if (storedFigures[storedFigsIndex].IsInside(mousePos))
                                {
                                    draggedFigure = storedFigures[storedFigsIndex];
                                    selectedFigure = draggedFigure;
                                    dragOffset = mousePos - draggedFigure.Position;
                                }
                                storedFigures.Remove(draggedFigure);
                                storedFigsIndex = 0;
                            }
                        }
                        else if (mousePos.X <= grid.GridSize.X && mousePos.Y <= grid.GridSize.Y)
                        {
                            if (placedFigures.Count > 0)
                            {
                                foreach(Figure fig in placedFigures)
                                {
                                    if (fig.IsInside(mousePos) && !fig.isStatic)
                                    {
                                        draggedFigure = fig;
                                        selectedFigure = fig;
                                        dragOffset = mousePos - draggedFigure.Position;
                                        break;
                                    }
                                }
                                placedFigures.Remove(draggedFigure);
                            }
                        }
                    }
                    else
                    {
                        if(mousePos.X <= grid.GridSize.X + draggedFigure.Size.X * grid.Interval && mousePos.Y <= grid.GridSize.Y + draggedFigure.Size.Y * grid.Interval)
                        {
                            draggedFigure.SnapToGrid(grid);
                            if (draggedFigure.CanPlace(grid, placedFigures))
                            {
                                placedFigures.Add(draggedFigure);
                                if (storedFigures.Count > 0)
                                    selectedFigure = storedFigures[0];
                                else
                                    selectedFigure = null;
                                draggedFigure = null;
                                if (storedFigures.Count == 0)
                                    gameWon = true;
                                else
                                    Draw();
                            }
                        }
                    }
                    break;
                case MouseKey.Right:
                    if(draggedFigure != null)
                    {
                        draggedFigure.Position = rightGrid.Position;
                        storedFigures.Insert(0, draggedFigure);
                        draggedFigure = null;
                        storedFigsIndex = 0;
                        selectedFigure = storedFigures[storedFigsIndex];

                        Draw();
                    }
                    else if (mousePos.X <= grid.GridSize.X && mousePos.Y <= grid.GridSize.Y)
                    {
                        Figure temp = null;
                        foreach(Figure fig in placedFigures)
                        {
                            if (fig.IsInside(mousePos) && !fig.isStatic)
                            {
                                temp = fig;
                                break;
                            }
                        }
                        if(temp != null)
                        {
                            placedFigures.Remove(temp);
                            temp.Position = rightGrid.Position;
                            storedFigures.Insert(0, temp);
                            storedFigsIndex = 0;
                            selectedFigure = storedFigures[storedFigsIndex];

                            Draw();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void MouseMoveEvent(Vector2 mousePos)
        {
            if(draggedFigure != null)
            {
                draggedFigure.Position = mousePos - dragOffset;
                Draw();
            }
        }

        public void Draw()
        {
            renderer.DrawRectangle(background, Color.White, 3, true);
            grid.Draw(renderer);
            rightGrid.Draw(renderer);

            foreach (Figure fig in placedFigures)
            {
                fig.Draw(renderer);
            }
            if(storedFigures.Count > 0)
            {
                storedFigures[storedFigsIndex].Draw(renderer);
            }
            if (draggedFigure != null)
                draggedFigure.Draw(renderer);
        }
    }
}
