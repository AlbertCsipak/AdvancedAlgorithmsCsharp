namespace AdvancedAlgCsharp.InterFaces
{
	public interface IGeneticAlgorithm
	{
		int BatchSize { get; set; }
		int Generation { get; set; }
		int SampleSize { get; set; }

		void CreateSolutions();
		void Crossover();
		void EvaluateFitnesses();
		void MutateSolutions(double percentage);
		void PrintCollection();
		void SortCollection();
		void TakeBestSolutions();
	}
}