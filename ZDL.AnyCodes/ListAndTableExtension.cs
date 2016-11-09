/*----------------------------------------------------------------
 *  Copyright (C) 2016 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：Class1
 *  所属项目：
 *  创建用户：张德良
 *  创建时间：2016/11/5 星期六 上午 10:45:28
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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using Match = System.Text.RegularExpressions.Match;

using Microsoft.CSharp;
using Newtonsoft.Json;

namespace ZDL.AnyCodes
{
    public static class ListAndTableExtension
    {    
        /// <summary>
        /// 匿名类的转换方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<T> FromTable<T>(DataTable dataTable,T anonymous)
        {
            List<T> list = new List<T>();
            if (dataTable == null || dataTable.Rows.Count == 0)
                return list;
            //取当前匿名类的构造函数
            var constructor = typeof(T).GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                           .OrderBy(c => c.GetParameters().Length).First();
            //取当前构造函数的参数
            var parameters = constructor.GetParameters();
            var values = new object[parameters.Length];
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = 0;
                foreach (ParameterInfo item in parameters)
                {
                    object itemValue = null;
                    if (dataTable.Columns.Contains(item.Name))
                    {
                        itemValue = Convert.ChangeType(dr[item.Name], item.ParameterType);
                    }
                    values[index++] = itemValue;
                }
                T entity = (T)constructor.Invoke(values);
                list.Add(entity);
            }
            return list;
        }

        /// <summary>
        /// json字符串转匿名类型对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static T FromJsonObject<T>(string json, T anonymous)
        {
            var _obj = JsonConvert.DeserializeAnonymousType<T>(json, anonymous);

            return _obj;
           
        }

        /// <summary>
        /// json字符串转匿名列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<T> FromJsonArray<T>(string json, T anonymous)
        {          
            var list = JsonConvert.DeserializeObject<List<T>>(json);

            return list;
        }
    }
}