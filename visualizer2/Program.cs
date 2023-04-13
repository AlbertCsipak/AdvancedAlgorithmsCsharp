using AdvancedAlgCsharp.Models.Bases;
using AdvancedAlgCsharp.Models.TravellingSalesman;

namespace visualizer2
{
	public static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		public static TravellingSalesman geneticAlgorithm = new TravellingSalesman();
		[STAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();

			geneticAlgorithm.BatchSize = 10000;

			for (int i = 0; i < 15; i++)
			{
				Town town = new Town();
				town.ID = i;
				town.X = Utilities.RND.Value.Next(100, 800);
				town.Y = Utilities.RND.Value.Next(100, 600);
				geneticAlgorithm.AllTheTowns.Add(town);
			}
			;
			geneticAlgorithm.CreateSolutions();

			Task task = new Task(t =>
			{
				while (true)
				{
					geneticAlgorithm.EvaluateFitnesses();

					//stopwatch.Restart();
					geneticAlgorithm.SortCollection();
					//Console.WriteLine(stopwatch.ElapsedMilliseconds);

					geneticAlgorithm.PrintCollection();
					;
					//stopwatch.Restart();
					geneticAlgorithm.TakeBestSolutions();
					//Console.WriteLine(stopwatch.ElapsedMilliseconds);

					//stopwatch.Restart();
					geneticAlgorithm.MutateSolutions(0.4f); //0.6 a legjobb 
																	//Console.WriteLine(stopwatch.ElapsedMilliseconds);

					//stopwatch.Restart();
					geneticAlgorithm.Crossover();

					Thread.Sleep(1);
				}
				;
			}, TaskCreationOptions.LongRunning);

			task.Start();

			Application.Run(new Form1());
		}
	}
}