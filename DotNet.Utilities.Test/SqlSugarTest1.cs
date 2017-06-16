using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlSugar;
using Txooo;

namespace DotNet.Utilities.Test
{
    [TestClass]
    public class SqlSugarTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var db = GetInstance("DapperDemo"))
            {
                var _userList = db.Ado.SqlQuery<UserInfo>("select * from users");
            }
        }

        public SqlSugarClient GetInstance(string dbName)
        {
            var _connStr = TxDataHelper.GetDataHelper(dbName).DataConnection.ConnectionString;
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = _connStr, DbType = DbType.SqlServer });
            
            return db;
        }
    }

    public class UserInfo
    {
        [SugarColumn(ColumnName ="user_id")]
        public int UserId { get; set; }

        [SugarColumn(ColumnName = "user_name")]
        public string Username { get; set; }

        [SugarColumn(ColumnName = "user_pwd")]
        public string Userpwd { get; set; }

        [SugarColumn(ColumnName = "reg_time")]
        public DateTime Regtime { get; set; }
    }
}
