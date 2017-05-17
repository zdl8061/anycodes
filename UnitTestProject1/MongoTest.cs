using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace UnitTestProject1
{
    [TestClass]
    public class MongoTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var connectionString = "127.0.0.1";
            var server = MongoHelper<Entity>.GetDBInstance(connectionString);
            var database = server.GetDatabase("test");
            var collection = database.GetCollection<Entity>("entities");

            var entity = new Entity { Name = "Tom" };
            collection.Insert(entity);
            var id = entity.Id;

            var query = Query.EQ("_id", id);
            entity = collection.FindOne(query);

            entity.Name = "Dick";
            collection.Save(entity);

            var update = Update.Set("Name", "Harry");
            collection.Update(query, update);

            collection.Remove(query);
        }
    }

    public class Entity
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}
