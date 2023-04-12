using System;
using System.Drawing;
using System.Windows.Forms;

namespace visualizer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			// Create points
			Point[] points = new Point[] {
				new Point(50, 50),
				new Point(100, 200),
				new Point(200, 150),
				new Point(300, 300)
			};

			// Create a pen for the lines
			Pen pen = new Pen(Color.Black, 2);

			// Get the graphics object to draw on
			Graphics g = e.Graphics;

			// Draw lines between the points
			for (int i = 0; i < points.Length - 1; i++)
			{
				g.DrawLine(pen, points[i], points[i + 1]);
			}
		}
	}
}
