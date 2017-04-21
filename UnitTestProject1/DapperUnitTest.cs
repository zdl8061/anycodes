using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class DapperUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var _result = DapperDemo.OneToOne();
        }
    }
}
