/*----------------------------------------------------------------
 *  Copyright (C) 2015 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：ClueRepository
 *  所属项目：
 *  创建用户：张德良
 *  创建时间：2017/5/25 星期四 上午 11:19:14
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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.AppBmPush.Persistence
{
    public class ClueRepository : IRepository<ClueInfo>
    {
        public void Add(ClueInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string where)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTable(string sql, params IDataParameter[] ps)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar(string sql, params IDataParameter[] ps)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSql(string sql, params IDataParameter[] ps)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClueInfo> FindAll(string where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClueInfo> FindAll(string where, string order)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClueInfo> FindAll(int pageIndex, int pageSize, string where, string order, out int count)
        {
            throw new NotImplementedException();
        }

        public ClueInfo FindBy(string primaryKey)
        {
            throw new NotImplementedException();
        }

        public void Update(string where)
        {
            throw new NotImplementedException();
        }
    }
}
