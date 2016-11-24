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
    [Group(Title = "商店管理", Icon = "fa-file", IsShow = true)]
    public class StoreController : PowerBaseController
    {
        public IStoreInfoService storeInfoService = IocHelper.AutofacResolveNamed<IStoreInfoService>("Store.StoreInfoService");

        [Power(ModuleName = "storeManage", IsShow = true,Title="店铺管理", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult Index()
        {
            return View();
        }
        [Power(ModuleName = "storeManage", actionEnum = EnumsModel.ActionEnum.Show)]
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
        [Power(ModuleName = "storeManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult StoreAdd() { 
            return View();
        }
        [HttpPost]
        [Power(ModuleName = "storeManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult StoreAdd(StoreInfo store)
        {
            return View();
        }
        [HttpGet]
        [Power(ModuleName = "storeManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult StoreUpdate(int storeId) 
        {
            return View();
        }

        [HttpPost]
        [Power(ModuleName = "storeManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult StoreUpdate(StoreInfo store)
        {
            return View();
        }
        [HttpPost]
        [Power(ModuleName = "storeManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult StoreSwitch(bool isSwitch) {
            return View();
        }
    }
}
