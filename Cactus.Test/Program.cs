using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cactus.SQLiteService;
using Cactus.MSSQLService;
using Cactus.MySQLService;
using Cactus.Model;
using Cactus.Common;
using System.IO;
using System.Diagnostics;


namespace Cactus.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //if (File.Exists(@"G:\workspace\Cactus\Cactus.Test\key\keyword01.txt") == false)
            //{
            //    File.Create(@"G:\workspace\Cactus\Cactus.Test\key\keyword01.txt");
            //}
            //Dictionary<string, string> keys = new Dictionary<string, string>();
            //System.IO.StreamWriter writer = new System.IO.StreamWriter(@"G:\workspace\Cactus\Cactus.Test\key\keyword01.txt");
            //using (System.IO.StreamReader reader = new System.IO.StreamReader(@"G:\workspace\Cactus\Cactus.Test\key\keyword.txt", Encoding.UTF8))
            //{
            //    while (reader.EndOfStream == false)
            //    {
            //        string key = reader.ReadLine();
            //        string value="";
            //        keys.TryGetValue(key, out value);
            //        if (string.IsNullOrEmpty(value))
            //        {
            //            keys.Add(key, key);
            //            writer.WriteLine(key);
            //        }
            //    }
            //}
            string s = "/{2}/{1}/{0}.cshtml";

            Console.WriteLine(string.Format(s, "1", "2", "3", "4"));
            Console.WriteLine(string.Format(s, "1", "2", "3",string.Empty));
            Console.WriteLine(string.Format(s, "1", "2", string.Empty, string.Empty));
            Console.WriteLine(string.Format(s, "1", string.Empty, string.Empty, string.Empty));
            //using (System.IO.StreamReader reader = new System.IO.StreamReader(@"G:\workspace\Cactus\Cactus.Test\key\30万中文分词词库.txt", Encoding.UTF8))
            //{
            //    while (reader.EndOfStream == false)
            //    {
            //        string value = reader.ReadLine();
            //        int i = value.IndexOf('\t');
            //        string t1 = value.Substring(i + 1, value.Length - i - 1);
            //        int j = t1.IndexOf('\t');
            //        string t2 = t1.Substring(0, j);
            //        writer.WriteLine(t2);
            //    }
            //}
            //Console.WriteLine("2");
            //using (System.IO.StreamReader reader = new System.IO.StreamReader(@"G:\workspace\Cactus\Cactus.Test\key\百度分词词库.txt", Encoding.UTF8))
            //{
            //    while (reader.EndOfStream == false)
            //    {
            //        string value = reader.ReadLine();
            //        int i = value.IndexOf(' ');
            //        string t1 = value.Substring(i + 1, value.Length - i - 1);
            //        writer.WriteLine(t1);
            //    }
            //}
            //Console.WriteLine("3");
            //using (System.IO.StreamReader reader = new System.IO.StreamReader(@"G:\workspace\Cactus\Cactus.Test\key\四十万可用搜狗txt词库.txt", Encoding.UTF8))
            //{
            //    while (reader.EndOfStream == false)
            //    {
            //        string value = reader.ReadLine();
            //        int i = value.IndexOf(' ');
            //        string t1 = value.Substring(0,  i + 1);
            //        writer.WriteLine(t1);
            //    }
            //}
            //Console.WriteLine("4");
            //using (System.IO.StreamReader reader = new System.IO.StreamReader(@"G:\workspace\Cactus\Cactus.Test\key\dict.txt", Encoding.UTF8))
            //{
            //    while (reader.EndOfStream == false)
            //    {
            //        writer.WriteLine(reader.ReadLine());
            //    }
            //}
            //Console.WriteLine("5");
            //using (System.IO.StreamReader reader = new System.IO.StreamReader(@"G:\workspace\Cactus\Cactus.Test\key\fingerDic.txt", Encoding.UTF8))
            //{
            //    while (reader.EndOfStream == false)
            //    {
            //        writer.WriteLine(reader.ReadLine());
            //    }
            //}
            //Console.WriteLine("6");
            //using (System.IO.StreamReader reader = new System.IO.StreamReader(@"G:\workspace\Cactus\Cactus.Test\key\httpcws_dict.txt", Encoding.UTF8))
            //{
            //    while (reader.EndOfStream == false)
            //    {
            //        writer.WriteLine(reader.ReadLine());
            //    }
            //}
            //Console.WriteLine("7");
            //using (System.IO.StreamReader reader = new System.IO.StreamReader(@"G:\workspace\Cactus\Cactus.Test\key\out.txt", Encoding.UTF8))
            //{
            //    while (reader.EndOfStream == false)
            //    {
            //        string value = reader.ReadLine();
            //        writer.WriteLine(value);
            //    }
            //}
            //Console.WriteLine("8");
            //using (System.IO.StreamReader reader = new System.IO.StreamReader(@"G:\workspace\Cactus\Cactus.Test\key\四十万汉语大词库.txt", Encoding.UTF8))
            //{
            //    while (reader.EndOfStream == false)
            //    {
            //        writer.WriteLine(reader.ReadLine());
            //    }
            //}
            //Console.WriteLine("9");
            //using (System.IO.StreamReader reader = new System.IO.StreamReader(@"G:\workspace\Cactus\Cactus.Test\key\五笔词库.txt", Encoding.UTF8))
            //{
            //    while (reader.EndOfStream == false)
            //    {
            //        writer.WriteLine(reader.ReadLine());
            //    }
            //}
            //Console.WriteLine("10");
            //writer.Close();
            //writer.Dispose();

            //sqlite
           // sqlite_Test();
            //mysql
            //mysql_Test();
            //mssql
            //mssql_Test();
            //Console.WriteLine(HIO.RemoveHead("1111122222", "1"));
            //Console.WriteLine(HIO.PathParse(@"//////////Documents/Tencent Files/702295399/FileRecv", 2, true));
            //Console.WriteLine(HIO.PathParse(@"//////////Documents/Tencent Files/702295399/FileRecv", 1, false));
            //Console.WriteLine(HIO.PathParse(@"\\\\\\\\\\\Documents\Tencent Files\702295399\FileRecv", 2, true));
            //Console.WriteLine(HIO.PathParse(@"\\\\\\\\\\\Documents\Tencent Files\702295399\FileRecv", 1,false));
            //string s=Path.GetFullPath("//////////Documents/Tencent Files/702295399/FileRecv/test.txt");
            //string ss = "111122222";
            //string dd = ss.Remove(0, 4);
            //bool b1 = System.IO.File.Exists(@"D:\vs2010Workspace\Cactus\Cactus.Web");
            //bool b2 = System.IO.File.Exists(@"D:\vs2010Workspace\Cactus\Cactus.Web\Web.config");
            //bool b3 = System.IO.File.Exists(@"D:\vs2010Workspace\Cactus\Cactus.Web\favicon");
            //bool b4 = System.IO.File.Exists(@"D:\vs2010Workspace\Cactus\Cactus.Web\favicon123");
            //bool b5 = System.IO.Directory.Exists(@"D:\vs2010Workspace\Cactus\Cactus.Web");
            //bool b6 = System.IO.Directory.Exists(@"D:\vs2010Workspace\Cactus\Cactus.Web\");
            //bool b7 = System.IO.Directory.Exists(@"D:\vs2010Workspace\Cactus\Cactus.Web\favicon");

            //_s.Reset();
            //_s.Start();
            //for (int i = 0; i < 10000000; i++)
            //{
            //    StringHelper.GetTimeStamp_old2();
            //}
            //_s.Stop();
            //Console.WriteLine(_s.ElapsedMilliseconds);
            ////
            Console.ReadKey();
        }
        #region sqlite
        static Cactus.SQLiteService.UserService sqlite_user = new SQLiteService.UserService();
        static Cactus.SQLiteService.RoleService sqlite_role = new SQLiteService.RoleService();
        //static Cactus.SQLiteService.PowerConfigService sqlite_power = new SQLiteService.PowerConfigService();
        static void sqlite_Test() {
            //sqlite_ActionGroupTest();
            //sqlite_ActionTest();
            //sqlite_RoleTest();
            ////sqlite_UserTest();
            //Model.Sys.PowerConfig p=new Model.Sys.PowerConfig();
            //p.PowerGroupList = new List<Model.Sys.PowerGroup>();
            //List<Model.Sys.Power> pl = new List<Model.Sys.Power>();
            //pl.Add(new Model.Sys.Power() { Name = "角色管理", ParamStr = "/Admin/Sys/RoleList", IsShow = true, GroupId = 101, NoId = "1001" });
            //pl.Add(new Model.Sys.Power() { Name = "添加角色", ParamStr = "", IsShow = false, GroupId = 101, NoId = "1002" });
            //pl.Add(new Model.Sys.Power() { Name = "更新角色", ParamStr = "", IsShow = false, GroupId = 101, NoId = "1003" });
            //pl.Add(new Model.Sys.Power() { Name = "删除角色", ParamStr = "", IsShow = false, GroupId = 101, NoId = "1004" });
            //pl.Add(new Model.Sys.Power() { Name = "权限分配", ParamStr = "", IsShow = true, GroupId = 101, NoId = "1005" });
            //p.PowerGroupList.Add(new Model.Sys.PowerGroup() { GroupName = "网站设置", Id = 101, PowerList = pl,IsShow=true });
            //p.PowerGroupList.Add(new Model.Sys.PowerGroup() { GroupName = "博客管理", Id = 100, PowerList = pl, IsShow = true });

            //sqlite_power.SaveConfig(p, @"D:\vs2010Workspace\Cactus\PowerConfig.config");
            Console.WriteLine("1111");
            Console.ReadKey();
        }
        static void sqlite_RoleTest()
        {
            try
            {
                Console.WriteLine("RoleTest Test");
                sqlite_role.Insert(new Model.Sys.Role()
                { 
                    RoleName="RoleTest1", ActionIds=""
                });
                sqlite_role.Insert(new Model.Sys.Role()
                {
                    RoleName = "RoleTest2",
                    ActionIds = ""
                });
                sqlite_role.Delete("2");
                sqlite_role.Update(new Model.Sys.Role()
                {
                    RoleName = "RoleTest2222",
                    ActionIds = "1,2,3",
                    Role_Id=1
                });
                Model.Sys.Role re = sqlite_role.Find(1);
                Console.WriteLine("Role_Id:" + re.Role_Id);
                Console.WriteLine("RoleName:" + re.RoleName);
                Console.WriteLine("ActionIds:" + re.ActionIds);
                Console.WriteLine("RoleTest Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("RoleTest Error");
                ConsoleError(ex);
            }
        }
        static void sqlite_UserTest()
        {
            
            try
            {
                Console.WriteLine("UserTest Test");
                //sqlite_user.Insert(new Model.Sys.User()
                //{
                //    AddTime = DateTime.Now,
                //    Avatar = "/image.png",
                //    Email = "702295399@qq.com",
                //    IsSuperUser = true,
                //    LastLoginIp = "127.0.0.1",
                //    LastLoginTime = DateTime.Now,
                //    Name = "702295399@qq.com",
                //    NickName = "漫漫洒洒2",
                //    Password = CryptoHelper.MD5Encrypt("123456789"),
                //    Phone = "138888888888",
                //    Qq = "702295399",
                //    RoleId = 1
                //});
                //sqlite_user.Delete("2");
                sqlite_user.Update(new Model.Sys.User()
                {
                    AddTime = DateTime.Now,
                    Avatar = "/image.png",
                    Email = "702295399@qq.com",
                    IsSuperUser = false,
                    LastLoginIp = "127.0.0.1",
                    LastLoginTime = DateTime.Now,
                    Name = "702295399@qq.com",
                    NickName = "漫漫洒洒1111",
                    Password = CryptoHelper.MD5Encrypt("123456789"),
                    Phone = "138888888888",
                    Qq = "702295399",
                    RoleId = 1,
                    User_Id = 1
                });

                //Model.Sys.User ur = sqlite_user.Find(1);
                //int count=0;
                
                //List<Model.Sys.User> list=sqlite_user.ToPagedList(10, 20, "User_Id desc", out count);
                //foreach (var ur in list) {
                //    Console.WriteLine("User_Id:" + ur.User_Id 
                //        + "\t" + "Avatar:" + ur.Avatar
                //        + "\t" + "Email:" + ur.Email
                //        + "\t" + "IsSuperUser:" + ur.IsSuperUser
                //        + "\t" + "LastLoginIp:" + ur.LastLoginIp
                //        + "\t" + "LastLoginTime:" + ur.LastLoginTime
                //        + "\t" + "Name:" + ur.Name
                //        + "\t" + "NickName:" + ur.NickName
                //        + "\t" + "Password:" + ur.Password
                //        + "\t" + "Phone:" + ur.Phone
                //        + "\t" + "Qq:" + ur.Qq
                //        + "\t" + "Role_Id:" + ur.Role.Role_Id
                //        + "\t" + "RoleName:" + ur.Role.RoleName);
                //}

                Console.WriteLine("UserTest Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserTest Error");
                ConsoleError(ex);
            }
        }

        #endregion

        #region mysql
        static Cactus.MySQLService.UserService mysql_user = new MySQLService.UserService();
        static Cactus.MySQLService.RoleService mysql_role = new MySQLService.RoleService();
        static void mysql_Test()
        {
            //mysql_ActionGroupTest();
            //mysql_ActionTest();
            //mysql_RoleTest();
            mysql_UserTest();
            Console.ReadKey();
        }
        static void mysql_RoleTest()
        {
            try
            {
                Console.WriteLine("mysql_RoleTest Test");
                mysql_role.Insert(new Model.Sys.Role()
                {
                    RoleName = "RoleTest1",
                    ActionIds = ""
                });
                mysql_role.Insert(new Model.Sys.Role()
                {
                    RoleName = "RoleTest2",
                    ActionIds = ""
                });
                mysql_role.Delete("2");
                mysql_role.Update(new Model.Sys.Role()
                {
                    RoleName = "RoleTest2222",
                    ActionIds = "1,2,3",
                    Role_Id = 1
                });
                Model.Sys.Role re = mysql_role.Find(1);
                Console.WriteLine("Role_Id:" + re.Role_Id);
                Console.WriteLine("RoleName:" + re.RoleName);
                Console.WriteLine("ActionIds:" + re.ActionIds);
                Console.WriteLine("mysql_RoleTest Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("mysql_RoleTest Error");
                ConsoleError(ex);
            }
        }
        static void mysql_UserTest()
        {

            try
            {
                Console.WriteLine("mysql_UserTest Test");
                mysql_user.Insert(new Model.Sys.User()
                {
                    AddTime = DateTime.Now,
                    Avatar = "/image.png",
                    Email = "702295399@qq.com",
                    IsSuperUser = true,
                    LastLoginIp = "127.0.0.1",
                    LastLoginTime = DateTime.Now,
                    Name = "702295399@qq.com",
                    NickName = "漫漫洒洒",
                    Password = "123456789",
                    Phone = "138888888888",
                    Qq = "702295399",
                    RoleId = 1
                });
                mysql_user.Insert(new Model.Sys.User()
                {
                    AddTime = DateTime.Now,
                    Avatar = "/image.png",
                    Email = "702295399@qq.com",
                    IsSuperUser = true,
                    LastLoginIp = "127.0.0.1",
                    LastLoginTime = DateTime.Now,
                    Name = "702295399@qq.com",
                    NickName = "漫漫洒洒2",
                    Password = "123456789",
                    Phone = "138888888888",
                    Qq = "702295399",
                    RoleId = 1
                });
                mysql_user.Delete("2");
                mysql_user.Update(new Model.Sys.User()
                {
                    AddTime = DateTime.Now,
                    Avatar = "/image.png",
                    Email = "702295399@qq.com",
                    IsSuperUser = false,
                    LastLoginIp = "127.0.0.1",
                    LastLoginTime = DateTime.Now,
                    Name = "702295399@qq.com",
                    NickName = "漫漫洒洒1111",
                    Password = "123456789",
                    Phone = "138888888888",
                    Qq = "702295399",
                    RoleId = 1,
                    User_Id = 1
                });

                Model.Sys.User ur = mysql_user.Find(1);
                Console.WriteLine("User_Id:" + ur.User_Id);
                Console.WriteLine("Avatar:" + ur.Avatar);
                Console.WriteLine("Email:" + ur.Email);
                Console.WriteLine("IsSuperUser:" + ur.IsSuperUser);
                Console.WriteLine("LastLoginIp:" + ur.LastLoginIp);
                Console.WriteLine("LastLoginTime:" + ur.LastLoginTime);
                Console.WriteLine("Name:" + ur.Name);
                Console.WriteLine("NickName:" + ur.NickName);
                Console.WriteLine("Password:" + ur.Password);
                Console.WriteLine("Phone:" + ur.Phone);
                Console.WriteLine("Qq:" + ur.Qq);
                Console.WriteLine("Role_Id:" + ur.Role.Role_Id);
                Console.WriteLine("RoleName:" + ur.Role.RoleName);
                Console.WriteLine("mysql_UserTest Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("mysql_UserTest Error");
                ConsoleError(ex);
            }
        }
        
        #endregion

        #region mssql
        static Cactus.MSSQLService.UserService mssql_user = new MSSQLService.UserService();
        static Cactus.MSSQLService.RoleService mssql_role = new MSSQLService.RoleService();
        static void mssql_Test()
        {
            //mssql_ActionGroupTest();
            //mssql_ActionTest();
            //mssql_RoleTest();
            mssql_UserTest();
            Console.ReadKey();
        }
        static void mssql_RoleTest()
        {
            try
            {
                Console.WriteLine("mssql_RoleTest Test");
                mssql_role.Insert(new Model.Sys.Role()
                {
                    RoleName = "RoleTest1",
                    ActionIds = ""
                });
                mssql_role.Insert(new Model.Sys.Role()
                {
                    RoleName = "RoleTest2",
                    ActionIds = ""
                });
                mssql_role.Delete("2");
                mssql_role.Update(new Model.Sys.Role()
                {
                    RoleName = "RoleTest2222",
                    ActionIds = "1,2,3",
                    Role_Id = 1
                });
                Model.Sys.Role re = mssql_role.Find(1);
                Console.WriteLine("Role_Id:" + re.Role_Id);
                Console.WriteLine("RoleName:" + re.RoleName);
                Console.WriteLine("ActionIds:" + re.ActionIds);
                Console.WriteLine("mssql_RoleTest Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("mssql_RoleTest Error");
                ConsoleError(ex);
            }
        }
        static void mssql_UserTest()
        {

            try
            {
                Console.WriteLine("mssql_UserTest Test");
                mssql_user.Insert(new Model.Sys.User()
                {
                    AddTime = DateTime.Now,
                    Avatar = "/image.png",
                    Email = "702295399@qq.com",
                    IsSuperUser = true,
                    LastLoginIp = "127.0.0.1",
                    LastLoginTime = DateTime.Now,
                    Name = "702295399@qq.com",
                    NickName = "漫漫洒洒",
                    Password = "123456789",
                    Phone = "138888888888",
                    Qq = "702295399",
                    RoleId = 1
                });
                mssql_user.Insert(new Model.Sys.User()
                {
                    AddTime = DateTime.Now,
                    Avatar = "/image.png",
                    Email = "702295399@qq.com",
                    IsSuperUser = true,
                    LastLoginIp = "127.0.0.1",
                    LastLoginTime = DateTime.Now,
                    Name = "702295399@qq.com",
                    NickName = "漫漫洒洒2",
                    Password = "123456789",
                    Phone = "138888888888",
                    Qq = "702295399",
                    RoleId = 1
                });
                mssql_user.Delete("2");
                mssql_user.Update(new Model.Sys.User()
                {
                    AddTime = DateTime.Now,
                    Avatar = "/image.png",
                    Email = "702295399@qq.com",
                    IsSuperUser = false,
                    LastLoginIp = "127.0.0.1",
                    LastLoginTime = DateTime.Now,
                    Name = "702295399@qq.com",
                    NickName = "漫漫洒洒1111",
                    Password = "123456789",
                    Phone = "138888888888",
                    Qq = "702295399",
                    RoleId = 1,
                    User_Id = 1
                });

                Model.Sys.User ur = mssql_user.Find(1);
                Console.WriteLine("User_Id:" + ur.User_Id);
                Console.WriteLine("Avatar:" + ur.Avatar);
                Console.WriteLine("Email:" + ur.Email);
                Console.WriteLine("IsSuperUser:" + ur.IsSuperUser);
                Console.WriteLine("LastLoginIp:" + ur.LastLoginIp);
                Console.WriteLine("LastLoginTime:" + ur.LastLoginTime);
                Console.WriteLine("Name:" + ur.Name);
                Console.WriteLine("NickName:" + ur.NickName);
                Console.WriteLine("Password:" + ur.Password);
                Console.WriteLine("Phone:" + ur.Phone);
                Console.WriteLine("Qq:" + ur.Qq);
                Console.WriteLine("Role_Id:" + ur.Role.Role_Id);
                Console.WriteLine("RoleName:" + ur.Role.RoleName);
                Console.WriteLine("mssql_UserTest Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("mssql_UserTest Error");
                ConsoleError(ex);
            }
        }
        #endregion

        static void ConsoleError(Exception ex)
        {
            Console.WriteLine("Data:" + ex.Data);
            Console.WriteLine("InnerException:" + ex.InnerException);
            Console.WriteLine("Message:" + ex.Message);
            Console.WriteLine("Source:" + ex.Source);
            Console.WriteLine("StackTrace:" + ex.StackTrace);
            Console.WriteLine("TargetSite:" + ex.TargetSite);
        }
    }
}
