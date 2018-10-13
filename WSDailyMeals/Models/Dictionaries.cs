using System.Collections.Generic;

namespace WSDailyMeals.Models
{
    static public class Dictionaries {

        public static List<string> GetCategories(string comida) {
            switch (comida) {
                case "Desayuno":
                    return new List<string>() { "" };
                case "Colacion":
                    return new List<string>() { "" };
                case "Comida":
                    return new List<string>() { "" };
                case "Cena":
                    return new List<string>() { "" };
            }
            return null;
        }

        public static Dictionary<string, double> GetMinValues(string macroNutriente) {
            Dictionary<string, double> MinValues = new Dictionary<string, double>();
            double valor = 0;
            switch (macroNutriente) {
                case "Carbohidratos":
                    MinValues.Add("categoria", valor);
                    break;
                case "Proteinas":
                    MinValues.Add("categoria", valor);
                    break;
                case "Lipidos":
                    MinValues.Add("categoria", valor);
                    break;
                case "Sodio":
                    MinValues.Add("categoria", valor);
                    break;
                case "Colesterol":
                    MinValues.Add("categoria", valor);
                    break;
            }
            return MinValues;
        }

        public static Dictionary<string, double> GetMaxValues(string macroNutriente)
        {
            Dictionary<string, double> MaxValues = new Dictionary<string, double>();
            double valor = 0;
            switch (macroNutriente)
            {
                case "Carbohidratos":
                    MaxValues.Add("categoria", valor);
                    break;
                case "Proteinas":
                    MaxValues.Add("categoria", valor);
                    break;
                case "Lipidos":
                    MaxValues.Add("categoria", valor);
                    break;
                case "Sodio":
                    MaxValues.Add("categoria", valor);
                    break;
                case "Colesterol":
                    MaxValues.Add("categoria", valor);
                    break;
            }
            return MaxValues;
        }
    }
}