/*----------------------------------------------------------------
 *  Copyright (C) 2016 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：TxDataV2
 *  所属项目：
 *  创建用户：张德良
 *  创建时间：2016/11/11 星期五 下午 15:27:26
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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Txooo;

namespace ZDL.AnyCodes
{
    public class TxDataV2<T> //where T:Entity
    {
        public int SqlInsert(string sql, TxDataHelper dbhelper, params object[] sqlParams)
        {

            string _dataBase = "";
            var _hashTable = new Hashtable();

            var _matches = Regex.Matches(sql, "@p\\d", RegexOptions.IgnoreCase);
            for (int i = 0; i < _matches.Count; i++)
            {
                _hashTable[_matches[i].Value] = sqlParams[i];
            }

            var _dbFun = new Func<TxDataHelper, int>(db =>
            {
                db.SpFileValue = _hashTable;
                return db.SqlExecute(sql, db.SpFileValue);
            });

            if (dbhelper == null)
            {
                using (dbhelper = Txooo.TxDataHelper.GetDataHelper(_dataBase))
                {
                    return _dbFun(dbhelper);
                }
            }
            else
            {
                return _dbFun(dbhelper);
            }
        }

        public DataTable SqlSelect(string sql, params object[] sqlParams)
        {

            string _dataBase = "";
            var _hashTable = new Hashtable();

            var _matches = Regex.Matches(sql, "@p\\d", RegexOptions.IgnoreCase);
            for (int i = 0; i < _matches.Count; i++)
            {
                _hashTable[_matches[i].Value] = sqlParams[i];
            }

            using (Txooo.TxDataHelper helper = Txooo.TxDataHelper.GetDataHelper(_dataBase))
            {
                helper.SpFileValue = _hashTable;

                return helper.SqlGetDataTable(sql, helper.SpFileValue);
            }

        }

        public void SqlDelete(string sql, params object[] sqlParams)
        {

        }
    }
}
