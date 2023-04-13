namespace AdvancedAlgCsharp.Models
{
	public class GeneticAlgorithm
	{
		public int Generation { get; set; }
		public int BatchSize { get; set; }
		public int SampleSize { get { return BatchSize / 10; } }

		public string ScoresText = string.Empty;
		public Gene BestGene { get; set; }
		public List<Gene> Collection = new List<Gene>();
		public List<Gene> SampleCollection = new List<Gene>();
		public List<Town> AllTheTowns = new List<Town>();

		public GeneticAlgorithm() { }

		public void CreateSolutions()
		{
			for (int i = 0; i < BatchSize; i++)
			{
				Gene gene = new Gene();
				gene.Towns = AllTheTowns.ConvertAll(i => i);
				;
				Utilities.Shuffle(gene.Towns);
				;
				Collection.Add(gene);
				;
			}

		}
		public void EvaluateFitnesses()
		{
			Parallel.ForEach(Collection, t =>
			{
				t.Fitness();
			});
		}
		public void SortCollection()
		{
			Collection = Collection.OrderBy(t => t.Score).ToList();
		}
		public void PrintCollection()
		{
			;
			Generation++;
			BestGene = Collection[0].DeepCopy();

			ScoresText = "Generation: " + Generation.ToString() + ".\n";

			//Console.WriteLine(Generation);
			for (int i = 0; i < 25; i++)
			{
				//Console.WriteLine(i + 1 + ".\t" + Collection[i].ToString());
				ScoresText += i + 1 + "." + Collection[i].ToString() + "\n";
			}
			;
		}
		public void TakeBestSolutions()
		{
			SampleCollection.Clear();
			for (int i = 0; i < SampleSize; i++)
			{
				SampleCollection.Add(Collection[i].DeepCopy());
			}
			;
			Collection.Clear();
			for (int i = 0; i < SampleSize / 100; i++)
			{
				Collection.Add(SampleCollection[i].DeepCopy());
			}
			;
		}

		public void MutateSolutions(float percentage)
		{
			Parallel.ForEach(SampleCollection, t =>
			{
				t.Mutate(percentage);
			});
		}

		public void Crossover()
		{
			Parallel.For(SampleSize / 100, BatchSize, t =>
			{
				Gene tmp = new Gene();
				tmp.Score = 0;

				float rnd1 = Utilities.RND.Value.NextSingle();

				if (rnd1 < 0.8)
				{
					for (int j = 0; j < AllTheTowns.Count; j++)
					{
						bool pass = false;
						int killSwitch = 0;
						while (pass != true && killSwitch < 50)
						{
							int rnd = Utilities.RND.Value.Next(0, SampleSize - 1);
							Town town = SampleCollection[rnd].Towns[j];
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
					;
					Utilities.Shuffle(tmp.Towns);
					;
				}

				lock (Collection)
				{
					Collection.Add(tmp);
				}

			});
		}
	}
}
