using AdvancedAlgCsharp.Models.TravellingSalesman;

namespace visualizer2
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			;
			System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
			timer.Interval = 1;
			timer.Tick += new EventHandler(timer_Tick);
			timer.Start();


		}
		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			if (Program.geneticAlgorithm.BestGene != null)
			{

				// Create a new pen with a black color and a width of 1 pixel
				Pen pen = new Pen(Color.Red, 5);
				Pen pen2 = new Pen(Color.Black, 1);

				SolidBrush myBrush = new SolidBrush(Color.Red);
				Graphics formGraphics;
				formGraphics = this.CreateGraphics();

				for (int i = 0; i < (Program.geneticAlgorithm.BestGene as SalesmanGene)?.Towns.Count; i++)
				{

					int x = (Program.geneticAlgorithm.BestGene as SalesmanGene).Towns[i].X;
					int y = (Program.geneticAlgorithm.BestGene as SalesmanGene).Towns[i].Y;
					formGraphics.FillEllipse(myBrush, new Rectangle(x - 5, y - 5, 10, 10));

					if (i < (Program.geneticAlgorithm.BestGene as SalesmanGene)?.Towns.Count - 1)
					{

						e.Graphics.DrawLine(pen2, x, y, (Program.geneticAlgorithm.BestGene as SalesmanGene).Towns[i + 1].X, (Program.geneticAlgorithm.BestGene as SalesmanGene).Towns[i + 1].Y);
					}
				}
				pen.Dispose();
				pen2.Dispose();
				myBrush.Dispose();
				formGraphics.Dispose();
				// Dispose of the pen object to free up resources
			}

		}

		private void timer_Tick(object sender, EventArgs e)
		{
			this.Refresh();
			label1.Text = Program.geneticAlgorithm.ScoresText;
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}
}