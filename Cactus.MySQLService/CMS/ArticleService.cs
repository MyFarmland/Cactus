using Cactus.IService.CMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cactus.Model.CMS;
using System.Data;
using Cactus.Common;
using Dapper.Common;

namespace Cactus.MySQLService.CMS
{
    public class ArticleService : IArticleService
    {
        public ArticleService()
        {

        }

        public bool IsTop(int id, bool on_off)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                int i = conn.Execute("UPDATE cms_article SET IsTop=@IsTop WHERE Article_Id =@Id", new { IsTop = on_off, Id = id });
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool IsShow(int id, bool on_off)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                int i = conn.Execute("UPDATE cms_article SET IsShow=@IsShow WHERE Article_Id =@Id", new { IsShow = on_off, Id = id });
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool IsUseColumn(int ColumnId)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                var sql = string.Format("SELECT ColumnId FROM cms_article as a WHERE a.ColumnId=@ColumnId LIMIT {0}, {1}", 0, 1);
                int i = conn.Query<int>(sql, new { ColumnId = ColumnId }).SingleOrDefault();
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool IsUseTitle(string title, int ignoreId)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                var sql = string.Format("SELECT c.Article_Id FROM cms_article as c WHERE c.Title=@title and c.Article_Id not in (@ignoreId) LIMIT {0}, {1}", 0, 1);
                int i = conn.Query<int>(sql, new { title = title, ignoreId = ignoreId }).SingleOrDefault();
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool Insert(Model.CMS.Article entity)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                int i = conn.Execute("INSERT INTO cms_article(ColumnId,Tags,ArticleContent,Title,CreateTime,LastTime,Browse,Author,IsTop,IsShow)" +
                    "VALUES(@ColumnId,@Tags,@ArticleContent,@Title,@CreateTime,@LastTime,@Browse,@Author,@IsTop,@IsShow)", entity);
                if (i > 0) { return true; } else { return false; }
            }
        }
        public int InsertForInt(Model.CMS.Article entity)
        {
            return 0;
        }
        public bool InsertBatch(System.Collections.Generic.List<Model.CMS.Article> datas)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.CMS.Article entity)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                conn.Execute("UPDATE cms_article SET ColumnId=@ColumnId,Tags=@Tags,ArticleContent=@ArticleContent,Title=@Title,CreateTime=@CreateTime,LastTime=@LastTime,Browse=@Browse,Author=@Author,IsTop=@IsTop,IsShow=@IsShow WHERE Article_Id =@Article_Id", entity);
            }
        }

        public void Delete(string ids)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                conn.Execute(string.Format("DELETE FROM cms_article WHERE Article_Id in ({0})", ids));
            }
        }

        public System.Collections.Generic.List<Model.CMS.Article> GetAll()
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                return conn.Query<Model.CMS.Article, Model.CMS.Column, Model.CMS.Article>("select a.*,b.* from cms_article as a left join cms_column as b on a.ColumnId=b.Column_Id", (article, column) =>
                {
                    if (column != null)
                        article.Column = column;
                    return article;
                }, null, null, "Column_Id", null, null).ToList();
            }
        }
        public System.Collections.Generic.List<Model.CMS.Article> ToSearchList(int pageIndex, int pageSize, string searchTitle, int sort, out int count)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                //sqlite使用||链接字符串
                string sql01 = "select count(Article_Id) from cms_article where Title like concat('%',@Title,'%')";
                count = conn.Query<int>(sql01, new { Title = searchTitle }).SingleOrDefault();
                Model.CMS.Article articleTemp = new Model.CMS.Article();
                string sql = "select a.*,b.* from cms_article as a left join cms_column as b on a.ColumnId=b.Column_Id";
                string sort_str = "DESC";
                if (sort == 1) { sort_str = "DESC"; } else if (sort == 2) { sort_str = "ASC"; }
                string query = string.Format(@"SELECT * from ({0})as c where Title like concat('%',@Title,'%') ORDER BY CreateTime {1} 
                    LIMIT {2},{3}", sql, sort_str, (pageIndex - 1) * pageSize, pageIndex * pageSize);

                return conn.Query<Model.CMS.Article, Model.CMS.Column, Model.CMS.Article>(query, (article, column) =>
                {
                    if (column != null)
                        article.Column = column;
                    return article;
                }, new { Title = searchTitle }, null, "Column_Id", null, null).ToList();
            }
        }
        public System.Collections.Generic.List<Model.CMS.Article> ToPagedList(int pageIndex, int pageSize, string keySelector, out int count)
        {
            /*
                * firstIndex:起始索引
                * pageSize:每页显示的数量
                * orderColumn:排序的字段名
                * sql:可以是简单的单表查询语句，也可以是复杂的多表联合查询语句
            */
            //using (IDbConnection conn = SqlString.GetMySqlConnection())
            //{
            //    string sql = "select a.*,b.* from cms_article as a left join cms_column as b on a.ColumnId=b.Column_Id";
            //    string sql01 = "select count(*) from cms_article";
            //    count = conn.Query<int>(sql01).SingleOrDefault();
            //    //string query = "select top " + pageSize + " o.* from (select row_number() over(order by " + keySelector + ") as rownumber,* from(" + sql + ") as oo) as o where rownumber>" + (pageIndex-1) * pageSize;
            //    string query = "SELECT * from (" + sql + ")as c  ORDER BY " + keySelector + " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            //    return conn.Query<Model.CMS.Article, Model.CMS.Column, Model.CMS.Article>(query, (article, column) =>
            //    {
            //        if (column != null)
            //            article.Column = column;
            //        return article;
            //    }, null, null, "Column_Id", null, null).ToList();
            //}
            return this.ToPagedList(pageIndex, pageSize, "", keySelector, out count);
        }

        public System.Collections.Generic.List<Model.CMS.Article> ToPagedList(int pageIndex, int pageSize, string where, string keySelector, out int count)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                string sql = "select a.*,b.* from cms_article as a left join cms_column as b on a.ColumnId=b.Column_Id";
                string sql01 = "select count(*) from cms_article " + where;
                count = conn.Query<int>(sql01).SingleOrDefault();
                Model.CMS.Article articleTemp = new Model.CMS.Article();
                string query = string.Format(@"SELECT * from ({0})as c {1} ORDER BY {2} LIMIT {3},{4} ", sql, where, keySelector, (pageIndex - 1) * pageSize, pageSize * pageSize);

                return conn.Query<Model.CMS.Article, Model.CMS.Column, Model.CMS.Article>(query, (article, column) =>
                {
                    if (column != null)
                        article.Column = column;
                    return article;
                }, null, null, "Column_Id", null, null).ToList();
            }
        }

        public Model.CMS.Article Find(int id)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                string query = "select a.*,b.* from cms_article as a left join cms_column as b on a.ColumnId=b.Column_Id WHERE a.Article_Id = @id";
                return conn.Query<Model.CMS.Article, Model.CMS.Column, Model.CMS.Article>(query, (article, column) =>
                {
                    if (column != null)
                        article.Column = column;
                    return article;
                }, new { id = id }, null, "Column_Id", null, null).SingleOrDefault();
            }
        }
    }
}
