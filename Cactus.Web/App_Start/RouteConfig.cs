using System.Web.Mvc;
using System.Web.Routing;

namespace Cactus.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Content/{*pathInfo}");//忽略资源目录
            routes.IgnoreRoute("Html/{*pathInfo}");//忽略静态html文件目录
            routes.IgnoreRoute("PureUI/{*pathInfo}");//忽略静态html文件目录
            routes.IgnoreRoute("{*allgif}", new { allgif = @".*\.gif(/.*)?" }); // 忽略对路径中包含 .gif 的 URL 路由
            routes.IgnoreRoute("{*alljpg}", new { alljpg = @".*\.jpg(/.*)?" }); // 忽略对路径中包含 .jpg 的 URL 路由
            routes.IgnoreRoute("{*allbmp}", new { allbmp = @".*\.bmp(/.*)?" }); // 忽略对路径中包含 .jpg 的 URL 路由
            routes.IgnoreRoute("{*alljpeg}", new { alljpeg = @".*\.jpeg(/.*)?" }); // 忽略对路径中包含 .jpg 的 URL 路由
            routes.IgnoreRoute("{*allpng}", new { allpng = @".*\.png(/.*)?" }); // 忽略对路径中包含 .png 的 URL 路由
            routes.IgnoreRoute("{*allico}", new { allpng = @".*\.ico(/.*)?" }); // 忽略对路径中包含 .png 的 URL 路由
            routes.RouteExistingFiles = true;

            /*路由匹配的顺序是从上到下的*/
            #region Blog
            routes.MapRoute(
                name: "Blog_Action",
                url: "Blog/{action}.html",
                defaults: new { controller = "Blog", action = "Introduce", id = UrlParameter.Optional },
                namespaces: new string[] { "Cactus.Controllers.Controllers" }//指定寻找的命名空间
            );
            routes.MapRoute(
                name: "Blog_Action_Optional",
                url: "Blog/{action}/{id}.html",
                defaults: new { controller = "Blog", action = "Introduce", id = UrlParameter.Optional },
                namespaces: new string[] { "Cactus.Controllers.Controllers" }//指定寻找的命名空间
            );
            #endregion

            #region Store
            routes.MapRoute(
                name: "Store_Action",
                url: "Store/{action}.html",
                defaults: new { controller = "Store", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Cactus.Controllers.Controllers" }//指定寻找的命名空间
            );
            routes.MapRoute(
                name: "Store_Action_Optional",
                url: "Store/{action}/{id}.html",
                defaults: new { controller = "Store", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Cactus.Controllers.Controllers" }//指定寻找的命名空间
            );
            #endregion

            #region 全局默认
            routes.MapRoute(
                name: "Default_Html",
                url: "{controller}/{action}.html/{id}",
                defaults: new { controller = "Blog", action = "Introduce", id = UrlParameter.Optional },
                namespaces: new string[] { "Cactus.Controllers.Controllers" }//指定寻找的命名空间
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Blog", action = "Introduce", id = UrlParameter.Optional },
                namespaces: new string[] { "Cactus.Controllers.Controllers" }//指定寻找的命名空间
            );
            #endregion
        }
    }
}