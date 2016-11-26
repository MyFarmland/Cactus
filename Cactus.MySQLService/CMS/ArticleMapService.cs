using Cactus.Common;
using Dapper.Common;
using Cactus.IService.CMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Cactus.MySQLService.CMS
{
    public class ArticleMapService : IArticleMapService
    {
        public bool isFind(int id, string Ip, long begin_timestamp, long end_timestamp)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                int i = conn.Query<int>("SELECT count(a.ArticleId) FROM cms_articlemap as a WHERE a.Ip=@Ip and a.ArticleId=@ArticleId and CreateTs>=@begin_timestamp and CreateTs<=end_timestamp LIMIT 0, 1",
                    new { ArticleId = id, Ip = Ip, begin_timestamp = begin_timestamp, end_timestamp = end_timestamp }).SingleOrDefault();
                if (i > 0) { return true; } else { return false; }
            }
        }
        public bool InsertMap(int id, string Ip, long timestamp)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                int i = conn.Execute("INSERT INTO cms_articlemap(Ip,CreateTs,ArticleId)" +
                    "VALUES(@Ip,@CreateTs,@ArticleId);", new { Ip = Ip, CreateTs = timestamp, ArticleId = id });
                if (i > 0) { return true; } else { return false; }
            }
        }
    }
}
