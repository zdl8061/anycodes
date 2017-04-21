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

        public static IEnumerable<Customer> OneToOne()
        {
            List<Customer> userList = new List<Customer>();

            using (IDbConnection conn = TxDataHelper.GetDataHelper("DapperDemo").DataConnection)
            {
                string sqlCommandText = @"SELECT c.UserId,c.Username AS UserName,
c.PasswordHash AS [Password],c.Email,c.PhoneNumber,c.IsFirstTimeLogin,c.AccessFailedCount,
c.CreationDate,c.IsActive,r.RoleId,r.RoleName 
    FROM dbo.CICUser c WITH(NOLOCK) 
INNER JOIN CICUserRole cr ON cr.UserId = c.UserId 
INNER JOIN CICRole r ON r.RoleId = cr.RoleId";




                userList = conn.Query<Customer, Role, Customer>(sqlCommandText,
                                                                (user, role) => { user.Role = role; return user; },
                                                                null,
                                                                null,
                                                                true,
                                                                "RoleId",
                                                                null,
                                                                null).ToList();
            }
            return userList;
        }

        public static void CustomMapping<T>()
        {
            var map = new CustomPropertyTypeMap(typeof(T),
                (type, columnName) => type.GetProperties().Where(prop => prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName)).FirstOrDefault());
            SqlMapper.SetTypeMap(typeof(T), map);
        }

        public static void OneToMany()
        {
            Console.WriteLine("One To Many");
            List<User> userList = new List<User>();

            using (IDbConnection connection = TxDataHelper.GetDataHelper("DapperDemo").DataConnection)
            {

                string sqlCommandText3 = @"SELECT c.UserId,
       c.Username      AS UserName,
       c.PasswordHash  AS [Password],
       c.Email,
       c.PhoneNumber,
       c.IsFirstTimeLogin,
       c.AccessFailedCount,
       c.CreationDate,
       c.IsActive,
       r.RoleId,
       r.RoleName
FROM   dbo.CICUser c WITH(NOLOCK)
       LEFT JOIN CICUserRole cr
            ON  cr.UserId = c.UserId
       LEFT JOIN CICRole r
            ON  r.RoleId = cr.RoleId";

                var lookUp = new Dictionary<int, User>();
                userList = connection.Query<User, Role, User>(sqlCommandText3,
                    (user, role) =>
                    {
                        User u;
                        if (!lookUp.TryGetValue(user.UserId, out u))
                        {
                            lookUp.Add(user.UserId, u = user);
                        }
                        u.Role.Add(role);
                        return user;
                    }, null, null, true, "RoleId", null, null).ToList();
                var result = lookUp.Values;
            }

            if (userList.Count > 0)
            {
                userList.ForEach((item) => Console.WriteLine("UserName:" + item.UserName +
                                             "----Password:" + item.Password +
                                             "-----Role:" + item.Role.First().RoleName +
                                             "\n"));

                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No Data In UserList!");
            }
        }

        public static void InsertObject()
        {
            string sqlCommandText = @"INSERT INTO CICUser(Username,PasswordHash,Email,PhoneNumber)VALUES(
    @UserName,
    @Password,
    @Email,
    @PhoneNumber
)";
            using (IDbConnection conn = TxDataHelper.GetDataHelper("DapperDemo").DataConnection)
            {
                User user = new User();
                user.UserName = "Dapper";
                user.Password = "654321";
                user.Email = "Dapper@infosys.com";
                user.PhoneNumber = "13795666243";
                int result = conn.Execute(sqlCommandText, user);
                if (result > 0)
                {
                    Console.WriteLine("Data have already inserted into DB!");
                }
                else
                {
                    Console.WriteLine("Insert Failed!");
                }

                Console.ReadLine();
            }
        }

        /// <summary>
        /// Execute StoredProcedure and map result to POCO
        /// </summary>
        /// <param name="sqlConnnectionString"></param>
        public static void ExecuteStoredProcedure()
        {
            List<User> users = new List<User>();
            using (IDbConnection cnn = TxDataHelper.GetDataHelper("DapperDemo").DataConnection)
            {
                users = cnn.Query<User>("dbo.p_getUsers",
                                        new { UserId = 2 },
                                        null,
                                        true,
                                        null,
                                        CommandType.StoredProcedure).ToList();
            }
            if (users.Count > 0)
            {
                users.ForEach((user) => Console.WriteLine(user.UserName + "\n"));
            }
            Console.ReadLine();
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

    public class User
    {
        public User()
        {
            Role = new List<Role>();
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsFirstTimeLogin { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public List<Role> Role { get; set; }
    }
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
    public class Customer
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsFirstTimeLogin { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public Role Role { get; set; }
    }
}
