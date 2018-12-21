using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PuzzleGameLibrary;
using PuzzleGameLibrary.Interfaces;
using Rectangle = PuzzleGameLibrary.Rectangle;

namespace PuzzleGame
{
    class FormsRenderer : IRenderer
    {
        Graphics activeFormGfx;

        public FormsRenderer(Graphics gfx)
        {
            activeFormGfx = gfx;
        }


        public void DrawRectangle(Rectangle rect, Color color, int thickness = 3, bool fill = false)
        {
            if (!fill)
            {
                using (Pen pen = new Pen(color, thickness))
                    activeFormGfx.DrawRectangle(pen, rect.Position.X, rect.Position.Y, rect.Size.X, rect.Size.Y);
            }
            else
            {
                using (SolidBrush brush = new SolidBrush(color))
                    activeFormGfx.FillRectangle(brush, rect.Position.X, rect.Position.Y, rect.Size.X, rect.Size.Y);
            }
        }
        public void DrawLine(Line line, Color color, int thickness = 3)
        {
            using (Pen pen = new Pen(color, thickness))
                activeFormGfx.DrawLine(pen, line.Start.X, line.Start.Y, line.End.X, line.End.Y);
        }
    }
}
