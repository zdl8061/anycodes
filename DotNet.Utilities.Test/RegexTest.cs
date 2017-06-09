using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZDL.AnyCodes.Task;
using System.IO;
using System.Collections.Generic;
using Txooo;
using ZDL.AnyCodes;
using System.Threading;
using System.Linq;
using System.Text.RegularExpressions;

namespace DotNet.Utilities.Test
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
        public void TestMethod3()
        {
            //int evertCount = 1000;

            //List<string> _wordList = new List<string>();
            //using (StreamReader sr = new StreamReader("TXT.txt"))
            //{
            //    while (sr.Peek() > -1)
            //    {
            //        string _word = sr.ReadLine().Replace(" ", "").Replace("/r", "").Replace("/n", "");

            //        _wordList.Add(_word);
            //    }
            //}

            //var _pages = (_wordList.Count + evertCount - 1) / evertCount;


            //for (int i = 1; i <= _pages; i++)
            //{
            //    int _pageIndex = i;

            //    var _currentList = _wordList.Skip((_pageIndex - 1) * evertCount).Take(evertCount);

            //    using (TxDataHelper helper = TxDataHelper.GetDataHelper("TxoooCMS08"))
            //    {
            //        foreach (string word in _currentList)
            //        {
            //            try
            //            {
            //                string _sql = "INSERT INTO [dbo].[bad_words]([keyword]) VALUES(@K)";
            //                helper.SpFileValue["@k"] = word;
            //                helper.SqlExecute(_sql, helper.SpFileValue);
            //            }
            //            catch (Exception)
            //            {
            //                continue;
            //            }

            //        }
            //    }

            //    Thread.Sleep(2000);
            //}

            //Console.ReadLine();
        }


        [TestMethod]
        public void TestMethod2()
        {
            ZDL.AnyCodes.FilterService.FilterServiceSoapClient _client =
                new ZDL.AnyCodes.FilterService.FilterServiceSoapClient();

            //HttpTools _req = new HttpTools("http://world.huanqiu.com/exclusive/2016-12/9781710.html");
            //string _content = _req.Get();

            //_client.UpdateWords();
            var _result = _client.FilterUserData("阿斯顿发送到发送到发斯蒂芬买卖枪支飞飞2323让3");
        }

        [TestMethod]
        public void TestMethod4()
        {
            string _s = "<title>fefeffe</title>";

            var _match = Regex.Match(_s, "(?<=<title>).*(?=</title>)");

            string _v = _match.Value;


            _s = "@a @@b @@@c d";
            _match = _match = Regex.Match(_s, "@(?!@)\\w");
            _v = _match.Value;
        }
    }
}
