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
        internal Level Level { get; private set; }
        public bool gameWon = false;

        List<Figure> placedFigures = new List<Figure>(0);
        List<Figure> storedFigures = new List<Figure>(0);
        int shownFigureIndex = 0;

        Figure selectedFigure = null;
        Figure draggedFigure = null;
        Vector2 dragOffset = null;

        public IRenderer Renderer { get; private set; }

        public Grid Grid { get; private set; }
        public Grid RightGrid { get; private set; }

        Rectangle background;

        public Scene(string levelPath, Vector2 windowSize, IRenderer renderer)
        {
            Renderer = renderer;
            Level = new Level(levelPath);
            Grid = Level.Grid;
            RightGrid = Level.RightGrid;
            placedFigures = Level.placedFigures;
            storedFigures = Level.storedFigures;
            selectedFigure = storedFigures[0];
            background = new Rectangle(new Vector2(0, 0), windowSize);
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
                        if(shownFigureIndex > 0)
                            shownFigureIndex--;
                        else
                            shownFigureIndex = storedFigures.Count - 1;
                        if(draggedFigure == null)
                            selectedFigure = storedFigures[shownFigureIndex];
                        Draw();
                    }
                    break;
                case Key.D:
                    if (storedFigures.Count > 1)
                    {
                        if (shownFigureIndex < storedFigures.Count - 1)
                            shownFigureIndex++;
                        else
                            shownFigureIndex = 0;
                        if (draggedFigure == null)
                            selectedFigure = storedFigures[shownFigureIndex];
                        Draw();
                    }
                    break;
                case Key.F5:  //Meant for Continue function, currently overwrites current level savefile
                    Level.SerializeLevel();
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
                        if (mousePos.X >= RightGrid.Position.X && mousePos.Y <= RightGrid.GridSize.Y && storedFigures.Count > 0)
                        {
                            if (storedFigures[shownFigureIndex].IsInside(mousePos))
                            {
                                draggedFigure = storedFigures[shownFigureIndex];
                                selectedFigure = draggedFigure;
                                dragOffset = mousePos - draggedFigure.Position;
                            }
                            storedFigures.Remove(draggedFigure);
                            shownFigureIndex = 0;
                        }
                        else if (mousePos.X <= Grid.GridSize.X && mousePos.Y <= Grid.GridSize.Y && placedFigures.Count > 0)
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
                    else
                    {
                        if(mousePos.X <= Grid.GridSize.X + draggedFigure.Size.X * Grid.Interval && mousePos.Y <= Grid.GridSize.Y + draggedFigure.Size.Y * Grid.Interval)
                        {
                            if (draggedFigure.SnapToGrid(Grid).CanPlace(Grid, placedFigures))
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
                        draggedFigure.Position = RightGrid.Position;
                        storedFigures.Insert(0, draggedFigure);
                        draggedFigure = null;
                        shownFigureIndex = 0;
                        selectedFigure = storedFigures[shownFigureIndex];

                        Draw();
                    }
                    else if (mousePos.X <= Grid.GridSize.X && mousePos.Y <= Grid.GridSize.Y)
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
                            temp.Position = RightGrid.Position;
                            storedFigures.Insert(0, temp);
                            shownFigureIndex = 0;
                            selectedFigure = storedFigures[shownFigureIndex];

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
            Renderer.DrawRectangle(background, Color.White, 3, true);
            Grid.Draw(Renderer);
            RightGrid.Draw(Renderer);

            for (int i = placedFigures.Count - 1; i >= 0; i--)
            {
                placedFigures[i].Draw(Renderer);
            }
            if(storedFigures.Count > 0)
            {
                storedFigures[shownFigureIndex].Draw(Renderer);
            }
            if (draggedFigure != null)
                draggedFigure.Draw(Renderer);
        }
    }
}
