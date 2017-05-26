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
            string _data = "{\"MsgBody\":[{\"MsgType\":\"TIMTextElem\",\"MsgContent\":{\"Text\":\"fasdfdsfdsf\"}},{\"MsgType\":\"TIMFaceElem\",\"MsgContent\":{\"Index\":3,\"Data\":\"[发呆]\"}},{\"MsgType\":\"TIMTextElem\",\"MsgContent\":{\"Text\":\"nexid = 2 & brandid = 2 & dialogid = \"}}],\"CallbackCommand\":\"C2C.CallbackAfterSendMsg\",\"From_Account\":\"VISITOR_11\",\"To_Account\":\"TXUSER_235500\",\"MsgTime\":1495788601}";

            var _chat = JsonConvert.DeserializeObject<ChatInfo>(_data);

           
            MongoHelper.InsertOne<ChatInfo>("chatlog", _chat);

            var _c = MongoHelper.GetOne<ChatInfo>("chatlog", new Document("To_Account", "TXUSER_235500"));

            //var _json = JObject.Parse(JsonConvert.SerializeObject(_chat));

            //var _doc = new Document();//_json.ToDictionary()           

            //MongoHelper.InsertOne<Document>("chatlog", _doc);
        }
    }   

    public class ChatInfo
    {
        public IList<MsgBodyInfo> MsgBody { get; set; }
        public string CallbackCommand { get; set; }
        public string From_Account { get; set; }
        public string To_Account { get; set; }
        public int MsgTime { get; set; }

        public class MsgContentInfo
        {
            public string Text { get; set; }
            public int? Index { get; set; }
            public string Data { get; set; }
        }

        public class MsgBodyInfo
        {
            public string MsgType { get; set; }
            public MsgContentInfo MsgContent { get; set; }
        }
    }
}
