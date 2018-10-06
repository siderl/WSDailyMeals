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
    }
}