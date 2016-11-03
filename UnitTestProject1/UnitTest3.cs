using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void TestMethod1()
        {
            //using (TxDataHelper helper = TxDataHelper.GetDataHelper("TxoooAgent"))
            //{
            //    string _sql = "select * from sales_product_index";

            //    DataTable _dt = helper.SqlGetDataTable(_sql);

            //    string json = JsonConvert.SerializeObject(_dt);

            //    string cjson = ZipHelper.Compress(json);
            //}

            //var _root = new JObject();
            //var _get = new JObject{
            //      { "@action", "handle-user-input.jsp" },
            //      { "@numdigits", "1"},
            //      { "Play", "menu.wav" }
            //};
            //_root.Add("Get", _get);
            //_root.Add("Play", "sorrybye.wav");
            //_root.Add("Redirect", "/welcome/voice");
            //var _res = new JObject();
            //_res.Add("Response", _root);

            //var c = JsonConvert.DeserializeXNode(_res.ToString());
            //var s = JsonConvert.SerializeXNode(c);



        }

        [TestMethod]
        public void TestMethod2()
        {

            string _str = "status=000000;name=abcde;errcode=0".Replace(";", "&");

            var s = HttpUtility.ParseQueryString(_str);

            var d = s.AllKeys.ToDictionary(k => k, k => s[k]);

            //var _r = JsonConvert.SerializeObject(d);
        }
    }
}
