using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Text.RegularExpressions;
using System.Dynamic;
using System.Collections.Generic;
using ZDL.AnyCodes;

using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DotNet.Utilities.Test
{
    [TestClass]
    public class JsonTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void TestMethod2()
        {
            string _input = @"<title>丰富是打发是打发斯蒂芬发生的方式为水电费</title>";

            var _regex = new Regex("(?<=<title>).*(?=</title>)");

            var _match = _regex.Match(_input).Value;
        }

        [TestMethod]
        public void TestMethod3()
        {
            dynamic _goods = new ExpandoObject();

            var _dicGoods = (IDictionary<string, object>)_goods;

            _dicGoods["name"] = "asdf";
            _dicGoods["age"] = 12;

            _goods.addtime = DateTime.Now;

            string name = _goods.name;
            int age = _goods.age;
            var time = _goods.addtime;
        }

        [TestMethod]
        public void TestMethod4()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("GuidType", typeof(string));

            for (int i = 0; i < 10; i++) 
            {
                DataRow dr = dataTable.NewRow();
                dr["Name"] = "STRING" + i;
                dr["ID"] = i;
                if (i % 2 == 0)
                    dr["GuidType"] = "GuidType" + i;
                else
                {
                    dr["GuidType"] = DBNull.Value;
                }
                dataTable.Rows.Add(dr);
            }

            string s = ClassGenerating.DataTableToClass(dataTable, "UserInfo");

            var t = new { name = "asdf", age = 12, addtime = DateTime.Now };

            s = ClassGenerating.DynamicToClass(t, "userinfo");

            dynamic _goods = new ExpandoObject();
            _goods.clue_name = "123";
            _goods.clue_phone_txid = 123;
            _goods.origin_type = 3;

            var _r = JsonConvert.SerializeObject(_goods);

            var abc = new { clue_name = "", clue_phone_txid = 0, origin_type = 0 };

            var _t = JsonConvert.DeserializeAnonymousType(_r,abc );
            
        }

        [TestMethod]
        public void TestMethod5()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("GuidType", typeof(string));

            for (int i = 0; i < 10; i++)
            {
                DataRow dr = dataTable.NewRow();
                dr["Name"] = "STRING" + i;
                dr["ID"] = i;
                if (i % 2 == 0)
                    dr["GuidType"] = "GuidType" + i;
                else
                {
                    dr["GuidType"] = DBNull.Value;
                }
                dataTable.Rows.Add(dr);
            }

            var _aType = new { Name = "", ID = 0, GuidType = "" };

            var _list = ListAndTableExtension.FromTable(dataTable, _aType);


            var _json = JsonConvert.SerializeObject(_list);

            var _jsonObj = JsonConvert.SerializeObject(_list[0]);

            var _r = ListAndTableExtension.FromJsonObject(_jsonObj, _aType);
           

            var _r2 = ListAndTableExtension.FromJsonArray(_json, _aType);

           

        }

        [TestMethod]
        public void TestMethod6()
        {
            //var _obj = new { name = "stevevai", age = 40 };

            //string _json = JsonConvert.SerializeObject(_obj);


            var _obj2 = JsonConvert.DeserializeAnonymousType("{\"name\":\"stevevai\",\"age\":40}", new { name = "", age = 0 });



            string xml = "<Test><Name>Test class</Name><X>100</X><Y>200</Y></Test>";

            XDocument doc = XDocument.Parse(xml);
            string json = JsonConvert.SerializeXNode(doc);



            string _json = "{\"name\":\"stevevai\",\"age\":40}";

            JObject jO = JObject.Parse(_json);

            jO["count"] = 100;

            var _re = jO.ToString();



            JArray arr = new JArray();
            arr.Add(JObject.Parse("{\"name\":\"a\",\"age\":1}"));
            arr.Add(JObject.Parse("{\"name\":\"b\",\"age\":2}"));
            arr.Add(JObject.Parse("{\"name\":\"c\",\"age\":3}"));

            JObject j2 = new JObject();
            j2["name"] = "malmsteen";
            j2["li"] = arr;

            string j2str = j2.ToString();


        }

    }
}
