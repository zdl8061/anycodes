/*----------------------------------------------------------------
 *  Copyright (C) 2016 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：Class1
 *  所属项目：
 *  创建用户：张德良
 *  创建时间：2016/11/3 星期四 下午 17:06:15
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

namespace ZDL.AnyCodes
{
    public class GuidHelper
    {
        /// <summary>
        /// GUID去掉中划线
        /// </summary>
        /// <returns></returns>
        public static string Get()
        {
            string guid = System.Guid.NewGuid().ToString();

            return guid.Replace("-", "");
        }

        /// <summary>
        /// 根据GUID获取16位的唯一字符串
        /// </summary>
        /// <returns></returns>
        public static string To16String()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                i *= ((int)b + 1);
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        public static long ToLongID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>  
        /// 生成22位唯一的数字 并发可用  
        /// </summary>  
        /// <returns></returns>  
        public static string UniqueID()
        {
            System.Threading.Thread.Sleep(1); //保证yyyyMMddHHmmssffff唯一  
            Random d = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            string strUnique = DateTime.Now.ToString("yyyyMMddHHmmssffff") + d.Next(1000, 9999);
            return strUnique;
        }
    }
}
