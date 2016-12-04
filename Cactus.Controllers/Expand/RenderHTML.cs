using Cactus.Common;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace Cactus.Controllers.Expand
{
    public static class RenderHTML
    {
        public static bool Render(Controller controller, string viewPath, string targetPath)
        {
            ViewEngineResult viewResult = ViewEngines.Engines.FindView(controller.ControllerContext, viewPath, "");
            IView view = viewResult.View;
            ViewDataDictionary vd = controller.ViewData;
            TempDataDictionary td = controller.TempData;
            try
            {
                using (StringWriter writer = new StringWriter())
                {
                    ViewContext viewContext = new ViewContext(controller.ControllerContext, view, vd, td, writer);
                    viewContext.View.Render(viewContext, writer);
                    System.IO.File.WriteAllText(targetPath, StringHelper.Compress(writer.ToString()), Encoding.UTF8);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}