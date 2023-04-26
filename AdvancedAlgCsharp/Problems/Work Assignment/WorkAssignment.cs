using HaladoAlg.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgCsharp.Problems.Work_Assignment
{
	public class WorkAssignment : NSGAII
	{
		public bool IsActive { get; set; }
		public int PopulationSize = 1000;
		public List<Person> People = new List<Person>();
		public List<Person> Fronts = new List<Person>();
		public void CreatePopulation() {
			People.AddRange(Enumerable.Range(0, PopulationSize).Select(t => new Person()).ToList());
		}
	}
}
