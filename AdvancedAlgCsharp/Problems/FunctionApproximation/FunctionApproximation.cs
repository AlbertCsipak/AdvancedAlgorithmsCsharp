using AdvancedAlgCsharp.Algorithms;
using AdvancedAlgCsharp.Algorithms.ParticleSwarmAlgorithm;
using AdvancedAlgCsharp.Problems.SmallestBoundaryPolygon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgCsharp.Problems.FunctionApproximation
{
	public class FunctionApproximation : ParticleSwarmAlgorithm
	{
		public bool IsActive { get; set; }
		public int Generation = 0;

		public override void CreateCritters(int minx, int maxx, int miny, int maxy)
		{
			for (int i = 0; i < PopulationSize; i++)
			{
				int x = Utilities.threadSafeRND.Value.Next(minx, maxx);
				int y = Utilities.threadSafeRND.Value.Next(miny, maxy);
				FunctionCritter functionCritter = new FunctionCritter(x, y);
				Critters.Add(functionCritter);
			}
		}
	}
}
