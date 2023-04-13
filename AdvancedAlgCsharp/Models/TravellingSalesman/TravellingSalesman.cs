using AdvancedAlgCsharp.Models.Bases;

namespace AdvancedAlgCsharp.Models.TravellingSalesman
{
	public sealed class TravellingSalesman : GeneticAlgorithm
	{
		public List<Town> AllTheTowns = new List<Town>();
		public string? ScoresText { get; private set; }

		public override void CreateSolutions()
		{
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
			//base.PrintCollection();

			Generation++;
			BestGene = Collection[0].DeepCopy();

			ScoresText = "Generation: " + Generation.ToString() + ".\n";

			for (int i = 0; i < 25; i++)
			{
				ScoresText += i + 1 + "." + Collection[i].ToString() + "\n";
			}
		}
		public override void Crossover()
		{
			Parallel.For(SampleSize / 100, BatchSize, t =>
			{
				SalesmanGene tmp = new SalesmanGene();

				if (Utilities.RND.Value.NextSingle() < 0.8)
				{
					for (int j = 0; j < AllTheTowns.Count; j++)
					{
						bool pass = false;
						int killSwitch = 0;
						while (pass != true && killSwitch < 50)
						{
							int rnd = Utilities.RND.Value.Next(0, SampleSize - 1);
							Town town = (SampleCollection[rnd] as SalesmanGene).Towns[j];

							if (!tmp.Towns.Contains(town))
							{
								tmp.Towns.Add(town);
								pass = true;
							}
							else
							{
								killSwitch++;
							}
						}
					}
				}

				if (tmp.Towns.Count != AllTheTowns.Count)
				{
					tmp.Towns = AllTheTowns.ConvertAll(i => i);
					Utilities.Shuffle(tmp.Towns);
				}

				lock (Collection)
				{
					Collection.Add(tmp);
				}

			});
		}
	}
}
