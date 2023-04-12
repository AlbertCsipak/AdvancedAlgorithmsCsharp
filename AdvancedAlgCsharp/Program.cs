using AdvancedAlgCsharp.Models;
using System;
using System.Diagnostics;
using System.Drawing;

namespace AdvancedAlgCsharp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm();
			geneticAlgorithm.BatchSize = 10000;

			for (int i = 0; i < 20; i++)
			{
				Town town = new Town();
				town.ID = i;
				town.X = Utilities.RND.Value.Next(0, 10000);
				town.Y = Utilities.RND.Value.Next(0, 10000);
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
				geneticAlgorithm.MutateSolutions(0.3f);
				//Console.WriteLine(stopwatch.ElapsedMilliseconds);

				//stopwatch.Restart();
				geneticAlgorithm.Crossover();
				//Console.WriteLine(stopwatch.ElapsedMilliseconds);

				System.Threading.Thread.Sleep(1);
			}

		}
	}
}