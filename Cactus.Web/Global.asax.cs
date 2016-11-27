using Cactus.Common;
using System;
using System.Diagnostics;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Cactus.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            HIO.logDirPath = System.IO.Path.Combine(HIO.AppRootPath, "App_Data", HIO.logDir);//初始化log地址
            IocConfig.BuildMvcContainer();
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            AreaRegistration.RegisterAllAreas();
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Disabled);//全局禁用session
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Cactus.Crond.TaskManager.Instance.Initialize(AppDomain.CurrentDomain.BaseDirectory + System.IO.Path.Combine("Configuration", "Plan.config"));
            Cactus.Crond.TaskManager.Instance.Start();
            Debug.WriteLine("Cactus启动成功");
        }
        protected void Application_Error(object sender, EventArgs e) {
            // 在出现未处理的错误时运行的代码 
#if DEBUG
            Exception ex = Server.GetLastError().GetBaseException();
            StringBuilder str = new StringBuilder();
            string ip = "";
            if (Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null)
            {
                ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
            }
            else
            {
                ip = Request.ServerVariables.Get("Remote_Addr").ToString().Trim();
            }
            str.Append("Ip:" + ip + Environment.NewLine);
            str.Append("浏览器:" + Request.Browser.Browser.ToString() + Environment.NewLine);
            str.Append("浏览器版本:" + Request.Browser.MajorVersion.ToString() + Environment.NewLine);
            str.Append("操作系统:" + Request.Browser.Platform.ToString() + Environment.NewLine);
            str.Append("错误信息：" + Environment.NewLine);
            str.Append("页面：" + Request.Url.ToString() + Environment.NewLine);
            HIO.AsyncWriteLog(str.ToString(), ex);
            //处理完及时清理异常
#endif
            Server.ClearError();
            //跳转至出错页面 
            Response.Redirect("~/Error/Page404"); 
        }
    }
}