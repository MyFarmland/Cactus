using System.Web.Mvc;

namespace Cactus.Controllers.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //Area的路由和全局的路由是区分开的
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Root", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Cactus.Controllers.Areas.Admin.Controllers" }//指定寻找的命名空间
            );
        }
    }
}
