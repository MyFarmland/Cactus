using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.Security;
using Cactus.Controllers.Expand;
using Cactus.IService;
using Cactus.Model.Sys.Enums;
using Cactus.Model.Sys;
using Cactus.Common;
using Cactus.Controllers.Filters;
using Cactus.Model.Other;

namespace Cactus.Controllers.Controllers
{

    [Exception]
    public class AdminLoginController : AdminBaseController
    {

        [HttpGet]
        public ActionResult Index()
        {
            if (this.LoginUser != null)
            {
                return Redirect("/Admin/");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Index(string VCode, string UserName, string Password)
        {
            VCode = VCode.ToLower().Trim();
            string name = "cactus_";
            VCode = name + VCode;
            //最后判断来源
            if (CryptoHelper.MD5Encrypt(VCode) == CookieHelper.GetCookieValue("code"))
            {
                User u = userServer.CheckLogin(UserName.Trim(), Password.Trim());
                if (u == null)
                {
                    return Json(new ResultModel { msg = "用户不存在", pass = false });
                }
                else
                {
                    string sid="cactus_"+StringHelper.GetGuidString();
                    CookieHelper.SetCookie("Admin", sid);
                    //更新用户登录信息
                    u.LastLoginTime = DateTime.Now;
                    u.LastLoginIp = WebHelper.GetClientIPAddress();
                    userServer.Update(u);
                    //加入缓存
                    //CacheHelper.SetCache(Constant.CacheKey.LoginAdminInfoCacheKey + "_" + sid, u);
                    base.cacheService.Add(Constant.CacheKey.LoginAdminInfoCacheKey + "_" + sid,
                        new HTools.CacheObj()
                        {
                            value = u,
                            AbsoluteExpiration = new DateTimeOffset(DateTime.Now).AddDays(1)
                        });
                    //清除验证码code
                    CookieHelper.ClearCookie("code");
                    return Json(new ResultModel { msg = "登陆成功",pass = true });
                }
            }
            else
            {
                return Json(new ResultModel { msg = "验证码错误", pass = false });
            }
        }
        [HttpGet]
        public void VCode()
        {
            string code = "";
            ValidateCode.CreateValidateGraphic(out code, 4, 140, 40, 24);
            string name = "cactus_";//需要改成配置*|*
            code = CryptoHelper.MD5Encrypt(name + code);
            CookieHelper.SetCookie("code", code);
        }
    }
}
