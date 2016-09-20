using Cactus.Common;
using Dapper.Common;
using Cactus.IService.CMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cactus.SQLiteService.CMS
{
    public class TempPageService : ITempPageService
    {
        public TempPageService() { }
        public bool Insert(Model.CMS.TempPage entity)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                int i = conn.Execute("INSERT INTO cms_temppage(TempName,TempByname,TempParameter,TempPath,TempContent,CreateTime,LastTime)" +
                    "VALUES(@TempName,@TempByname,@TempParameter,@TempPath,@TempContent,@CreateTime,@LastTime)", entity);
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool InsertBatch(List<Model.CMS.TempPage> datas)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.CMS.TempPage entity)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                conn.Execute("UPDATE cms_temppage SET TempName=@TempName,TempByname=@TempByname,TempParameter=@TempParameter,TempPath=@TempPath,TempContent=@TempContent,CreateTime=@CreateTime,LastTime=@LastTime WHERE TempPage_Id =@TempPage_Id", entity);
            }
        }

        public void Delete(string ids)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                conn.Execute(string.Format("DELETE FROM cms_temppage WHERE TempPage_Id in ({0})", ids));
            }
        }

        public List<Model.CMS.TempPage> GetAll()
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                return conn.Query<Model.CMS.TempPage>("select * from cms_temppage").ToList();
            }
        }

        public List<Model.CMS.TempPage> ToPagedList(int pageIndex, int pageSize, string keySelector, out int count)
        {
            /*
                * firstIndex:起始索引
                * pageSize:每页显示的数量
                * orderColumn:排序的字段名
                * sql:可以是简单的单表查询语句，也可以是复杂的多表联合查询语句
            */
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                string sql = "cms_temppage";
                string sql01 = "select count(TempPage_Id) from cms_temppage";
                count = conn.Query<int>(sql01).SingleOrDefault();

                string query = "SELECT * from (" + sql + ")as c ORDER BY " + keySelector + " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
                return conn.Query<Model.CMS.TempPage>(query).ToList();
            }
        }

        public Model.CMS.TempPage Find(int id)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                string query = "select a.* from cms_temppage as a WHERE a.TempPage_Id = @id";
                return conn.Query<Model.CMS.TempPage>(query, new { id = id }).SingleOrDefault();
            }
        }
    }
}
