using Cactus.Common;
using Cactus.Controllers.Expand;
using Cactus.Controllers.Filters;
using Cactus.IService.Store;
using Cactus.Model.Other;
using Cactus.Model.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cactus.Controllers.Areas.Admin.Controllers
{
    [Exception]
    [Group(GroupName = "商店管理", NoGroupId = "Store2016", IsShow = true, Icon = "fa-shopping-cart")]
    public class StoreController : PowerBaseController
    {
        public IStoreInfoService storeInfoService = IocHelper.AutofacResolveNamed<IStoreInfoService>("Store.StoreInfoService");
        

        [Power(IsSuper = false, IsShow = true, PowerId = "Store_A101", Icon = "fa-home", PowerName = "店铺管理", PowerDes = "店铺管理")]
        public ActionResult Index()
        {
            return View();
        }
        [Power(IsSuper = false, IsShow = false, PowerId = "Store_A101", Icon = "fa-home", PowerName = "店铺管理", PowerDes = "店铺管理")]
        public ActionResult StoreList(int page)
        {
            PageTurnModel pageturn = new PageTurnModel() { ItemSize = 10 };
            pageturn.PageIndex = page;
            int count = 0;
            var list = this.storeInfoService.ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "Store_Id", out  count).Select(t => new
            {
                t.Store_Id,
                t.StoreName,
                t.StoreSwitch,
                LastTime = t.LastTime.ToString("yyyy-MM-dd HH:mm:ss"),
                CreateTime = t.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")
            });
            pageturn.CountSize = count;
            return Json(new RowResultModel { rows = list, pagination = pageturn, pass = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Power(IsSuper = false, IsShow = false, PowerId = "Store_A102", PowerName = "添加店铺", PowerDes = "添加店铺")]
        public ActionResult StoreAdd() { 
            return View();
        }
        [HttpPost]
        [Power(IsSuper = false, IsShow = false, PowerId = "Store_A102", PowerName = "添加店铺", PowerDes = "添加店铺")]
        public ActionResult StoreAdd(StoreInfo store)
        {
            return View();
        }
        [HttpGet]
        [Power(IsSuper = false, IsShow = false, PowerId = "Store_A103", PowerName = "修改店铺", PowerDes = "修改店铺")]
        public ActionResult StoreUpdate(int storeId) 
        {
            return View();
        }

        [HttpPost]
        [Power(IsSuper = false, IsShow = false, PowerId = "Store_A103", PowerName = "修改店铺", PowerDes = "修改店铺")]
        public ActionResult StoreUpdate(StoreInfo store)
        {
            return View();
        }
        [HttpPost]
        [Power(IsSuper = false, IsShow = false, PowerId = "Store_A104", PowerName = "店铺开关", PowerDes = "店铺开关")]
        public ActionResult StoreSwitch(bool isSwitch) {
            return View();
        }
    }
}
