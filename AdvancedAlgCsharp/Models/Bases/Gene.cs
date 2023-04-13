
namespace AdvancedAlgCsharp.Models.Bases
{
    public abstract class Gene
    {
        public int Id { get; set; }
        public float Score { get; set; }

        public abstract Gene DeepCopy();
        public abstract void Fitness();
        public abstract void Mutate(float mutationChance);
    }
}