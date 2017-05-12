using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace UnitTestProject1
{
    [TestClass]
    public class DapperUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var _result = DapperDemo.OneToOne();

            var _result2 = DapperDemo.OneToMany();

            //var _result3 = DapperDemo.InsertObject();

            var _result4 = DapperDemo.GetPager(null, null, 1);

            var _json = JsonConvert.SerializeObject(_result4);
        }
    }
}
