using Autofac;
using Autofac.Integration.Mvc;
using Cactus.Common;
using Cactus.IService;
using Cactus.Model.Sys;
using Cactus.Model.Sys.Enums;
using HTools;
using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Security;
using Cactus.Controllers.Filters;

namespace Cactus.Controllers.Expand
{
    //基础站点信息
    [Exception]
    public class BaseController : Controller
    {

        public ISiteConfigService siteConfigService = IocHelper.AutofacResolveNamed<ISiteConfigService>("SiteConfigService");
        public ICacheService cacheService = IocHelper.AutofacResolveNamed<ICacheService>("CacheService");
        
        /// <summary>
        /// 站点配置信息
        /// </summary>
        public SiteConfig Config = null;
        /// <summary>
        /// 执行前读取站点配置信息
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CacheObj obj=cacheService.Get(Constant.CacheKey.SiteConfigCacheKey);
            this.Config = (obj != null && obj.value != null) ? (obj.value as SiteConfig) : null;
            if (this.Config == null)
            {
                this.Config = siteConfigService.LoadConfig(Constant.SiteConfigPath);
                cacheService.Add(Constant.CacheKey.SiteConfigCacheKey,
                    new CacheObj()
                    {
                        value = Config,
                        AbsoluteExpiration = new TimeSpan(1, 0, 0, 0)
                    });
            }
            if (this.Config != null)
            {
                ViewData["_SiteConfig"] = this.Config;
            }
        }

        /// <summary>
        /// 返回结果前附加数据
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}
