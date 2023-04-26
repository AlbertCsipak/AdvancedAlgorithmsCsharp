using AdvancedAlgCsharp.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaladoAlg.Solvers
{
	public class Person
	{
		public float[] Properties { get; set; }

		public float CrowdingDistance = 0;
		public int Rank = 0;

		public static int NumberOfProperties = 2;
		public static int PopulationSize = 100;

		public Person()
		{
			GenerateValues();
		}
		public Person(float[] props)
		{
			this.Properties = props;
		}

		private void GenerateValues()
		{
			this.Properties = new float[NumberOfProperties];
			for (int i = 0; i < NumberOfProperties; i++)
			{
				this.Properties[i] = (float)Utilities.threadSafeRND.Value.NextDouble();

			}
		}
	}
}
