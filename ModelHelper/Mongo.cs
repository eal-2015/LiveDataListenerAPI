using MongoDB.Driver;
using System;


namespace ModelHelper
{
    public class Mongo
    {
        public IMongoCollection<TrafficMeasurement> ConnectToMeasurement(string databaseName, string collectionName)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase(databaseName); // is made if not already there
            return database.GetCollection<TrafficMeasurement>(collectionName); // is made if not already there
        }

        public IMongoCollection<VemcoMeasurement> ConnectToVemco(string databaseName, string collectionName)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase(databaseName); // is made if not already there
            return database.GetCollection<VemcoMeasurement>(collectionName); // is made if not already there
        }

        public IMongoCollection<VemcoStation> ConnectToVemcoStations(string databaseName, string collectionName)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase(databaseName); // is made if not already there
            return database.GetCollection<VemcoStation>(collectionName); // is made if not already there
        }
    }
}
