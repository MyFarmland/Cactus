using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Cactus.Controllers.Controllers
{
    public class ErrorController : Controller
    {
        /*
         *错误页引导,用于程序中错误跳转
         *400
         *500
         *Error
         * */

        public ActionResult Page404()
        {
            if (HttpContext.Request.IsAjaxRequest()) {
                return Content("404");
            }
            return View("404");
        }

        public ActionResult Page500()
        {
            if (HttpContext.Request.IsAjaxRequest())
            {
                return Content("500");
            }
            return View("500");
        }

        public ActionResult Error()
        {
            if (HttpContext.Request.IsAjaxRequest())
            {
                return Content("Error");
            }
            return View("Error");
        }

        public ActionResult SiteClose() {
            if (HttpContext.Request.IsAjaxRequest())
            {
                return Content("SiteClose");
            }
            return View("SiteClose");
        }
    }
}
