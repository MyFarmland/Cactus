using Cactus.Common;
using Cactus.Controllers.Expand;
using Cactus.Model.Other;
using Cactus.Model.Sys;
using Cactus.Model.Sys.Enums;
using System;
using System.Web.Mvc;

namespace Cactus.Controllers.Controllers
{
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
                User u = userService.CheckLogin(UserName.Trim(), Password.Trim());
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
                    userService.Update(u);
                    //加入缓存
                    base.cacheService.Add(Constant.CacheKey.LoginAdminInfoCacheKey + "_" + sid,
                        new HTools.CacheObj()
                        {
                            value = u,
                            AbsoluteExpiration = new TimeSpan(1, 0, 0, 0)
                        });
                    //清除验证码code
                    CookieHelper.ClearCookie("code");
                    base.logService.WriteLog(u.User_Id, "登录系统");
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
