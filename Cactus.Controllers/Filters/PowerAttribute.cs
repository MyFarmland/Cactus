using Autofac;
using Autofac.Integration.Mvc;
using Cactus.Common;
using Cactus.IService;
using Cactus.Model.Other;
using Cactus.Model.Sys;
using Cactus.Model.Sys.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace Cactus.Controllers.Filters
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class PowerAttribute : FilterAttribute, IActionFilter
    {
        public PowerAttribute() { }
        /// <summary>
        /// 权限标示名
        /// </summary>
        public string PowerId { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string PowerName { get; set; }
        /// <summary>
        /// 功能描述
        /// </summary>
        public string PowerDes { get; set; }
        /// <summary>
        /// 是否显示（用于管理列表菜单）
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 是否超级管理员应用
        /// </summary>
        public bool IsSuper { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        protected User LoginUser = null;

        protected PowerConfig Power = null;

        //public IPowerConfigService powerConfigService = AutofacDependencyResolver.Current.ApplicationContainer.Resolve<IPowerConfigService>();
        public IPowerConfigService powerConfigService = IocHelper.AutofacResolveNamed<IPowerConfigService>("PowerConfigService");
        
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var token = CookieHelper.GetCookieValue("Admin");
            this.LoginUser = CacheHelper.GetCache(Constant.CacheKey.LoginAdminInfoCacheKey + "_" + token) as User;
            bool b = false;
            if (this.IsSuper == false)
            {
                //非超级管理员专属操作

                //权限id集合
                string[] acts = LoginUser.Role.ActionIds.Split(',');

                this.Power = CacheHelper.GetCache(Constant.CacheKey.PowerConfigCacheKey) as PowerConfig;

                if (this.Power == null)
                {
                    this.Power = powerConfigService.LoadConfig(Constant.PowerConfigPath);
                    CacheHelper.SetCache(Constant.CacheKey.PowerConfigCacheKey, this.Power);
                }
                try
                {
                    if (this.Power != null)
                    {
                        //Power.PowerGroupList.Where(p=>p.PowerList.FirstOrDefault(t => t.Id == PowerId)==null);
                        foreach (var li in this.Power.PowerGroupList)
                        {
                            var p=li.PowerList.FirstOrDefault(t => t.NoPowerId == PowerId);
                            if (p != null)
                            {
                                if (acts.Contains(p.NoPowerId))
                                {
                                    //存在权限
                                    b = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    b = false;
                }
            }
            //超级管理员都可以使用
            if (this.LoginUser.IsSuperUser)
            {
                b = true;
            }

            #region 无权限执行
            if (b == false)
            { //无权限执行
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult()
                    {
                        ContentEncoding = Encoding.UTF8,
                        Data = new ResultModel
                        {
                            pass = false,
                            msg = "无权访问"
                        },
                        JsonRequestBehavior=JsonRequestBehavior.AllowGet
                    };              
                }
                else
                {
                    filterContext.Controller.ViewData["ErrorMessage"] = "无权访问";//filterContext.Exception.Message + " 亲！您犯错了哦！";//得到报错的内容
                    filterContext.Result = new ViewResult()//new一个url为Error视图
                    {
                        ViewName = "Error",/*在Shard文件夹下*/
                        ViewData = filterContext.Controller.ViewData//view视图的属性中的viewdata被赋值
                    };
                }
            }
            #endregion
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //throw new NotImplementedException();
        }
    }
}
