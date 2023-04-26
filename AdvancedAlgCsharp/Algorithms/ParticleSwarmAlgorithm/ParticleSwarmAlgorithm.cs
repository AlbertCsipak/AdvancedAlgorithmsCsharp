using AdvancedAlgCsharp.Problems.FunctionApproximation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgCsharp.Algorithms.ParticleSwarmAlgorithm
{
	public abstract class ParticleSwarmAlgorithm
	{
		public static MyPoint GlobalOptimum = new MyPoint();

		public List<Critter> Critters = new List<Critter>();
		public int PopulationSize = 1000;
		public abstract void CreateCritters(int minx, int maxx, int miny, int maxy);
		public virtual void MoveCritters(int minx, int maxx, int miny, int maxy) {
			Parallel.ForEach(Critters, t =>
			{
				t.Move(minx, maxx, miny, maxy);
			});
		}
		public virtual void EvaluateFitnesses()
		{
			Parallel.ForEach(Critters, t =>
			{
				t.Fitness();
			});
		}

	}
}
