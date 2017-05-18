/*----------------------------------------------------------------
 *  Copyright (C) 2015 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：CacheHelper
 *  所属项目：
 *  创建用户：张德良
 *  创建时间：2017/5/18 星期四 上午 9:50:52
 *  
 *  功能描述：
 *          1、
 *          2、 
 * 
 *  修改标识：  
 *  修改描述：
 *  待 完 善：
 *          1、 
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleThreadSync
{
    public class BrandCache
    {
        public static IList<BrandInfo> m_brandList = new List<BrandInfo>();

        public static void ClearCache()
        {
            m_brandList.Clear();
        }
    }

    public class BrandInfo
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public event Action AddEvent;

        public void Add()
        {
            //db insert

            if (AddEvent != null)
                AddEvent();
        }
    }

    public class BrandInfoTest
    {
        public void Test()
        {
            BrandInfo _info = new BrandInfo { BrandId = 12, BrandName = "test" };
            _info.AddEvent += () => { BrandCache.ClearCache(); };
            _info.Add();
        }

    }
}
