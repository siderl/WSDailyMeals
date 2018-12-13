using System;
using System.Collections.Generic;
using WSDailyMeals.Models;
//using 

namespace WSDailyMeals.Algorithm
{
    public class Core
    {
        private readonly Random selector;
        Dictionaries dict;

        public Individuo result { get; private set; }

        /// <summary>
        /// Algoritmo Genetico Continuo
        /// </summary>
        /// <param name="Lips">Objetivo de Lipidos</param>
        /// <param name="Carbs">Objetivo de Carbohidratos</param>
        /// <param name="Prots">Objetivo de Proteinas</param>
        /// <param name="popSize">Tamaño de población</param>
        /// <param name="gCount">Conteo de generación</param>
        public Core(double Lips, double Carbs, double Prots, int popSize = 50, int gCount = 100)
        {
            double factor = .05;
            int dimensions = 10;
            
            selector = new Random((int)DateTime.Now.Ticks);
            List<Individuo> padres = new List<Individuo>(popSize);
            List<Individuo> hijos = new List<Individuo>(popSize);

            dict = new Dictionaries();
            
            for (int i = 0; i < popSize; i++)
            {
                Individuo x = new Individuo(ref selector, Lips, Carbs, Prots, ref dict);
                padres.Add(x);
                //Because the Random function is based on time.
                System.Threading.Thread.Sleep(5);
            }

            int bestSon = -1;
            UInt16 gens = 0;
            double maxFitness = 0;
            while (maxFitness < 95)//while (gens < gCount)
            {
                while (padres.Count != hijos.Count)
                {
                    Individuo firstFather = Ruleta(padres);
                    Individuo secondFather;
                    bool SameCromosome;
                    do
                    {
                        secondFather = Ruleta(padres);
                        SameCromosome = true;
                        for (int i = 0; i < dimensions; i++)
                        {
                            if (firstFather.dieta[i] != secondFather.dieta[i]) {
                                SameCromosome = false;
                                break;
                            }
                        }
                    } while (SameCromosome);
                    int CrossingFactor = selector.Next(1, dimensions);
                    hijos.Add(new Individuo(firstFather, secondFather, CrossingFactor, dimensions));
                    hijos.Add(new Individuo(secondFather, firstFather, CrossingFactor, dimensions));
                }

                MutateSons(hijos, factor, dimensions);

                padres.Clear();
                int son = 1;
                bestSon = -1;
                maxFitness = 0;
                foreach (Individuo f in hijos)
                {
                    f.CalculateFitness();
                    padres.Add(f);
                    if (maxFitness < f.Fitness) {
                        maxFitness = f.Fitness;
                        bestSon = son - 1;
                    }
                    son++;

                }
                hijos.Clear();
                gens++;
            }
            result = padres[bestSon];
        }

        private void MutateSons(List<Individuo> sons, double mutationFactor, int dimensions)
        {
            int mutatedCount = (int)(mutationFactor * sons.Count * dimensions);
            for (int i = 0; i < mutatedCount; i++)
            {
                int j = selector.Next(0, sons.Count * dimensions);
                int mutatedIndividual = j / dimensions;
                int mutatedAlelo = j % dimensions;
                sons[mutatedIndividual].Mutate(mutatedAlelo, ref dict);
            }
        }

        private Individuo Ruleta(List<Individuo> padres)
        {
            double temp = selector.NextDouble();
            double totalFitness = 0, sumatoria = 0;

            for (int i = 0; i < padres.Count; i++)
                totalFitness += padres[i].Fitness;

            for (int i = 0; i < padres.Count; i++)
            {
                sumatoria += padres[i].Fitness;
                if (temp < (sumatoria / totalFitness))
                    return padres[i];
            }
            return padres[padres.Count - 1];
        }
    }
}