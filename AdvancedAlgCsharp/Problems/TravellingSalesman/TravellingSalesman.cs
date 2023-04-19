using AdvancedAlgCsharp.Algorithms;
using AdvancedAlgCsharp.Algrithms.Bases;

namespace AdvancedAlgCsharp.Problems.TravellingSalesman
{
	public sealed class TravellingSalesman : GeneticAlgorithm
	{
		public bool IsActive { get; set; }
		public List<Town> AllTheTowns = new List<Town>();
		public string? ScoresText { get; private set; }
		public override void CreateSolutions()
		{
			Collection.Clear();
			for (int i = 0; i < BatchSize; i++)
			{
				SalesmanGene gene = new SalesmanGene();
				gene.Towns = AllTheTowns.ConvertAll(i => i);
				Utilities.Shuffle(gene.Towns);
				Collection.Add(gene);
			}

		}
		public override void PrintCollection()
		{
			Generation++;
			BestGene = Collection[0].DeepCopy();

			ScoresText = "Generation: " + Generation.ToString() + ".\n";

			for (int i = 0; i < 25; i++)
			{
				ScoresText += i + 1 + "." + Collection[i].ToString() + "\n";
			}
		}
		SalesmanGene PickOneRandom()
		{
			SalesmanGene tmp = new SalesmanGene();

			float chance = Utilities.threadSafeRND.Value.NextSingle();

			if (chance < 0.2)
			{
				tmp = SampleCollection[Utilities.threadSafeRND.Value.Next(SampleSize - SampleSize / 10, SampleSize)].DeepCopy() as SalesmanGene;
			}
			else if (chance < 0.5)
			{
				tmp = SampleCollection[Utilities.threadSafeRND.Value.Next(SampleSize / 10, SampleSize - SampleSize / 10)].DeepCopy() as SalesmanGene;
			}
			else
			{
				tmp = SampleCollection[Utilities.threadSafeRND.Value.Next(0, SampleSize / 10)].DeepCopy() as SalesmanGene;
			}
			return tmp;
		}
		public override void Crossover()
		{
			Parallel.For(SampleSize / 100, BatchSize, t =>
			{
				SalesmanGene parentA = PickOneRandom();
				SalesmanGene parentB = PickOneRandom();

				int start = Utilities.threadSafeRND.Value.Next(0, parentA.Towns.Count);
				int end = Utilities.threadSafeRND.Value.Next(start + 1, parentA.Towns.Count);

				List<Town> newTowns = new List<Town>();

				for (int i = start; i < end; i++)
				{
					newTowns.Add(parentA.Towns[i]);
				}

				for (int i = 0; i < parentB.Towns.Count; i++)
				{
					if (!newTowns.Contains(parentB.Towns[i]))
					{
						newTowns.Add(parentB.Towns[i]);
					}
				}

				SalesmanGene newGene = new SalesmanGene();

				newGene.Towns = newTowns.ConvertAll(i => i);

				lock (Collection)
				{
					Collection.Add(newGene);
				}

			});
		}
	}
}
