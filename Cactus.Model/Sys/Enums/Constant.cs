using System.Collections.Generic;
using System.Web;

namespace Cactus.Model.Sys.Enums
{
    public class Constant
    {

        /// <summary>
        /// 站点配置文件路径
        /// </summary>
        public static string SiteConfigPath = HttpContext.Current.Server.MapPath("/Configuration/SiteConfig.config");
        /// <summary>
        /// 站点配置文件路径
        /// </summary>
        public static string BlogConfigPath = HttpContext.Current.Server.MapPath("/Configuration/BlogConfig.config");
        /// <summary>
        /// 权限配置文件路径
        /// </summary>
        public static string PowerConfigPath = HttpContext.Current.Server.MapPath("/Configuration/PowerConfig.config");
        /// <summary>
        /// 商户权限配置文件路径
        /// </summary>
        public static string StoreActionPath = HttpContext.Current.Server.MapPath("/Configuration/StoreAction.config");
        /// <summary>
        /// 主题配置文件路径
        /// </summary>
        public static string PathConfigPath = HttpContext.Current.Server.MapPath("/Configuration/PathConfig.config");
        /// <summary>
        /// 站点缓存键集合
        /// </summary>

        public static class CacheKey
        {
            /// <summary>
            /// 站点信息缓存key
            /// </summary>
            public static string SiteConfigCacheKey = "CACHE_SITE_CONFIG";
            /// <summary>
            /// Blog信息缓存key
            /// </summary>
            public static string BlogConfigCacheKey = "CACHE_BLOG_CONFIG";
            /// <summary>
            /// 权限信息缓存key
            /// </summary>
            public static string PowerConfigCacheKey = "CACHE_POWER_CONFIG";
            /// <summary>
            /// 管理员信息缓存key
            /// </summary>
            public static string LoginAdminInfoCacheKey = "CACHE_LOGIN_ADMIN";
            /// <summary>
            /// 会员信息缓存key
            /// </summary>
            public static string LoginMemberInfoCacheKey = "CACHE_LOGIN_MEMBER";
            /// <summary>
            /// 路径配置缓存key
            /// </summary>
            public static string PathConfigCacheKey = "CACHE_PATH_CONFIG";

            public static Dictionary<string, string> List = new Dictionary<string, string>();
            static CacheKey() {
                List.Add(SiteConfigCacheKey, "站点信息缓存");
                List.Add(PowerConfigCacheKey, "权限信息缓存");
                List.Add(LoginAdminInfoCacheKey, "管理员信息缓存");
                List.Add(LoginMemberInfoCacheKey, "会员信息缓存");
                List.Add(BlogConfigCacheKey, "博客信息缓存");
                List.Add(PathConfigCacheKey, "路径配置缓存");
            }
        }
    }
}
