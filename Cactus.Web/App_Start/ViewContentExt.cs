using System.Collections.Generic;

namespace System.Web.Mvc
{
    public static class ViewContentExt
    {
        /// <summary>
        /// 权限判断
        /// </summary>
        /// <param name="context"></param>
        /// <param name="UserActions"></param>
        /// <param name="ModuleName"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsPower(this ViewContext context, string ModuleName, Cactus.Model.Other.EnumsModel.ActionEnum action)
        {
            try {
                var loginUser = context.ViewData["_LoginUser"] as Cactus.Model.Sys.User;
                if (loginUser.IsSuperUser) { return true; }
                List<string> actions = new List<string>(loginUser.Role.ActionIds.Split(','));
                return actions.Contains(context.RouteData.Values["controller"].ToString() + "|" + ModuleName + "|" + action.ToString());
            }
            catch { return false; }
        }
    }
}