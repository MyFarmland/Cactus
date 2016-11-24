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
    public class StoreController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Cat()
        {
            return View();
        }
        public ActionResult Info()
        {
            return View();
        }
        public ActionResult Item()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult New_List()
        {
            return View();
        }
        public ActionResult Pay()
        {
            return View();
        }
        public ActionResult User_Form()
        {
            return View();
        }
        public ActionResult User_Index()
        {
            return View();
        }
        public ActionResult User_Login()
        {
            return View();
        }
        public ActionResult User_Reg()
        {
            return View();
        }
    }
}
