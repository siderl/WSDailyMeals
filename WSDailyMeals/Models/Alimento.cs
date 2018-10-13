using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSReportApp.Models
{
    public class Alimento
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [BsonElement("alimento")]
        public string AlimentoName { get; set; }

        [BsonElement("cantidadSugerida")]
        public object CantidadSugerida { get; set; }

        [BsonElement("unidad")]
        public object Unidad { get; set; }

        [BsonElement("pesoBruto")]
        public object PesoBruto { get; set; }

        [BsonElement("pesoNeto")]
        public object PesoNeto { get; set; }

        [BsonElement("energiaKcal")]
        public object EnergiaKcal { get; set; }

        [BsonElement("proteina")]
        public object Proteina { get; set; }

        [BsonElement("lipidos")]
        public object Lipidos { get; set; }

        [BsonElement("carboHidratos")]
        public object CarboHidratos { get; set; }

        [BsonElement("fibra")]
        public object Fibra { get; set; }

        [BsonElement("categoria")]
        public object Categoria { get; set; }

        [BsonElement("sodio")]
        public object Sodio { get; set; }

        [BsonElement("colesterol")]
        public object Colesterol { get; set; }

        public double KcalProteina {
            get {
                if (String.IsNullOrEmpty(Proteina.ToString())) return 0;
                if (Proteina is int) { return ((int)Proteina) * 4.0; }
                return ((double)Proteina) * 4.0;
            }
        }

        public double KcalCarbos {
            get {
                if (String.IsNullOrEmpty(CarboHidratos.ToString())) return 0;
                if (CarboHidratos is int) { return ((int)CarboHidratos) * 4.0; }
                return ((double)CarboHidratos) * 4.0;
            }
        }

        public double KcalLipidos {
            get {
                if (String.IsNullOrEmpty(Lipidos.ToString())) return 0;
                if (Lipidos is int) { return ((int)Lipidos) * 9.0; }
                return ((double)Lipidos) * 9.0;
            }
        }

        

        public void Mutate(int Idx)
        {
            /*** TODO: Preguntar al profe si podemos seleccionar otro alimento de la misma categoría
             *  ¿Qué es más eficiente/mejor?
             *  ¿Cambiar al individuo completamente? (Seleccionando un índice diferente dentro de la misma categoria) (menos tardado, pero posiblemente menos accurate y no respeta la mutacion al 100%)
             *  ¿O calcular atributos nuevos y buscar al alimento que se asemeje más a esos atributos? (dentro de la misma categoría) (más tardado, pero respeta más la mutación)
             */
            switch (Idx) {
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

        public override string ToString()
        {
            string temp = "[";
            for (int i = 0; i < dimensions; i++)
            {
                temp += Cromosoma[i].ToString("N2") + ", ";
            }
            temp = temp.TrimEnd(new char[] { ' ', ',' });
            temp += "]";
            return "Cromosoma: " + temp + " Fitness: " + fitness.ToString("N2");
        }
    }
}