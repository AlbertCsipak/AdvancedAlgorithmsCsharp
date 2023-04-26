using AdvancedAlgCsharp.Algorithms;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HaladoAlg.Solvers
{
    public class NSGAII
    {
        public List<Person> NSGAIIMethod(List<Person> population, int maxGeneration = 50)
        {
            List<Person> people = population.ToList();
            List<Person> children = new List<Person>();

            int generationCtr = 0;
            while (generationCtr < maxGeneration)
            {
                people = Selection(people, children);
                children = MakeNewPopulation(people);

                generationCtr++;
            }
            return people;
        }

        private List<Person> MakeNewPopulation(List<Person> parents)
        {
            var tmpList = parents.OrderBy(t => Guid.NewGuid()).ToList();
                
            List<Person> children = new List<Person>();

            while (children.Count<Person.PopulationSize)
            {
                children.Add(new Person(Crossover(tmpList[Utilities.threadSafeRND.Value.Next(tmpList.Count)], tmpList[Utilities.threadSafeRND.Value.Next(tmpList.Count)] ).Properties));
            }
            return children;
        }

        //mutáció= a szülő meg b szülő átlagos értékeit kapja
        private Person Crossover(Person a, Person b)
        {
            Person childOne = new Person();
            for (int i = 0; i < Person.NumberOfProperties; i++)
            {
                childOne.Properties[i] = (a.Properties[i] + b.Properties[i]) / 2;
            }
            return childOne;
        }


        private bool Dominate(Person a, Person b)
        {
            bool atLeastOneBetter = false;
            for (int i = 0; i < Person.NumberOfProperties; i++)
            {
                if (a.Properties[i] < b.Properties[i])
                    return false;
                else if (a.Properties[i] > b.Properties[i])
                    atLeastOneBetter = true;
            }
            return atLeastOneBetter;
        }
        
        public void NonDominatedSort(List<Person> population)
        {
            int[] cntDominated = new int[population.Count];
            List<List<int>> listDominate = new List<List<int>>();
            for (int i = 0; i < population.Count; ++i) listDominate.Add(new List<int>());

            for (int i = 0; i < population.Count; ++i)
            {
                for (int j = 0; j < population.Count; ++j)
                {
                    if (Dominate(population[i], population[j]))
                    {
                        cntDominated[j] += 1;
                        listDominate[i].Add(j);
                    }
                }
            }

            int frontCtr = 0;
            bool running = true;
            while (running)
            {
                running = false;
                List<int> currentFront = new List<int>();

                for (int i = 0; i < population.Count; ++i)
                {
                    if (cntDominated[i] == 0)
                    {
                        running = true;

                        cntDominated[i] = -1;
                        currentFront.Add(i);

                    }
                }

                foreach (int i in currentFront)
                {
                    population[i].Rank = frontCtr;

                    foreach (var j in listDominate[i])
                    {
                        cntDominated[j] -= 1;
                    }
                }
                frontCtr++;
            }
        }

        private void CalculateCrowdingDistance(List<Person> population)
        {
            int n = population.Count;
            for (int i = 0; i < n; i++)
            {
                population[i].CrowdingDistance = 0.0f;
            }

            for (int m = 0; m < Person.NumberOfProperties; m++)
            {
                population = population.OrderBy(t => t.Properties[m]).ToList();

                population[0].CrowdingDistance = float.PositiveInfinity;
                population[n - 1].CrowdingDistance = float.PositiveInfinity;

                double minObjective = population[0].Properties[m];
                double maxObjective = population[n - 1].Properties[m];
                ;

                for (int i = 1; i < n - 1; i++)
                {
                    double distance = population[i + 1].Properties[m] - population[i - 1].Properties[m];
                    distance /= maxObjective - minObjective;
                    population[i].CrowdingDistance += (float)distance;
                }
            }
        }


        private List<Person> Selection(List<Person> population,List<Person> children)
        {
            List<Person> newPopulation = new List<Person>();
                
            List<Person> fullPopulation = new List<Person>();

            fullPopulation.AddRange(population.ToList());
            fullPopulation.AddRange(children.ToList());

            NonDominatedSort(fullPopulation);

            int rankWanted = 0;
            while (newPopulation.Count < Person.PopulationSize)
            {
                List<Person> tmpAddList = fullPopulation.Where(t => t.Rank == rankWanted).ToList();
                if ((newPopulation.Count + tmpAddList.Count) <= Person.PopulationSize)
                {
                    newPopulation.AddRange(tmpAddList);
                }
                else
                {
                    CalculateCrowdingDistance(tmpAddList);
                    tmpAddList = tmpAddList.OrderByDescending(t => t.CrowdingDistance).ToList();
                    int numberNeeded = (Person.PopulationSize - newPopulation.Count);
                    for (int i = 0; i < numberNeeded; i++)
                    {
                        newPopulation.Add(tmpAddList[i]);
                    }
                }
                rankWanted++;
            }
            return newPopulation;
        }
    }
  

}
