using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PuzzleGame
{
    public partial class Menu : Form
    {
        public string currentPath;
        DirectoryInfo d;
        FileInfo[] Files;

        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Hide();
            button3.Hide();
            label1.Show();
            levelsBox.Show();
            button2.Show();

            currentPath = Path.GetDirectoryName(Application.ExecutablePath);
            d = new DirectoryInfo(currentPath);
            Files = d.GetFiles("*.bin");
            foreach (FileInfo file in Files)
            {
                levelsBox.Items.Add(file);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(levelsBox.SelectedItem != null)
            {
                Game g = new Game(levelsBox.SelectedItem.ToString());
                g.Show();
                Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Instructions instr = new Instructions();
            instr.ShowDialog(this);
        }
    }
}
