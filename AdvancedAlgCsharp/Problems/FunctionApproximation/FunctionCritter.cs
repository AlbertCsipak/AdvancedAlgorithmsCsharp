using AdvancedAlgCsharp.Algorithms.ParticleSwarmAlgorithm;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgCsharp.Problems.FunctionApproximation
{
	public class FunctionCritter : Critter
	{
		public FunctionCritter()
		{
			ID = IDCounter++;
			ID = IDCounter;
		}
		public FunctionCritter(int x, int y)
		{
			ID = IDCounter++;
			ID = IDCounter;
			this.X = x;
			this.Y = y;
			this.LocalOptimum.X = this.X;
			this.LocalOptimum.Y	= this.Y;
		}
		public override Critter DeepCopy()
		{
			FunctionCritter functionCritter = new FunctionCritter();
			functionCritter.X = X;
			functionCritter.Y = Y;
			functionCritter.SpeedX = SpeedX;
			functionCritter.SpeedY = SpeedY;
			functionCritter.GlobalOptimum = GlobalOptimum;
			functionCritter.LocalOptimum = LocalOptimum;
			return functionCritter;
		}
	}
}
