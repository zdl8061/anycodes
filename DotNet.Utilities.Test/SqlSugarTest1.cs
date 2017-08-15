using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlSugar;
using Txooo;
using System.Collections.Generic;
using Newtonsoft.Json;

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
                //var _userList = db.Ado.SqlQuery<UserInfo>("SELECT * FROM Users");//字段名和表明必须和库一样，映射没用
                var _userList = db.Queryable<UserInfo>().ToList();

                JsonSerializerSettings jsetting = new JsonSerializerSettings();
                jsetting.ContractResolver = new LimitPropsContractResolver(new string[] { "UserId", "Username" , "Regtime", "MyPermission" , "PermissionId", "PermissionName" });
                var _json = JsonConvert.SerializeObject(_userList, jsetting);
            }
        }

        public SqlSugarClient GetInstance(string dbName)
        {
            var _connStr = TxDataHelper.GetDataHelper(dbName).DataConnection.ConnectionString;
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = _connStr, DbType = DbType.SqlServer });
            
                return db;
        }
    }

    [SugarTable("Users")]
    public class UserInfo : ModelContext
    {

        [SugarColumn(ColumnName = "user_id")]
        public int UserId { get; set; }

        [SugarColumn(ColumnName = "user_name")]
        public string Username { get; set; }

        [SugarColumn(ColumnName = "user_pwd")]
        public string Userpwd { get; set; }

        [SugarColumn(ColumnName = "reg_time")]
        public DateTime Regtime { get; set; }


        [SugarColumn(IsIgnore = true)]
        public List<UserPermission> MyPermission
        {
            get
            {
                return  base.CreateMapping<UserPermission>().Where(p => p.UserId == this.UserId).ToList();
            }
        }
    }

    [SugarTable("UserPermissions")]
    public class UserPermission: ModelContext
    {
        [SugarColumn(ColumnName = "userid")]
        public int UserId { get; set; }

        [SugarColumn(ColumnName = "perm_id")]
        public int PermissionId { get; set; }


        [SugarColumn(IsIgnore = true)]
        public string PermissionName
        {
            get {
                var perm = base.CreateMapping<Permission>().First(p => p.PermissionId == this.PermissionId);

                return perm.PermissionName;
            }
        }
    }

    [SugarTable("Permissions")]
    public class Permission : ModelContext
    {
        [SugarColumn(ColumnName = "perm_id")]
        public int PermissionId { get; set; }

        [SugarColumn(ColumnName = "perm_name")]
        public string PermissionName { get; set; }

        [SugarColumn(ColumnName = "perm_url")]
        public string PermissionUrl { get; set; }
    }
}
