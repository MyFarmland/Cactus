using Cactus.Common;
using Cactus.IService;
using Cactus.Model.Sys;
using Cactus.Model.Sys.Config;
using Cactus.Model.Sys.Enums;
using System;
using System.Web.Mvc;

namespace Cactus.Controllers.Expand
{
    //后段管理使用
    public class PowerBaseController : AdminBaseController
    {
        public IRoleServer roleServer = IocHelper.AutofacResolveNamed<IRoleServer>("RoleServer");
        public IPowerConfigService powerConfigService = IocHelper.AutofacResolveNamed<IPowerConfigService>("PowerConfigService");
        public IPathConfigService pathConfigService = IocHelper.AutofacResolveNamed<IPathConfigService>("PathConfigService");

        protected PowerAdmin Power = null;
        public PathConfig pathConfig = null;

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
            if (obj != null && obj.value != null)
            {
                if (obj.value is Newtonsoft.Json.Linq.JObject)
                {
                    PowerAdmin power = obj.value.ParseJSON<PowerAdmin>();
                    this.Power = power;
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
                base.cacheService.Add(Constant.CacheKey.PowerConfigCacheKey,
                    new HTools.CacheObj() { value = this.Power, AbsoluteExpiration = new TimeSpan(1, 0, 0, 0) });
            }
            if (this.pathConfig == null)
            {
                this.pathConfig = pathConfigService.LoadConfig(Constant.PathConfigPath);
                base.cacheService.Add(Constant.CacheKey.PathConfigCacheKey,
                    new HTools.CacheObj() { value = this.pathConfig, AbsoluteExpiration = new TimeSpan(1, 0, 0, 0) });
            }
            if (this.pathConfig != null)
            {
                ViewData["_PathConfig"] = this.pathConfig;
            }
            if (base.LoginUser != null)
            {
                ViewData["_LoginUser"] = base.LoginUser;
            }
            if (this.Power != null)
            {
                ViewData["_Power"] = this.Power;
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
