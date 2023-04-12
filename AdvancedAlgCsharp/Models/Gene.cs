using AdvancedAlgCsharp.InterFaces;

namespace AdvancedAlgCsharp.Models
{
	public class Gene
	{
		public float Score { get; set; }
		public List<Town> Towns { get; set; }
		public Gene()
		{
			Towns = new List<Town>();
			Score = 0;
		}
		public Gene DeepCopy() { 
			Gene gene = new Gene();
			gene.Score = this.Score;
			gene.Towns = new List<Town>(this.Towns);
			return gene;
				
		}
		public void Fitness()
		{
			float sum_length = 0;
			for (int i = 0; i < Towns.Count - 1; i++)
			{
				Town t1 = Towns[i];
				Town t2 = Towns[i + 1];
				sum_length += (float)Math.Sqrt(Math.Pow(t1.X - t2.X, 2) + Math.Pow(t1.Y - t2.Y, 2));
			}
			Score = sum_length;
		}

		public void Mutate(float mutationChance)
		{
			for (int i = 0; i < this.Towns.Count; i++)
			{
				float chance = Utilities.RND.Value.NextSingle();
				if (chance < mutationChance)
				{
					int random1 = Utilities.RND.Value.Next(0, Towns.Count - 1);
					int random2 = Utilities.RND.Value.Next(0, Towns.Count - 1);

					Town tmp1 = Towns[random1];
					Town tmp2 = Towns[random2];

					Towns[random1] = tmp2;
					Towns[random2] = tmp1;
				}
			}

		}
		string TownsToString()
		{
			string s = "";
			foreach (var item in Towns)
			{
				s += item.ID + ",";
			}
			return s;

		}
		public override string ToString()
		{
			return $"Score: {Score:F2} \t\tTown orders: {TownsToString()}";
		}
	}
}
