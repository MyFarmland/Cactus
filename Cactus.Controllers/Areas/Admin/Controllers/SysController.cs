using Cactus.Common;
using Cactus.Controllers.Expand;
using Cactus.Controllers.Filters;
using Cactus.Model.Other;
using Cactus.Model.Sys;
using Cactus.Model.Sys.Config;
using Cactus.Model.Sys.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Cactus.Controllers.Areas.Admin.Controllers
{
    [Group(Title = "系统管理", Icon = "fa-file", IsShow = true)]
    public class SysController : PowerBaseController
    {
        #region 权限查阅
        [HttpGet]
        [Power(ModuleName = "powerManage", Title = "权限查阅", IsShow = true, actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult SetPower() { return View(); }
        //[HttpPost]
        [Power(ModuleName = "powerManage", actionEnum = EnumsModel.ActionEnum.Build)]
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
                string areStr = "/Admin";
                //
                PowerAdmin _powerConf = new PowerAdmin();
                if (_powerConf.list == null)
                {
                    _powerConf.list = new List<PowerGroup>();
                }
                foreach (Type t in t_arr)//针对每个类型获取详细信息
                {

                    string className = t.Name;
                    if (className.EndsWith("Controller"))//是否控制器
                    {
                        //获取class的Attribute
                        object[] att_class = t.GetCustomAttributes(false);
                        PowerGroup _group = null;
                        //string _controller = className.Replace("Controller", "");
                        string _controller = className.Remove(className.IndexOf("Controller"), ("Controller").Length);
                        foreach (object obj in att_class)
                        {
                            Type _t = obj.GetType();
                            string t_name = _t.ToString();
                            if (t_name == _g)
                            {
                                _group = new PowerGroup();
                                _group.Name = _controller;
                                PropertyInfo p_Title = _t.GetProperty("Title");
                                object _ss = p_Title.GetValue(obj, null);
                                if (_ss != null)
                                {
                                    _group.Title = _ss.ToString();
                                }
                                PropertyInfo p_Des = _t.GetProperty("Des");
                                object _dd = p_Des.GetValue(obj, null);
                                if (_dd != null)
                                {
                                    _group.Des = _dd.ToString();
                                }
                                PropertyInfo p_IsShow = _t.GetProperty("IsShow");
                                object _cc = p_IsShow.GetValue(obj, null);
                                if (_cc != null)
                                {
                                    _group.IsShow = Convert.ToBoolean(_cc.ToString());
                                }
                                PropertyInfo p_Icon = _t.GetProperty("Icon");
                                object _oo = p_Icon.GetValue(obj, null);
                                if (_oo != null)
                                {
                                     _group.Icon = _oo.ToString();
                                }
                                break;
                            }
                        }
                        if (_group != null && _group.module == null)
                        {
                            _group.module = new List<PowerModule>();
                            List<PowerModule> modules = new List<PowerModule>();
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
                                        PowerModule _power = new PowerModule();
                                        PropertyInfo p_Name = _t.GetProperty("ModuleName");
                                        object _pp = p_Name.GetValue(obj, null);
                                        if (_pp != null)
                                        {
                                            _power.Name = _pp.ToString();
                                        }
                                        PropertyInfo p_actionEnum = _t.GetProperty("actionEnum");
                                        object _aa = p_actionEnum.GetValue(obj, null);
                                        if (_aa != null)
                                        {
                                            _power.Action_Type = _aa.ToString();
                                        }
                                        PropertyInfo p_IsShow = _t.GetProperty("IsShow");
                                        object _cc = p_IsShow.GetValue(obj, null);
                                        if (_cc != null)
                                        {
                                            _power.IsShow = Convert.ToBoolean(_cc.ToString());
                                        }
                                        PropertyInfo p_Title = _t.GetProperty("Title");
                                        object _ss = p_Title.GetValue(obj, null);
                                        if (_ss != null)
                                        {
                                            _power.Title = _ss.ToString();
                                        }
                                        PropertyInfo p_Icon = _t.GetProperty("Icon");
                                        object _oo = p_Icon.GetValue(obj, null);
                                        if (_oo != null)
                                        {
                                            _power.Icon = _oo.ToString();
                                        }
                                        _power.ParamStr = areStr + "/" + _controller + "/" + methodName;
                                        modules.Add(_power);
                                    }
                                }
                            }

                            //去重后的模块
                            Dictionary<string, PowerModule> module_dic_temp = new Dictionary<string, PowerModule>();
                            //去重后的模块Action
                            Dictionary<string, Dictionary<string, string>> moduleaction_dic = new Dictionary<string, Dictionary<string, string>>();
                            //过滤重复模块名
                            foreach (var module in modules)
                            {
                                if (module.IsShow)
                                {
                                    if (!module_dic_temp.ContainsKey(module.Name))
                                    {
                                        module_dic_temp.Add(module.Name, module);
                                    }
                                    else
                                    {
                                        module_dic_temp[module.Name] = module;
                                    }
                                }
                                else
                                {
                                    if (!module_dic_temp.ContainsKey(module.Name))
                                    {
                                        module_dic_temp.Add(module.Name, module);
                                    }
                                }

                                if (!moduleaction_dic.ContainsKey(module.Name))
                                {
                                    Dictionary<string, string> dic = new Dictionary<string, string>();
                                    dic.Add(module.Action_Type, module.Action_Type);
                                    moduleaction_dic.Add(module.Name, dic);
                                }
                                else
                                {
                                    Dictionary<string, string> dic = moduleaction_dic[module.Name];
                                    if (!dic.ContainsKey(module.Action_Type))
                                    {
                                        dic.Add(module.Action_Type, module.Action_Type);
                                    }
                                    moduleaction_dic[module.Name] = dic;
                                }
                            }
                            Dictionary<string, PowerModule> module_dic = new Dictionary<string, PowerModule>();
                            //合并操作标示
                            foreach (var dic in module_dic_temp)
                            {
                                if (moduleaction_dic.ContainsKey(dic.Key))
                                {
                                    string Action_Type = "";
                                    foreach (var m_dic in moduleaction_dic[dic.Key])
                                    {
                                        if (string.IsNullOrEmpty(Action_Type))
                                        {
                                            Action_Type += m_dic.Value;
                                        }
                                        else
                                        {
                                            Action_Type += "," + m_dic.Value;
                                        }
                                    }
                                    dic.Value.Action_Type = Action_Type;
                                    module_dic.Add(dic.Key, dic.Value);
                                }
                            }
                            //
                            _group.module = module_dic.Values.ToList();
                            _powerConf.list.Add(_group);
                        }
                    }
                }
                SerializeHelper.Serialize(_powerConf, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "PowerConfig.config"));

                if (Constant.CacheKey.List[Cactus.Model.Sys.Enums.Constant.CacheKey.PowerConfigCacheKey].Count() > 0)
                {
                    base.cacheService.Remove(Cactus.Model.Sys.Enums.Constant.CacheKey.PowerConfigCacheKey);
                }
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

        #endregion
        //初步完成
        #region 用户
        [Power(ModuleName = "userManage",Title="用户管理", IsShow=true,  actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult UserIndex()
        {
            int count = 0;
            PageTurnModel pageturn = new PageTurnModel() { ItemSize = 10 };
            pageturn.PageIndex = 1;
            var list = this.userService.ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "User_Id", out  count);
            pageturn.CountSize = count;
            ViewData["UserList"] = list;
            ViewData["Pageturn"] = pageturn;
            return View("UserList");
        }
        [Power(ModuleName = "userManage", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult UserList(int? page)
        {
            if (!page.HasValue) { page = 1; }
            PageTurnModel pageturn = new PageTurnModel() { ItemSize = 10 };
            pageturn.PageIndex = page;
            int count = 0;
            var list = this.userService.ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "User_Id", out  count).Select(t => new
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
        [HttpGet]
        [Power(ModuleName = "userManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult UserAlterFace(int id)
        {
            var act = this.userService.Find(id);
            ViewData["User"] = act;
            return View();
        }
        [HttpPost]
        [Power(ModuleName = "userManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult UserAlterFacePost(int Id)
        {
            var avatarFile = Request.Files["AvatarFile"];
            string Avatar = "";
            if (avatarFile != null && avatarFile.ContentLength > 0)
            {
                if (avatarFile.ContentLength <= this.pathConfig.dic["avatar"].FileSize * 1024)
                {
                    var avatarName = avatarFile.FileName;
                    var avatarExt = Path.GetExtension(avatarName);

                    string FileType = avatarName.Substring(avatarName.LastIndexOf(".") + 1);//取得类型
                    FileType = FileType.ToLower();

                    if (!String.IsNullOrEmpty(avatarExt)
                        && Config.ImgExtensions.Split('*').Contains(avatarExt))
                    {
                        avatarName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + FileType;
                        //保存原图
                        if (!System.IO.Directory.Exists(MyPath.TempPath))
                        {
                            System.IO.Directory.CreateDirectory(MyPath.TempPath);
                        }
                        var savePath = Path.Combine(MyPath.TempPath, avatarName);
                        avatarFile.SaveAs(savePath);
                        if (!System.IO.Directory.Exists(this.pathConfig.dic["avatar"].DirPath))
                        {
                            System.IO.Directory.CreateDirectory(this.pathConfig.dic["avatar"].DirPath);
                        }
                        //缩略图路径
                        var thumbPath = Path.Combine(this.pathConfig.dic["avatar"].DirPath, "Avatar_" + Id + avatarExt);


                        //生成头像缩略图
                        ImageHelper.MakeThumbnailImage(savePath, thumbPath, 48, 48, "HW");
                        //记得删除原图
                        //DirFileHelper.DeleteFile(savePath);
                        //System.IO.Directory.Delete(savePath,true);
                        System.IO.File.Delete(savePath);

                        Avatar = this.pathConfig.dic["avatar"].WebPath + "/" + "Avatar_" + Id + avatarExt;

                        Model.Sys.User u = this.userService.Find(Id);
                        u.Avatar = Avatar;
                        this.userService.Update(u);

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
        [Power(ModuleName = "userManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult UserResetPwd(int id)
        {
            var act = this.userService.Find(id);
            try
            {
                act.Password = CryptoHelper.MD5Encrypt("123456");
                this.userService.Update(act);
                return Json(new ResultModel { msg = "密码已经重置为：123456", pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new ResultModel { msg = "重置失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(ModuleName = "userManage", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult UserInfo(int id)
        {
            var act = this.userService.Find(id);
            ViewData["User"] = act;
            return View();
        }
        [HttpGet]
        [Power(ModuleName = "userManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult UserAdd()
        {
            ViewData["RoleList"] = this.roleService.GetAll().ToList();
            return View();
        }
        [HttpPost]
        [Power(ModuleName = "userManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult UserAdd(string UserName, string Password, string NickName, string Email, string Phone, string Qq, int RoleId)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return Json(new ResultModel { msg = "用户名为空", pass = false });
            }
            if (string.IsNullOrEmpty(Password))
            {
                return Json(new ResultModel { msg = "密码为空", pass = false });
            }
            if (string.IsNullOrEmpty(NickName))
            {
                return Json(new ResultModel { msg = "昵称为空", pass = false });
            }
            UserName = UserName.Trim();
            Password = Password.Trim();
            NickName = NickName.Trim();
            Email = Email.Trim();
            Phone = Phone.Trim();
            Qq = Qq.Trim();
            if (this.userService.IsUseName(UserName, 0))
            {
                return Json(new ResultModel { msg = "改用户名已经在使用", pass = true });
            }
            var b = this.userService.Insert(new Model.Sys.User()
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
        [Power(ModuleName = "userManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult UserUpdate(int id)
        {
            ViewData["RoleList"] = this.roleService.GetAll().ToList();
            ViewData["User"] = this.userService.Find(id);
            return View();
        }
        [HttpPost]
        [Power(ModuleName = "userManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult UserUpdate(string NickName, string Email, string Phone, string Qq, int RoleId, int User_Id)
        {
            Model.Sys.User muser = this.userService.Find(User_Id);
            if (string.IsNullOrEmpty(NickName))
            {
                return Json(new ResultModel { msg = "昵称为空", pass = false });
            }

            muser.NickName = NickName.Trim();
            muser.Email = Email.Trim();
            muser.Phone = Phone.Trim();
            muser.Qq = Qq.Trim();
            muser.RoleId = RoleId;
            try
            {
                this.userService.Update(muser);
                return Json(new ResultModel { msg = "修改成功", pass = true });
            }
            catch
            {
                return Json(new ResultModel { msg = "修改失败", pass = false });
            }
        }
        [Power(ModuleName = "userManage", actionEnum = EnumsModel.ActionEnum.Delete)]
        public ActionResult UserDelete(string ids)
        {
            try
            {
                this.userService.Delete(ids);
                return Json(new ResultModel { msg = "删除成功", pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new ResultModel { msg = "删除失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        //初步完成
        #region 角色
        [Power(ModuleName = "roleManage", Title = "角色管理", IsShow = true, actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult RoleList(int? page)
        {
            PageTurnModel pageturn = new PageTurnModel() { ItemSize = 100 };
            if (!page.HasValue)
            {
                page = 1;
                pageturn.PageIndex = page;
                int count = 0;
                var list = this.roleService
                    .ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "Role_Id", out count);
                pageturn.CountSize = count;

                ViewData["RoleList"] = list;
                return View();
            }
            else
            {
                pageturn.PageIndex = page;
                int count = 0;
                var list = this.roleService
                    .ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "Role_Id", out count);
                pageturn.CountSize = count;

                return Json(new RowResultModel { rows = list.Select(t => new { t.Role_Id, t.RoleName }), pagination = pageturn, pass = true });
            }
        }
        [HttpGet]
        [Power(ModuleName = "roleManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult RoleAdd()
        {
            return View();
        }
        
        [HttpPost]
        [Power(ModuleName = "roleManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult RoleAdd(string RoleName, string ActionIds)
        {
            if (string.IsNullOrEmpty(RoleName)) {
                return Json(new ResultModel { msg = "角色名为空", pass = false });
            }
            if (this.roleService.IsUseName(RoleName.Trim(), 0))
            {
                return Json(new ResultModel { msg = "改角色名正在使用", pass = false });
            }
            var b = this.roleService.Insert(new Model.Sys.Role()
            {
                RoleName = RoleName.Trim(),
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
        [Power(ModuleName = "roleManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult RoleUpdate(int id)
        {
            var act = this.roleService.Find(id);
            ViewData["Role"] = act;
            return View(act);
        }
        
        [HttpPost]
        [Power(ModuleName = "roleManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult RoleUpdate(string RoleName, string ActionIds,int Id)
        {
            if (string.IsNullOrEmpty(RoleName))
            {
                return Json(new ResultModel { msg = "角色名为空", pass = false });
            }
            Model.Sys.Role role = this.roleService.Find(Id);
            if (this.roleService.IsUseName(RoleName.Trim(), role.Role_Id))
            {
                return Json(new ResultModel { msg = "改角色名正在使用", pass = false });
            }
            role.RoleName = RoleName.Trim();
            role.ActionIds = ActionIds;
            try
            {
                this.roleService.Update(role);
                return Json(new ResultModel { msg = "修改成功", pass = true });
            }
            catch
            {
                return Json(new ResultModel { msg = "修改失败", pass = false });
            }
        }
        
        [HttpPost]
        [Power(ModuleName = "roleManage", actionEnum = EnumsModel.ActionEnum.Delete)]
        public ActionResult RoleDelete(string ids)
        {
            try
            {
                this.roleService.Delete(ids);
                return Json(new ResultModel { msg = "删除成功", pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new ResultModel { msg = "删除失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        //初步完成
        #region 网站参数
        [Power(ModuleName = "sysManage", IsShow = true, Title = "网站参数", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult SysIndex()
        {
            return View();
        }
        [Power(ModuleName = "sysManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult SysSave(SiteConfig site)
        {
            try
            {
                this.Config.SiteName = site.SiteName.Trim();
                this.Config.SiteTitle = site.SiteTitle.Trim();
                this.Config.SiteCrod = site.SiteCrod.Trim();
                this.Config.SiteKeywords = site.SiteKeywords;
                this.Config.SiteDescr = site.SiteDescr;
                this.Config.SiteCountCode = site.SiteCountCode;
                this.Config.SiteCopyRight = site.SiteCopyRight;
                this.Config.SiteStatus = site.SiteStatus;
                this.Config.SiteStaticDir = site.SiteStaticDir;
                this.Config.ImgExtensions = site.ImgExtensions;
                this.Config.HtmlDir = site.HtmlDir;
                this.Config.PageDir = site.PageDir;
                this.Config.PageExtension = site.PageExtension;
                string s = HttpContext.Request.Form["SiteStatus"].ToString();
                this.siteConfigService.SaveConfig(this.Config, Model.Sys.Enums.Constant.SiteConfigPath);

                if (Constant.CacheKey.List[Cactus.Model.Sys.Enums.Constant.CacheKey.SiteConfigCacheKey].Count() > 0)
                {
                    base.cacheService.Remove(Cactus.Model.Sys.Enums.Constant.CacheKey.SiteConfigCacheKey);
                }
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
        [Power(ModuleName = "sysManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult SysImage() 
        {
            return View();
        }
        [Power(ModuleName = "sysManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult SysDefaultAvatar()
        {
            var avatarFile = Request.Files["SysDefaultAvatar"];
            if (avatarFile != null)
            {
                var avatarName = avatarFile.FileName;
                var avatarExt = Path.GetExtension(avatarName);
                if (!System.IO.Directory.Exists(this.pathConfig.dic["sys"].DirPath))
                {
                    System.IO.Directory.CreateDirectory(this.pathConfig.dic["sys"].DirPath);
                }
                //保存原图
                var savePath = Path.Combine(MyPath.TempPath, "DefaultAvatar" + avatarExt);
                if (WebHelper.saveUploadFile(avatarFile, savePath, Config.ImgExtensions.Split('*'), this.pathConfig.dic["sys"].FileSize))
                {
                    var thumbPath = Path.Combine(this.pathConfig.dic["sys"].DirPath, "DefaultAvatar" + avatarExt);
                    //生成头像缩略图
                    ImageHelper.MakeThumbnailImage(savePath, thumbPath, 48, 48, "HW");
                    System.IO.File.Delete(savePath);
                    this.Config.DefaultAvatar = this.pathConfig.dic["sys"].WebPath + "/DefaultAvatar" + avatarExt;
                    this.siteConfigService.SaveConfig(this.Config, Model.Sys.Enums.Constant.SiteConfigPath);
                    if (Constant.CacheKey.List[Cactus.Model.Sys.Enums.Constant.CacheKey.SiteConfigCacheKey].Count() > 0)
                    {
                        base.cacheService.Remove(Cactus.Model.Sys.Enums.Constant.CacheKey.SiteConfigCacheKey);
                    }
                    return Json(new ResultModel { pass = true, append = new { url = this.Config.DefaultAvatar } });
                }
                else
                {
                    return Json(new ResultModel { msg = "上传文件错误,注意文件大小" + this.pathConfig.dic["sys"].FileSize + "kb以内或文件类型为" + Config.ImgExtensions, pass = false });
                }
            }
            else {
                return Json(new ResultModel { msg = "上传文件错误", pass = false });
            }
        }
        [Power(ModuleName = "sysManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult SysSiteLogo()
        {
            var avatarFile = Request.Files["SysSiteLogo"];
            if (avatarFile != null)
            {
                var avatarName = avatarFile.FileName;
                var avatarExt = Path.GetExtension(avatarName);
                if (!System.IO.Directory.Exists(this.pathConfig.dic["sys"].Name))
                {
                    System.IO.Directory.CreateDirectory(this.pathConfig.dic["sys"].Name);
                }
                var savePath = Path.Combine(this.pathConfig.dic["sys"].DirPath, "SiteLogo" + avatarExt);
                if (WebHelper.saveUploadFile(avatarFile, savePath, Config.ImgExtensions.Split('*'), this.pathConfig.dic["sys"].FileSize))
                {
                    this.Config.SiteLogo = this.pathConfig.dic["sys"].WebPath + "/SiteLogo" + avatarExt;
                    this.siteConfigService.SaveConfig(this.Config, Model.Sys.Enums.Constant.SiteConfigPath);
                    if (Constant.CacheKey.List[Cactus.Model.Sys.Enums.Constant.CacheKey.SiteConfigCacheKey].Count() > 0)
                    {
                        base.cacheService.Remove(Cactus.Model.Sys.Enums.Constant.CacheKey.SiteConfigCacheKey);
                    }
                    return Json(new ResultModel { msg = "上传成功", pass = true, append = new { url = this.Config.SiteLogo } });
                }
                else
                {
                    return Json(new ResultModel { msg = "上传文件错误,注意文件大小" + this.pathConfig.dic["sys"].FileSize + "kb以内或文件类型为" + Config.ImgExtensions, pass = false });
                }
            }
            else
            {
                return Json(new ResultModel { msg = "上传文件错误", pass = false });
            }
        }
        #endregion
        
        //初步完成
        #region 文件地址
        [Power(ModuleName = "pathManage", IsShow = true, Title = "文件地址", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult SysPath()
        {
            return View();
        }
        [Power(ModuleName = "pathManage", IsShow = false, Title = "文件地址", actionEnum = EnumsModel.ActionEnum.Edit)]
        [HttpGet]
        public ActionResult PathUpdate(string name) {
            PathModel p = new PathModel();
            if (this.pathConfig.dic.TryGetValue(name, out p))
            {
                p.DirPath = p.DirPath.Replace("/", ";");
                p.DirPath = p.DirPath.Replace("\\", ";");
                p.WebPath = p.WebPath.Replace("/", ";");
                p.WebPath = p.WebPath.Replace("\\", ";");
                ViewData["PathModel"] = p; 
                return View();
            }
            return Content("不存在");
        }
        [Power(ModuleName = "pathManage", IsShow = false, Title = "文件地址", actionEnum = EnumsModel.ActionEnum.Edit)]
        [HttpPost]
        public ActionResult PathUpdate(PathModel pm) {
            PathModel p = new PathModel();
            if (this.pathConfig.dic.TryGetValue(pm.Name, out p))
            {
                pm.DirPath = pm.DirPath.Replace(";",System.IO.Path.DirectorySeparatorChar.ToString());
                pm.WebPath = pm.WebPath.Replace(";", pathConfig.WebSeparatorChar);
                this.pathConfig.dic[pm.Name] = pm;
                this.pathConfigService.SaveConfig(this.pathConfig, Constant.PathConfigPath);
                if (Constant.CacheKey.List[Cactus.Model.Sys.Enums.Constant.CacheKey.PathConfigCacheKey].Count() > 0)
                {
                    base.cacheService.Remove(Cactus.Model.Sys.Enums.Constant.CacheKey.PathConfigCacheKey);
                }
                return Json(new ResultModel
                {
                    pass = true,
                    msg = "修改成功"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new ResultModel
                {
                    pass = true,
                    msg = "修改失败，不存在"
                }, JsonRequestBehavior.AllowGet);
            }
        
        }
        [Power(ModuleName = "pathManage", IsShow = false, Title = "文件地址", actionEnum = EnumsModel.ActionEnum.Add)]
        [HttpPost]
        public ActionResult PathAdd(PathModel pm) {
            PathModel p = new PathModel();
            if (this.pathConfig == null) {
                this.pathConfig = new PathConfig();
                if (this.pathConfig.dic == null) { this.pathConfig.dic = new SerializableDictionary<string, PathModel>(); }
            }
            if (this.pathConfig.dic.TryGetValue(pm.Name, out p))
            {
                return Json(new ResultModel
                {
                    pass = true,
                    msg = "添加失败，已经存在"
                }, JsonRequestBehavior.AllowGet);
            }
            else {
                pm.DirPath = pm.DirPath.Replace(";", System.IO.Path.DirectorySeparatorChar.ToString());
                pm.WebPath = pm.WebPath.Replace(";", pathConfig.WebSeparatorChar);
                this.pathConfig.dic.Add(pm.Name,pm);
                this.pathConfigService.SaveConfig(this.pathConfig, Constant.PathConfigPath);
                return Json(new ResultModel
                {
                    pass = true,
                    msg = "操作成功"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(ModuleName = "pathManage", IsShow = false, Title = "文件地址", actionEnum = EnumsModel.ActionEnum.Add)]
        [HttpGet]
        public ActionResult PathAdd() { return View(); }
        [Power(ModuleName = "pathManage", IsShow = false, Title = "文件地址", actionEnum = EnumsModel.ActionEnum.Delete)]
        public ActionResult PathDelete(string name) {
            PathModel p = new PathModel();
            if (this.pathConfig.dic.TryGetValue(name, out p))
            {
                this.pathConfig.dic.Remove(name);
                this.pathConfigService.SaveConfig(this.pathConfig, Constant.PathConfigPath);
                if (Constant.CacheKey.List[Cactus.Model.Sys.Enums.Constant.CacheKey.PathConfigCacheKey].Count() > 0)
                {
                    base.cacheService.Remove(Cactus.Model.Sys.Enums.Constant.CacheKey.PathConfigCacheKey);
                }
                return Json(new ResultModel
                {
                    pass = true,
                    msg = "删除成功"
                }, JsonRequestBehavior.AllowGet);
            }
            return Content("不存在");
        }

        #endregion

        #region 缓存管理
        [HttpGet]
        [Power(ModuleName = "cacheManage", IsShow = true, Title = "缓存管理", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult CacheManage()
        {
            ViewData["CacheKey"] = Constant.CacheKey.List;
            return View();
        }
        [HttpGet]
        [Power(ModuleName = "cacheManage", actionEnum = EnumsModel.ActionEnum.Delete)]
        public ActionResult CacheClear()
        {
            base.cacheService.RemoveAll();
            return Json(new ResultModel
            {
                pass = true,
                msg = "清理成功"
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Power(ModuleName = "cacheManage", actionEnum = EnumsModel.ActionEnum.Delete)]
        public ActionResult CacheClearKey(string key)
        {
            try
            {
                key = key.Trim();
                if (Constant.CacheKey.List[key].Count() > 0)
                {
                    base.cacheService.Remove(key);
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
        #endregion

        //初步完成
        #region 日志管理
        [Power(ModuleName = "logManage",IsShow=true, Title="日志管理", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult LogManager() {
            return View(); 
        }
        [Power(ModuleName = "logManage", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult LogList(int? page)
        {
            if (!page.HasValue) { page = 1; }
            PageTurnModel pageturn = new PageTurnModel() { ItemSize = 10 };
            pageturn.PageIndex = page;
            int count = 0;
            var list = this.logService.ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "Log_Id", out  count).Select(t => new
            {
                 Log_Id = t.Log_Id,
                 LogInfo = t.LogInfo,
                 UserId = t.UserId,
                 UserName = t.User.Name,
                 AddTime = StringHelper.GetTime(t.CreateTs).ToString("yyyy-MM-dd HH:mm:ss")
            });
            pageturn.CountSize = count;
            return Json(new RowResultModel { rows = list, pagination = pageturn, pass = true }, JsonRequestBehavior.AllowGet);
        }
        [Power(ModuleName = "logManage", actionEnum = EnumsModel.ActionEnum.Delete)]
        public ActionResult LogDelete(int id) {
            ResultModel _result = null;
            if (this.logService.DeteleLog(id.ToString()))
            {
                _result = new ResultModel
                {
                    msg = "删除成功",
                    pass = true
                };
            }
            else {
                  _result = new ResultModel
                {
                    msg = "删除失败",
                    pass = false
                };
            }
            
            return Json(_result, JsonRequestBehavior.AllowGet);
        }

        //[Power(ModuleName = "logManage", actionEnum = EnumsModel.ActionEnum.Show)]
        //public ActionResult LogList()
        //{
        //    if (System.IO.Directory.Exists(HIO.logDirPath))
        //    {
        //        FileInfo[] file_list = new DirectoryInfo(HIO.logDirPath).GetFiles();
        //        if (file_list.Length > 1000) {
        //            ResultModel error_result = new ResultModel
        //            {
        //                msg = "文件太多",
        //                pass = false
        //            };
        //            return Json(error_result, JsonRequestBehavior.AllowGet);
        //        }
        //        List<Model.CMS.Ext.FInfo> f_list = new List<Model.CMS.Ext.FInfo>();
        //        for (int i = 0; i < file_list.Length; i++)
        //        {
        //            f_list.Add(new Model.CMS.Ext.FInfo(file_list[i]));
        //        }
        //        ResultModel _result = new ResultModel
        //        {
        //            msg = "获取成功",
        //            pass = true,
        //            append = new { f_list = f_list }
        //        };
        //        return Json(_result, JsonRequestBehavior.AllowGet);
        //    }
        //    else {
        //        ResultModel _result = new ResultModel
        //        {
        //            msg = "获取成功",
        //            pass = false
        //        };
        //        return Json(_result, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //[Power(ModuleName = "logManage", actionEnum = EnumsModel.ActionEnum.Show)]
        //public ActionResult LogInfo(string filename) {
        //    if (filename.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
        //    {
        //        ResultModel _result = new ResultModel
        //        {
        //            msg = "文件名非法",
        //            pass = false                    
        //        };
        //        return Json(_result, JsonRequestBehavior.AllowGet);
        //    }
        //    else {
        //        string path = HIO.logDirPath + System.IO.Path.DirectorySeparatorChar + filename;
        //        if (System.IO.File.Exists(path))
        //        {
        //            var stream=new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        //            int count = 1024 * 1024;
        //            if (stream.Length > count)
        //            {
        //                stream.Dispose();
        //                ResultModel error_result = new ResultModel
        //                {
        //                    msg = "文件过大",
        //                    pass = false
        //                };
        //                return Json(error_result, JsonRequestBehavior.AllowGet);
        //            }
        //            byte[] by = new byte[stream.Length];
        //            stream.Read(by, 0, (int)stream.Length);
        //            string result = System.Text.Encoding.UTF8.GetString(by).Trim();
                    
        //            int i=result.Length;
        //            stream.Dispose();
        //            ResultModel _result = new ResultModel
        //            {
        //                msg = "获取成功",
        //                pass = true,
        //                append = new { content = result }
        //            };
        //            return Json(_result, JsonRequestBehavior.AllowGet);
        //        }
        //        else {
        //            ResultModel _result = new ResultModel
        //            {
        //                msg = "文件不存在",
        //                pass = false
        //            };
        //            return Json(_result, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //}
        //[Power(ModuleName = "logManage", actionEnum = EnumsModel.ActionEnum.Delete)]
        //public ActionResult LogClear() {
        //    Directory.Delete(HIO.logDirPath, true);
        //    Directory.CreateDirectory(HIO.logDirPath);
        //    ResultModel _result = new ResultModel
        //    {
        //        msg = "清空成功",
        //        pass = true
        //    };
        //    return Json(_result, JsonRequestBehavior.AllowGet);
        //}
        //[Power(ModuleName = "logManage", actionEnum = EnumsModel.ActionEnum.Delete)]
        //public ActionResult LogDelete(string filename)
        //{
        //    if (filename.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
        //    {
        //        ResultModel _result = new ResultModel
        //        {
        //            msg = "文件名非法",
        //            pass = false
        //        };
        //        return Json(_result, JsonRequestBehavior.AllowGet);
        //    }
        //    else {
        //        string path = HIO.logDirPath + System.IO.Path.DirectorySeparatorChar + filename;
        //        if (System.IO.File.Exists(path))
        //        {
        //            System.IO.File.Delete(path);
        //            ResultModel _result = new ResultModel
        //            {
        //                msg = "删除成功",
        //                pass = true
        //            };
        //            return Json(_result, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            ResultModel _result = new ResultModel
        //            {
        //                msg = "文件不存在",
        //                pass = false
        //            };
        //            return Json(_result, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //}
        #endregion
    }
}
