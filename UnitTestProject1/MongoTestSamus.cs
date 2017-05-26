using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MongoDB;
using System.Collections.Generic;
using System.Collections;

namespace UnitTestProject1
{
    [TestClass]
    public class MongoTestSamus
    {
        [TestMethod]
        public void TestMethod1()
        {
            var _chat = new ChatInfo()
            {
                Name = "asdf",
                Email = "hu2@dsf.com",
                Level = 2,
                Link = new LinkInfo()
                {
                    Family = "fengtaiqu ",
                    Mobile = "13563212254"
                },
                LinkCols = new List<LinkInfo>()
                {
                    new LinkInfo{  Family="lskdjfie", Mobile="15325632125"},
                    new LinkInfo{ Family="erjoeijrgeerog", Mobile="18654512215"}
                }
            };

           
            MongoHelper.InsertOne<ChatInfo>("chatlog", _chat);

            //var _json = JObject.Parse(JsonConvert.SerializeObject(_chat));

            //var _doc = new Document();//_json.ToDictionary()           

            //MongoHelper.InsertOne<Document>("chatlog", _doc);
        }
    }

    public class ChatInfo
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public int Level { get; set; }

        public LinkInfo Link { get; set; }

        public IList<LinkInfo> LinkCols { get; set; }
    }

    public class LinkInfo
    {
        public string Mobile { get; set; }

        public string Family { get; set; }
    }
}
