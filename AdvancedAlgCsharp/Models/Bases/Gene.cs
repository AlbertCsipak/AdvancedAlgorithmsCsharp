
namespace AdvancedAlgCsharp.Models.Bases
{
	public abstract class Gene
	{
		public int Id { get; protected set; }
		public float Score { get; protected set; }

		public abstract Gene DeepCopy();
		public abstract void Fitness();
		public abstract void Mutate(float mutationChance);
	}
}