using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSReportApp.Models;

namespace WSDailyMeals.DAOS
{
    public class DAOAliments
    {
        public List<Alimento> GetAliments()
        {
            // Produccion
            //MongoClient client = new MongoClient("mongodb://ec2-18-219-187-111.us-east-2.compute.amazonaws.com:27017");

            //Pruebas
            MongoClient client = new MongoClient("mongodb://localhost:27017");

            IMongoDatabase db = client.GetDatabase("DailyMeals");

            IMongoCollection<Alimento> devCollection = db.GetCollection<Alimento>("alimentos");

            return devCollection.Find(FilterDefinition<Alimento>.Empty).ToList();
        }
    }
}