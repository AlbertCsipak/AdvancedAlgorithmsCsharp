using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using AdvancedAlgCsharp.Models;

namespace vis
{
	public partial class Form1 : Form
	{
		static Point[] points = new Point[10];
		public Form1()
		{
			InitializeComponent();
			//this.Paint += Form1_Paint;

			//GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm();
			//geneticAlgorithm.BatchSize = 10000;
			//geneticAlgorithm.CreateSolutions();

			//for (int i = 0; i < 10; i++)
			//{
			//	Town town = new Town();
			//	town.ID = i;
			//	town.X = i * 100;
			//	town.Y = i * 100;

			//	geneticAlgorithm.AllTheTowns.Add(town);
				
			//}
			
			//points[i].X = town.X;
			//points[i].Y = town.Y;

			//geneticAlgorithm.EvaluateFitnesses();
			//;
			//geneticAlgorithm.SortCollection();
			//;

			//geneticAlgorithm.PrintCollection();
			//;
			//geneticAlgorithm.TakeBestSolutions();
			//;
			//geneticAlgorithm.MutateSolutions(0.5);
			//;
			//geneticAlgorithm.Crossover();
			;
			System.Threading.Thread.Sleep(1);


		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			// Create points


			// Create a pen for the lines
			Pen pen = new Pen(Color.Black, 2);

			// Create a brush for the dots
			Brush brush = new SolidBrush(Color.Red);

			// Get the graphics object to draw on
			Graphics g = e.Graphics;

			// Draw dots and lines between the points
			for (int i = 0; i < points.Length - 1; i++)
			{
				g.FillEllipse(brush, points[i].X-5 , points[i].Y - 5, 10, 10); // Draw dot
				g.DrawLine(pen, points[i], points[i + 1]); // Draw line
			}

			// Draw last dot
			g.FillEllipse(brush, points[points.Length - 1].X - 2, points[points.Length - 1].Y - 2, 5, 5);
		}
	}
}
