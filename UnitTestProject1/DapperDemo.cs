using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using Dapper;
using System.Linq;
using Txooo;

namespace UnitTestProject1
{
    /// <summary>
    /// DapperDemo 的摘要说明
    /// </summary>
    [TestClass]
    public class DapperDemo
    {
        public DapperDemo()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO:  在此处添加测试逻辑
            //
        }

        private static void OneToOne(string sqlConnectionString)
        {
            List<Customer> userList = new List<Customer>();

            using (IDbConnection conn = TxDataHelper.GetDataHelper("TxoooCloud").DataConnection)
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

            if (userList.Count > 0)
            {
                userList.ForEach((item) => Console.WriteLine("UserName:" + item.UserName +
                                                             "----Password:" + item.Password +
                                                             "-----Role:" + item.Role.RoleName +
                                                             "\n"));

                Console.ReadLine();
            }
        }

        private static void OneToMany(string sqlConnectionString)
        {
            Console.WriteLine("One To Many");
            List<User> userList = new List<User>();

            using (IDbConnection connection = TxDataHelper.GetDataHelper("TxoooCloud").DataConnection)
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

        public static void InsertObject(string sqlConnectionString)
        {
            string sqlCommandText = @"INSERT INTO CICUser(Username,PasswordHash,Email,PhoneNumber)VALUES(
    @UserName,
    @Password,
    @Email,
    @PhoneNumber
)";
            using (IDbConnection conn = TxDataHelper.GetDataHelper("TxoooCloud").DataConnection)
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
        public static void ExecuteStoredProcedure(string sqlConnnectionString)
        {
            List<User> users = new List<User>();
            using (IDbConnection cnn = TxDataHelper.GetDataHelper("TxoooCloud").DataConnection)
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
        public static void ExecuteStoredProcedureWithParms(string sqlConnnectionString)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserName", "cooper");
            p.Add("@Password", "123456");
            p.Add("@LoginActionType", null, DbType.Int32, ParameterDirection.ReturnValue);
            using (IDbConnection cnn = TxDataHelper.GetDataHelper("TxoooCloud").DataConnection)
            {
                cnn.Execute("dbo.p_validateUser", p, null, null, CommandType.StoredProcedure);
                int result = p.Get<int>("@LoginActionType");
                Console.WriteLine(result);
            }

            Console.ReadLine();
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
