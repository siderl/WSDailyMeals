using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text;

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

        public double multiplier = 1.0;

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

        public double getKcal() {
            if (String.IsNullOrEmpty(EnergiaKcal.ToString())) return 0.0;
            if (EnergiaKcal is int) { return ((int)EnergiaKcal) * 1.0; }
            if (EnergiaKcal is double) { return ((double)EnergiaKcal); }
            return 0.0;
        }
        

        public override string ToString()
        {
            StringBuilder temp = new StringBuilder();
            temp.Append(AlimentoName);
            temp.Append(" (");
            temp.Append(Categoria);
            temp.Append("): ");
            temp.Append("Carbos: ");
            temp.Append(KcalCarbos);
            temp.Append(" Prots: ");
            temp.Append(KcalProteina);
            temp.Append(" Lips: ");
            temp.Append(KcalLipidos);
            return temp.ToString();
        }

        public static bool operator ==(Alimento t1, Alimento t2) {
            if (ReferenceEquals(t1, t2))
            {
                return true;
            }

            if (ReferenceEquals(t1, null))
            {
                return false;
            }
            if (ReferenceEquals(t2, null))
            {
                return false;
            }

            if (t1.ID == t2.ID) {
                return true;
            }
            return false;
        }

        static public bool operator !=(Alimento t1, Alimento t2)
        {
            return !(t1 == t2);
        }

        public override bool Equals (object obj)
        {
            if (obj is Alimento) {
                if (ReferenceEquals(obj, this))
                {
                    return true;
                }

                if (ReferenceEquals(obj, null))
                {
                    return false;
                }

                if (((Alimento)obj).ID == ID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}