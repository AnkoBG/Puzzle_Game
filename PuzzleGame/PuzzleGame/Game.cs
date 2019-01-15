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
    public partial class Game : Form
    {
        FormsRenderer renderer;
        Grid grid;
        Grid rightGrid;
        Scene scene;
        PuzzleGameLibrary.Rectangle background = new PuzzleGameLibrary.Rectangle(new Vector2(0, 0), new Vector2(2000, 2000));
        Menu mainMenu;
        

        public Game(Menu _mainMenu)
        {
            //To be used when creating custom levels, currently unusable
            InitializeComponent();
            mainMenu = _mainMenu;
            renderer = new FormsRenderer(CreateGraphics());

            grid = new Grid(renderer, new Vector2(Width, Height), new Vector2(11, 5), Color.LightGray);
            rightGrid = new Grid(renderer, new Vector2(Width, Height), new Vector2(4, 4), Color.LightGray, true, grid.Interval);

            List<Figure> pf = new List<Figure>(0);
            List<Figure> sf = new List<Figure>(0);

            Level level = new Level("Master_1.bin");
            scene = new Scene(level, renderer);
            
        }

        public Game(Menu _mainMenu, string levelName)
        {
            InitializeComponent();
            mainMenu = _mainMenu;
            renderer = new FormsRenderer(CreateGraphics());
            grid = new Grid(renderer, new Vector2(Width, Height), new Vector2(11, 5), Color.Red);
            rightGrid = new Grid(renderer, new Vector2(Width, Height), new Vector2(4, 4), Color.Green, true, grid.Interval);
            Level level = new Level(levelName);
            scene = new Scene(level, renderer);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            scene.Draw();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    scene.MouseKeyEvent(MouseKey.Left, new Vector2(e.X, e.Y));
                    break;
                case MouseButtons.Right:
                    scene.MouseKeyEvent(MouseKey.Right, new Vector2(e.X, e.Y));
                    break;
                default:
                    scene.MouseKeyEvent(MouseKey.Invalid, new Vector2(e.X, e.Y));
                    break;
            }
            if(scene.gameWon)
            {
                WinScreen win = new WinScreen(mainMenu);
                win.Show();
                Close();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.R:
                    scene.KeyEvent(PuzzleGameLibrary.Key.R, new Vector2(MousePosition.X, MousePosition.Y));
                    break;
                case Keys.F:
                    scene.KeyEvent(PuzzleGameLibrary.Key.F, new Vector2(MousePosition.X, MousePosition.Y));
                    break;
                case Keys.A:
                    scene.KeyEvent(PuzzleGameLibrary.Key.A, new Vector2(MousePosition.X, MousePosition.Y));
                    break;
                case Keys.D:
                    scene.KeyEvent(PuzzleGameLibrary.Key.D, new Vector2(MousePosition.X, MousePosition.Y));
                    break;
                case Keys.F5:
                    scene.KeyEvent(PuzzleGameLibrary.Key.F5, new Vector2(MousePosition.X, MousePosition.Y));
                    break;
                default:
                    scene.KeyEvent(PuzzleGameLibrary.Key.Invalid, new Vector2(MousePosition.X, MousePosition.Y));
                    break;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            scene.MouseMoveEvent(new Vector2(e.X, e.Y));
        }
    }
}
