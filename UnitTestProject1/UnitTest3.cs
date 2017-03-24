using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using ZDL.AnyCodes;
using System.Dynamic;
using System.Collections;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void TestMethod1()
        {
            var _root = new JObject();
            var _get = new JObject{
                  { "@action", "handle-user-input.jsp" },
                  { "@numdigits", "1"},
                  { "Play", "menu.wav" }
            };
            _root.Add("Get", _get);
            _root.Add("Play", "sorrybye.wav");
            _root.Add("Redirect", "/welcome/voice");
            var _res = new JObject();
            _res.Add("Response", _root);

            var c = JsonConvert.DeserializeXNode(_res.ToString());
            var s = JsonConvert.SerializeXNode(c);

            //{ "Response":{ "Get":{ "@action":"handle-user-input.jsp","@numdigits":"1","Play":"menu.wav"},"Play":"sorrybye.wav","Redirect":"/welcome/voice"} }

            var _n = new JObject
            {
                {"Response",new JObject
                {
                    { "Get",new JObject
                    {
                        { "@action","handle-user-input.jsp"},
                        { "@numdigits",1},
                        { "Play","menu.wav"}
                    } },
                    { "Play","sorrybye.wav"},
                    { "Redirect","/welcome/voice"}
                } }
            };

            var t = _n.ToString();



        }

        [TestMethod]
        public void TestMethod2()
        {

            string _str = "status=000000;name=abcde;errcode=0".Replace(";", "&");

            var s = HttpUtility.ParseQueryString(_str);

            //var d = s.AllKeys.ToDictionary(k => k, k => s[k]);

            var _r = JsonConvert.SerializeObject(s);
        }

        [TestMethod]
        public void TestMethod3()
        {

            string _sql = "select * from test where name=@p0 and age=@p1 and tt=@p2";


            //TxDataV2.SpInsert("TxoooCMS", "SP_ZDL_InsertNews", new Hashtable
            //{
            //    { "@add","111"}
            //});

        }

        [TestMethod]
        public void TestMethod4()
        {

            OrderTest _order = new OrderTest();
            _order.OnPaySuccess += (o, e) =>
            {
                string s = "asdf";

                
            };

            _order.PayOK();

        }

      
    }
}
