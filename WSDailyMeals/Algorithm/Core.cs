using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSReportApp.Models;
//using 

namespace WSDailyMeals.Algorithm
{
    public class Core
    {
        private Random selector;

        /// <summary>
        /// Algoritmo Genetico Continuo
        /// </summary>
        /// <param name="popSize">Tamaño de población</param>
        /// <param name="gCount">Conteo de generación</param>
        /*public Core(string comida, int popSize = 20, int gCount = 100)
        {
            //TODO: hardcode mutationFactor. Consultar con Tonatiuh
            // consultar tamaño optimo de poblaciones
            double factor = .5;
            int dimensions = 5;
            
            selector = new Random((int)DateTime.Now.Ticks);
            

            List<Alimento> padres = new List<Alimento>(popSize);
            List<Alimento> hijos = new List<Alimento>(popSize);

            //
            Console.WriteLine("Padres.");
            for (int i = 0; i < popSize; i++)
            {
                Alimento x = new Alimento();
                padres.Add(x);
                Console.WriteLine("Padre " + i.ToString() + ": " + x.ToString());
                //Because the Random function is based on time.
                System.Threading.Thread.Sleep(5);
            }

            UInt16 gens = 0;
            while (gens < genCount)
            {
                while (padres.Count != hijos.Count)
                {
                    Alimento firstFather = Ruleta(padres);
                    Alimento secondFather;
                    bool SameCromosome;
                    do
                    {
                        secondFather = Ruleta(padres);
                        SameCromosome = true;
                        for (int i = 0; i < dimensions; i++)
                        {
                            if (firstFather.KcalProteina != secondFather.KcalProteina)
                            {
                                SameCromosome = false;
                                break;
                            }
                            if (firstFather.KcalLipidos != secondFather.KcalLipidos)
                            {
                                SameCromosome = false;
                                break;
                            }
                            if (firstFather.KcalCarbos != secondFather.KcalCarbos)
                            {
                                SameCromosome = false;
                                break;
                            }
                        }
                    } while (SameCromosome);
                    int CrossingFactor = selector.Next(1, dimensions);
                    hijos.Add(new Alimento(firstFather, secondFather, CrossingFactor, dimensions, ref selector));
                    hijos.Add(new Alimento(firstFather, secondFather, CrossingFactor, dimensions, ref selector));
                }

                MutateSons(hijos, factor, dimensions);

                padres.Clear();
                Console.WriteLine("Generación: " + gens + " con factor de mutacion: " + factor.ToString("P"));
                int son = 1;
                double maxFitness = 0;
                foreach (Alimento f in hijos)
                {
                    Console.WriteLine("Hijo " + son + ": " + f);
                    f.CalculateFitness();
                    padres.Add(f);
                    if(maxFitness )
                    son++;

                }
                hijos.Clear();
                gens++;
            }
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }*/

        private void MutateSons(List<Alimento> sons, double mutationFactor, int dimensions)
        {
            int mutatedCount = (int)(mutationFactor * sons.Count * dimensions);
            for (int i = 0; i < mutatedCount; i++)
            {
                int j = selector.Next(0, sons.Count * dimensions);
                int mutatedIndividual = j / dimensions;
                int mutatedAlelo = j % dimensions;
                sons[mutatedIndividual].Mutate(mutatedAlelo);
            }
        }

        private Alimento Ruleta(List<Alimento> padres)
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