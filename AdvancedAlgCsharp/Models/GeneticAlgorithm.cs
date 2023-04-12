using System;
using System.ComponentModel;

namespace AdvancedAlgCsharp.Models
{
	public class GeneticAlgorithm
	{
		public int Generation { get; set; }
		public int BatchSize { get; set; }
		public int SampleSize { get { return BatchSize / 100; } }

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
			Parallel.ForEach(Collection, t => {
				t.Fitness();
			});
		}
		public void SortCollection()
		{
			Collection = Collection.OrderBy(t=>t.Score).ToList();
		}
		public void PrintCollection()
		{
			Generation++;
			Console.WriteLine(Generation);
			for (int i = 0; i < 20; i++)
			{
				Console.WriteLine(Collection[i].ToString());
			}
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
			for (int i = 0; i < SampleSize/10; i++)
			{
				Collection.Add(SampleCollection[i].DeepCopy());
			}
			;
		}

		public void MutateSolutions(float percentage)
		{
			Parallel.ForEach(SampleCollection, t => {
				t.Mutate(percentage);
			});
		}

		public void Crossover()
		{
			Parallel.For(SampleSize/10, BatchSize, t => {
				Gene tmp = new Gene();
				tmp.Score = 0;

				float rnd1 = Utilities.RND.Value.NextSingle();

				if (rnd1<0.8f)
				{
					for (int j = 0; j < AllTheTowns.Count; j++)
					{
						bool pass = false;
						int killSwitch = 0;
						while (pass != true && killSwitch < 10)
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
