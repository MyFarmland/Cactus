using Cactus.Common;
using Dapper.Common;
using Cactus.IService.CMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.MySQLService.CMS
{
    public class CommentMapService : ICommentMapService
    {
        public bool isFind(int id, string Ip, long begin_timestamp, long end_timestamp)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                int i = conn.Query<int>("SELECT count(a.CommentId) FROM cms_commentmap as a WHERE a.Ip=@Ip and a.CommentId=@CommentId and CreateTs>=@begin_timestamp and CreateTs<=end_timestamp LIMIT 0, 1",
                    new { CommentId = id, Ip = Ip, begin_timestamp = begin_timestamp, end_timestamp = end_timestamp }).SingleOrDefault();
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool InsertMap(int id, string Ip, long timestamp)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                int i = conn.Execute("INSERT INTO cms_commentmap(Ip,CreateTs,CommentId)" +
                    "VALUES(@Ip,@CreateTs,@CommentId);", new { Ip = Ip, CreateTs = timestamp, CommentId = id });
                if (i > 0) { return true; } else { return false; }
            }
        }
    }
}
