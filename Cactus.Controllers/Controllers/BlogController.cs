using Cactus.Common;
using Cactus.Controllers.Expand;
using Cactus.IService.CMS;
using System.Web.Mvc;


namespace Cactus.Controllers.Controllers
{
    public class BlogController : BaseController
    {
        // 以后根据autofac.config中的name生成
        public IColumnService columnService = IocHelper.AutofacResolveNamed<IColumnService>("CMS.ColumnService");
        public IArticleService articleService = IocHelper.AutofacResolveNamed<IArticleService>("CMS.ArticleService");
        //
        // GET: /Blog/

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Detail() 
        {
            return View();
        }

        public ActionResult Introduce() {
            return View();
        }

    }
}
