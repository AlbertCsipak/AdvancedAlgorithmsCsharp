using AdvancedAlgCsharp.Models.Bases;

namespace visualizer2
{
    public static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		public static GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm();
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
					Program.geneticAlgorithm.EvaluateFitnesses();

					//stopwatch.Restart();
					Program.geneticAlgorithm.SortCollection();
					//Console.WriteLine(stopwatch.ElapsedMilliseconds);

					Program.geneticAlgorithm.PrintCollection();
					;
					//stopwatch.Restart();
					Program.geneticAlgorithm.TakeBestSolutions();
					//Console.WriteLine(stopwatch.ElapsedMilliseconds);

					//stopwatch.Restart();
					Program.geneticAlgorithm.MutateSolutions(0.4f); //0.6 a legjobb 
																	//Console.WriteLine(stopwatch.ElapsedMilliseconds);

					//stopwatch.Restart();
					Program.geneticAlgorithm.Crossover();

					Thread.Sleep(1);
				}
				;
			}, TaskCreationOptions.LongRunning);

			task.Start();

			Application.Run(new Form1());
		}
	}
}