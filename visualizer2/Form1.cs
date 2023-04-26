using AdvancedAlgCsharp.Problems.TravellingSalesman;
using System.Drawing;

namespace visualizer2
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			this.DoubleBuffered = true;
			System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
			timer.Interval = 1;
			timer.Tick += new EventHandler(timer_Tick);
			timer.Start();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			this.Refresh();
		}
		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			if (Program.geneticAlgorithm.IsActive)
			{
				DrawTravellingSalesman(sender, e);
			}
			else if (Program.smallestBoundaryPolygon.IsActive)
			{
				DrawSmallestBoundaryPolygon(sender, e);
			}
			else if (Program.functionApproximation.IsActive)
			{
				DrawFunctionApproximation(sender, e);
			}
			else if (Program.workAssignment.IsActive)
			{
				DrawWorkAssignmentProblem(sender, e);
			}
			else
			{
				label1.Text = "error :)";
			}
		}
		void DrawFunctionApproximation(object sender, PaintEventArgs e)
		{

			label1.Text = "GlobalOptimum: ";
			label1.Text += "\nGeneration: " + Program.functionApproximation.Generation.ToString();

			Pen pen = new Pen(Color.BlueViolet, 6);
			Pen pen2 = new Pen(Color.Black, 2);
			Pen pen3 = new Pen(Color.Red, 6);

			//poly draw
			for (int i = 0; i < Program.functionApproximation.Critters.Count; i++)
			{
				int x = Program.functionApproximation.Critters[i].X;
				int y = Program.functionApproximation.Critters[i].Y;
				e.Graphics.DrawEllipse(pen, new Rectangle(x - 3, y - 3, 6, 6));
			}
		}
		void DrawSmallestBoundaryPolygon(object sender, PaintEventArgs e)
		{

			label1.Text = "Boundary: "+Program.smallestBoundaryPolygon.LastFitness.ToString();
			label1.Text += "\nGeneration: " + Program.smallestBoundaryPolygon.Generation.ToString();

			Pen pen = new Pen(Color.BlueViolet, 6);
			Pen pen2 = new Pen(Color.Black, 2);
			Pen pen3 = new Pen(Color.Red, 6);

			//poly draw
			for (int i = 0; i < Program.smallestBoundaryPolygon.Polygon.Count; i++)
			{
				int x = Program.smallestBoundaryPolygon.Polygon[i].X;
				int y = Program.smallestBoundaryPolygon.Polygon[i].Y;
				e.Graphics.DrawEllipse(pen, new Rectangle(x - 3, y - 3, 6, 6));
			}

			//boundary draw
			for (int i = 0; i < Program.smallestBoundaryPolygon.Points.Count; i++)
			{
				int x = Program.smallestBoundaryPolygon.Points[i].X;
				int y = Program.smallestBoundaryPolygon.Points[i].Y;
				e.Graphics.DrawEllipse(pen3, new Rectangle(x - 3, y - 3, 6, 6));

				if (i < Program.smallestBoundaryPolygon.Points.Count - 1)
				{
					e.Graphics.DrawLine(pen2, x, y, Program.smallestBoundaryPolygon.Points[i + 1].X, Program.smallestBoundaryPolygon.Points[i + 1].Y);
				}
				else
				{
					//utolsót az elsővel
					e.Graphics.DrawLine(pen2, x, y, Program.smallestBoundaryPolygon.Points[0].X, Program.smallestBoundaryPolygon.Points[0].Y);
				}
			}
		}
		void DrawTravellingSalesman(object sender, PaintEventArgs e)
		{
			label1.Text = Program.geneticAlgorithm.ScoresText;

			if (Program.geneticAlgorithm.BestGene != null)
			{
				// Create a new pen with a black color and a width of 1 pixel
				Pen pen = new Pen(Color.BlueViolet, 6);
				Pen pen2 = new Pen(Color.Black, 2);

				for (int i = 0; i < (Program.geneticAlgorithm.BestGene as SalesmanGene)?.Towns.Count; i++)
				{

					int x = (Program.geneticAlgorithm.BestGene as SalesmanGene).Towns[i].X;
					int y = (Program.geneticAlgorithm.BestGene as SalesmanGene).Towns[i].Y;
					e.Graphics.DrawEllipse(pen, new Rectangle(x - 3, y - 3, 6, 6));
					if (i < (Program.geneticAlgorithm.BestGene as SalesmanGene)?.Towns.Count - 1)
					{

						e.Graphics.DrawLine(pen2, x, y, (Program.geneticAlgorithm.BestGene as SalesmanGene).Towns[i + 1].X, (Program.geneticAlgorithm.BestGene as SalesmanGene).Towns[i + 1].Y);
					}
				}
			}
		}
		void DrawWorkAssignmentProblem(object sender, PaintEventArgs e)
		{
			if (Program.workAssignment.Fronts != null)
			{
				Pen pen = new Pen(Color.BlueViolet, 5);

				foreach (var p in Program.workAssignment.Fronts)
				{
					var tmpPts = PointCalculator(p.Properties[0], p.Properties[1]);

					e.Graphics.DrawEllipse(pen, new Rectangle((int)tmpPts[0], (int)tmpPts[1],5, 5));
				}
			}
		}
		private float[] PointCalculator(float x, float y)
		{
			float[] coordinates = new float[2];
			coordinates[0] = (float)((this.Width - 400) * (1 - x)) + 100;
			coordinates[1] = (float)((this.Height - 300) * y) + 100;

			return coordinates;
		}
	}
}