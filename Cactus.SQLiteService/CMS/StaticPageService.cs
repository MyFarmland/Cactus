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
    public class StaticPageService : IStaticPageService
    {
        public StaticPageService()
		{

		}
        public bool Insert(Model.CMS.StaticPage entity)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                int i = conn.Execute("INSERT INTO cms_staticpage(PageName,PagePath,PageParameter,CreateTime,LastTime,TempPageId)" +
                    "VALUES(@PageName,@PagePath,@PageParameter,@CreateTime,@LastTime,@TempPageId)", entity);
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool InsertBatch(List<Model.CMS.StaticPage> datas)
        {
            throw new NotImplementedException();
        }
        public void UpdatePath(int id, string path)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                conn.Execute("UPDATE cms_staticpage SET PagePath=@path WHERE Page_Id =@id", new { id = id, path = path });
            }
        }

        public void Update(Model.CMS.StaticPage entity)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                conn.Execute("UPDATE cms_staticpage SET PageName=@PageName,PageParameter=@PageParameter,CreateTime=@CreateTime,LastTime=@LastTime,TempPageId =@TempPageId WHERE Page_Id =@Page_Id", entity);
            }
        }

        public void Delete(string ids)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                conn.Execute(string.Format("DELETE FROM cms_staticpage WHERE Page_Id in ({0})", ids));
            }
        }

        public List<Model.CMS.StaticPage> GetAll()
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                return conn.Query<Model.CMS.StaticPage, Model.CMS.TempPage, Model.CMS.StaticPage>("select a.*,b.* from cms_staticpage as a left join cms_temppage as b on a.TempPageId=b.TempPage_Id", (staticPage, tempPage) =>
                {
                    if (tempPage != null)
                        staticPage.TempPage = tempPage;
                    return staticPage;
                }, null, null, "TempPage_Id", null, null).ToList();
            }
        }

        public List<Model.CMS.StaticPage> ToPagedList(int pageIndex, int pageSize, string keySelector, out int count)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                string sql = "select a.*,b.* from cms_staticpage as a left join cms_temppage as b on a.TempPageId=b.TempPage_Id";
                string sql01 = "select count(Page_Id) from cms_staticpage";
                count = conn.Query<int>(sql01).SingleOrDefault();
                string query = "SELECT * from (" + sql + ")as c  ORDER BY " + keySelector + " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
                return conn.Query<Model.CMS.StaticPage, Model.CMS.TempPage, Model.CMS.StaticPage>(query, (staticPage, tempPage) =>
                {
                    if (tempPage != null)
                        staticPage.TempPage = tempPage;
                    return staticPage;
                }, null, null, "TempPage_Id", null, null).ToList();
            }
        }

        public Model.CMS.StaticPage Find(int id)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                string query = "select a.*,b.* from cms_staticpage as a left join cms_temppage as b on a.TempPageId=b.TempPage_Id WHERE a.Page_Id = @id";
                return conn.Query<Model.CMS.StaticPage, Model.CMS.TempPage, Model.CMS.StaticPage>(query, (staticPage, tempPage) =>
                {
                    if (tempPage != null)
                        staticPage.TempPage = tempPage;
                    return staticPage;
                }, new { id = id }, null, "TempPage_Id", null, null).SingleOrDefault();
            }
        }
    }
}
