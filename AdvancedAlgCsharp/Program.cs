using AdvancedAlgCsharp.Models;
using System.Diagnostics;

namespace AdvancedAlgCsharp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm();
			geneticAlgorithm.BatchSize = 10000;

			for (int i = 0; i < 10; i++)
			{
				Town town = new Town();
				town.ID = i;
				town.X = Utilities.RND.Value.Next(0, 200);
				town.Y = Utilities.RND.Value.Next(0, 200);
				geneticAlgorithm.AllTheTowns.Add(town);
			}
			;
			geneticAlgorithm.CreateSolutions();

			Stopwatch stopwatch = new Stopwatch();
			while (true)
			{
				Console.Clear();

				//stopwatch.Restart();
				geneticAlgorithm.EvaluateFitnesses();
				//Console.WriteLine(stopwatch.ElapsedMilliseconds);

				//stopwatch.Restart();
				geneticAlgorithm.SortCollection();
				//Console.WriteLine(stopwatch.ElapsedMilliseconds);

				geneticAlgorithm.PrintCollection();
				;
				//stopwatch.Restart();
				geneticAlgorithm.TakeBestSolutions();
				//Console.WriteLine(stopwatch.ElapsedMilliseconds);

				//stopwatch.Restart();
				geneticAlgorithm.MutateSolutions(0.6f);
				//Console.WriteLine(stopwatch.ElapsedMilliseconds);

				//stopwatch.Restart();
				geneticAlgorithm.Crossover();
				//Console.WriteLine(stopwatch.ElapsedMilliseconds);

				System.Threading.Thread.Sleep(1);
			}

		}
	}
}