using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
//using Txooo;
using System.Data.Linq.Mapping;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Data.SqlClient;

namespace DotNet.Utilities
{
    public class DapperDemo
    {
        static DapperDemo()
        {
            CustomMapping<UserInfo>();
            CustomMapping<CustomerInfo>();
            CustomMapping<PermissionInfo>();
        }

        /// <summary>
        /// 只适用1对1，对多时会重复数据
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<CustomerInfo> OneToOne()
        {

            List<CustomerInfo> userList = new List<CustomerInfo>();

            //TxDataHelper.GetDataHelper("DapperDemo").DataConnection

            using (IDbConnection conn = new SqlConnection())
            {
                string sqlCommandText = @"SELECT A.*,C.*
                                          FROM [dbo].[Users] A
                                          INNER JOIN [dbo].[UserPermissions] B
                                          ON A.user_id=B.userid
                                          INNER JOIN [dbo].[Permissions] C
                                          ON B.perm_id=C.perm_id";


                userList = conn.Query<CustomerInfo, PermissionInfo, CustomerInfo>(sqlCommandText,
                                                                (user, perm) => { user.Permission = perm; return user; },
                                                                null,
                                                                null,
                                                                true,
                                                                "perm_id",
                                                                null,
                                                                null).ToList();
            }
            return userList;
        }

        public static void CustomMapping<T>()
        {
            var map = new CustomPropertyTypeMap(typeof(T),
                (type, columnName) => type.GetProperties().Where(prop => prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name.ToLower() == columnName.ToLower())).FirstOrDefault());
            SqlMapper.SetTypeMap(typeof(T), map);
        }

        /// <summary>
        /// 一对多
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<UserInfo> OneToMany()
        {
            List<UserInfo> userList = new List<UserInfo>();

            using (IDbConnection connection = new SqlConnection())
            {

                string sqlCommandText3 = @"SELECT A.*,C.*
                                          FROM [dbo].[Users] A
                                          INNER JOIN [dbo].[UserPermissions] B
                                          ON A.user_id=B.userid
                                          INNER JOIN [dbo].[Permissions] C
                                          ON B.perm_id=C.perm_id";

                var lookUp = new Dictionary<int, UserInfo>();
                userList = connection.Query<UserInfo, PermissionInfo, UserInfo>(sqlCommandText3,
                    (user, perm) =>
                    {
                        UserInfo u;
                        if (!lookUp.TryGetValue(user.UserId, out u))
                        {
                            lookUp.Add(user.UserId, u = user);
                        }
                        u.Perms.Add(perm);
                        return user;
                    }, null, null, true, "perm_id", null, null).ToList();
                return lookUp.Values;
            }
        }

        /// <summary>
        /// 插入数据参数名必须和属性名相同
        /// </summary>
        public static int InsertObject()
        {
            string sqlCommandText = @"INSERT INTO [dbo].[Users] ([user_name] ,[user_pwd] ,[reg_time]) VALUES (@username ,@userpwd ,@regtime)";

            using (IDbConnection conn = new SqlConnection())
            {
                UserInfo user = new UserInfo();
                user.Username = "Dapper";
                user.Userpwd = "654321";
                user.Regtime = DateTime.Now;

                int result = conn.Execute(sqlCommandText, user);
                return result;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="sqlConnnectionString"></param>
        public static IEnumerable<UserInfo> ExecuteStoredProcedure()
        {
            List<UserInfo> users = new List<UserInfo>();
            using (IDbConnection cnn = new SqlConnection())
            {
                users = cnn.Query<UserInfo>("dbo.p_getUsers",
                                        new { UserId = 2 },
                                        null,
                                        true,
                                        null,
                                        CommandType.StoredProcedure).ToList();
            }
            return users;
        }

        /// <summary>
        /// Execute StroedProcedure and get result from return value
        /// </summary>
        /// <param name="sqlConnnectionString"></param>
        public static void ExecuteStoredProcedureWithParms()
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserName", "cooper");
            p.Add("@Password", "123456");
            p.Add("@LoginActionType", null, DbType.Int32, ParameterDirection.ReturnValue);
            using (IDbConnection cnn = new SqlConnection())
            {
                cnn.Execute("dbo.p_validateUser", p, null, null, CommandType.StoredProcedure);
                int result = p.Get<int>("@LoginActionType");
                Console.WriteLine(result);
            }

            Console.ReadLine();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="name"></param>
        /// <param name="loginName"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PageDataView<UserInfo> GetPager(string name, string loginName, int page, int pageSize = 10)
        {
            PageCriteria criteria = new PageCriteria();
            criteria.Condition = "1=1";
            if (!string.IsNullOrEmpty(name))
                criteria.Condition += string.Format(" and Name like '%{0}%'", name);
            if (!string.IsNullOrEmpty(loginName))
                criteria.Condition += string.Format(" and LoginName like '%{0}%'", loginName);
            criteria.CurrentPage = page;
            criteria.Fields = "*";
            criteria.PageSize = pageSize;
            criteria.TableName = "[Users] A";
            criteria.PrimaryKey = "user_id";
            var r = Common.GetPageData<UserInfo>("DapperDemo", criteria);
            return r;
        }

        public void TraceMessage(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Trace.WriteLine("message: " + message);
            Trace.WriteLine("member name: " + memberName);
            Trace.WriteLine("source file path: " + sourceFilePath);
            Trace.WriteLine("source line number: " + sourceLineNumber);
        }
    }

    public class UserInfo
    {
        public UserInfo()
        {
            Perms = new List<PermissionInfo>();
        }

        /// <summary> 
        ///   
        /// </summary> 
        [Column(Name = "user_id")]
        public int UserId { get; set; }

        /// <summary> 
        ///   
        /// </summary> 
        [Column(Name = "user_name")]
        public string Username { get; set; }

        /// <summary> 
        ///   
        /// </summary> 
        [Column(Name = "user_pwd")]
        public string Userpwd { get; set; }

        /// <summary> 
        ///   
        /// </summary> 
        [Column(Name = "reg_time")]
        public DateTime Regtime { get; set; }


        public List<PermissionInfo> Perms { get; set; }
    }

    public class PermissionInfo
    {
        /// <summary> 
        ///   
        /// </summary> 
        [Column(Name = "perm_id")]
        public int PermId { get; set; }

        /// <summary> 
        ///   
        /// </summary> 
        [Column(Name = "perm_name")]
        public string PermName { get; set; }

        /// <summary> 
        ///   
        /// </summary> 
        [Column(Name = "perm_url")]
        public string PermUrl { get; set; }

        /// <summary> 
        ///   
        /// </summary> 
        [Column(Name = "add_time")]
        public DateTime AddTime { get; set; }
    }
    public class CustomerInfo
    {
        /// <summary> 
        ///   
        /// </summary> 
        [Column(Name = "user_id")]
        public int UserId { get; set; }

        /// <summary> 
        ///   
        /// </summary> 
        [Column(Name = "user_name")]
        public string Username { get; set; }

        /// <summary> 
        ///   
        /// </summary> 
        [Column(Name = "user_pwd")]
        public string Userpwd { get; set; }

        /// <summary> 
        ///   
        /// </summary> 
        [Column(Name = "reg_time")]
        public DateTime Regtime { get; set; }

        public PermissionInfo Permission { get; set; }
    }
}
