using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MongoDB;

namespace UnitTestProject1
{
    [TestClass]
    public class MongoTestSamus
    {
        [TestMethod]
        public void TestMethod1()
        {
            //MongoHelper.InsertOne<ChatInfo>("chatlog", new ChatInfo { Name = "open", Email = "open@txooo.com", Level = 1 });

            var _json = JObject.Parse(JsonConvert.SerializeObject(new ChatInfo { Name = "open", Email = "open@txooo.com", Level = 1 }));

            var _doc = new Document(_json.ToDictionary());

            MongoHelper.InsertOne<Document>("chatlog", _doc);
        }
    }

    public class ChatInfo
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public int Level { get; set; }


    }
}
