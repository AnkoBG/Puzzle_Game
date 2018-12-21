using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PuzzleGameLibrary;

namespace PuzzleGame
{
    public partial class Form1 : Form
    {
        FormsRenderer renderer;
        Grid grid;

        public Form1()
        {
            InitializeComponent();
            renderer = new FormsRenderer(CreateGraphics());
            grid = new Grid(renderer, 600, new Vector2(8, 4), Color.DarkGray);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            grid.Draw(renderer);
        }
    }
}
