using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.IService.CMS
{
    /// <summary>
    /// 用于文章的点赞
    /// </summary>
    public interface IArticleMapService
    {
        bool isFind(int id, string Ip, long begin_timestamp, long end_timestamp);

        bool InsertMap(int id, string Ip, long timestamp);
    }
}
