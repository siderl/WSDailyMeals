using System.Collections.Generic;
using System.Linq;
using WSDailyMeals.DAOS;
using WSReportApp.Models;

namespace WSDailyMeals.Models
{
    public class Dictionaries {

        public List<Alimento> drinks { get; private set; }
        public List<Alimento> collations { get; private set; }
        public List<Alimento> breakfasts { get; private set; }
        public List<Alimento> fittings { get; private set; }
        public List<Alimento> soups { get; private set; }
        public List<Alimento> strongMeals { get; private set; }
        public List<double> multipliers { get; private set; }


        public Dictionaries() {
            DaoAliments daoAliments = new DaoAliments();
            List<Alimento> aliments = daoAliments.GetAlimentsForDiet();
            drinks = aliments.Where(x => (string)x.Categoria == "platillos:bebidas")
                .ToList();

            collations = aliments.Where(x => (string)x.Categoria == "fruta" ||
                                             (string)x.Categoria == "verdura" || 
                                             (string)x.Categoria == "aceites y grasas con proteina")
                .ToList();

            breakfasts = aliments.Where(x => (string)x.Categoria == "platillos:desayuno")
                .ToList();

            fittings = aliments.Where(x => (string)x.Categoria == "platillos:guarniciones")
                .ToList();

            soups = aliments.Where(x => (string)x.Categoria == "platillos:sopas" ||
                                        (string)x.Categoria == "platillos:sopas secas")
                .ToList();

            strongMeals = aliments.Where(x => (string)x.Categoria == "platillos:plato fuerte")
                .ToList();
            multipliers = new List<double>() { 1, 1.5, 2, 2.5, 3 };
        }
    }
}