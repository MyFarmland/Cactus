using System;
using Cactus.Common;
using Dapper.Common;
using Cactus.IService.CMS;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
namespace Cactus.SQLiteService.CMS
{
    public class CommentService : ICommentService
    {
        public CommentService()
		{
		}

        public bool Insert(Model.CMS.Comment entity)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                int i = conn.Execute("INSERT INTO cms_comment(ArticleId,Content,CreateTime,Nickname,Email,VoteFavour,VoteOppose)" +
                    "VALUES(@ArticleId,@Content,@CreateTime,@Nickname,@Email,@VoteFavour,@VoteOppose)", entity);
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool InsertBatch(List<Model.CMS.Comment> datas)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.CMS.Comment entity)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                conn.Execute("UPDATE cms_comment SET ArticleId=@ArticleId,Content=@Content,CreateTime=@CreateTime,Nickname=@Nickname,Email=@Email,VoteFavour=@VoteFavour,VoteOppose=@VoteOppose WHERE Comment_Id =@Comment_Id", entity);
            }
        }

        public void Delete(string ids)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                conn.Execute(string.Format("DELETE FROM cms_comment WHERE Comment_Id in ({0})", ids));
            }
        }

        public List<Model.CMS.Comment> GetAll()
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                return conn.Query<Model.CMS.Comment, Model.CMS.Article, Model.CMS.Comment>("select a.*,b.* from cms_comment as a left join cms_article as b on a.ArticleId=b.Article_Id ",
                    (comment, article) =>
                {
                    if (comment != null)
                        comment.Article = article;
                    return comment;
                }, null, null, "Article_Id", null, null).ToList();
            }
        }

        public List<Model.CMS.Comment> ToPagedList(int pageIndex, int pageSize, string keySelector, out int count)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                //sqlite使用||链接字符串
                string sql01 = "select count(Comment_Id) from cms_comment";
                count = conn.Query<int>(sql01).SingleOrDefault();
                Model.CMS.Article articleTemp = new Model.CMS.Article();
                string sql = "select a.*,b.* from cms_comment as a left join cms_article as b on a.ArticleId=b.Article_Id";
                string query = "SELECT * from (" + sql + ")as c ORDER BY CreateTime "+
                    " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;

                return conn.Query<Model.CMS.Comment, Model.CMS.Article, Model.CMS.Comment>(query,
                    (comment, article) =>
                {
                    if (comment != null)
                        comment.Article = article;
                    return comment;
                }, null, null, "Article_Id", null, null).ToList();
            }
        }

        public Model.CMS.Comment Find(int id)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                string query = "select a.*,b.* from cms_comment as a left join cms_article as b on a.ArticleId=b.Article_Id WHERE a.Comment_Id = @id";
                return conn.Query<Model.CMS.Comment, Model.CMS.Article, Model.CMS.Comment>(query, (comment,article) =>
                {
                    if (comment != null)
                        comment.Article = article;
                    return comment;
                }, new { id = id }, null, "Article_Id", null, null).SingleOrDefault();
            }
        }

        public bool isVoteFavour(int Id)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                int i = conn.Execute("UPDATE cms_comment SET VoteFavour=VoteFavour+1 where Comment_Id=@Id", new { Id = Id });
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool isVoteOppose(int Id)
        {
            using (IDbConnection conn = SqlString.GetSQLiteConnection())
            {
                int i = conn.Execute("UPDATE cms_comment SET VoteOppose=VoteOppose+1 where Comment_Id=@Id", new { Id = Id });
                if (i > 0) { return true; } else { return false; }
            }
        }
    }
}
