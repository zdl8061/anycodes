using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DotNet.Utilities.Test
{
    [TestClass]
    public class ThreadTaskTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //new Thread(Go).Start();  // .NET 1.0开始就有的
            //Task.Factory.StartNew(Go); // .NET 4.0 引入了 TPL

            Test();
        }

        static async Task Test()
        {
            var _info = Task.Run<string>(() => { return Go(); }); // .NET 4.5 新增了一个Run的方法 (如果加await 下面不会立即执行）
            Console.WriteLine("22232232322323");
            Console.WriteLine(_info.Result);
        }

        static async Task<string> Go()
        {
            //Thread.Sleep(3000);
            await Task.Delay(4000);

            return "asdfasdfasdf";
        }
    }
}
