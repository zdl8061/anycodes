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
    public class TxDataV2
    {
        public static int SqlInsert(string dataBase, string sql, params object[] sqlParams)
        {
            try
            {
                var _hashTable = new Hashtable();

                var _matches = Regex.Matches(sql, "@p\\d", RegexOptions.IgnoreCase);
                for (int i = 0; i < _matches.Count; i++)
                {
                    _hashTable[_matches[i].Value] = sqlParams[i];
                }

                using (Txooo.TxDataHelper helper = Txooo.TxDataHelper.GetDataHelper(dataBase))
                {
                    helper.SpFileValue = _hashTable;

                    return helper.SqlExecute(sql, helper.SpFileValue);
                }
            }
            catch (Exception ex)
            {
                TxLogHelper.GetLogger("TxDataV2").Error(ex.Source + "|" + ex.TargetSite + "|" + ex.Message);

                return 0;
            }
        }

        public static DataTable SqlSelect(string dataBase, string sql, params object[] sqlParams)
        {
            try
            {
                var _hashTable = new Hashtable();

                var _matches = Regex.Matches(sql, "@p\\d", RegexOptions.IgnoreCase);
                for (int i = 0; i < _matches.Count; i++)
                {
                    _hashTable[_matches[i].Value] = sqlParams[i];
                }

                using (Txooo.TxDataHelper helper = Txooo.TxDataHelper.GetDataHelper(dataBase))
                {
                    helper.SpFileValue = _hashTable;

                    return helper.SqlGetDataTable(sql, helper.SpFileValue);
                }
            }
            catch (Exception ex)
            {
                TxLogHelper.GetLogger("TxDataV2").Error(ex.Source + "|" + ex.TargetSite + "|" + ex.Message);


            }
            return null;
        }
    }
}
