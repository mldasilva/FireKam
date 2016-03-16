using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MotionDetection
{
    class DataAccess
    {
        protected static IMongoClient client;
        protected static IMongoDatabase database;

        public DataAccess()
        {

        }

        //insert function
        static public async Task<bool> Insert(BsonDocument document)
        {
            bool result = false;
            client = new MongoClient();
            database = client.GetDatabase("firekam");
            var collection = database.GetCollection<BsonDocument>("record");
            //    var document = new BsonDocument
            //    {
            //        {"Camera", "Camera1"},
            //        {"Time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
            //        {"MotionCout", motionCount }
            //    };
            await collection.InsertOneAsync(document);

            return result;
        }

        //find by camera
        static public async Task<string> FindByCamera(string camera)
        {
            client = new MongoClient();
            database = client.GetDatabase("firekam");
            var collection = database.GetCollection<BsonDocument>("record"); 
            var filter = Builders<BsonDocument>.Filter.Eq("Camera", "kevin");
            var result = await collection.Find(filter).ToListAsync();
            
            return result.First().ToString();
        }

        //find by time
        static public async Task<string> FindByTime(string minTime, string maxTime)
        {
            client = new MongoClient();
            database = client.GetDatabase("firekam");
            var collection = database.GetCollection<BsonDocument>("record");

            var filter = Builders<BsonDocument>.Filter.Gt("Time", minTime) & Builders<BsonDocument>.Filter.Lt("Time", maxTime);
            //var resultFirst = await collection.Find(filter).ToListAsync();
            var aggregate = collection.Aggregate().Match(filter).Group(new BsonDocument { {"_id", 0 }, { "MotionCount", new BsonDocument("$sum", "$MotionCout") } });
            var results = await aggregate.ToListAsync();
            if(results.Count == 0)
            {
                return "no result";
            }
            return results[0].GetElement("MotionCount").Value.ToString();
        }

        //test
        static public async void test ()
        {
            string a = await FindByCamera("camera1");
        }

    }
}
