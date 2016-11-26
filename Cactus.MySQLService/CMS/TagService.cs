using Cactus.IService.CMS;
using System;
using Cactus.Common;
using Dapper.Common;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Cactus.MySQLService.CMS
{
    public class TagService : ITagService
    {
        public TagService()
        {
        }
        public bool IsUseTagName(string tagName, int tagId)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                int i = conn.Query<int>("SELECT c.Tag_Id FROM cms_tag as c WHERE c.TagName==@tagName and c.Tag_Id not in (@tagId) LIMIT 0,1", new { tagName = tagName, tagId = tagId }).SingleOrDefault();
                if (i > 0) { return true; } else { return false; }
            }
        }
        public bool Insert(Model.CMS.Tag entity)
        {
            entity.LastTime = DateTime.Now;
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                int i = conn.Execute("INSERT INTO cms_tag(TagName,TagDes,LastTime,CreateTime)" +
                    "VALUES(@TagName,@TagDes,@LastTime,@CreateTime)", entity);
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool InsertBatch(List<Model.CMS.Tag> datas)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.CMS.Tag entity)
        {
            entity.LastTime = DateTime.Now;
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                conn.Execute("UPDATE cms_tag SET TagName=@TagName,TagDes=@TagDes,LastTime=@LastTime WHERE Tag_Id =@Tag_Id", entity);
            }
        }

        public void Delete(string ids)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                conn.Execute(string.Format("DELETE FROM cms_tag WHERE Tag_Id in ({0})", ids));
            }
        }

        public List<Model.CMS.Tag> GetAll()
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                return conn.Query<Model.CMS.Tag>("select * from cms_tag").ToList();
            }
        }

        public List<Model.CMS.Tag> ToPagedList(int pageIndex, int pageSize, string keySelector, out int count)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                string sql01 = "select count(Tag_Id) from cms_tag";
                count = conn.Query<int>(sql01).SingleOrDefault();
                string sql = "select a.* from cms_tag as a";
                string query = "SELECT * from (" + sql + ")as c ORDER BY CreateTime " +
                    " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
                return conn.Query<Model.CMS.Tag>(query).ToList();
            }
        }

        public List<Model.CMS.Article> GetTagToArticle(int tag, int pageIndex, int pageSize, out int count)
        {
            return GetTagToArticle(tag, pageIndex, pageSize, 1, out count);
        }

        public List<Model.CMS.Article> GetTagToArticle(int tag, int pageIndex, int pageSize, int sort, out int count)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                //sqlite使用||链接字符串
                string sql01 = "select count(m_TagId) from cms_tagmap where m_TagId=@tagid";
                count = conn.Query<int>(sql01, new { tagid = tag }).SingleOrDefault();
                string sql = " SELECT a.*,b.* from cms_tagmap as a LEFT JOIN cms_article as b on a.m_ArticleId=b.Article_Id";
                string query = "SELECT * from (" + sql + ")as c ORDER BY CreateTime " +
                    " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;

                return conn.Query<Model.CMS.TagMap, Model.CMS.Article, Model.CMS.Article>(query, (tagMap, article) =>
                {
                    return article;
                }, null, null, "Article_Id", null, null).ToList();
            }
        }

        public Model.CMS.Tag Find(int id)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                string query = "select * from cms_tag as a WHERE a.Tag_Id = @id";
                return conn.Query<Model.CMS.Tag>(query, new { id = id }).SingleOrDefault();
            }
        }

        public bool InsertToMap(int articleId, int tagId)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                int i = conn.Execute("INSERT INTO cms_tagmap(m_TagId,m_ArticleId)" +
                    "VALUES(@m_TagId,@m_ArticleId)", new { m_TagId = tagId, m_ArticleId = articleId });
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool InsertToMapBatch(int articleId, string tagIds)
        {

            int[] tags = Array.ConvertAll<string, int>(tagIds.Split(','), s => Convert.ToInt32(s));

            string sql = "INSERT INTO cms_tagmap(m_TagId,m_ArticleId)VALUES";
            for (int i = 0; i < tags.Length; i++)
            {
                if (i == 0)
                {
                    sql += "(" + tags[i] + "," + articleId + ")";
                }
                else
                {
                    sql += ",(" + tags[i] + "," + articleId + ")";
                }
            }
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {

                int i = conn.Execute(sql);
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool UpdateToMap(int articleId, int tagId)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                int i = conn.Execute("UPDATE cms_tagmap SET m_TagId=@m_TagId WHERE m_ArticleId =@m_ArticleId",
                    new { m_TagId = tagId, m_ArticleId = articleId });
                if (i > 0) { return true; } else { return false; }
            }
        }

        public void DeteteToMap(int articleId, int tagId)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                conn.Execute("DELETE FROM cms_tagmap WHERE m_TagId=@m_TagId and m_ArticleId=@m_ArticleId",
                    new { m_ArticleId = articleId, tagId = tagId });
            }
        }

        public void DeteteToMap(int articleId)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                conn.Execute("DELETE FROM cms_tagmap WHERE m_ArticleId=@m_ArticleId", new { m_ArticleId = articleId });
            }
        }
        public List<Cactus.Model.CMS.Tag> FindArticleTags(int articleId)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                string query = "select b.* from cms_tagmap as a left JOIN cms_tag as b on a.m_TagId=b.Tag_Id where a.m_ArticleId=@m_ArticleId";
                return conn.Query<Model.CMS.Tag>(query, new { m_ArticleId = articleId }).ToList();
            }
        }
    }
}
