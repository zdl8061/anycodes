﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using System.Diagnostics;
using System.Collections.Generic;

namespace DotNet.Utilities.Test
{
    [TestClass]
    public class MongoDbTest
    {
        static MongoDBHelper db;

        [TestMethod]
        public void TestMethod1()
        {
            //创建Mongodb的数据库实例
            db = new MongoDBHelper("mongodb://localhost:27017/", "TxIM");

            #region 1000W条数据的初始化
            //InitData();
            #endregion

            //Console.WriteLine("Mongodb 中自己的Skip-Limit分页与自定义的Where-Limit分页效率测试（毫秒）：");
            ////各种分页 尺寸的测试 具体注释我也不写了 
            //PagerTest(1, 100);//这个测试忽略，估计第一次查询之后会相应的缓存下数据  导致之后的查询很快
            //PagerTest(3, 100);
            //PagerTest(30, 100);
            //PagerTest(300, 100);
            //PagerTest(300, 1000);
            //PagerTest(3000, 100);
            //PagerTest(30000, 100);
            //PagerTest(300000, 100);

            //Console.ReadKey();



            string _data = "{\"MsgBody\":[{\"MsgType\":\"TIMTextElem\",\"MsgContent\":{\"Text\":\"123456789\"}},{\"MsgType\":\"TIMFaceElem\",\"MsgContent\":{\"Index\":3,\"Data\":\"[发呆]\"}},{\"MsgType\":\"TIMTextElem\",\"MsgContent\":{\"Text\":\"nexid = 2 & brandid = 2 & dialogid = \"}}],\"CallbackCommand\":\"C2C.CallbackAfterSendMsg\",\"From_Account\":\"VISITOR_100\",\"To_Account\":\"TXUSER_235500\",\"MsgTime\":1495788601}";
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

            db.Insert<BsonDocument>(_bson, "chatlog");

            var _chat = db.Find<BsonDocument>(Query.EQ("From_Account", "VISITOR_11"), "chatlog");

            var _t = db.Find<BsonDocument>(Query.And(Query.EQ("From_Account", "VISITOR_11"), Query.EQ("To_Account", "TXUSER_235500")
               , Query.EQ("MsgBody.MsgType", "TIMTextElem")),
               2, 10, new SortByDocument { { "_id", -1 } },
               "chatlog");

            db.Remove<BsonDocument>(Query.EQ("From_Account", "123456789"), "chatlog");

            db.Update<BsonDocument>(Query.EQ("From_Account", "123456789"),
                Update.Set("From_Account", "1532153215")
                , "chatlog");

        }

        /// <summary>
        /// 分页的测试
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页尺寸</param>
        static void PagerTest(int pageIndex, int pageSize)
        {
            //分页查询条件空（封装中会转恒真条件） 排序条件空（转为ObjectId递增） 设定页码 也尺寸

            Console.WriteLine("页码{0},页尺寸{1}", pageIndex, pageSize);
            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            List<LogInfo> list1 = db.Find<LogInfo>(null, pageIndex, pageSize, null);
            sw1.Stop();
            Console.WriteLine("Skip-Limit方式分页耗时：{0}", sw1.ElapsedMilliseconds);
            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            //这里以Logid索引为标志 如果集合里面没有这些主键标志的话 完全可以使用自己的ObjectId来做 帮助类里面也是封装好的
            //根据页码计算的LogId也只是简单的模拟 实际中这些LogId不一定会连续 这种方式分页一般不是传页码 而是传最后一个标志的值
            List<LogInfo> list2 = db.Find<LogInfo>(null, "LogId", (pageIndex - 1) * pageSize, pageSize, 1);
            sw2.Stop();
            Console.WriteLine("Where-Limit方式分页耗时：{0}\r\n", sw2.ElapsedMilliseconds);
        }

        /// <summary>
        /// 初始化一下数据
        /// </summary>
        static void InitData()
        {
            //创建 测试日志类的索引 索引的配置在LogInfo类的特性中
            db.CreateIndex<LogInfo>();

            //初始化日志的集合
            List<LogInfo> list = new List<LogInfo>();
            int temp = 0;

            //插入1000W条 测试的数据
            for (int i = 1; i <= 10000000; i++)
            {
                list.Add(new LogInfo
                {
                    LogId = i,
                    Content = "content" + i.ToString(),
                    CreateTime = DateTime.Now
                });

                //temp计数  并作大于100的判断
                if (++temp >= 100)
                {
                    //大于等于100就清零
                    temp = 0;
                    //用封装好的方法批量插入数据
                    db.Insert<LogInfo>(list);
                    //插入数据之后将当前数据清空掉
                    list.Clear();
                }
            }
        }
    }

    /// <summary>
    /// 测试表格的一个实体类
    /// </summary>
    public class LogInfo
    {
        public ObjectId _id;

        /// <summary>
        /// 日志Id  这里的特性 可以在帮助类中识别出
        /// </summary>
        [MongoDBFieldAttribute(true, Unique = true)]
        public int LogId { get; set; }

        /// <summary>
        /// 日志的内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 日志的创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
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
