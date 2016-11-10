﻿using System.Drawing;
using System.Windows.Forms;

namespace GraphicsDemo
{
    public partial class Demo10 : Form
    {
        public Demo10()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush brush = new SolidBrush(Color.Red);
            g.FillEllipse(brush, 50, 50, 100, 100);
        }
    }
}