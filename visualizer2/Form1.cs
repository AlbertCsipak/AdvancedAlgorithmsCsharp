using AdvancedAlgCsharp.Problems.TravellingSalesman;

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
			else
			{

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

	}
}