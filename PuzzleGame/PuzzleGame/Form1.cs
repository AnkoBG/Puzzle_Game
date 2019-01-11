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
        Figure fig1;
        PuzzleGameLibrary.Rectangle background = new PuzzleGameLibrary.Rectangle(new Vector2(0, 0), new Vector2(2000, 2000));

        public Form1()
        {
            InitializeComponent();
            renderer = new FormsRenderer(CreateGraphics());
            grid = new Grid(renderer, 800, new Vector2(8, 4), Color.Red);
            fig1 = new Figure(new Vector2(0, 0), new Vector2(2, 3),
                new Cell[,] { 
                { new Cell(new Vector2(0,0),grid.Interval,Color.Black), new Cell(new Vector2(0, 1), grid.Interval, Color.Black), new Cell(new Vector2(0, 2), grid.Interval, Color.Black) },
                { new Cell(new Vector2(1,0),grid.Interval,Color.Black, true), new Cell(new Vector2(1,1),grid.Interval,Color.Black, true), new Cell(new Vector2(1,2),grid.Interval,Color.Black) }
                }, Color.DarkGray);
            
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            grid.Draw(renderer);

            fig1.Draw(renderer);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (fig1.IsInside(new Vector2(e.Location.X, e.Location.Y)))
            {
                fig1.Rotate(false);

                renderer.DrawRectangle(background, Color.White, 3, true);
                grid.Draw(renderer);

                fig1.Draw(renderer);
            }
        }
    }
}
