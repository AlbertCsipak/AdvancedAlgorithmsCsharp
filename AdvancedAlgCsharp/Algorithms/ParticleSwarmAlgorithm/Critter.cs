using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgCsharp.Algorithms.ParticleSwarmAlgorithm
{
	public abstract class Critter
	{
		public static int IDCounter = 0;
		public int ID { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public int SpeedX { get; set; }
		public int SpeedY { get; set; }
		public MyPoint LocalOptimum = new MyPoint();
		public MyPoint GlobalOptimum = ParticleSwarmAlgorithm.GlobalOptimum;
		public abstract Critter DeepCopy();
		public virtual void Move(int minx, int maxx, int miny, int maxy) {
			int killSwitch = 0;
			while (killSwitch<500)
			{
				this.SpeedX = Utilities.threadSafeRND.Value.Next(-1, 1+1);
				this.SpeedY = Utilities.threadSafeRND.Value.Next(-1, 1+1);

				if (this.X + this.SpeedX > minx && this.X + this.SpeedX < maxx && this.Y + this.SpeedY > miny && this.Y+this.SpeedY < maxy)
				{
					this.X += this.SpeedX;
					this.Y += this.SpeedY;
					break;
				}

				killSwitch++;
			}
		}

		public virtual void Fitness() {

			if (true)
			{

			}

			if (true)
			{

			}
			
		}
	}
}
