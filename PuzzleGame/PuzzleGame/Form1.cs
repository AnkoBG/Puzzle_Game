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
            grid = new Grid(renderer, 800, new Vector2(8, 4), Color.Red);
            Figure fig1 = new Figure(new Vector2(0, 0), new Vector2(2, 3), new bool[,] { { true, true, true }, { false, false, true }}, Color.DarkGray);
            fig1.Rotate(true);
            fig1.Rotate(true);
            fig1.Rotate(true);
            fig1.Rotate(true);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            grid.Draw(renderer);
        }
    }
}
