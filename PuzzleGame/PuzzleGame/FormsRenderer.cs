using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PuzzleGameLibrary;
using PuzzleGameLibrary.Interfaces;
using Rectangle = PuzzleGameLibrary.Rectangle;
using System.Windows.Forms;

namespace PuzzleGame
{
    class FormsRenderer : IRenderer
    {
        Form form;

        public FormsRenderer(Form _form)
        {
            form = _form;
        }


        public void DrawRectangle(Rectangle rect, Color color, int thickness = 3, bool fill = false)
        {
            using (var gfx = form.CreateGraphics())
            {
                if (!fill)
                {
                    using (Pen pen = new Pen(color, thickness))
                        gfx.DrawRectangle(pen, rect.Position.X, rect.Position.Y, rect.Size.X, rect.Size.Y);
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(color))
                        gfx.FillRectangle(brush, rect.Position.X, rect.Position.Y, rect.Size.X, rect.Size.Y);
                }
            }
        }
        public void DrawLine(Line line, Color color, int thickness = 3)
        {
            using (var gfx = form.CreateGraphics())
            {
                using (Pen pen = new Pen(color, thickness))
                {
                    gfx.DrawLine(pen, line.Start.X, line.Start.Y, line.End.X, line.End.Y);
                }
            }
        }

        public void Clear(Color background)
        {
            using (var gfx = form.CreateGraphics())
            {
                gfx.Clear(background);
            }
        }
    }
}
