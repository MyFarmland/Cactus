﻿using Cactus.Common;
using Dapper.Common;
using Cactus.IService.CMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cactus.MySQLService.CMS
{
    public class StaticPageService : IStaticPageService
    {
        public StaticPageService()
        {

        }
        public bool Insert(Model.CMS.StaticPage entity)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
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

        public void Update(Model.CMS.StaticPage entity)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                conn.Execute("UPDATE cms_staticpage SET PageName=@PageName,PagePath=@PagePath,PageParameter=@PageParameter,CreateTime=@CreateTime,LastTime=@LastTime,TempPageId =@TempPageId WHERE Page_Id =@Page_Id", entity);
            }
        }

        public void UpdatePath(int id, string path)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                conn.Execute("UPDATE cms_staticpage SET PagePath=@path WHERE Page_Id =@id", new { id = id, path = path });
            }
        }
        public void Delete(string ids)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                conn.Execute(string.Format("DELETE FROM cms_staticpage WHERE Page_Id in ({0})", ids));
            }
        }

        public List<Model.CMS.StaticPage> GetAll()
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
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
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                string sql = @"select a.Page_Id,a.PageName,a.TempPageId,a.PageParameter,a.CreateTime,a.LastTime, b.TempPage_Id,b.TempName,b.TempByname,b.TempParameter,b.TempPath,b.TempContent 
                            from cms_staticpage as a left join cms_temppage as b on a.TempPageId=b.TempPage_Id";
                string sql01 = "select count(*) from cms_staticpage";
                count = conn.Query<int>(sql01).SingleOrDefault();
                //string query = "select top " + pageSize + " o.* from (select row_number() over(order by " + keySelector + ") as rownumber,* from(" + sql + ") as oo) as o where rownumber>" + (pageIndex-1) * pageSize;
                string query = "SELECT * from (" + sql + ")as c  ORDER BY " + keySelector + " LIMIT " + (pageIndex - 1) * pageSize + "," + pageIndex * pageSize;
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
            using (IDbConnection conn = SqlString.GetMySqlConnection())
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

        public bool IsUsePageName(string pageName, int ignoreId)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                int i = conn.Query<int>("SELECT c.Page_Id FROM cms_staticpage as c WHERE c.PageName=@PageName and c.Page_Id not in (@ignoreId) LIMIT 0,1", new { PageName = pageName, ignoreId = ignoreId }).SingleOrDefault();
                if (i > 0) { return true; } else { return false; }
            }
        }
    }
}
