using System;
using Cactus.Common;
using Dapper.Common;
using Cactus.IService.CMS;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cactus.MSSQLService.CMS
{
	public class ArticleService:IArticleService
	{
		public ArticleService ()
		{
		}

        public bool IsTop(int id, bool on_off)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                int i = conn.Execute("UPDATE cms_article SET IsTop=@IsTop WHERE Article_Id =@Id", new { Id = id, IsTop = on_off });
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool IsShow(int id, bool on_off)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                int i = conn.Execute("UPDATE cms_article SET IsShow=@IsShow WHERE Article_Id =@Id", new { Id = id, IsShow = on_off });
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool IsUseColumn(int ColumnId)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                int i = conn.Query<int>("SELECT top 1 ColumnId FROM cms_article as a WHERE a.ColumnId=@ColumnId ", new { ColumnId = ColumnId }).SingleOrDefault();
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool IsUseTitle(string title, int ignoreId)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                int i = conn.Query<int>("SELECT top 1 c.Article_Id FROM cms_article as c WHERE c.Title=@title and c.Article_Id not in (@ignoreId) ", new { title = title, ignoreId = ignoreId }).SingleOrDefault();
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool Insert(Model.CMS.Article entity)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                int i = conn.Execute("INSERT INTO cms_article(ColumnId,Tags,Tagids,ArticleContent,Title,CreateTime,LastTime,[Browse],Author,IsTop,IsShow)" +
                    "VALUES(@ColumnId,@Tags,@TagIds,@ArticleContent,@Title,@CreateTime,@LastTime,@Browse,@Author,@IsTop,@IsShow);", entity);
                if (i > 0) { return true; } else { return false; }
            }
        }
        public int InsertForInt(Model.CMS.Article entity)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                int i = conn.Query<int>("INSERT INTO cms_article(ColumnId,Tags,Tagids,ArticleContent,Title,CreateTime,LastTime,[Browse],Author,IsTop,IsShow)" +
                    "VALUES(@ColumnId,@Tags,@TagIds,@ArticleContent,@Title,@CreateTime,@LastTime,@Browse,@Author,@IsTop,@IsShow);SELECT @@IDENTITY;", entity).SingleOrDefault();
                return i;
            }
        }

        public bool InsertBatch(System.Collections.Generic.List<Model.CMS.Article> datas)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.CMS.Article entity)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                conn.Execute("UPDATE cms_article SET ColumnId=@ColumnId,Tags=@Tags,Tagids=@TagIds,ArticleContent=@ArticleContent,Title=@Title,CreateTime=@CreateTime,LastTime=@LastTime,[Browse]=@Browse,Author=@Author,IsTop=@IsTop,IsShow=@IsShow WHERE Article_Id =@Article_Id", entity);
            }
        }

        public void Delete(string ids)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                conn.Execute(string.Format("DELETE FROM cms_article WHERE Article_Id in ({0})", ids));
            }
        }

        public System.Collections.Generic.List<Model.CMS.Article> GetAll()
        {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                return conn.Query<Model.CMS.Article, Model.CMS.Column, Model.CMS.Article>("select a.*,b.* from cms_article as a left join cms_column as b on a.ColumnId=b.Column_Id", (article, column) =>
                {
                    if (column != null)
                        article.Column = column;
                    return article;
                }, null, null, "Column_Id", null, null).ToList();
            }
        }

        public System.Collections.Generic.List<Model.CMS.Article> ToSearchList(int pageIndex, int pageSize, string searchTitle, int sort, out int count) {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                //sqlite使用||链接字符串
                string sql01 = "select count(Article_Id) from cms_article where Title like '%'+@Title+'%'";
                count = conn.Query<int>(sql01, new { Title = searchTitle }).SingleOrDefault();
                Model.CMS.Article articleTemp = new Model.CMS.Article();
                string sql = "select a.*,b.* from cms_article as a left join cms_column as b on a.ColumnId=b.Column_Id";
                //string sort_str = "DESC";
                //if (sort == 1) { sort_str = "DESC"; } else if (sort == 2) { sort_str = "ASC"; }
                string query = "select top " + pageSize + " o.* from (select row_number() over(order by CreateTime) as rownumber,* from(" + sql + ") as oo) as o where rownumber>" + (pageIndex - 1) * pageSize;
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
            return this.ToPagedList(pageIndex, pageSize, "", keySelector,out count);
        }

        public System.Collections.Generic.List<Model.CMS.Article> ToPagedList(int pageIndex, int pageSize, string where, string keySelector, out int count)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                string sql = "select a.*,b.* from cms_article as a left join cms_column as b on a.ColumnId=b.Column_Id";
                string sql01 = "select count(Article_Id) from cms_article " + where;
                count = conn.Query<int>(sql01).SingleOrDefault();
                Model.CMS.Article articleTemp = new Model.CMS.Article();
                //string query = "select * from(select ROW_NUMBER() OVER(ORDER BY "+ keySelector + " ) AS RowNumber, *from (" + sql + ")as c " + where + ") T where RowNumber BETWEEN "+ ((pageIndex - 1) * pageSize+1) + " and " + pageIndex*pageSize;
                string query = "select top " + pageSize + " o.* from (select row_number() over(order by " + keySelector + ") as rownumber,* from(" + sql + ") as oo " + where + ") as o where rownumber>" + (pageIndex - 1) * pageSize;
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
            using (IDbConnection conn = SqlString.GetSqlConnection())
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


        public bool IsLike(int Id)
        {
            throw new NotImplementedException();
        }
    }
}

