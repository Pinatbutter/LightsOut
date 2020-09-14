using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class MainForm : Form
    {
        private const int GridOffset = 25;
        private const int GridLength = 200;
        private const int NumCells = 3;
        private const int CellLength = GridLength / NumCells;
        private int changeCells = 0;

        private bool[,] grid;
        private Random rand;

        public void changeNumberofCells(int size)
        {
            changeCells = size;

            rand = new Random();
            grid = new bool[NumCells + changeCells, NumCells + changeCells];

            for (int r = 0; r < NumCells + changeCells; r++)
                for (int c = 0; c < NumCells + changeCells; c++)
                    grid[r, c] = true;


            // Redraw grid
            this.Invalidate();
        }
        public MainForm()
        {
            InitializeComponent();

            rand = new Random();
            grid = new bool[NumCells + changeCells, NumCells + changeCells];

            for (int r = 0; r < NumCells + changeCells; r++)
                for (int c = 0; c < NumCells + changeCells; c++)
                    grid[r, c] = true;
        }

        private void gameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int r = 0; r < NumCells + changeCells; r++)
                for (int c = 0; c < NumCells + changeCells; c++)
                    grid[r, c] = rand.Next(2) == 1;

            // Redraw grid
            this.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int r = 0; r < NumCells + changeCells; r++)
            {
                for (int c = 0; c < NumCells + changeCells; c++)
                {
                    // Get proper pen and brush for on/off
                    // grid section
                    Brush brush;
                    Pen pen;
                    if (grid[r, c])
                    {
                        pen = Pens.Black;
                        brush = Brushes.White; // On
                    }
                    else
                    {
                        pen = Pens.White;
                        brush = Brushes.Black; // Off
                    }
                    // Determine (x,y) coord of row and col to draw rectangle
                    int x = c * CellLength + GridOffset;
                    int y = r * CellLength + GridOffset;
                    // Draw outline and inner rectangle
                    g.DrawRectangle(pen, x, y, CellLength, CellLength);
                    g.FillRectangle(brush, x + 1, y + 1, CellLength - 1, CellLength - 1);
                }
            }
        }


        private bool PlayerWon()
        {
            bool gameWon = true;

            for (int r = 0; r < NumCells + changeCells; r++)
                for (int c = 0; c < NumCells + changeCells; c++)
                    if (grid[r, c] == true)
                        gameWon = false;
            return gameWon;
        }
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < GridOffset || e.X > CellLength * (NumCells + changeCells) + GridOffset || e.Y < GridOffset || e.Y > CellLength * (NumCells + changeCells) + GridOffset)
                return;
            // Find row, col of mouse press
            int r = (e.Y - GridOffset) / CellLength;
            int c = (e.X - GridOffset) / CellLength;
            // Invert selected box and all surrounding boxes
            for (int i = r - 1; i <= r + 1; i++)
                for (int j = c - 1; j <= c + 1; j++)
                    if (i >= 0 && i < NumCells + changeCells && j >= 0 && j < NumCells + changeCells)
                        grid[i, j] = !grid[i, j];
            // Redraw grid
            this.Invalidate();
            // Check to see if puzzle has been solved
            if (PlayerWon())
            {
                // Display winner dialog box
                MessageBox.Show(this, "Congratulations! You've won!", "Lights Out!",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog(this);
        }

        private void x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeNumberofCells(0);
            x3ToolStripMenuItem.Checked = true;
            x4ToolStripMenuItem.Checked = false;
            x5ToolStripMenuItem.Checked = false;
        }

        private void x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeNumberofCells(1);
            x3ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = true;
            x5ToolStripMenuItem.Checked = false;
        }

        private void x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeNumberofCells(2);
            x3ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = false;
            x5ToolStripMenuItem.Checked = true;
        }
    }
}