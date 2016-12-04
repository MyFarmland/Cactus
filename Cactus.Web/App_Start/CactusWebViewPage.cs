using Cactus.Common;
using Cactus.IService.CMS;
using System.Web.Mvc;

namespace Cactus.Web
{
    public abstract class CactusWebViewPage<T> : WebViewPage<T>
    {
        /// <summary>
        /// 文章服务
        /// </summary>
        public IArticleService articleService = IocHelper.AutofacResolveNamed<IArticleService>("CMS.ArticleService");
        /// <summary>
        /// 栏目服务
        /// </summary>
        public IColumnService columnService = IocHelper.AutofacResolveNamed<IColumnService>("CMS.ColumnService");
    }
}