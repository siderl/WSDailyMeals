using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
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
        [DataMember]
        public double totalKCal;
        [DataMember]
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
            //Multiplicador aleatorio de la colacion
            double multiplier = dic.multipliers[selector.Next(dic.multipliers.Count)];
            Alimento collation = dic.collations[selector.Next(dic.collations.Count)];
            collation.multiplier = multiplier;
            MultiplyPortion(ref collation);
            this.dieta.Add(collation);
            
            //Comida
            this.dieta.Add(dic.drinks[selector.Next(dic.drinks.Count)]);
            this.dieta.Add(dic.strongMeals[selector.Next(dic.strongMeals.Count)]);

            //Multiplicador aleatorio de la guarnicion
            Alimento fitting = dic.fittings[selector.Next(dic.fittings.Count)];
            fitting.multiplier = multiplier;
            MultiplyPortion(ref fitting);
            this.dieta.Add(fitting);

            //Multiplicador aleatorio de las porciones de sopa
            Alimento soup = dic.soups[selector.Next(dic.soups.Count)];
            soup.multiplier = multiplier;
            MultiplyPortion(ref soup);
            this.dieta.Add(soup);

            //Colacion 2
            //Multiplicador aleatorio de la colacion
            Alimento collation1 = dic.collations[selector.Next(dic.collations.Count)];
            collation1.multiplier = multiplier;
            MultiplyPortion(ref collation1);
            this.dieta.Add(collation1);
            
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

            totalKCal = sumatoriaLips + sumatoriaCarbs + sumatoriaProts;

            double fitnessTotal = ((fitnessLips + fitnessCarbs + fitnessProts) / 300) * 100;
            Fitness = fitnessTotal;
            return fitnessTotal;
        }
        
        public void Mutate(int Idx, ref Dictionaries dict)
        {
            //obtener lista
            List<Alimento> candidatos = new List<Alimento>();
            switch (Idx) {
                case 0: //bebida cena y desayuno
                case 8:
                    candidatos = dict.drinks.Where(x => x.ID != dieta[Idx].ID).ToList();
                    break;
                case 1: //platillo cena y desayuno
                case 9:
                    candidatos = dict.breakfasts.Where(x => x.ID != dieta[Idx].ID).ToList();
                    break;
                case 2: //colaciones 1 y 2(fruta|verdura|grasas con proteína)
                case 7:
                    candidatos = dict.collations.Where(x => x.ID != dieta[Idx].ID).ToList();
                    break;
                case 3: //bebida comida
                    candidatos = dict.drinks.Where(x => x.ID != dieta[Idx].ID).ToList();
                    break;
                case 4: //plato fuerte
                    candidatos = dict.strongMeals.Where(x => x.ID != dieta[Idx].ID).ToList();
                    break;
                case 5: //guarnicion comida
                    candidatos = dict.fittings.Where(x => x.ID != dieta[Idx].ID).ToList();
                    break;
                case 6: //sopa|sopa seca
                    candidatos = dict.soups.Where(x => x.ID != dieta[Idx].ID).ToList();
                    break;
            }

            Random r = new Random((int)DateTime.Now.Ticks);
            int newIdx = r.Next(0, candidatos.Count);
            Alimento newMeal = candidatos[newIdx];
            if (Idx == 5 || Idx == 6 || Idx == 2 || Idx == 7) {
                if (String.IsNullOrEmpty(newMeal.Proteina.ToString())) { newMeal.Proteina = 0; }
                if (newMeal.Proteina is int) { newMeal.Proteina = (int)newMeal.Proteina / newMeal.multiplier; }
                else if (newMeal.Proteina is double) { newMeal.Proteina = ((double)newMeal.Proteina) / newMeal.multiplier; }

                if (String.IsNullOrEmpty(newMeal.CarboHidratos.ToString())) { newMeal.CarboHidratos = 0; }
                if (newMeal.CarboHidratos is int) { newMeal.CarboHidratos = (int)newMeal.CarboHidratos / newMeal.multiplier; }
                else if (newMeal.CarboHidratos is double) { newMeal.CarboHidratos = ((double)newMeal.CarboHidratos) / newMeal.multiplier; }

                if (String.IsNullOrEmpty(newMeal.Lipidos.ToString())) { newMeal.Lipidos = 0; }
                if (newMeal.Lipidos is int) { newMeal.Lipidos = (int)newMeal.Lipidos / newMeal.multiplier; }
                else if (newMeal.Lipidos is double) { newMeal.Lipidos = ((double)newMeal.Lipidos) / newMeal.multiplier; }

                double multiplier = dict.multipliers[r.Next(dict.multipliers.Count)];
                newMeal.multiplier = multiplier;

                if (String.IsNullOrEmpty(newMeal.Proteina.ToString())) { newMeal.Proteina = 0; }
                if (newMeal.Proteina is int) { newMeal.Proteina = (int)newMeal.Proteina * multiplier; }
                else if (newMeal.Proteina is double) { newMeal.Proteina = ((double)newMeal.Proteina) * multiplier; }

                if (String.IsNullOrEmpty(newMeal.CarboHidratos.ToString())) { newMeal.CarboHidratos = 0; }
                if (newMeal.CarboHidratos is int) { newMeal.CarboHidratos = (int)newMeal.CarboHidratos * multiplier; }
                else if (newMeal.CarboHidratos is double) { newMeal.CarboHidratos = ((double)newMeal.CarboHidratos) * multiplier; }

                if (String.IsNullOrEmpty(newMeal.Lipidos.ToString())) { newMeal.Lipidos = 0; }
                if (newMeal.Lipidos is int) { newMeal.Lipidos = (int)newMeal.Lipidos * multiplier; }
                else if (newMeal.Lipidos is double) { newMeal.Lipidos = ((double)newMeal.Lipidos) * multiplier; }
            }
            dieta[Idx] = newMeal;
        }

        public override string ToString()
        {
            StringBuilder temp = new StringBuilder();
            temp.Append("KiloCalorias Totales: ");
            temp.Append(totalKCal);
            return temp.ToString();
        }

        private void MultiplyPortion(ref Alimento food) {
            if (String.IsNullOrEmpty(food.Proteina.ToString())) { food.Proteina = 0; }
            if (food.Proteina is int) { food.Proteina = (int)food.Proteina * food.multiplier; }
            else if (food.Proteina is double) { food.Proteina = ((double)food.Proteina) * food.multiplier; }

            if (String.IsNullOrEmpty(food.CarboHidratos.ToString())) { food.CarboHidratos = 0; }
            if (food.CarboHidratos is int) { food.CarboHidratos = (int)food.CarboHidratos * food.multiplier; }
            else if (food.CarboHidratos is double) { food.CarboHidratos = ((double)food.CarboHidratos) * food.multiplier; }

            if (String.IsNullOrEmpty(food.Lipidos.ToString())) { food.Lipidos = 0; }
            if (food.Lipidos is int) { food.Lipidos = (int)food.Lipidos * food.multiplier; }
            else if (food.Lipidos is double) { food.Lipidos = ((double)food.Lipidos) * food.multiplier; }
        }
    }
}