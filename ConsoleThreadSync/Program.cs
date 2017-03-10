﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleThreadSync
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Thread(Go).Start();  // .NET 1.0开始就有的
            //Task.Factory.StartNew(Go); // .NET 4.0 引入了 TPL

            Test();           
            Console.ReadKey();
        }

        static async Task Test()
        {
            var _info = Task.Run<string>(() => { return Go(); }); // .NET 4.5 新增了一个Run的方法 (如果加await 下面不会立即执行）
            Console.WriteLine("22232232322323");
            Console.WriteLine(_info.Result);
        }

        static async Task<string>  Go()
        {
            //Thread.Sleep(3000);
            await Task.Delay(4000);
           
            return "asdfasdfasdf";
        }
    }
}
