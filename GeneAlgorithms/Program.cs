using System;

namespace GeneAlgorithms
{
    class SimpleDemoGA
    {
        Population population = new Population();
        Individual fittest;
        Individual secondFittest;
        int generationCount = 0;

        public static void Main(string[] args)
        {

            Random rn = new Random();

            SimpleDemoGA demo = new SimpleDemoGA();

            //Populyasiya yarat
            demo.population.initializePopulation(10);

            //Populyasiyanın fitness səviyyəsini hesabla
            demo.population.calculateFitness();

            Console.WriteLine("Generation: " + demo.generationCount + " Fittest: " + demo.population.fittest);

            //Populyasiya (gen birləşməsi) 5 fitlik dərəcəsinə çatana qədər davam et
            while (demo.population.fittest < 5)
            {
                ++demo.generationCount;

                // Genləri Seç
                demo.Selection();

                // Krossover et
                demo.Crossover();

                //Do mutation under a random probability
                if (rn.Next() % 7 < 5)
                {
                    demo.Mutation();
                }

                //Add fittest offspring to population
                demo.AddFittestOffspring();

                //Calculate new fitness value
                demo.population.calculateFitness();

                Console.WriteLine("Generation: " + demo.generationCount + " Fittest: " + demo.population.fittest);
            }

            Console.WriteLine("\nSolution found in generation " + demo.generationCount);
            Console.WriteLine("Fitness: " + demo.population.getFittest().fitness);
            Console.Write("Genes: ");
            for (int i = 0; i < 5; i++)
            {
                Console.Write(demo.population.getFittest().genes[i]);
            }

            Console.WriteLine("");

        }

        //Selection
        void Selection()
        {

            // Ən çox fitlik səviyyəsi olan individualı seç
            fittest = population.getFittest();

            // İkinci Ən çox fitlik səviyyəsi olan individualı seç
            secondFittest = population.getSecondFittest();
        }

        //Krossover metodu
        void Crossover()
        {
            Random rn = new Random();

            // İxtiyari krossover nöqtəsi
            int crossOverPoint = rn.Next(population.individuals[0].geneLength);

            // Valideynlər arasında value (dəyərləri) dəyişdir
            for (int i = 0; i < crossOverPoint; i++)
            {
                int temp = fittest.genes[i];
                fittest.genes[i] = secondFittest.genes[i];
                secondFittest.genes[i] = temp;

            }

        }

        // Mutasiya
        void Mutation()
        {
            Random rn = new Random();

            // İxtiyari mutasiya nöqtəsi seç
            int mutationPoint = rn.Next(population.individuals[0].geneLength);

            // Mutasiya nöqtəsində olan dəyərləri dəyiş (0->1 və 1->0) 
            if (fittest.genes[mutationPoint] == 0)
            {
                fittest.genes[mutationPoint] = 1;
            }
            else
            {
                fittest.genes[mutationPoint] = 0;
            }

            mutationPoint = rn.Next(population.individuals[0].geneLength);

            if (secondFittest.genes[mutationPoint] == 0)
            {
                secondFittest.genes[mutationPoint] = 1;
            }
            else
            {
                secondFittest.genes[mutationPoint] = 0;
            }
        }

        // Ən fit nəsli seç
        Individual GetFittestOffspring()
        {
            if (fittest.fitness > secondFittest.fitness)
            {
                return fittest;
            }
            return secondFittest;
        }


        // Ən fit individualı (və ya nəsli) Ən fitlərdən seçib əlavə et
        void AddFittestOffspring()
        {

            fittest.calcFitness();
            secondFittest.calcFitness();

            int leastFittestIndex = population.getLeastFittestIndex();

            population.individuals[leastFittestIndex] = GetFittestOffspring();
        }

    }
}