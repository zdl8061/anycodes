using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MongoDB;
using System.Collections.Generic;
using System.Collections;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace UnitTestProject1
{
    [TestClass]
    public class MongoTestSamus
    {
        [TestMethod]
        public void TestMethod1()
        {
            string _data = "{\"MsgBody\":[{\"MsgType\":\"TIMTextElem\",\"MsgContent\":{\"Text\":\"fasdfdsfdsf\"}},{\"MsgType\":\"TIMFaceElem\",\"MsgContent\":{\"Index\":3,\"Data\":\"[发呆]\"}},{\"MsgType\":\"TIMTextElem\",\"MsgContent\":{\"Text\":\"nexid = 2 & brandid = 2 & dialogid = \"}}],\"CallbackCommand\":\"C2C.CallbackAfterSendMsg\",\"From_Account\":\"VISITOR_11\",\"To_Account\":\"TXUSER_235500\",\"MsgTime\":1495788601}";
            BsonDocument _bson = BsonDocument.Parse(_data);



            //Query.All("name", "a", "b");//通过多个元素来匹配数组
            //Query.And(Query.EQ("name", "a"), Query.EQ("title", "t"));//同时满足多个条件
            //Query.EQ("name", "a");//等于
            //Query.Exists("type", true);//判断键值是否存在
            //Query.GT("value", 2);//大于>
            //Query.GTE("value", 3);//大于等于>=
            //Query.In("name", "a", "b");//包括指定的所有值,可以指定不同类型的条件和值
            //Query.LT("value", 9);//小于<
            //Query.LTE("value", 8);//小于等于<=
            //Query.Mod("value", 3, 1);//将查询值除以第一个给定值,若余数等于第二个给定值则返回该结果
            //Query.NE("name", "c");//不等于
            //Query.Nor(Array);//不包括数组中的值
            //Query.Not("name");//元素条件语句
            //Query.NotIn("name", "a", 2);//返回与数组中所有条件都不匹配的文档
            //Query.Or(Query.EQ("name", "a"), Query.EQ("title", "t"));//满足其中一个条件
            //Query.Size("name", 2);//给定键的长度
            //Query.Type("_id", BsonType.ObjectId);//给定键的类型
            //Query.Where(BsonJavaScript);//执行JavaScript
            //Query.Matches("Title", str);//模糊查询 相当于sql中like  -- str可包含正则表达式

            MongoDBHelper.InsertOne<BsonDocument>("chatlog", _bson);

            var _chat = MongoDBHelper.GetOne<BsonDocument>("chatlog", Query.EQ("From_Account", "VISITOR_11"));

            var _t = MongoDBHelper.GetAll<BsonDocument>("chatlog", Query.EQ("From_Account", "VISITOR_11"), new PagerInfo { Page = 2, PageSize = 20 });

            var _r =MongoDBHelper.Delete("chatlog", "5928f0459d491c26e81b0191");

            _r = MongoDBHelper.UpdateOne("chatlog", _bson);


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
