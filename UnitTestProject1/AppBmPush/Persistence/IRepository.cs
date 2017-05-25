/*----------------------------------------------------------------
 *  Copyright (C) 2015 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：Class1
 *  所属项目：
 *  创建用户：张德良
 *  创建时间：2017/5/25 星期四 上午 11:12:33
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

namespace UnitTestProject1.AppBmPush.Persistence
{
    public interface IRepository<T>
    {
        T FindBy(string primaryKey);
        IEnumerable<T> FindAll(string where);
        IEnumerable<T> FindAll(string where, string order);
        IEnumerable<T> FindAll(int pageIndex, int pageSize, string where, string order, out int count);
        void Add(T entity);
        void Delete(string where);
        void Update(string where);
        bool Exists(string where);

        #region 原始数据操作接口
        int ExecuteSql(string sql, params System.Data.IDataParameter[] ps);

        object ExecuteScalar(string sql, params System.Data.IDataParameter[] ps);

        System.Data.DataTable ExecuteDataTable(string sql, params System.Data.IDataParameter[] ps);

        #endregion
    }
}
