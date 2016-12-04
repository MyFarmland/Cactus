using Cactus.Common;
using Dapper.Common;
using Cactus.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Cactus.SQLiteService
{
    public class UserServer : IUserServer
    {
        public bool IsUseName(string username, int ignoreId) {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                int i = conn.Query<int>("SELECT c.User_Id FROM sys_user as c WHERE c.Name=@username and c.User_Id not in (@ignoreId) LIMIT 0,1", new { username = username, ignoreId = ignoreId }).SingleOrDefault();
                if (i > 0) { return true; } else { return false; }
            }
        }
        public Cactus.Model.Sys.User CheckLogin(string userName, string pwd)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                string query = "select a.*,b.* from sys_user as a left join sys_role as b on a.RoleId=b.Role_Id WHERE a.Name = @Name and a.Password=@Password";
                return conn.Query<Model.Sys.User, Model.Sys.Role, Model.Sys.User>(query, (user, role) =>
                {
                    if (role != null)
                        user.Role = role;
                    return user;
                }, new { Name = userName, Password = CryptoHelper.MD5Encrypt(pwd) }, null, "Role_Id", null, null).SingleOrDefault();
            }
        }
        public bool AlterPwd(int id, string oldPwd, string newPwd)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                var u = this.Find(id);
                if (u != null)
                {
                    if (u.Password == CryptoHelper.MD5Encrypt(oldPwd))
                    {
                        conn.Execute("UPDATE sys_user SET Password=@Password WHERE User_Id =@Id", new { Id = id, Password = CryptoHelper.MD5Encrypt(newPwd) });
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public bool AlterFace(int id, string picUrl)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                var u = this.Find(id);
                if (u != null)
                {
                    conn.Execute("UPDATE sys_user SET Avatar=@Avatar WHERE User_Id =@Id", new { Id = id, Avatar = picUrl });
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Insert(Model.Sys.User entity)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                int i = conn.Execute("INSERT INTO sys_user(RoleId,Name,Password,NickName,Avatar,Email,Phone,Qq,AddTime,LastLoginTime,LastLoginIp,IsSuperUser)" +
                    "VALUES(@RoleId,@Name,@Password,@NickName,@Avatar,@Email,@Phone,@Qq,@AddTime,@LastLoginTime,@LastLoginIp,@IsSuperUser)", entity);
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool InsertBatch(List<Model.Sys.User> datas)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Sys.User entity)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                conn.Execute("UPDATE sys_user SET RoleId=@RoleId,Name=@Name,Password=@Password,NickName=@NickName,Avatar=@Avatar,Email=@Email," +
                    "Phone=@Phone,Qq=@Qq,AddTime=@AddTime,LastLoginTime=@LastLoginTime,LastLoginIp=@LastLoginIp,IsSuperUser=@IsSuperUser WHERE User_Id =@User_Id", entity);
            }
        }

        public void Delete(string ids)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                conn.Execute(string.Format("DELETE FROM sys_user WHERE User_Id in ({0})", ids));
            }
        }

        public List<Model.Sys.User> GetAll()
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                Model.Sys.User userTemp = new Model.Sys.User();
                return conn.Query<Model.Sys.User, Model.Sys.Role, Model.Sys.User>("select a.*,b.* from sys_user as a left join sys_role as b on a.RoleId=b.Role_Id", (user, role) =>
                {
                    if (userTemp == null || userTemp.User_Id != user.User_Id)
                        userTemp = user;
                    if (role != null)
                        userTemp.Role = role;
                    return user;
                }, null, null, "Role_Id", null, null).ToList();
            }
        }

        public List<Model.Sys.User> ToPagedList(int pageIndex, int pageSize, string keySelector, out int count)
        {
            /*
                * firstIndex:起始索引
                * pageSize:每页显示的数量
                * orderColumn:排序的字段名
                * sql:可以是简单的单表查询语句，也可以是复杂的多表联合查询语句
            */
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                string sql = "select a.*,b.* from sys_user as a left join sys_role as b on a.RoleId=b.Role_Id";
                string sql01 = "select count(*) from sys_user";
                count = conn.Query<int>(sql01).SingleOrDefault();
                //string query = "select top " + pageSize + " o.* from (select row_number() over(order by " + keySelector + ") as rownumber,* from(" + sql + ") as oo) as o where rownumber>" + (pageIndex-1) * pageSize;
                string query = "SELECT * from (" + sql + ")as c  ORDER BY " + keySelector + " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;

                return conn.Query<Model.Sys.User, Model.Sys.Role, Model.Sys.User>(query, (user, role) =>
                {
                    if (role != null)
                        user.Role = role;
                    return user;
                }, null, null, "Role_Id", null, null).ToList();
            }
        }

        public Model.Sys.User Find(int id)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                string query = "select a.*,b.* from sys_user as a left join sys_role as b on a.RoleId=b.Role_Id WHERE a.User_Id = @id";
                return conn.Query<Model.Sys.User, Model.Sys.Role, Model.Sys.User>(query, (user, role) =>
                {
                    if (role != null)
                        user.Role = role;
                    return user;
                }, new { id = id }, null, "Role_Id", null, null).SingleOrDefault();
            }
        }
    }
}
