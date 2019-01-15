using System;
using System.Drawing;
using System.Windows.Forms;
using PuzzleGameLibrary;

namespace PuzzleGame
{
    public partial class Game : Form
    {
        FormsRenderer renderer;
        Scene scene;
        PuzzleGameLibrary.Rectangle background = new PuzzleGameLibrary.Rectangle(new Vector2(0, 0), new Vector2(2000, 2000));

        public Game(string levelPath)
        {
            InitializeComponent();
            renderer = new FormsRenderer(CreateGraphics());
            scene = new Scene(levelPath, new Vector2(Width, Height), renderer);
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
                WinScreen win = new WinScreen();
                win.Show();
                Hide();
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

        private void Game_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
                Application.Exit();
        }
    }
}
