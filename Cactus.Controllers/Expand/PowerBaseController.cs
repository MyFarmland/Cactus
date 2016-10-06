using Cactus.Common;
using Cactus.Model.Sys;
using Cactus.Model.Sys.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cactus.IService;
using Autofac;
using Autofac.Integration.Mvc;
using System.Security.Principal;

namespace Cactus.Controllers.Expand
{
    //后段管理使用
    public class PowerBaseController : AdminBaseController
    {
        public IRoleServer roleServer = IocHelper.AutofacResolveNamed<IRoleServer>("RoleServer");
        public IPowerConfigService powerConfigService = IocHelper.AutofacResolveNamed<IPowerConfigService>("PowerConfigService");

        protected PowerConfig Power = null;

        /// <summary>
        /// 获取已验证用户信息
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {

            base.OnAuthorization(filterContext);

            //获取已登录用户信息
            if (base.LoginUser == null)
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new ContentResult() { Content = "<a href=\"/AdminLogin/Index\">请登录</a>" };
                }
                else
                {
                    filterContext.Result = new RedirectResult("/AdminLogin/Index");
                }
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (base.LoginUser == null)
            {
                filterContext.Result = new RedirectResult("/AdminLogin");
            }
            HTools.CacheObj obj = base.cacheService.Get(Constant.CacheKey.PowerConfigCacheKey);
            this.Power = (obj != null && obj.value != null) ? (obj.value as PowerConfig) : null;
            //this.Power = CacheHelper.GetCache(Constant.CacheKey.PowerConfigCacheKey) as PowerConfig;
            if (this.Power == null)
            {
                this.Power = powerConfigService.LoadConfig(Constant.PowerConfigPath);
                //CacheHelper.SetCache(Constant.CacheKey.PowerConfigCacheKey, this.Power);
                base.cacheService.Add(Constant.CacheKey.PowerConfigCacheKey,
                    new HTools.CacheObj() { value = this.Power, AbsoluteExpiration = new DateTimeOffset(DateTime.Now).AddDays(1) });
            }
            if (base.LoginUser != null)
            {
                ViewData["LoginUser"] = base.LoginUser;
            }
            if (this.Power != null)
            {
                ViewData["Power"] = this.Power;
            }
        }

        /// <summary>
        /// 返回结果前附加数据
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}
