using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PuzzleGameLibrary.Interfaces
{
    public interface IRenderer
    {
        void DrawRectangle(Rectangle rect, Color color, int thickness = 3, bool fill = false);
        void Clear(Color background);
        void DrawLine(Line line, Color color, int thickness = 3);
    }
}
