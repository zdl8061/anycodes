using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Text.RegularExpressions;
using System.Dynamic;
using System.Collections.Generic;
using ZDL.AnyCodes;
using Newtonsoft.Json;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
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

            var _list = ListAndTableExtension.FromTable(dataTable,
                new { Name = "", ID = 0, GuidType = "" });

            var _json = JsonConvert.SerializeObject(_list);

        }

    }
}
