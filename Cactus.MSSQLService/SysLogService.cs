using Cactus.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper.Common;
using Cactus.Common;

namespace Cactus.MSSQLService
{
    public class SysLogService : ISysLogService
    {

        public void WriteLog(int uid, string info)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                int i = conn.Execute("INSERT INTO sys_log(UserId,LogInfo,CreateTs)" +
                    "VALUES(@UserId,@LogInfo,@CreateTs)", new { UserId = uid, LogInfo = info, CreateTs = StringHelper.GetTimeStamp() });
            }
        }

        public bool DeteleLog(string ids)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                int i=conn.Execute(string.Format("DELETE FROM sys_log WHERE Log_Id in ({0})", ids));
                if (i > 0) { return true; } else { return false; }
            }
        }

        public List<Model.Sys.SysLog> ToPagedList(int pageIndex, int pageSize, string keySelector, out int count)
        {
            /*
                * pageIndex:起始索引
                * pageSize:每页显示的数量
                * keySelector:排序的字段名
                * sql:可以是简单的单表查询语句，也可以是复杂的多表联合查询语句
            */
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                string sql = "select a.*,b.* from sys_log as a left join  sys_user as b on a.UserId=b.User_Id";
                string sql01 = "select count(*) from sys_log";
                count = conn.Query<int>(sql01).SingleOrDefault();
                Model.Sys.User userTemp = new Model.Sys.User();
                string query = "select top " + pageSize + " o.* from (select row_number() over(order by " + keySelector + ") as rownumber,* from(" + sql + ") as oo) as o where rownumber>" + (pageIndex - 1) * pageSize;
                return conn.Query<Model.Sys.SysLog, Model.Sys.User, Model.Sys.SysLog>(query, (log, user) =>
                {
                    if (user != null)
                        log.User = user;
                    return log;
                }, null, null, "User_Id", null, null).ToList();
            }
        }
    }
}
