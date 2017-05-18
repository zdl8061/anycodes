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
            var server = MongoHelper<User>.GetDBInstance(connectionString);
            var database = server.GetDatabase("nodetest1");
            var collection = database.GetCollection<User>("usercollection");

            var entity = new User { Username = "Tom" , Email="tom@sina.com"};
            collection.Insert(entity);
            var id = entity.Id;

            var query = Query.EQ("_id", id);
            entity = collection.FindOne(query);

            entity.Username = "Dick";
            collection.Save(entity);

            var update = Update.Set("Username", "Harry");
            collection.Update(query, update);

            collection.Remove(query);
        }
    }

    public class User
    {
        public ObjectId Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }
    }
}
