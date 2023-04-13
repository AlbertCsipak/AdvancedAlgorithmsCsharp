using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedAlgCsharp.Models.Bases;

namespace AdvancedAlgCsharp.Models.TravellingSalesman
{
    public class SalesmanGene : Gene
    {
        public List<Town> Towns = new List<Town>();

        public override Gene DeepCopy()
        {
            SalesmanGene gene = new SalesmanGene();
            gene.Score = Score;
            gene.Towns = new List<Town>(Towns);
            return gene;
        }
        public override void Fitness()
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

        public override void Mutate(float mutationChance)
        {
            for (int i = 0; i < Towns.Count; i++)
            {
                float chance = Utilities.RND.Value.NextSingle();
                if (chance < mutationChance)
                {
                    int random2 = Utilities.RND.Value.Next(0, Towns.Count - 1);

                    Town tmp1 = Towns[i];
                    Town tmp2 = Towns[random2];

                    Towns[i] = tmp2;
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
