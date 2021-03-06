﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class MainForm : Form
    {
        private LightsOutGame lightGame;
        private const int GridOffset = 25;
        private const int GridLength = 200;
        private const int CellLength = GridLength / 3;

        public void changeNumberOfCells(int size)
        {
            lightGame.GridSize = size;

            // Redraw grid
            lightGame.NewGame();
            this.Invalidate();
        }
        public MainForm()
        {
            InitializeComponent();
            lightGame = new LightsOutGame();
        }

        private void gameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           lightGame.NewGame();

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
            for (int r = 0; r < lightGame.GridSize; r++)
            {
                for (int c = 0; c < lightGame.GridSize; c++)
                {
                    // Get proper pen and brush for on/off
                    // grid section
                    Brush brush;
                    Pen pen;
                    if (lightGame.GetGridValue(r, c))
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


        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < GridOffset || e.X > CellLength * (lightGame.GridSize) + GridOffset || e.Y < GridOffset || e.Y > CellLength * (lightGame.GridSize) + GridOffset)
                return;
            // Find row, col of mouse press
            int r = (e.Y - GridOffset) / CellLength;
            int c = (e.X - GridOffset) / CellLength;
            // Invert selected box and all surrounding boxes
            lightGame.Move(r, c);

            // Redraw grid
            this.Invalidate();
            // Check to see if puzzle has been solved
            if (lightGame.IsGameOver())
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
            changeNumberOfCells(3);
            x3ToolStripMenuItem.Checked = true;
            x4ToolStripMenuItem.Checked = false;
            x5ToolStripMenuItem.Checked = false;
        }

        private void x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeNumberOfCells(4);
            x3ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = true;
            x5ToolStripMenuItem.Checked = false;
        }

        private void x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeNumberOfCells(5);
            x3ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = false;
            x5ToolStripMenuItem.Checked = true;
        }

        private void sizeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

}
