using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cactus.Common;
using Cactus.Model.Sys;
using Cactus.Model.Sys.Enums;
using Cactus.Controllers.Expand;
using Cactus.Controllers.Filters;
using Cactus.Model.Other;
using System.Reflection;

namespace Cactus.Controllers.Areas.Admin.Controllers
{
    [Exception]
    [Group(GroupName = "系统管理", NoGroupId = "Sys2016", IsShow = true, Icon = "fa-cog")]
    public class SysController : PowerBaseController
    {
        [HttpGet]
        [Power(IsSuper = true, IsShow = true,Icon="fa-edit",PowerId = "Sys_A101", PowerName = "权限设置", PowerDes = "打开权限设置页")]
        public ActionResult SetPower() { return View(); }
        [HttpPost]
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_A101", PowerName = "权限设置", PowerDes = "打开权限设置页")]
        public ActionResult SetPower(string param)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                string dir = Path.Combine(path, "bin");
                string filePath = dir + Path.DirectorySeparatorChar + param;
                byte[] filedata = System.IO.File.ReadAllBytes(filePath);
                Assembly assembly = Assembly.Load(filedata);
                Type[] t_arr = assembly.GetExportedTypes();

                Type _typePower = typeof(PowerAttribute);
                Type _typeGroup = typeof(GroupAttribute);
                string _p = _typePower.FullName;
                string _g = _typeGroup.FullName;
                //
                string areStr = "/Admin";
                //
                PowerConfig _powerConf = new PowerConfig();
                foreach (Type t in t_arr)//针对每个类型获取详细信息
                {

                    string className = t.Name;
                    if (className.EndsWith("Controller"))//是否控制器
                    {
                        //获取class的Attribute
                        object[] att_class = t.GetCustomAttributes(false);
                        PowerGroup _group = null;
                        string _controller = className.Replace("Controller", "");

                        foreach (object obj in att_class)
                        {
                            Type _t = obj.GetType();
                            string t_name = _t.ToString();
                            if (t_name == _g)
                            {
                                _group = new PowerGroup();
                                PropertyInfo p_NoGroupId = _t.GetProperty("NoGroupId");
                                object _cc = p_NoGroupId.GetValue(obj, null);
                                if (_cc != null)
                                {
                                    string _NoGroupId = _cc.ToString();
                                    _group.NoGroupId = _NoGroupId;
                                }
                                PropertyInfo p_GroupName = _t.GetProperty("GroupName");
                                object _tt = p_GroupName.GetValue(obj, null);
                                if (_tt != null)
                                {
                                    string _GroupName = _tt.ToString();
                                    _group.GroupName = _GroupName;
                                }
                                PropertyInfo p_IsShow = _t.GetProperty("IsShow");
                                object _ss = p_IsShow.GetValue(obj, null);
                                if (_ss != null)
                                {
                                    string _IsShow = _ss.ToString();
                                    _group.IsShow = Convert.ToBoolean(_IsShow);
                                }
                                PropertyInfo p_Icon = _t.GetProperty("Icon");
                                object _oo = p_Icon.GetValue(obj, null);
                                if (_oo != null)
                                {
                                    string _Icon = _oo.ToString();
                                    _group.Icon = _Icon;
                                }
                                break;
                            }
                        }
                        //获取方法信息
                        MethodInfo[] MethodInfo_arr = t.GetMethods();
                        foreach (MethodInfo m in MethodInfo_arr)
                        {
                            string methodName = m.Name;
                            object[] att_obj = m.GetCustomAttributes(false);
                            foreach (object obj in att_obj)
                            {
                                Type _t = obj.GetType();
                                string t_name = _t.ToString();
                                if (t_name == _p)
                                {
                                    Power _power = new Model.Sys.Power();
                                    PropertyInfo p_PowerId = _t.GetProperty("PowerId");
                                    object _tt = p_PowerId.GetValue(obj, null);
                                    if (_tt != null)
                                    {
                                        string _PowerId = _tt.ToString();
                                        _power.NoPowerId = _PowerId;
                                    }
                                    PropertyInfo p_PowerName = _t.GetProperty("PowerName");
                                    object _pp = p_PowerName.GetValue(obj, null);
                                    if (_pp != null)
                                    {
                                        string _PowerName = _pp.ToString();
                                        _power.PowerName = _PowerName;
                                    }
                                    PropertyInfo p_PowerDes = _t.GetProperty("PowerDes");
                                    object _ss = p_PowerDes.GetValue(obj, null);
                                    if (_ss != null)
                                    {
                                        string _PowerDes = _ss.ToString();
                                        _power.PowerDes = _PowerDes;
                                    }
                                    PropertyInfo p_IsShow = _t.GetProperty("IsShow");
                                    object _cc = p_IsShow.GetValue(obj, null);
                                    if (_cc != null)
                                    {
                                        string _IsShow = _cc.ToString();
                                        _power.IsShow = Convert.ToBoolean(_IsShow);
                                    }
                                    PropertyInfo p_Icon = _t.GetProperty("Icon");
                                    object _oo = p_Icon.GetValue(obj, null);
                                    if (_oo != null)
                                    {
                                        string _Icon = _oo.ToString();
                                        _power.Icon = _Icon;
                                    }
                                    _power.NoGroupId = _group.NoGroupId;
                                    _power.ParamStr = areStr + "/" + _controller + "/" + methodName;
                                    if (_group.PowerList == null)
                                    {
                                        _group.PowerList = new List<Power>();
                                    }
                                    if (_group.PowerList == null)
                                    {
                                        _group.PowerList = new List<Power>();
                                    }
                                    if (_group.PowerList.Count == 0 || _group.PowerList.All(p => p.NoPowerId != _power.NoPowerId))
                                    {
                                        _group.PowerList.Add(_power);
                                    }
                                }
                            }
                        }
                        if (_group != null)
                        {
                            if (_powerConf.PowerGroupList == null) { _powerConf.PowerGroupList = new List<PowerGroup>(); }
                            if (_powerConf.PowerGroupList.Count == 0
                                || _powerConf.PowerGroupList.All(g => g.NoGroupId != _group.NoGroupId))
                            {
                                _powerConf.PowerGroupList.Add(_group);
                            }
                        }
                    }
                }
                SerializeHelper.Serialize(_powerConf, AppDomain.CurrentDomain.BaseDirectory + "Configuration" + Path.AltDirectorySeparatorChar + "PowerConfig.config");
                return Json(new ResultModel
                {
                    pass = true,
                    msg = "初始化成功",
                    append = _powerConf
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new ResultModel { pass = false, msg = e.Message });
            }
        }

