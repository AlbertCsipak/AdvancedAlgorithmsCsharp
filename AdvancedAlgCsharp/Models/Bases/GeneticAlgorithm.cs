﻿using AdvancedAlgCsharp.Models.TravellingSalesman;

namespace AdvancedAlgCsharp.Models.Bases
{
    public abstract class GeneticAlgorithm
    {
        public int Generation { get; set; }
        public int BatchSize { get; set; }
        public int SampleSize { get { return BatchSize / 10; } }

        public Gene? BestGene;
        public List<Gene> Collection = new List<Gene>();
        public List<Gene> SampleCollection = new List<Gene>();

        public abstract void CreateSolutions();
        public abstract void Crossover();
        public virtual void EvaluateFitnesses()
        {
            Parallel.ForEach(Collection, t =>
            {
                t.Fitness();
            });
        }
        public virtual void SortCollection()
        {
            Collection = Collection.OrderBy(t => t.Score).ToList();
        }
        public virtual void PrintCollection()
        {
            Generation++;
            BestGene = Collection[0].DeepCopy();
            Console.WriteLine(Generation);
            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine(i + 1 + ".\t" + (Collection[i] as SalesmanGene).ToString());
            }
        }
        public virtual void TakeBestSolutions()
        {
            SampleCollection.Clear();
            for (int i = 0; i < SampleSize; i++)
            {
                SampleCollection.Add(Collection[i].DeepCopy());
            }
            Collection.Clear();
            for (int i = 0; i < SampleSize / 100; i++)
            {
                Collection.Add(SampleCollection[i].DeepCopy());
            }
        }

        public virtual void MutateSolutions(float percentage)
        {
            Parallel.ForEach(SampleCollection, t =>
            {
                t.Mutate(percentage);
            });
        }
    }
}