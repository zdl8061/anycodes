using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZDL.AnyCodes.Task;
using System.IO;
using System.Collections.Generic;
using Txooo;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void TestMethod1()
        {
            TaskManger.Login();
        }


        [TestMethod]
        public void TestMethod2()
        {
            ZDL.AnyCodes.FilterService.FilterServiceSoapClient _client =
                new ZDL.AnyCodes.FilterService.FilterServiceSoapClient();

            _client.UpdateWords();
            var _result = _client.FilterUserData("阿萨德假钞水电费");
        }

    }
}
