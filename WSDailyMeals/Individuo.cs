using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WSDailyMeals.Models;
using WSReportApp.Models;

namespace WSDailyMeals
{
    [DataContract]
    public class Individuo
    {
        [DataMember]
        private readonly double MetaLipidos, MetaCarbos, MetaProteinas;
        [DataMember]
        public readonly string hash;
        [DataMember]
        public List<Alimento> dieta = new List<Alimento>();
        //TODO: Agregar sumatoria de calorias como propiedad

        public double Fitness { get; private set; }

        public Individuo(ref Random selector, double Lipidos, double Carbos, double Proteinas, ref Dictionaries dic) {
            MetaLipidos = Lipidos;
            MetaCarbos = Carbos;
            MetaProteinas = Proteinas;
            
            //generacion de hash
            //int = dieta.GetHashCode()
            
            //Desayuno
            this.dieta.Add(dic.drinks[selector.Next(dic.drinks.Count)]);
            this.dieta.Add(dic.breakfasts[selector.Next(dic.breakfasts.Count)]);
            //Colacion 1
            this.dieta.Add(dic.collations[selector.Next(dic.collations.Count)]);
            //Comida
            this.dieta.Add(dic.drinks[selector.Next(dic.drinks.Count)]);
            this.dieta.Add(dic.strongMeals[selector.Next(dic.strongMeals.Count)]);
            this.dieta.Add(dic.fittings[selector.Next(dic.fittings.Count)]);
            this.dieta.Add(dic.soups[selector.Next(dic.soups.Count)]);
            //Colacion 2
            this.dieta.Add(dic.collations[selector.Next(dic.collations.Count)]);
            //Cena
            this.dieta.Add(dic.drinks[selector.Next(dic.drinks.Count)]);
            this.dieta.Add(dic.breakfasts[selector.Next(dic.breakfasts.Count)]);

            CalculateFitness();

        }
        
        public Individuo(Individuo Padre1, Individuo Padre2, int CrossingFactor, int dim)
        {
            Fitness = 0;
            dieta = new List<Alimento>(dim);

            MetaLipidos = Padre1.MetaLipidos;
            MetaCarbos = Padre1.MetaCarbos;
            MetaProteinas = Padre1.MetaProteinas;

            for (int i = 0; i < dieta.Capacity; i++)
            {
                if (i < CrossingFactor)
                    dieta.Add(Padre1.dieta[i]);
                else
                    dieta.Add(Padre2.dieta[i]);
            }
            CalculateFitness();
        }

        public double CalculateFitness()
        {
            double sumatoriaLips = 0, sumatoriaCarbs = 0, sumatoriaProts = 0; // sumatoriaKcal = 0
            double fitnessLips = 0, fitnessCarbs = 0, fitnessProts = 0;
            foreach (Alimento temp in dieta) {
                sumatoriaLips += temp.KcalLipidos;
                sumatoriaCarbs += temp.KcalCarbos;
                sumatoriaProts += temp.KcalProteina;
            }

            fitnessLips = (sumatoriaLips / MetaLipidos) * 100;
            if (fitnessLips > 100) { fitnessLips = 200 - fitnessLips; }
            fitnessCarbs = (sumatoriaCarbs / MetaCarbos) * 100;
            if (fitnessCarbs > 100) { fitnessCarbs = 200 - fitnessCarbs; }
            fitnessProts = (sumatoriaProts / MetaProteinas) * 100;
            if (fitnessProts > 100) { fitnessProts = 200 - fitnessProts; }

            double fitnessTotal = ((fitnessLips + fitnessCarbs + fitnessProts) / 300) * 100;
            Fitness = fitnessTotal;
            return fitnessTotal;
        }
        

        public void Mutate(int Idx, ref Dictionaries dict)
        {
            //obtener lista
            List<Alimento> candidatos = new List<Alimento>();
            switch (Idx) {
                case 0:
                case 8:
                    //bebida cena y desayuno - alimento que queremos mutar (usar LINQ)
                    candidatos = dict.drinks.Where(x => x.ID != dieta[Idx].ID).ToList();
                    break;
                case 1:
                case 9:
                    candidatos = dict.breakfasts.Where(x => x.ID != dieta[Idx].ID).ToList();
                    //platillo cena y desayuno - alimento que queremos mutar (usar LINQ)
                    break;
                case 2:
                case 7:
                    candidatos = dict.collations.Where(x => x.ID != dieta[Idx].ID).ToList();
                    //colaciones 1 y 2(fruta|verdura|grasas con proteína) - alimento que queremos mutar (usar LINQ)
                    break;
                case 3:
                    candidatos = dict.drinks.Where(x => x.ID != dieta[Idx].ID).ToList();
                    //bebida comida - alimento que queremos mutar (usar LINQ)
                    break;
                case 4:
                    candidatos = dict.strongMeals.Where(x => x.ID != dieta[Idx].ID).ToList();
                    //plato fuerte - alimento que queremos mutar (usar LINQ)
                    break;
                case 5:
                    candidatos = dict.fittings.Where(x => x.ID != dieta[Idx].ID).ToList();
                    //guarnicion comida - alimento que queremos mutar (usar LINQ)
                    break;
                case 6:
                    candidatos = dict.soups.Where(x => x.ID != dieta[Idx].ID).ToList();
                    // sopa|sopa seca - alimento que queremos mutar (usar LINQ)
                    break;
            }

            Random r = new Random((int)DateTime.Now.Ticks);
            int newIdx = r.Next(0, candidatos.Count);
            dieta[Idx] = candidatos[newIdx];
        }
    }
}