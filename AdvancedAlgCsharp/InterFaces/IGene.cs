namespace AdvancedAlgCsharp.InterFaces
{
	public interface IGene
	{
		int Score { get; set; }

		void Fitness();
		void Mutate();
	}
}