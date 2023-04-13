namespace AdvancedAlgCsharp.Models.Bases
{
	public class Utilities
	{
		public static void Shuffle<T>(List<T> list)
		{
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = RND.Value.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}
		public static ThreadLocal<Random> RND = new ThreadLocal<Random>(() => new Random());

		public static int Next()
		{
			return RND.Value.Next();
		}
	}
}
