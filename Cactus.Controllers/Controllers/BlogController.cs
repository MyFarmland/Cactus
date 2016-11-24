using Autofac;
using Autofac.Integration.Mvc;
using Cactus.Common;
using Cactus.Controllers.Expand;
using Cactus.Controllers.Filters;
using Cactus.IService.CMS;
using Cactus.Model.Other;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace Cactus.Controllers.Controllers
{
    public class BlogController : BlogBaseController
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
