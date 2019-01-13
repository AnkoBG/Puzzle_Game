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
        Scene scene;
        PuzzleGameLibrary.Rectangle background = new PuzzleGameLibrary.Rectangle(new Vector2(0, 0), new Vector2(2000, 2000));


        public Form1()
        {
            InitializeComponent();
            renderer = new FormsRenderer(CreateGraphics());
            grid = new Grid(renderer, new Vector2(Size.Width, Size.Height), new Vector2(6, 3), Color.Red);
            scene = new Scene(grid,renderer);
            //fig1 = new Figure(new Vector2(grid.Interval*5, 0),
            //    new Cell[,] { 
            //   { new Cell(new Vector2(0,0),grid.Interval), new Cell(new Vector2(1, 0), grid.Interval),new Cell(new Vector2(2, 0), grid.Interval) },
            //    { new Cell(new Vector2(0,1),grid.Interval, true), new Cell(new Vector2(1,1),grid.Interval, true),new Cell(new Vector2(2,1),grid.Interval) },
            //    { new Cell(new Vector2(0,2),grid.Interval, true), new Cell(new Vector2(1,2),grid.Interval, true),new Cell(new Vector2(2,2),grid.Interval) }
            //    }, Color.DarkGray);

        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            scene.Draw();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
        }
    }
}
