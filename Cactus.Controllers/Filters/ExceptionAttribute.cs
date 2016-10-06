using Cactus.Common;
using Cactus.Model.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cactus.Controllers.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled==false)
            {
                HIO.WriteLog(filterContext.Exception);
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult()
                    {
                        ContentEncoding = Encoding.UTF8,
                        Data = new ResultModel
                        {
                            pass = false,
                            msg = "Error"
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    RouteValueDictionary _dic = new RouteValueDictionary();
                    _dic.Add("controller", "Error");
                    _dic.Add("action", "Page404");
                    filterContext.Result = new RedirectToRouteResult("Default_Html", _dic);
                }

                filterContext.ExceptionHandled = true;
            }
        }
    }



}
