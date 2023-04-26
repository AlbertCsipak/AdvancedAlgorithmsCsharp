using AdvancedAlgCsharp.Algorithms;
using AdvancedAlgCsharp.Problems.FunctionApproximation;
using AdvancedAlgCsharp.Problems.SmallestBoundaryPolygon;
using AdvancedAlgCsharp.Problems.TravellingSalesman;
using AdvancedAlgCsharp.Problems.Work_Assignment;
using HaladoAlg.Solvers;

namespace visualizer2
{
    public static class Program
	{
		public static TravellingSalesman geneticAlgorithm = new TravellingSalesman();
		public static SmallestBoundaryPolygon smallestBoundaryPolygon = new SmallestBoundaryPolygon();
		public static FunctionApproximation functionApproximation = new FunctionApproximation();
		public static WorkAssignment workAssignment = new WorkAssignment();
		[STAThread]
		static void Main()
		{
			ApplicationConfiguration.Initialize();

			//problémák, csak ezt kell ki-be kommentelni
			TravellingSalesmanProblem();
			//SmallestBoundaryPolygonProblem();
			//WorkAssignmentProblem();

			Application.Run(new Form1());
		}
		static void TravellingSalesmanProblem()
		{
			geneticAlgorithm.IsActive = true;

			//városok száma 
			for (int i = 0; i < 64; i++)
			{
				Town town = new Town();
				town.ID = i;
				town.X = Utilities.threadSafeRND.Value.Next(100, 800);
				town.Y = Utilities.threadSafeRND.Value.Next(100, 600);
				geneticAlgorithm.AllTheTowns.Add(town);
			}

			//64 város esetén
			//5k ideális gyors keresésre de hajlamos beragadni olyan 80-90% tökéletességnél
			//10k lassab de majdnem tökéletes
			//50k bizonyos idő után garantál 100%-os út
			//hány egyed legyen egy populationben
			geneticAlgorithm.BatchSize = 10000;

			geneticAlgorithm.CreateSolutions();
			Task task = new Task(t =>
			{
				while (true)
				{
					geneticAlgorithm.EvaluateFitnesses();
					geneticAlgorithm.SortCollection();
					geneticAlgorithm.PrintCollection();
					geneticAlgorithm.TakeBestSolutions();
					geneticAlgorithm.Crossover();

					//mutation rate
					geneticAlgorithm.MutateSolutions(0.7f);

					Thread.Sleep(1);
				}
			}, TaskCreationOptions.LongRunning);

			task.Start();

		}
		static void SmallestBoundaryPolygonProblem()
		{
			smallestBoundaryPolygon.IsActive = true;

			//körülrajzolandó pontok száma
			for (int i = 0; i < 5; i++)
			{
				smallestBoundaryPolygon.Polygon.Add(new MyPoint(Utilities.threadSafeRND.Value.Next(200, 700), Utilities.threadSafeRND.Value.Next(200, 500)));
			}

			//polygon alakja
			//smallestBoundaryPolygon.Octagon();
			smallestBoundaryPolygon.Square();

			//mászás mértéke
			//5-20 a legjobb
			int change = 5;

			smallestBoundaryPolygon.LastFitness = smallestBoundaryPolygon.LengthOfBoundary(smallestBoundaryPolygon.Points);

			int idx = 0;
			Task task = new Task(t =>
			{
				while (true)
				{
					smallestBoundaryPolygon.Generation++;
					//if (smallestBoundaryPolygon.Generation%100==0)
					//{
					//	change = (change / 10)+10;
					//	;
					//}

					if (idx == smallestBoundaryPolygon.Points.Count)
					{
						idx = 0;
					}

					//random vagy direct neighbour választás
					List<MyPoint> newPoints = smallestBoundaryPolygon.RandomNeighbour(change);
					//List<MyPoint> newPoints = smallestBoundaryPolygon.DirectNeighbour(change, idx);

					idx++;
					smallestBoundaryPolygon.CurrentFitness = smallestBoundaryPolygon.LengthOfBoundary(newPoints);

					if (smallestBoundaryPolygon.CurrentFitness < smallestBoundaryPolygon.LastFitness)
					{
						smallestBoundaryPolygon.LastFitness = smallestBoundaryPolygon.CurrentFitness;
						smallestBoundaryPolygon.Points = newPoints.ConvertAll(t => t);
					}

					Thread.Sleep(1);
				}
			}, TaskCreationOptions.LongRunning);

			task.Start();
		}
		static void WorkAssignmentProblem()
		{
			workAssignment.IsActive = true;

			workAssignment.PopulationSize = 1000;

			workAssignment.CreatePopulation();

			var tmp = workAssignment.NSGAIIMethod(workAssignment.People,2);

			Task task = new Task(t =>
			{
				while (true)
				{
					tmp = workAssignment.NSGAIIMethod(tmp,2);
					workAssignment.Fronts = tmp.ToList();

					Thread.Sleep(1);
				}
			}, TaskCreationOptions.LongRunning);

			task.Start();

		}
		static void FunctionApproximationProblem() { 
			functionApproximation.IsActive = true;

			functionApproximation.PopulationSize = 100;

			functionApproximation.CreateCritters(100,800,100,500);

			Task task = new Task(t =>
			{
				while (true)
				{
					functionApproximation.Generation++;
					functionApproximation.MoveCritters(100,800,100,500);
					;
					functionApproximation.EvaluateFitnesses();


					Thread.Sleep(1);
				}
			}, TaskCreationOptions.LongRunning);

			task.Start();

		}
	}
}