        //初步完成
        #region 用户
        //修改头像
        [HttpGet]
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_B101", PowerName = "修改用户头像", PowerDes = "用户头像修改")]
        public ActionResult UserAlterFace(int id)
        {
            var act = this.userServer.Find(id);
            ViewData["User"] = act;
            return View();
        }
        [HttpPost]
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_B101", PowerName = "修改用户头像", PowerDes = "用户头像修改")]
        public ActionResult UserAlterFacePost(int Id)
        {
            var avatarFile = Request.Files["AvatarFile"];
            string Avatar = "";
            if (avatarFile != null && avatarFile.ContentLength > 0)
            {
                if (avatarFile.ContentLength <= 200 * 1024)
                {
                    var avatarName = avatarFile.FileName;
                    var avatarExt = Path.GetExtension(avatarName);

                    string FileType = avatarName.Substring(avatarName.LastIndexOf(".") + 1);//取得类型
                    FileType = FileType.ToLower();

                    if (!String.IsNullOrEmpty(avatarExt)
                        && Config.ImgExtensions.Contains(avatarExt))
                    {
                        avatarName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + FileType;
                        //保存原图
                        if (!System.IO.Directory.Exists(MyPath.TempPath))
                        {
                            System.IO.Directory.CreateDirectory(MyPath.AvatarPath);
                        }
                        var savePath = Path.Combine(MyPath.TempPath, avatarName);
                        avatarFile.SaveAs(savePath);
                        if (!System.IO.Directory.Exists(MyPath.AvatarPath))
                        {
                            System.IO.Directory.CreateDirectory(MyPath.AvatarPath);
                        }
                        //缩略图路径
                        var thumbPath = Path.Combine(MyPath.AvatarPath, "Avatar_" + Id + avatarExt);


                        //生成头像缩略图
                        ImageHelper.MakeThumbnailImage(savePath, thumbPath, 48, 48, "HW");
                        //记得删除原图
                        //DirFileHelper.DeleteFile(savePath);
                        //System.IO.Directory.Delete(savePath,true);
                        System.IO.File.Delete(savePath);

                        Avatar = MyPath.Web_AvatarPath + "/" + "Avatar_" + Id + avatarExt;

                        Model.Sys.User u = this.userServer.Find(Id);
                        u.Avatar = Avatar;
                        this.userServer.Update(u);

                        return Json(new ResultModel { pass = true,msg = "上传成功", append = new { url = Avatar } });
                    }
                    else
                    {
                        return Json(new ResultModel { msg = "上传文件类型错误", pass = false });
                    }
                }
                else
                {
                    return Json(new ResultModel { msg = "上传文件大小超出限制", pass = false });
                }
            }
            else
            {
                return Json(new ResultModel { msg = "上传文件错误", pass = false });
            }
        }
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_B102", PowerName = "重置用户密码", PowerDes = "重置用户密码")]
        public ActionResult UserResetPwd(int id)
        {
            var act = this.userServer.Find(id);
            try
            {
                act.Password = CryptoHelper.MD5Encrypt("123456");
                this.userServer.Update(act);
                return Json(new ResultModel { msg = "密码已经重置为：123456", pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new ResultModel { msg = "重置失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_B102", PowerName = "查看用户信息", PowerDes = "查看用户信息")]
        public ActionResult UserInfo(int id)
        {
            var act = this.userServer.Find(id);
            ViewData["User"] = act;
            return View();
        }
        [HttpGet]
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_B103", PowerName = "添加用户", PowerDes = "添加用户")]
        public ActionResult UserAdd()
        {
            ViewData["RoleList"] = this.roleServer.GetAll().ToList();
            return View();
        }
        [HttpPost]
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_B103", PowerName = "添加用户", PowerDes = "添加用户")]
        public ActionResult UserAdd(string UserName, string Password, string NickName, string Email, string Phone, string Qq, int RoleId)
        {

            var b = this.userServer.Insert(new Model.Sys.User()
            {
                Avatar = Config.DefaultAvatar,
                Name = UserName,
                Password = CryptoHelper.MD5Encrypt(Password),
                NickName = NickName,
                Email = Email,
                Phone = Phone,
                Qq = Qq,
                AddTime = DateTime.Now,
                IsSuperUser = false,
                RoleId = RoleId,
                LastLoginTime = DateTime.Now,
                LastLoginIp = "127.0.0.1"//目前这样写
            });

            if (b)
            {
                return Json(new ResultModel { msg = "添加成功", pass = true });
            }
            else
            {
                return Json(new ResultModel { msg = "添加失败", pass = false });
            }

        }
        [HttpGet]
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_B104", PowerName = "修改用户信息", PowerDes = "修改用户信息")]
        public ActionResult UserUpdate(int id)
        {
            ViewData["RoleList"] = this.roleServer.GetAll().ToList();
            ViewData["User"] = this.userServer.Find(id);
            return View();
        }
        [HttpPost]
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_B104", PowerName = "修改用户信息", PowerDes = "修改用户信息")]
        public ActionResult UserUpdate(string NickName, string Email, string Phone, string Qq, int RoleId, int User_Id)
        {
            Model.Sys.User muser = this.userServer.Find(User_Id);

            muser.NickName = NickName;
            muser.Email = Email;
            muser.Phone = Phone;
            muser.Qq = Qq;
            muser.RoleId = RoleId;

            try
            {
                this.userServer.Update(muser);
                return Json(new ResultModel { msg = "修改成功", pass = true });
            }
            catch
            {
                return Json(new ResultModel { msg = "修改失败", pass = false });
            }
        }
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_B104", PowerName = "删除用户", PowerDes = "删除用户")]
        public ActionResult UserDelete(string ids)
        {
            try
            {
                //int[] list = Array.ConvertAll<string, int>(ids.Split(','), s => int.Parse(s));
                this.userServer.Delete(ids);
                return Json(new ResultModel { msg = "删除成功", pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new ResultModel { msg = "删除失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(IsSuper = true, IsShow = true, PowerId = "Sys_B105", PowerName = "用户列表", PowerDes = "查看用户列表")]
        public ActionResult UserIndex()
        {
            int count = 0;
            PageTurnModel pageturn = new PageTurnModel() { ItemSize = 10 };
            pageturn.PageIndex = 1;
            var list = this.userServer.ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "User_Id", out  count);
            pageturn.CountSize = count;
            ViewData["UserList"] = list;
            ViewData["Pageturn"] = pageturn;
            return View("UserList");
        }
        [Power(IsSuper = true, IsShow = true, PowerId = "Sys_B105", PowerName = "用户列表", PowerDes = "查看用户列表")]
        public ActionResult UserList(int? page)
        {
            if (!page.HasValue) { page = 1; }
            PageTurnModel pageturn = new PageTurnModel() {  ItemSize=10 };
            pageturn.PageIndex = page;
            int count = 0;
            var list = this.userServer.ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "User_Id", out  count).Select(t => new
            {
                t.User_Id,
                t.Name,
                t.NickName,
                t.Role.RoleName,
                AddTime = t.AddTime.ToString("yyyy-MM-dd HH:mm:ss"),
                LastLoginTime = t.LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss")
            });
            pageturn.CountSize = count;
            return Json(new RowResultModel { rows = list, pagination = pageturn, pass = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //初步完成
        #region 角色

        [HttpGet]
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_C101", PowerName = "添加角色", PowerDes = "添加角色")]
        public ActionResult RoleAdd()
        {
            return View();
        }
        
        [HttpPost]
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_C101", PowerName = "添加角色", PowerDes = "添加角色")]
        public ActionResult RoleAdd(string RoleName, string ActionIds)
        {
            var b = this.roleServer.Insert(new Model.Sys.Role()
            {
                RoleName = RoleName,
                ActionIds = ActionIds
            });

            if (b)
            {
                return Json(new ResultModel { msg = "添加成功", pass = true });
            }
            else
            {
                return Json(new ResultModel { msg = "添加失败", pass = false });
            }
        }
        
        [HttpGet]
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_C102", PowerName = "更新角色信息", PowerDes = "更新角色信息")]
        public ActionResult RoleUpdate(int id)
        {
            var act = this.roleServer.Find(id);
            ViewData["Role"] = act;
            return View(act);
        }
        
        [HttpPost]
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_C102", PowerName = "更新角色信息", PowerDes = "更新角色信息")]
        public ActionResult RoleUpdate(string RoleName, string ActionIds,int Id)
        {
            Model.Sys.Role maction = new Model.Sys.Role()
            {
                RoleName = RoleName,
                Role_Id = Id,
                ActionIds = ActionIds
            };
            
            try
            {
                this.roleServer.Update(maction);
                return Json(new ResultModel { msg = "修改成功", pass = true });
            }
            catch
            {
                return Json(new ResultModel { msg = "修改失败", pass = false });
            }
        }
        
        [HttpPost]
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_C103", PowerName = "删除角色", PowerDes = "删除角色")]
        public ActionResult RoleDelete(string ids)
        {
            try
            {
                //int[] list = Array.ConvertAll<string, int>(ids.Split(','), s => int.Parse(s));
                this.roleServer.Delete(ids);
                return Json(new ResultModel { msg = "删除成功", pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new ResultModel { msg = "删除失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(IsSuper = true, IsShow = true, PowerId = "Sys_C104", PowerName = "角色列表", PowerDes = "查看角色列表")]
        public ActionResult RoleList(int? page)
        {
            PageTurnModel pageturn = new PageTurnModel() { ItemSize = 100 };
            if (!page.HasValue)
            {
                page = 1;
                pageturn.PageIndex = page;
                int count = 0;
                var list = this.roleServer
                    .ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "Role_Id", out count);
                pageturn.CountSize = count;

                ViewData["RoleList"] = list;
                return View();
            }
            else {
                pageturn.PageIndex = page;
                int count = 0;
                var list = this.roleServer
                    .ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "Role_Id", out count);
                pageturn.CountSize = count;

                return Json(new RowResultModel { rows = list.Select(t => new { t.Role_Id, t.RoleName }), pagination = pageturn, pass = true });
            }
        }
        #endregion

        //初步完成
        #region 网站参数

        [Power(IsSuper = true, IsShow = true, PowerId = "Sys_D101", PowerName = "网站设置", PowerDes = "网站设置")]
        public ActionResult SysIndex()
        {
            return View();
        }
        [Power(IsSuper = true, IsShow = true, PowerId = "Sys_D101", PowerName = "网站设置", PowerDes = "网站设置")]
        public ActionResult SysSave(SiteConfig site)
        {
            try
            {
                this.Config.SiteName = site.SiteName;
                this.Config.SiteTitle = site.SiteTitle;
                this.Config.SiteCrod = site.SiteCrod;
                this.Config.SiteKeywords = site.SiteKeywords;
                this.Config.SiteDescr = site.SiteDescr;
                this.Config.SiteCountCode = site.SiteCountCode;
                this.Config.SiteCopyRight = site.SiteCopyRight;
                this.Config.SiteStatus = site.SiteStatus;
                this.Config.SiteStaticDir = site.SiteStaticDir;
                string s = HttpContext.Request.Form["SiteStatus"].ToString();
                this.siteConfigService.SaveConfig(this.Config, Model.Sys.Enums.Constant.SiteConfigPath);

                CacheHelper.RemoveCache(Constant.CacheKey.SiteConfigCacheKey);
                return Json(new ResultModel
                {
                    pass = true,
                    msg = "操作成功"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e){
                return Json(new ResultModel
                {
                    pass = false,
                    msg = e.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(IsSuper = true, IsShow = true, PowerId = "Sys_D102", PowerName = "基本图片", PowerDes = "网站基本图片")]
        public ActionResult SysImage() 
        {
            return View();
        }
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_D103", PowerName = "上传默认头像", PowerDes = "上传默认用户头像")]
        public ActionResult SysDefaultAvatar()
        {
            var avatarFile = Request.Files["SysDefaultAvatar"];
            if (avatarFile != null)
            {
                var avatarName = avatarFile.FileName;
                var avatarExt = Path.GetExtension(avatarName);
                if (!System.IO.Directory.Exists(MyPath.SysPath))
                {
                    System.IO.Directory.CreateDirectory(MyPath.SysPath);
                }
                //保存原图
                var savePath = Path.Combine(MyPath.TempPath, "DefaultAvatar" + avatarExt);
                if (WebHelper.saveUploadFile(avatarFile, savePath, Config.ImgExtensions, MyPath.fileSize))
                {
                    var thumbPath = Path.Combine(MyPath.SysPath, "DefaultAvatar" + avatarExt);
                    //生成头像缩略图
                    ImageHelper.MakeThumbnailImage(savePath, thumbPath, 48, 48, "HW");
                    System.IO.File.Delete(savePath);
                    this.Config.DefaultAvatar = MyPath.Web_SysPath + "/DefaultAvatar" + avatarExt;
                    this.siteConfigService.SaveConfig(this.Config, Model.Sys.Enums.Constant.SiteConfigPath);
                    if (Constant.CacheKey.List[Constant.CacheKey.SiteConfigCacheKey].Count() > 0)
                    {
                        HttpRuntime.Cache.Remove(Constant.CacheKey.SiteConfigCacheKey);
                    }
                    return Json(new ResultModel { pass = true, append = new { url = this.Config.DefaultAvatar } });
                }
                else
                {
                    return Json(new ResultModel { msg = "上传文件错误,注意文件大小" + MyPath.fileSize + "kb以内或文件类型为" + Config.ImgExtensions, pass = false });
                }
            }
            else {
                return Json(new ResultModel { msg = "上传文件错误", pass = false });
            }
        }
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_D104", PowerName = "上传网站Logo", PowerDes = "上传网站")]
        public ActionResult SysSiteLogo()
        {
            var avatarFile = Request.Files["SysSiteLogo"];
            if (avatarFile != null)
            {
                var avatarName = avatarFile.FileName;
                var avatarExt = Path.GetExtension(avatarName);
                if (!System.IO.Directory.Exists(MyPath.SysPath))
                {
                    System.IO.Directory.CreateDirectory(MyPath.SysPath);
                }
                var savePath = Path.Combine(MyPath.SysPath, "SiteLogo" + avatarExt);
                if (WebHelper.saveUploadFile(avatarFile, savePath, Config.ImgExtensions, MyPath.fileSize))
                {
                    this.Config.SiteLogo = MyPath.Web_SysPath + "/SiteLogo" + avatarExt;
                    this.siteConfigService.SaveConfig(this.Config, Model.Sys.Enums.Constant.SiteConfigPath);
                    return Json(new ResultModel { msg = "上传成功", pass = true, append = new { url = this.Config.SiteLogo } });
                }
                else
                {
                    return Json(new ResultModel { msg = "上传文件错误,注意文件大小" + MyPath.fileSize + "kb以内或文件类型为" + Config.ImgExtensions, pass = false });
                }
            }
            else
            {
                return Json(new ResultModel { msg = "上传文件错误", pass = false });
            }
        }
        //清理全部缓存
        [HttpGet]
        [Power(IsSuper = true, IsShow = true, PowerId = "Sys_D105", PowerName = "网站缓存", PowerDes = "网站缓存")]
        public ActionResult CacheManage()
        {
            ViewData["CacheKey"] = Constant.CacheKey.List;
            return View();
        }
        [HttpGet]
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_D106", PowerName = "清除网站缓存", PowerDes = "清除网站缓存")]
        public ActionResult CacheClear()
        {
            //清理全局缓存
            List<string> keys = new List<string>();
            //检索应用程序缓存计数器
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            //得到所有的键值
            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }
            // 删除对应缓存
            for (int i = 0; i < keys.Count; i++)
            {
                HttpRuntime.Cache.Remove(keys[i]);
            }
            CacheHelper.RemoveAllCache();
            return Json(new ResultModel
            {
                pass = true,
                msg = "清理成功"
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_D106", PowerName = "清除网站缓存", PowerDes = "清除网站缓存")]
        public ActionResult CacheClearKey(string key)
        {
            try
            {
                if (Constant.CacheKey.List[key].Count() > 0)
                {
                    CacheHelper.RemoveCache(key);
                    return Json(new ResultModel
                    {
                        pass = true,
                        msg = "清理成功"
                    }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new ResultModel
                    {
                        pass = false,
                        msg = "清理失败"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception e) {
                return Json(new ResultModel
                {
                    pass = false,
                    msg = e.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_D106", PowerName = "清除网站缓存", PowerDes = "清除网站缓存")]
        public ActionResult CacheClearType(int type) {
            try
            {
                if (type == 3)
                {
                    //清理全局缓存
                    List<string> keys = new List<string>();
                    //检索应用程序缓存计数器
                    IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
                    //得到所有的键值
                    while (enumerator.MoveNext())
                    {
                        keys.Add(enumerator.Key.ToString());
                    }
                    // 删除对应缓存
                    for (int i = 0; i < keys.Count; i++)
                    {
                        HttpRuntime.Cache.Remove(keys[i]);
                    }
                }
                else if (type < 3)
                {
                    CacheHelper.RemoveAllCache(type);
                }
                return Json(new ResultModel
                {
                    pass = true,
                    msg = "清理成功"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new ResultModel
                {
                    pass = false,
                    msg = e.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_D106", PowerName = "清除网站缓存", PowerDes = "清除网站缓存")]
        public ActionResult CacheClearYesterday()
        {
            CacheHelper.RemoveAllCache(DateTime.Now.AddDays(-1),2);
            return Json(new ResultModel
            {
                pass = true,
                msg = "清理成功"
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 日志管理
        [Power(IsSuper = true, IsShow = true, PowerId = "Sys_E101", PowerName = "日志管理", PowerDes = "Error日志管理")]
        public ActionResult LogManager() {
            return View(); 
        }
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_E101", PowerName = "日志管理", PowerDes = "Error日志管理")]
        public ActionResult LogList()
        {
            if (System.IO.Directory.Exists(HIO.logDirPath))
            {
                FileInfo[] file_list = new DirectoryInfo(HIO.logDirPath).GetFiles();
                if (file_list.Length > 1000) {
                    ResultModel error_result = new ResultModel
                    {
                        msg = "文件太多",
                        pass = false
                    };
                    return Json(error_result, JsonRequestBehavior.AllowGet);
                }
                List<Model.CMS.Ext.FInfo> f_list = new List<Model.CMS.Ext.FInfo>();
                for (int i = 0; i < file_list.Length; i++)
                {
                    f_list.Add(new Model.CMS.Ext.FInfo(file_list[i]));
                }
                ResultModel _result = new ResultModel
                {
                    msg = "获取成功",
                    pass = true,
                    append = new { f_list = f_list }
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
            else {
                ResultModel _result = new ResultModel
                {
                    msg = "获取成功",
                    pass = false
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_E102", PowerName = "日志内容", PowerDes = "日志内容")]
        public ActionResult LogInfo(string filename) {
            if (filename.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
            {
                ResultModel _result = new ResultModel
                {
                    msg = "文件名非法",
                    pass = false                    
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
            else {
                string path = HIO.logDirPath + System.IO.Path.DirectorySeparatorChar + filename;
                if (System.IO.File.Exists(path))
                {
                    var stream=new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    int count = 1024 * 1024;
                    if (stream.Length > count)
                    {
                        stream.Dispose();
                        ResultModel error_result = new ResultModel
                        {
                            msg = "文件过大",
                            pass = false
                        };
                        return Json(error_result, JsonRequestBehavior.AllowGet);
                    }
                    byte[] by = new byte[stream.Length];
                    stream.Read(by, 0, (int)stream.Length);
                    string result = System.Text.Encoding.UTF8.GetString(by).Trim();
                    
                    int i=result.Length;
                    stream.Dispose();
                    ResultModel _result = new ResultModel
                    {
                        msg = "获取成功",
                        pass = true,
                        append = new { content = result }
                    };
                    return Json(_result, JsonRequestBehavior.AllowGet);
                }
                else {
                    ResultModel _result = new ResultModel
                    {
                        msg = "文件不存在",
                        pass = false
                    };
                    return Json(_result, JsonRequestBehavior.AllowGet);
                }
            }
        }
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_E103", PowerName = "清空日志", PowerDes = "清空日志")]
        public ActionResult LogClear() {
            Directory.Delete(HIO.logDirPath, true);
            Directory.CreateDirectory(HIO.logDirPath);
            ResultModel _result = new ResultModel
            {
                msg = "清空成功",
                pass = true
            };
            return Json(_result, JsonRequestBehavior.AllowGet);
        }
        [Power(IsSuper = true, IsShow = false, PowerId = "Sys_E104", PowerName = "删除日志", PowerDes = "删除日志")]
        public ActionResult LogDelete(string filename)
        {
            if (filename.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
            {
                ResultModel _result = new ResultModel
                {
                    msg = "文件名非法",
                    pass = false
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
            else {
                string path = HIO.logDirPath + System.IO.Path.DirectorySeparatorChar + filename;
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    ResultModel _result = new ResultModel
                    {
                        msg = "删除成功",
                        pass = true
                    };
                    return Json(_result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ResultModel _result = new ResultModel
                    {
                        msg = "文件不存在",
                        pass = false
                    };
                    return Json(_result, JsonRequestBehavior.AllowGet);
                }
            }
        }
        #endregion
    }
}
