using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using Dapper;
using System.Linq;
using Txooo;
using System.Data.Linq.Mapping;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace UnitTestProject1
{
    public class DapperDemo
    {

        public static IEnumerable<CustomerInfo> OneToOne()
        {
            CustomMapping<UserInfo>();
            CustomMapping<CustomerInfo>();
            CustomMapping<PermissionInfo>();

            List<CustomerInfo> userList = new List<CustomerInfo>();

            using (IDbConnection conn = TxDataHelper.GetDataHelper("DapperDemo").DataConnection)
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

        //        public static IEnumerable<UserInfo> OneToMany()
        //        {
        //            List<UserInfo> userList = new List<UserInfo>();

        //            using (IDbConnection connection = TxDataHelper.GetDataHelper("DapperDemo").DataConnection)
        //            {

        //                string sqlCommandText3 = @"SELECT c.UserId,
        //       c.Username      AS UserName,
        //       c.PasswordHash  AS [Password],
        //       c.Email,
        //       c.PhoneNumber,
        //       c.IsFirstTimeLogin,
        //       c.AccessFailedCount,
        //       c.CreationDate,
        //       c.IsActive,
        //       r.RoleId,
        //       r.RoleName
        //FROM   dbo.CICUser c WITH(NOLOCK)
        //       LEFT JOIN CICUserRole cr
        //            ON  cr.UserId = c.UserId
        //       LEFT JOIN CICRole r
        //            ON  r.RoleId = cr.RoleId";

        //                var lookUp = new Dictionary<int, UserInfo>();
        //                userList = connection.Query<UserInfo, PermissionInfo, UserInfo>(sqlCommandText3,
        //                    (user, role) =>
        //                    {
        //                        UserInfo u;
        //                        if (!lookUp.TryGetValue(user.UserId, out u))
        //                        {
        //                            lookUp.Add(user.UserId, u = user);
        //                        }
        //                        u.Role.Add(role);
        //                        return user;
        //                    }, null, null, true, "RoleId", null, null).ToList();
        //                var result = lookUp.Values;
        //            }

        //            return userList;

        //        }

        //        public static void InsertObject()
        //        {
        //            string sqlCommandText = @"INSERT INTO CICUser(Username,PasswordHash,Email,PhoneNumber)VALUES(
        //    @UserName,
        //    @Password,
        //    @Email,
        //    @PhoneNumber
        //)";
        //            using (IDbConnection conn = TxDataHelper.GetDataHelper("DapperDemo").DataConnection)
        //            {
        //                UserInfo user = new UserInfo();
        //                user.UserName = "Dapper";
        //                user.Password = "654321";
        //                user.Email = "Dapper@infosys.com";
        //                user.PhoneNumber = "13795666243";
        //                int result = conn.Execute(sqlCommandText, user);
        //                if (result > 0)
        //                {
        //                    Console.WriteLine("Data have already inserted into DB!");
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Insert Failed!");
        //                }

        //                Console.ReadLine();
        //            }
        //        }

        //        /// <summary>
        //        /// Execute StoredProcedure and map result to POCO
        //        /// </summary>
        //        /// <param name="sqlConnnectionString"></param>
        //        public static void ExecuteStoredProcedure()
        //        {
        //            List<UserInfo> users = new List<UserInfo>();
        //            using (IDbConnection cnn = TxDataHelper.GetDataHelper("DapperDemo").DataConnection)
        //            {
        //                users = cnn.Query<UserInfo>("dbo.p_getUsers",
        //                                        new { UserId = 2 },
        //                                        null,
        //                                        true,
        //                                        null,
        //                                        CommandType.StoredProcedure).ToList();
        //            }
        //            if (users.Count > 0)
        //            {
        //                users.ForEach((user) => Console.WriteLine(user.UserName + "\n"));
        //            }
        //            Console.ReadLine();
        //        }

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
            using (IDbConnection cnn = TxDataHelper.GetDataHelper("DapperDemo").DataConnection)
            {
                cnn.Execute("dbo.p_validateUser", p, null, null, CommandType.StoredProcedure);
                int result = p.Get<int>("@LoginActionType");
                Console.WriteLine(result);
            }

            Console.ReadLine();
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
