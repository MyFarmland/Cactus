using System;
using System.Diagnostics;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Cactus.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IocConfig.BuildMvcContainer();
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemeViewEngine());//使用主题引擎
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
            //Exception ex = Server.GetLastError().GetBaseException();
            //StringBuilder str = new StringBuilder();
            //string ip = "";
            //if (Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null)
            //{
            //    ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
            //}
            //else
            //{
            //    ip = Request.ServerVariables.Get("Remote_Addr").ToString().Trim();
            //}
            //str.Append("Ip:" + ip+Environment.NewLine);
            //str.Append("浏览器:" + Request.Browser.Browser.ToString()+Environment.NewLine);
            //str.Append("浏览器版本:" + Request.Browser.MajorVersion.ToString() + Environment.NewLine);
            //str.Append("操作系统:" + Request.Browser.Platform.ToString() + Environment.NewLine);
            //str.Append("错误信息：" + Environment.NewLine);
            //str.Append("页面：" + Request.Url.ToString() + Environment.NewLine);
            //str.Append("错误信息：" + ex.Message + Environment.NewLine);
            //str.Append("错误源：" + ex.Source + Environment.NewLine);
            //str.Append("异常方法：" + ex.TargetSite + Environment.NewLine);
            //str.Append("堆栈信息：" + ex.StackTrace + Environment.NewLine);
            //Cactus.Common.HIO.WriteLog(str.ToString());
            //处理完及时清理异常
#endif
            Server.ClearError();
            //跳转至出错页面 
            Response.Redirect("~/Error/Page404"); 
        }
    }
}