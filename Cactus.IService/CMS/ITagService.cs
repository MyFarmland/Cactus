using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.IService.CMS
{
    public interface ITagService : IBaseService<Cactus.Model.CMS.Tag>
    {
        bool IsUseTagName(string tagName,int tagId);
        /// <summary>
        /// 插入一对一对应map
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        bool InsertToMap(int articleId, int tagId);
        /// <summary>
        /// 批量插入一对一对应map
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="tagIds"></param>
        /// <returns></returns>
        bool InsertToMapBatch(int articleId, string tagIds);
        /// <summary>
        /// 更新一对一对应map
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        bool UpdateToMap(int articleId, int tagId);
        /// <summary>
        /// 删除一对一对应map
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="tagId"></param>
        void DeteteToMap(int articleId, int tagId);
        /// <summary>
        /// 删除该文章对应的tag
        /// </summary>
        /// <param name="articleId"></param>
        void DeteteToMap(int articleId);
        /// <summary>
        /// 根据文章id，查他所有的tag
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        List<Cactus.Model.CMS.Tag> FindArticleTags(int articleId);
        /// <summary>
        /// 分页，根据tagid查对应的文章
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<Cactus.Model.CMS.Article> GetTagToArticle(int tag, int pageIndex, int pageSize, out int count);
        /// <summary>
        /// 分页，根据tagid查对应的文章
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<Cactus.Model.CMS.Article> GetTagToArticle(int tag, int pageIndex, int pageSize, int sort, out int count);
    }
}
