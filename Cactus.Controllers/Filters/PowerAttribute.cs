using Autofac;
using Autofac.Integration.Mvc;
using Cactus.Common;
using Cactus.IService;
using Cactus.Model.Other;
using Cactus.Model.Sys;
using Cactus.Model.Sys.Enums;
using HTools;
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
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 权限操作枚举
        /// </summary>
        public EnumsModel.ActionEnum actionEnum { get; set; }
        /// <summary>
        /// 是否显示（用于管理列表菜单入口）
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 对外暴露的名称（IsShow为true时有效）
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        protected User LoginUser = null;

        protected PowerAdmin Power = null;

        public IPowerConfigService powerConfigService = IocHelper.AutofacResolveNamed<IPowerConfigService>("PowerConfigService");
        public ICacheService cacheService = IocHelper.AutofacResolveNamed<ICacheService>("CacheService");
        
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region 权限判断
            var token = CookieHelper.GetCookieValue("Admin");
            string _GroupName = filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            HTools.CacheObj obj = cacheService.Get(Constant.CacheKey.LoginAdminInfoCacheKey + "_" + token);
            if (obj != null && obj.value != null)
            {
                if (obj.value is Newtonsoft.Json.Linq.JObject)
                {
                    User user = obj.value.ParseJSON<User>();
                    this.LoginUser = user;
                }
                else
                {
                    this.LoginUser = (User)obj.value;
                }
            }
            else { this.LoginUser = null; }
            bool b = false;
            if (!this.LoginUser.IsSuperUser)
            {
                string[] acts = LoginUser.Role.ActionIds.Split(',');
                obj = cacheService.Get(Constant.CacheKey.PowerConfigCacheKey);
                if (obj != null && obj.value != null)
                {
                    if (obj.value is Newtonsoft.Json.Linq.JObject)
                    {
                        PowerAdmin powerAdmin = obj.value.ParseJSON<PowerAdmin>();
                        this.Power = powerAdmin;
                    }
                    else
                    {
                        this.Power = (PowerAdmin)obj.value;
                    }
                }
                else { this.Power = null; }
                if (this.Power == null)
                {
                    this.Power = powerConfigService.LoadConfig(Constant.PowerConfigPath);
                    cacheService.Add(Constant.CacheKey.PowerConfigCacheKey, new CacheObj() { value = this.Power, AbsoluteExpiration = new TimeSpan(1, 0, 0, 0) });
                }
                try
                {
                    if (this.Power != null)
                    {
                        var list = this.Power.list;
                        foreach (var li in list)
                        {
                            var p = li.module.FirstOrDefault(t => t.Name == ModuleName);
                            if (p != null)
                            {
                                string action_type = _GroupName + "|" + p.Name + "|" + actionEnum.ToString();//格式 模块名|操作
                                if (acts.Contains(action_type))
                                {
                                    b = true;//存在权限
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
            else {
                b = true;
            }
            #endregion

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

        }
    }
}
