using System;

namespace GeneAlgorithms
{
    //Individual class
    public class Individual
    {

      public  int fitness = 0;
        public int[] genes = new int[5];
        public int geneLength = 5;

        public Individual()
        {
            Random rn = new Random();

            //Set genes randomly for each individual
            for (int i = 0; i < genes.Length; i++)
            {
                genes[i] = Math.Abs(rn.Next() % 2);
            }

            fitness = 0;
        }

        //Calculate fitness
        public void calcFitness()
        {

            fitness = 0;
            for (int i = 0; i < 5; i++)
            {
                if (genes[i] == 1)
                {
                    ++fitness;
                }
            }
        }

    }
}