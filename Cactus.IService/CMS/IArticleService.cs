using System.Collections.Generic;

namespace Cactus.IService.CMS
{
	public interface IArticleService: IBaseService<Cactus.Model.CMS.Article>
	{
        int InsertForInt(Model.CMS.Article entity);
        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="id"></param>
        /// <param name="on_off"></param>
        /// <returns></returns>
		bool IsTop(int id,bool on_off);
        /// <summary>
        /// 是否显示
        /// </summary>
        /// <param name="id"></param>
        /// <param name="on_off"></param>
        /// <returns></returns>
        bool IsShow(int id, bool on_off);
        /// <summary>
        /// 栏目是否在使用
        /// </summary>
        /// <param name="ColumnId"></param>
        /// <returns></returns>
        bool IsUseColumn(int ColumnId);
        /// <summary>
        /// 标题是否在使用
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ignoreId"></param>
        /// <returns></returns>
        bool IsUseTitle(string title, int ignoreId);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <param name="keySelector"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<Cactus.Model.CMS.Article> ToPagedList(int pageIndex, int pageSize, string where, string keySelector, out int count);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchTitle"></param>
        /// <param name="sort">1:DESC 2:ASC</param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<Cactus.Model.CMS.Article> ToSearchList(int pageIndex, int pageSize, string searchTitle, int sort, out int count);

        bool IsPraise(int Id);
	}
}

