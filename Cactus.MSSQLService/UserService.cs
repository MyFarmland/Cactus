using Cactus.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper.Common;
using Cactus.Common;

namespace Cactus.MSSQLService
{
    public class UserService : IUserService
    {
        

        public Model.Sys.User CheckLogin(string userName, string pwd)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                string query = "select a.*,b.* from sys_user as a left join sys_role as b on a.RoleId=b.Role_Id WHERE a.Name = @Name and a.Password=@Password";
                return conn.Query<Model.Sys.User, Model.Sys.Role, Model.Sys.User>(query,(user,role)=>{
                    if (role != null)
                    user.Role = role;
                    return user;
                }, new { Name = userName, Password = CryptoHelper.MD5Encrypt(pwd) }, null, "Role_Id", null, null).SingleOrDefault();
            }
        }

        public bool AlterPwd(int id, string oldPwd, string newPwd)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
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
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
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
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
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
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                conn.Execute("UPDATE sys_user SET RoleId=@RoleId,Name=@Name,Password=@Password,NickName=@NickName,Avatar=@Avatar,Email=@Email," +
                    "Phone=@Phone,Qq=@Qq,AddTime=@AddTime,LastLoginTime=@LastLoginTime,LastLoginIp=@LastLoginIp,IsSuperUser=@IsSuperUser WHERE User_Id =@User_Id", entity);
            }
        }

        public void Delete(string ids)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                conn.Execute(string.Format("DELETE FROM sys_user WHERE User_Id in ({0})", ids));
            }
        }

        public List<Model.Sys.User> GetAll()
        {
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                return conn.Query<Model.Sys.User, Model.Sys.Role, Model.Sys.User>("select a.*,b.* from sys_user as a left join sys_role as b on a.RoleId=b.Role_Id", (user, role) =>
                {
                    if (role != null)
                        user.Role = role;
                    return user;
                }, null, null, "Role_Id", null, null).ToList();
            }
        }

        public List<Model.Sys.User> ToPagedList(int pageIndex, int pageSize, string keySelector,out int count)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                string sql = "select a.*,b.* from sys_user as a left join sys_role as b on a.RoleId=b.Role_Id";
                string sql01 = "select count(*) from sys_user";
                count =conn.Query<int>(sql01).SingleOrDefault();
                Model.Sys.User userTemp = new Model.Sys.User();
                string query = "select top " + pageSize + " o.* from (select row_number() over(order by " + keySelector + ") as rownumber,* from(" + sql + ") as oo) as o where rownumber>" + (pageIndex-1) * pageSize;
                return conn.Query<Model.Sys.User, Model.Sys.Role, Model.Sys.User>(query, (user,role) => {
                    if (role != null)
                        user.Role = role;
                    return user;
                }, null, null, "Role_Id", null, null).ToList();
            }
        }

        public Model.Sys.User Find(int id)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                string query = "select a.*,b.* from sys_user as a left join sys_role as b on a.RoleId=b.Role_Id WHERE a.User_Id = @id";
                return conn.Query<Model.Sys.User, Model.Sys.Role, Model.Sys.User>(query,(user,role)=>{
                    if (role != null)
                        user.Role = role;
                    return user;
                }, new { id = id }, null, "Role_Id", null, null).SingleOrDefault();
            }
        }

        public bool IsUseName(string username, int ignoreId)
        {
            throw new NotImplementedException();
        }
    }
}
