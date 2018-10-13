using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSDailyMeals
{
    public class Individuo
    {
        public double Fitness { get; private set; }

        public double CalculateFitness(double metaLipidos, double metaCarbos, double metaProteinas, double metaColesterol, double metaSodio)
        {
            if (KcalLipidos > metaLipidos) { return 0; }
            if (KcalCarbos > metaCarbos) { return 0; }
            if (KcalProteina > metaProteinas) { return 0; }
            //if (String.IsNullOrEmpty(Colesterol.ToString())) { Colesterol = 0; }
            //if (String.IsNullOrEmpty(Sodio.ToString())) { Sodio = 0; }
            //if (((double)Colesterol) > metaColesterol) { return 0; }
            //if (((double)Sodio) > metaLipidos) { return 0; }

            double fitnessCarbos = (KcalCarbos / metaCarbos) * 100;
            double fitnessLipidos = (KcalLipidos / metaLipidos) * 100;
            double fitnessProteinas = (KcalLipidos / metaLipidos) * 100;

            double fitnessTotal = fitnessCarbos + fitnessLipidos + fitnessProteinas;
            Fitness = (fitnessTotal / 300) * 100;
            return Fitness;
        }



        public void Mutate(int Idx)
        {
            /*** TODO: Preguntar al profe si podemos seleccionar otro alimento de la misma categoría
             *  ¿Qué es más eficiente/mejor?
             *  ¿Cambiar al individuo completamente? (Seleccionando un índice diferente dentro de la misma categoria) (menos tardado, pero posiblemente menos accurate y no respeta la mutacion al 100%)
             *  ¿O calcular atributos nuevos y buscar al alimento que se asemeje más a esos atributos? (dentro de la misma categoría) (más tardado, pero respeta más la mutación)
             */
            switch (Idx)
            {
                case 0:
                    //mutar Carbos
                    break;
                case 1:
                    //mutar Proteinas
                    break;
                case 2:
                    //mutar Lipidos
                    break;
                case 3:
                    //mutar Sodio
                    break;
                case 4:
                    //mutar Colesterol
                    break;
            }
            //calculateFitness();
        }
    }
}