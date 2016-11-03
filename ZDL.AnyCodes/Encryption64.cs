/*----------------------------------------------------------------
 *  Copyright (C) 2016 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：Class1
 *  所属项目：
 *  创建用户：张德良
 *  创建时间：2016/11/3 星期四 下午 17:02:51
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
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ZDL.AnyCodes
{
    public class Encryption64
    {
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="stringToDecrypt"></param>
        /// <returns></returns>
        public static string Decrypt(string stringToDecrypt)
        {
            byte[] key = { };

            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };

            byte[] inputByteArray = new byte[stringToDecrypt.Length];

            try
            {
                key = Encoding.UTF8.GetBytes("!#$c25?9".Substring(0, 8));

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                inputByteArray = Convert.FromBase64String(stringToDecrypt);

                using (MemoryStream ms = new MemoryStream())
                {
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);

                    cs.Write(inputByteArray, 0, inputByteArray.Length);

                    cs.FlushFinalBlock();

                    Encoding encoding = Encoding.UTF8;

                    return encoding.GetString(ms.ToArray());
                }
            }

            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="stringToEncrypt">字符串</param>    
        /// <returns></returns>
        public static string Encrypt(string stringToEncrypt)
        {

            byte[] key = { };

            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };

            byte[] inputByteArray; //Convert.ToByte(stringToEncrypt.Length)

            try
            {

                key = Encoding.UTF8.GetBytes("!#$c25?9".Substring(0, 8));

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);

                using (MemoryStream ms = new MemoryStream())
                {
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);

                    cs.Write(inputByteArray, 0, inputByteArray.Length);

                    cs.FlushFinalBlock();

                    return Uri.EscapeDataString(Convert.ToBase64String(ms.ToArray()));
                }
            }

            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
    public static class UrlParam
    {
        //http://b.txooo.com/Default.html?s=vz4TBceU0Dg%3D&k=GWe7iZ9aLi8%3D
        //string s = HttpContext.Current.Request.Get<string>("s");
        //int k = HttpContext.Current.Request.Get<int>("k");
        //string es = HttpContext.Current.Request.GetEncrypt<string>("s");
        //int ek = HttpContext.Current.Request.GetEncrypt<int>("k");

        public static T Get<T>(this HttpRequest req, string paramName)
        {
            string _param = req.Params[paramName];

            try
            {
                return (T)Convert.ChangeType(_param, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }

        public static T GetEncrypt<T>(this HttpRequest req, string paramName)
        {
            string _param = req.Params[paramName];

            try
            {
                string _decode = Encryption64.Decrypt(_param);

                return (T)Convert.ChangeType(_decode, typeof(T));
            }
            catch
            {
                //return default(T);  lyy 2014/5/7改的
                return (T)Convert.ChangeType(_param, typeof(T)); ;
            }
        }

        public static string Encrypt(string input, bool isUrlCode = true)
        {
            return isUrlCode ? Uri.EscapeDataString(Encryption64.Encrypt(input)) : Encryption64.Encrypt(input);
        }
    }
}
