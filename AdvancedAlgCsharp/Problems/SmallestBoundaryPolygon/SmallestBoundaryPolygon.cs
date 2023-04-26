using AdvancedAlgCsharp.Algorithms;
using AdvancedAlgCsharp.Algorithms.HillClimbing;

namespace AdvancedAlgCsharp.Problems.SmallestBoundaryPolygon
{
    public class SmallestBoundaryPolygon : HillClimbingAlgorithm
	{
		public bool IsActive { get; set; }
		public List<MyPoint> Polygon = new List<MyPoint>();
		public List<MyPoint> Points = new List<MyPoint>();
		public float LastFitness = -10;
		public float CurrentFitness = -1;
		public int Generation = 0;
		public void Octagon()
		{
			MyPoint point1 = new MyPoint(100, 100);
			MyPoint point2 = new MyPoint(450, 100);
			MyPoint point3 = new MyPoint(800, 100);
			MyPoint point4 = new MyPoint(800, 350);

			Points.Add(point1);
			Points.Add(point2);
			Points.Add(point3);
			Points.Add(point4);

			MyPoint point5 = new MyPoint(800, 600);
			MyPoint point6 = new MyPoint(450, 600);
			MyPoint point7 = new MyPoint(100, 600);
			MyPoint point8 = new MyPoint(100, 350);

			Points.Add(point5);
			Points.Add(point6);
			Points.Add(point7);
			Points.Add(point8);
		}
		public void Square()
		{
			MyPoint point1 = new MyPoint(100, 100);
			MyPoint point2 = new MyPoint(800, 100);
			MyPoint point3 = new MyPoint(800, 600);
			MyPoint point4 = new MyPoint(100, 600);

			Points.Add(point1);
			Points.Add(point2);
			Points.Add(point3);
			Points.Add(point4);
		}
		public float LengthOfBoundary(List<MyPoint> solution)
		{
			float sum_length = 0;

			for (int li = 0; li < solution.Count - 1; li++)
			{
				MyPoint p1 = solution[li];
				MyPoint p2 = solution[(li + 1) % solution.Count];
				sum_length += (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
			}
			return sum_length;
		}
		public List<MyPoint> DirectNeighbour(int changeValue, int idx)
		{
			List<MyPoint> newList = new List<MyPoint>();

			foreach (var item in Points)
			{
				newList.Add(item.DeepCopy());
			}

			if (Utilities.threadSafeRND.Value.NextSingle() < 0.5)
			{
				changeValue *= -1;
			}

			if (Utilities.threadSafeRND.Value.NextSingle() < 0.5)
			{
				newList[idx].X += changeValue;
			}
			else
			{
				newList[idx].Y += changeValue;
			}

			for (int pi = 0; pi < Polygon.Count; pi++)
			{
				for (int li = 0; li < newList.Count; li++)
				{
					var helper = DistanceFromLine(newList[li], newList[(li + 1) % newList.Count], Polygon[pi]);
					if (helper < 0)
					{
						return Points;
					}
				}

			}

			return newList;
		}
		public List<MyPoint> RandomNeighbour(int changeValue)
		{
			List<MyPoint> newList = new List<MyPoint>();

			foreach (var item in Points)
			{
				newList.Add(item.DeepCopy());
			}

			int idx = Utilities.threadSafeRND.Value.Next(0, newList.Count);

			if (Utilities.threadSafeRND.Value.NextSingle() < 0.5)
			{
				changeValue *= -1;
			}

			if (Utilities.threadSafeRND.Value.NextSingle() < 0.5)
			{
				newList[idx].X += changeValue;
			}
			else
			{
				newList[idx].Y += changeValue;
			}

			if (!AreTherePointsWithSameCoordinates(newList))
			{
				;
				return Points;
			}


			for (int pi = 0; pi < Polygon.Count; pi++)
			{
				for (int li = 0; li < newList.Count; li++)
				{
					var helper = DistanceFromLine(newList[li], newList[(li + 1) % newList.Count], Polygon[pi]);
					if (helper < 0)
					{
						return Points;
					}
				}

			}

			return newList;
		}
		bool AreTherePointsWithSameCoordinates(List<MyPoint> newList) {
			int counter = 0;
			foreach (var outer in newList)
			{
				foreach (var inner in newList)
				{
					if (outer.X == inner.X && outer.Y==inner.Y)
					{
						;
						counter++;
					}
				}
			}
			return counter==newList.Count;
		}
		//lp1 a bal pont lp2 a jobb pont p a amihez hozzáér a vonal
		float DistanceFromLine(MyPoint lp1, MyPoint lp2, MyPoint p)
		{
			// https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line
			return -(float)((lp2.Y - lp1.Y) * p.X - (lp2.X - lp1.X) * p.Y + lp2.X * lp1.Y - lp2.Y * lp1.X) / (float)Math.Sqrt(Math.Pow(lp2.Y - lp1.Y, 2) + Math.Pow(lp2.X - lp1.X, 2));
		}

	}
}
