using Cactus.Controllers.Expand;
using Cactus.Controllers.Filters;
using Cactus.IService.CMS;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cactus.Model.Other;
using Cactus.Model.Sys.Enums;
using Cactus.Common;
using System.IO;
using System.Collections.Generic;
using Cactus.Model.CMS;
using System.Text;
using Newtonsoft.Json.Linq;
using Cactus.IService;
using Cactus.Model.Sys;

namespace Cactus.Controllers.Areas.Admin.Controllers
{
    [Group(Title = "文章管理", Icon = "fa-file", IsShow = true)]
    public class BlogController : PowerBaseController
    {
        public IArticleService articleService = IocHelper.AutofacResolveNamed<IArticleService>("CMS.ArticleService");
        public IColumnService columnService = IocHelper.AutofacResolveNamed<IColumnService>("CMS.ColumnService");
        public ITempPageService tempPageService = IocHelper.AutofacResolveNamed<ITempPageService>("CMS.TempPageService");
        public IStaticPageService staticPageService = IocHelper.AutofacResolveNamed<IStaticPageService>("CMS.StaticPageService");
        public ITagService tagService = IocHelper.AutofacResolveNamed<ITagService>("CMS.TagService");
        public ICommentService commentService = IocHelper.AutofacResolveNamed<ICommentService>("CMS.CommentService");
        public IThemeConfigService themeConfigService = IocHelper.AutofacResolveNamed<IThemeConfigService>("CMS.ThemeConfigService");
        //
        // GET: /Admin/Blog/
        #region Blog
        [Power(ModuleName = "blogManage", Title = "文章管理",actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult BlogIndex()
        {
            return View();
        }
        [Power(ModuleName = "blogManage", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult BlogList(int? page, string title)
        {
            if (!page.HasValue) { page = 1; }
            PageTurnModel pageturn = new PageTurnModel() { ItemSize = 10 };
            pageturn.PageIndex = page;
            int count = 0;
            var list = this.articleService.ToSearchList(pageturn.PageIndex.Value, pageturn.ItemSize, title, 1, out  count).Select(t => new
            {
                t.Article_Id,
                t.Title,
                t.Column.ColumnName,
                LastTime = t.LastTime.ToString("yyyy-MM-dd HH:mm:ss"),
                CreateTime = t.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                t.IsShow,
                t.IsTop
            });
            pageturn.CountSize = count;
            return Json(new RowResultModel { rows = list, pagination = pageturn, pass = true }, JsonRequestBehavior.AllowGet);
        }
        [Power(ModuleName = "blogManage",  actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult BlogByColumnId(int? ColumnId)
        {
            if (!ColumnId.HasValue) { ColumnId = 0; }
            var col = this.columnService.Find(ColumnId.Value);
            if (col != null) { ViewData["Column"] = col; }
            return View();
        }
        [Power(ModuleName = "blogManage", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult BlogListByColumnId(int ColumnId, string title, int? page)
        {
            if (!page.HasValue) { page = 1; }
            PageTurnModel pageturn = new PageTurnModel() { ItemSize = 10 };
            pageturn.PageIndex = page;
            int count = 0;
            //(int pageIndex, int pageSize, string where, string keySelector, out int count)
            var list = this.articleService.ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "where ColumnId=" + ColumnId, " CreateTime DESC", out  count).Select(t => new
            {
                t.Article_Id,
                t.Title,
                t.Column.ColumnName,
                LastTime = t.LastTime.ToString("yyyy-MM-dd HH:mm:ss"),
                CreateTime = t.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                t.IsShow,
                t.IsTop
            });
            pageturn.CountSize = count;
            return Json(new RowResultModel { rows = list, pagination = pageturn, pass = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Power(ModuleName = "blogManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult BlogAdd() {
            ViewData["ColumnList"] = this.columnService.GetAll();
            return View();
        }
        [Power(ModuleName = "blogManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult BlogAddByColumnId(int ColumnId)
        {
            var col = this.columnService.Find(ColumnId);
            var colList =new List<Cactus.Model.CMS.Column>();
            colList.Add(col);
            colList.AddRange(this.columnService.FindByPid(ColumnId));
            ViewData["ColumnList"] = colList;
            return View();
        }
        [HttpPost]
        [Power(ModuleName = "blogManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult BlogAdd(Model.CMS.Article art)
        {
            art.Title = art.Title.Trim();
            if (this.articleService.IsUseTitle(art.Title, 0))
            {
                return Json(new ResultModel { msg = "标题与其他文章相同", pass = false });
            }
            else
            {
                art.LastTime = DateTime.Now;
                art.CreateTime = art.LastTime;
                art.Title = art.Title.Trim();
                int aid = this.articleService.InsertForInt(art);
                if (string.IsNullOrEmpty(art.Tags) == false)
                {
                    bool b = tagService.InsertToMapBatch(aid, art.TagIds);
                    if (b == false)
                    {
                        return Json(new ResultModel { msg = "插入失败", pass = true });
                    }
                }
                if (aid > 0)
                {
                    return Json(new ResultModel { msg = "添加成功", pass = true });
                }
                else
                {
                    return Json(new ResultModel { msg = "添加失败", pass = false });
                }
            }
        }
        [HttpGet]
        [Power(ModuleName = "blogManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult BlogUpdate(int id)
        {
            Model.CMS.Article art = this.articleService.Find(id);
            art.ArticleContent = System.Web.HttpUtility.HtmlDecode(art.ArticleContent);
            ViewData["Blog"] = art;
            var colList = this.columnService.FindByPid(art.Column.Pid);
            ViewData["ColumnList"] = colList;
            return View();  
        }
        [HttpPost]
        [Power(ModuleName = "blogManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult BlogUpdate(Model.CMS.Article art)
        {
            try
            {
                art.Title = art.Title.Trim();
                if (this.articleService.IsUseTitle(art.Title, art.Article_Id))
                {
                    return Json(new ResultModel { msg = "标题重复", pass = false });
                }
                else
                {
                    Model.CMS.Article model = this.articleService.Find(art.Article_Id);
                    model.Article_Id = art.Article_Id;
                    model.ArticleContent = art.ArticleContent;
                    model.Author = art.Author;
                    model.ColumnId = art.ColumnId;
                    if (art.Tags != model.Tags) 
                    {
                        tagService.DeteteToMap(art.Article_Id);
                        bool b=tagService.InsertToMapBatch(art.Article_Id,art.TagIds);
                        if (b == false) {
                            return Json(new ResultModel { msg = "插入失败", pass = true });
                        }
                    }
                    model.Tags = art.Tags;
                    model.TagIds = art.TagIds;
                    model.Title = art.Title;
                    model.IsShow = art.IsShow;
                    model.IsTop = art.IsTop;
                    model.LastTime = DateTime.Now;
                    model.ImgUrl = art.ImgUrl;
                    model.Digest = art.Digest;
                    model.SEO_DES = art.SEO_DES;
                    model.SEO_Title = art.SEO_Title;
                    model.SEO_Keywords = art.SEO_Keywords;
                    this.articleService.Update(model);
                    return Json(new ResultModel { msg = "修改成功", pass = true });
                }
            }
            catch
            {
                return Json(new ResultModel { msg = "修改失败", pass = false });
            }
        }
        [Power(ModuleName = "blogManage", actionEnum = EnumsModel.ActionEnum.Delete)]
        public ActionResult BlogDelete(string ids)
        {
            try
            {
                this.articleService.Delete(ids);
                return Json(new ResultModel { msg = "删除成功", pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new ResultModel { msg = "删除失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(ModuleName = "blogManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult BlogIsTop(int id, bool on_off)
        {
            try
            {
                this.articleService.IsTop(id, on_off);
                return Json(new ResultModel { msg = "置顶成功", pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new ResultModel { msg = "置顶失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(ModuleName = "blogManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult BlogIsShow(int id,bool on_off)
        {
            try
            {
                this.articleService.IsShow(id, on_off);
                return Json(new ResultModel { msg = "操作成功",pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new ResultModel { msg = "操作失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(ModuleName = "columnManage", Title = "栏目管理", IsShow = true, actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult BlogColumnManage()
        {
            ViewData["ColumnList"] = this.columnService.GetAll();
            return View();
        }
        [HttpGet]
        [Power(ModuleName = "columnManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult ColumnAdd()
        {
            return View();
        }
        [Power(ModuleName = "columnManage", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult ColumnAddByColumnId(int ColumnId)
        {
            var column = this.columnService.Find(ColumnId);
            ViewData["Column"] = column;
            return View();
        }
        [HttpPost]
        [Power(ModuleName = "columnManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult ColumnAdd(Model.CMS.Column column)
        {
            if (column.Pid > 0)
            {
                var col = this.columnService.Find(column.Pid);
                if (col == null)
                {
                    column.Pid = 0;
                    column.Lv = 1;
                }
                else
                {
                    column.Lv = col.Lv + 1 ;
                }
            }
            else { column.Pid = 0; column.Lv = 1; }
            column.ColumnName = column.ColumnName.Trim();
            if (!this.columnService.IsUseColumnName(column.ColumnName,0))
            {
                column.ColumnName = column.ColumnName.Trim();
                var b = this.columnService.Insert(column);
                if (b)
                {
                    return Json(new ResultModel { msg = "添加成功", pass = true });
                }
                else
                {
                    return Json(new ResultModel { msg = "添加失败", pass = false });
                }
            }
            else { return Json(new ResultModel { msg = "类目名已经存在", pass = false }); }
        }
        [HttpGet]
        [Power(ModuleName = "columnManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult ColumnUpdate(int ColumnId)
        {
            ViewData["Column"] = this.columnService.Find(ColumnId);
            return View();    
        }
        [HttpPost]
        [Power(ModuleName = "columnManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult ColumnUpdate(Model.CMS.Column column)
        {
            try
            {
                column.ColumnName = column.ColumnName.Trim();
                if (this.columnService.IsUseColumnName(column.ColumnName, column.Column_Id))
                {
                    return Json(new ResultModel { msg = "栏目名重复", pass = false });
                }
                else {
                    column.ColumnName = column.ColumnName.Trim();
                    this.columnService.Update(column);
                    return Json(new ResultModel { msg = "修改成功", pass = true });
                }
            }
            catch
            {
                return Json(new ResultModel { msg = "修改失败", pass = false });
            }
        }
        [HttpPost]
        [Power(ModuleName = "columnManage", actionEnum = EnumsModel.ActionEnum.Delete)]
        public ActionResult ColumnDelete(string ids)
        {
            try
            {
                int id = Convert.ToInt32(ids);
                if (this.articleService.IsUseColumn(id))
                {
                    return Json(new ResultModel { msg = "删除失败,该栏目正在使用", pass = false }, JsonRequestBehavior.AllowGet);
                }
                else {
                    this.columnService.Delete(ids);
                    return Json(new ResultModel { msg = "删除成功",pass = true }, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch
            {
                return Json(new ResultModel { msg = "删除失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 全站静态
        
        #endregion

        #region UploadImg
        [Power(ModuleName = "blogManage", actionEnum = EnumsModel.ActionEnum.Upload)]
        public ActionResult UploadImg()
        {
            HttpFileCollectionBase files = Request.Files;
            if (files.Count <= 0)
            {
                return Content("error|file is null");
            }
            HttpPostedFileBase file = files[0];
            if (file == null)
            {
                return Content("error|file is null");
            }
            else
            {
                var FileName = file.FileName;
                var Ext = Path.GetExtension(FileName);
                string currentFileName = StringHelper.GetTimeStamp() + Ext;
                if (!System.IO.Directory.Exists(this.pathConfig.dic["edit"].DirPath))
                {
                    System.IO.Directory.CreateDirectory(this.pathConfig.dic["edit"].DirPath);
                }

                var targetPath = Path.Combine(this.pathConfig.dic["edit"].DirPath, DateTime.Now.ToString("yyyyMMdd"));
                if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }
                var savePath = Path.Combine(targetPath, currentFileName);

                if (WebHelper.saveUploadFile(file, savePath, Config.ImgExtensions.Split('*'), this.pathConfig.dic["edit"].FileSize))
                {
                    //获取图片url地址
                    string imgUrl = this.pathConfig.dic["edit"].WebPath+ "/" + DateTime.Now.ToString("yyyyMMdd") + "/" + currentFileName;
                    return Content(imgUrl);
                }
                else {
                    return Content("error|save file is error");
                }
            }
        }
        [Power(ModuleName = "blogManage", actionEnum = EnumsModel.ActionEnum.Upload)]
        public ActionResult UploadImg2() {
            var coverFile = Request.Files["BlogImg"];
            if (coverFile != null)
            {
                var FileName = coverFile.FileName;
                var Ext = Path.GetExtension(FileName);
                if (!System.IO.Directory.Exists(this.pathConfig.dic["cover"].DirPath))
                {
                    System.IO.Directory.CreateDirectory(this.pathConfig.dic["cover"].DirPath);
                }
                var currentFileName=DateTime.Now.ToString("yyyyMMddHHmmss")+Ext;
                var targetPath = Path.Combine(this.pathConfig.dic["cover"].DirPath, DateTime.Now.ToString("yyyyMMdd"));
                if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }
                var savePath = Path.Combine(targetPath, currentFileName);
                if (WebHelper.saveUploadFile(coverFile, savePath, Config.ImgExtensions.Split('*'), this.pathConfig.dic["cover"].FileSize))
                {
                    return Json(new ResultModel { msg = "上传成功", pass = true, append = new { url = this.pathConfig.dic["cover"].WebPath + "/" + DateTime.Now.ToString("yyyyMMdd") + "/" + currentFileName } });
                }
                else
                {
                    return Json(new ResultModel { msg = "上传文件错误,注意文件大小" + this.pathConfig.dic["cover"].FileSize + "kb以内或文件类型为" + Config.ImgExtensions, pass = false });
                }
            }
            else
            {
                return Json(new ResultModel { msg = "上传文件错误", pass = false });
            }
        }
        #endregion

        #region 文件管理
        [Power(ModuleName = "fileManage",IsShow=true,Title="文件管理", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult FileManage()
        {
            string _root = MyPath.AppPath + "File";//文件主目录（根目录）
            if (Directory.Exists(_root)==false) {
                Directory.CreateDirectory(_root);
            }
            return View();
        }
        [Power(ModuleName = "fileManage", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult FileList(string currentDir,string viewDir)
        {
            currentDir = currentDir.Trim();
            string _root = MyPath.AppPath + "File";//文件主目录（根目录）
            string target = "";
            viewDir = viewDir.Trim();
            if (string.IsNullOrEmpty(viewDir)) {
                target = _root + Path.DirectorySeparatorChar + currentDir;//目标目录
            } 
            else {
                target = _root + Path.DirectorySeparatorChar + currentDir + Path.DirectorySeparatorChar + viewDir;//目标目录
            }
            string fullpath = "";
            try
            {
                fullpath = Path.GetFullPath(target);//完整绝对路径
            }
            catch(Exception e){
                //防止出现权限等文件常识性错误
                ResultModel _result = new ResultModel
                {
                    msg = "获取失败," + e.Message,
                    pass = false
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
            bool isRoot = false;
            if (fullpath.StartsWith(_root))//满足根目录（不允许访问其他目录）
            {
                if (_root == fullpath) {
                    isRoot = true;
                }
                else if ((_root+@"\") == fullpath)
                {
                    isRoot = true;
                }
                string lastPath = fullpath.Remove(0, _root.Length);
                if (System.IO.File.Exists(fullpath))//防止访问的是一个单独文件
                {
                    ResultModel _result = new ResultModel
                    {
                        msg = "获取失败,参数错误,不允许访问单独文件",
                        pass = false,
                        append = new { dir_list = "", f_list = "", isRoot = isRoot, RootPath = isRoot ? Path.DirectorySeparatorChar.ToString() : lastPath }
                    };
                    return Json(_result, JsonRequestBehavior.AllowGet);
                }
                if (Directory.Exists(fullpath))
                {
                    DirectoryInfo[] dir_list = new DirectoryInfo(fullpath).GetDirectories();
                    List<Model.CMS.Ext.DInfo> d_list = new List<Model.CMS.Ext.DInfo>();
                    for (int i = 0; i < dir_list.Length; i++)
                    {
                        d_list.Add(new Model.CMS.Ext.DInfo(dir_list[i]));
                    }
                    FileInfo[] file_list = new DirectoryInfo(fullpath).GetFiles();
                    List<Model.CMS.Ext.FInfo> f_list = new List<Model.CMS.Ext.FInfo>();
                    for (int i = 0; i < file_list.Length; i++)
                    {
                        f_list.Add(new Model.CMS.Ext.FInfo(file_list[i]));
                    }
                    ResultModel _result = new ResultModel
                    {
                        msg = "获取成功",
                        pass = true,
                        append = new { dir_list = d_list, f_list = f_list, isRoot = isRoot, RootPath = isRoot ? Path.DirectorySeparatorChar.ToString() : lastPath }
                    };
                    return Json(_result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ResultModel _result = new ResultModel
                    {
                        msg = "获取成功",
                        pass = true,
                        append = new { dir_list = new { }, f_list = new { }, isRoot = isRoot, RootPath = isRoot ? Path.DirectorySeparatorChar.ToString() : lastPath }
                    };
                    return Json(_result, JsonRequestBehavior.AllowGet);
                }
            }
            else {
                ResultModel _result = new ResultModel
                {
                    msg = "获取失败,参数错误，不能访问其他目录",
                    pass = false,
                    append = new { dir_list = new { }, f_list = new { }, isRoot = isRoot, RootPath = fullpath }
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(ModuleName = "fileManage", actionEnum = EnumsModel.ActionEnum.Delete)]
        public ActionResult FileDel(string currentDir, string fileOrDir)
        {
            currentDir = currentDir.Trim();
            fileOrDir = fileOrDir.Trim();
            if (string.IsNullOrEmpty(fileOrDir))
            {
                ResultModel _result = new ResultModel
                {
                    msg = "参数错误",
                    pass = false
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
            string _root = MyPath.AppPath + "File";//文件主目录（根目录）
            string target = _root + Path.DirectorySeparatorChar + currentDir + Path.DirectorySeparatorChar + fileOrDir;//目标目录或者文件
            string fullpath = "";
            try
            {
                fullpath = Path.GetFullPath(target);//完整绝对路径
            }
            catch (Exception e)
            {
                //防止出现权限等文件常识性错误
                ResultModel _result = new ResultModel
                {
                    msg = "参数错误," + e.Message,
                    pass = false
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
            if (fullpath.StartsWith(_root))//满足根目录（不允许访问其他目录）
            {
                if (System.IO.File.Exists(fullpath))
                {
                    try
                    {
                        System.IO.File.Delete(fullpath);
                        ResultModel _result = new ResultModel
                        {
                            msg = "删除成功",
                            pass = true
                        };
                        return Json(_result, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception e)
                    {
                        ResultModel _result = new ResultModel
                        {
                            msg = "删除失败," + e.Message,
                            pass = true
                        };
                        return Json(_result, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    if (System.IO.Directory.Exists(fullpath))
                    {
                        try
                        {
                            System.IO.Directory.Delete(fullpath, true);
                            ResultModel _result = new ResultModel
                            {
                                msg = "删除成功",
                                pass = false
                            };
                            return Json(_result, JsonRequestBehavior.AllowGet);
                        }
                        catch(Exception e){
                            ResultModel _result = new ResultModel
                            {
                                msg = "删除失败," + e.Message,
                                pass = true
                            };
                            return Json(_result, JsonRequestBehavior.AllowGet);
                        }
                        
                    }
                    else
                    {
                        ResultModel _result = new ResultModel
                        {
                            msg = "文件或者目录未找到",
                            pass = false
                        };
                        return Json(_result, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                ResultModel _result = new ResultModel
                {
                    msg = "参数错误",
                    pass = false
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(ModuleName = "fileManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult FileRename(string currentDir, string oldName, string newName)
        {
            currentDir = currentDir.Trim();
            if (string.IsNullOrEmpty(oldName))
            {
                ResultModel _result = new ResultModel
                {
                    msg = "参数错误",
                    pass = false
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(newName))
            {
                ResultModel _result = new ResultModel
                {
                    msg = "参数错误",
                    pass = false
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
            string _root = MyPath.AppPath + "File";//文件主目录（根目录）
            string target_old = _root + Path.DirectorySeparatorChar + currentDir + Path.DirectorySeparatorChar + oldName;//目标目录或者文件
            string target_new = _root + Path.DirectorySeparatorChar + currentDir + Path.DirectorySeparatorChar + newName;//目标目录或者文件
            string oldpath = "", newPath="";
            try
            {
                oldpath = Path.GetFullPath(target_old);//完整绝对路径
                newPath = Path.GetFullPath(target_new);//完整绝对路径
            }
            catch (Exception e)
            {
                //防止出现权限等文件常识性错误
                ResultModel _result = new ResultModel
                {
                    msg = "参数错误," + e.Message,
                    pass = false
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
            if (oldpath.StartsWith(_root) && newPath.StartsWith(_root))//满足根目录（不允许访问其他目录）
            {
                if (System.IO.File.Exists(oldpath))
                {
                    try
                    {
                        System.IO.File.Move(oldpath, newPath);
                        ResultModel _result = new ResultModel
                        {
                            msg = "命名成功",
                            pass = true
                        };
                        return Json(_result, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception e)
                    {
                        ResultModel _result = new ResultModel
                        {
                            msg = "命名失败," + e.Message,
                            pass = false
                        };
                        return Json(_result, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (System.IO.Directory.Exists(oldpath))
                    {
                        try
                        {
                            System.IO.Directory.Move(oldpath, newPath);
                            ResultModel _result = new ResultModel
                            {
                                msg = "命名成功",
                                pass = true
                            };
                            return Json(_result, JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception e)
                        {
                            ResultModel _result = new ResultModel
                            {
                                msg = "命名失败," + e.Message,
                                pass = false
                            };
                            return Json(_result, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        ResultModel _result = new ResultModel
                        {
                            msg = "文件或者目录未找到",
                            pass = false
                        };
                        return Json(_result, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                ResultModel _result = new ResultModel
                {
                    msg = "参数错误",
                    pass = false
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(ModuleName = "fileManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult FileNewDir(string currentDir, string newDirName) {
            currentDir = currentDir.Trim();
            newDirName = newDirName.Trim();
            if (string.IsNullOrEmpty(newDirName))
            {
                ResultModel _result = new ResultModel
                {
                    msg = "参数错误",
                    pass = false
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
            if (newDirName.IndexOf('.') > 0)
            {
                ResultModel _result = new ResultModel
                {
                    msg = "参数错误",
                    pass = false
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
            string _root = MyPath.AppPath + "File";//文件主目录（根目录）
            string target = _root + Path.DirectorySeparatorChar + currentDir + Path.DirectorySeparatorChar + newDirName;//目标目录或者文件
            string fullpath = "";
            try
            {
                fullpath = Path.GetFullPath(target);//完整绝对路径
            }
            catch (Exception e)
            {
                //防止出现权限等文件常识性错误
                ResultModel _result = new ResultModel
                {
                    msg = "参数错误," + e.Message,
                    pass = false
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
            if (fullpath.StartsWith(_root))//满足根目录（不允许访问其他目录）
            {
                if (System.IO.Directory.Exists(fullpath))
                {
                    ResultModel _result = new ResultModel
                    {
                        msg = "目录已经存在",
                        pass = false
                    };
                    return Json(_result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(fullpath);
                        ResultModel _result = new ResultModel
                        {
                            msg = "创建成功",
                            pass = true
                        };
                        return Json(_result, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception e)
                    {
                        ResultModel _result = new ResultModel
                        {
                            msg = "创建失败," + e.Message,
                            pass = true
                        };
                        return Json(_result, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                ResultModel _result = new ResultModel
                {
                    msg = "参数错误",
                    pass = false
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(ModuleName = "fileManage", actionEnum = EnumsModel.ActionEnum.Upload)]
        public ActionResult FileUpload(string currentDir) {
            currentDir = currentDir.Trim();
            string _root = MyPath.AppPath + "File";//文件主目录（根目录）
            string target = _root + Path.DirectorySeparatorChar + currentDir;//目标目录或者文件
            string fullpath = "";
            try
            {
                fullpath = Path.GetFullPath(target);//完整绝对路径
            }
            catch (Exception e)
            {
                //防止出现权限等文件常识性错误
                ResultModel _result = new ResultModel
                {
                    msg = "参数错误," + e.Message,
                    pass = false
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
            if (fullpath.StartsWith(_root))//满足根目录（不允许访问其他目录）
            {
                HttpFileCollectionBase files = Request.Files;
                if (files.Count <= 0)
                {
                    ResultModel _result = new ResultModel
                    {
                        msg = "参数错误",
                        pass = false
                    };
                    return Json(_result, JsonRequestBehavior.AllowGet);
                }
                HttpPostedFileBase file = files[0];
                if (file == null)
                {
                    ResultModel _result = new ResultModel
                    {
                        msg = "参数错误",
                        pass = false
                    };
                    return Json(_result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string filePath = fullpath;
                    if (!filePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    {
                        filePath = filePath + Path.DirectorySeparatorChar;
                    }

                    filePath = filePath + file.FileName;
                    if (WebHelper.saveUploadFile(file, filePath, 1024 * 4))
                    {
                        ResultModel _result = new ResultModel
                        {
                            msg = "上传成功",
                            pass = true
                        };
                        return Json(_result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ResultModel _result = new ResultModel
                        {
                            msg = "上传失败",
                            pass = false
                        };
                        return Json(_result, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else {
                ResultModel _result = new ResultModel
                {
                    msg = "参数错误",
                    pass = false
                };
                return Json(_result, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 单页管理

        [Power(ModuleName = "aloneManage", IsShow = true, Title = "单页管理", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult AloneManage()
        {
            return View();
        }
        [HttpPost]
        [Power(ModuleName = "aloneManage", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult AloneManage(int? page)
        {
            if (!page.HasValue) { page = 1; }
            PageTurnModel pageturn = new PageTurnModel() { ItemSize = 10 };
            pageturn.PageIndex = page;
            int count = 0;
            var list = this.staticPageService.ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "CreateTime DESC", out  count).Select(t => new
            {
                t.Page_Id,
                t.PageName,
                TempName=t.TempPage.TempName,
                t.PagePath,
                CreateTime = t.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                LastTime = t.LastTime.ToString("yyyy-MM-dd HH:mm:ss")
            });
            pageturn.CountSize = count;
            return Json(new RowResultModel { rows = list, pagination = pageturn, pass = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Power(ModuleName = "aloneManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult AloneHtmlAdd()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        [Power(ModuleName = "aloneManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult AloneHtmlAdd(string PageName, string PageParameter, int TempPageId)
        {
            try {
                if (string.IsNullOrEmpty(PageName)) { return Json(new ResultModel { msg = "添加失败", pass = false }, JsonRequestBehavior.AllowGet); }
                if (string.IsNullOrEmpty(PageParameter)) { return Json(new ResultModel { msg = "添加失败", pass = false }, JsonRequestBehavior.AllowGet); }
                PageName = PageName.Trim();
                if (this.staticPageService.IsUsePageName(PageName, 0)) {
                    return Json(new ResultModel { msg = "该页面名已经使用", pass = false }, JsonRequestBehavior.AllowGet);
                }
                StaticPage _page = new StaticPage();
                _page.CreateTime = DateTime.Now;
                _page.LastTime = _page.CreateTime;
                _page.PageName = PageName.Trim();
                _page.PageParameter = PageParameter;
                _page.TempPageId = TempPageId;
                this.staticPageService.Insert(_page);
                return Json(new ResultModel { msg = "添加成功", pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch {
                return Json(new ResultModel { msg = "添加失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        [Power(ModuleName = "aloneManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult AloneHtmlUpdate(int id)
        {
            ViewData["staticPage"] = this.staticPageService.Find(id);
             return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        [Power(ModuleName = "aloneManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult AloneHtmlUpdate(int Page_Id, string PageName, string PageParameter)
        {
            try {
                if (string.IsNullOrEmpty(PageName)) { return Json(new ResultModel { msg = "修改失败", pass = false }, JsonRequestBehavior.AllowGet); }
                if (string.IsNullOrEmpty(PageParameter)) { return Json(new ResultModel { msg = "修改失败", pass = false }, JsonRequestBehavior.AllowGet); }
                StaticPage _page = this.staticPageService.Find(Page_Id);
                if (_page==null) { return Json(new ResultModel { msg = "修改失败", pass = false }, JsonRequestBehavior.AllowGet); }
                if (this.staticPageService.IsUsePageName(PageName, _page.Page_Id))
                {
                    return Json(new ResultModel { msg = "该页面名已经使用", pass = false }, JsonRequestBehavior.AllowGet);
                }
                _page.PageName = PageName.Trim();
                _page.PageParameter = PageParameter;
                _page.LastTime = DateTime.Now;
                this.staticPageService.Update(_page);
                return Json(new ResultModel { msg = "修改成功", pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch {
                return Json(new ResultModel { msg = "修改失败", pass = false }, JsonRequestBehavior.AllowGet); 
            }
        }
        [Power(ModuleName = "aloneManage", actionEnum = EnumsModel.ActionEnum.Delete)]
        public ActionResult AloneHtmlDelete(string ids)
        {
            try
            {
                this.staticPageService.Delete(ids);
                return Json(new ResultModel { msg = "删除成功", pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new ResultModel { msg = "删除失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(ModuleName = "aloneManage", actionEnum = EnumsModel.ActionEnum.Build)]
        public ActionResult AloneHtmlRender(int id)
        {
            StaticPage _page = this.staticPageService.Find(id);
            JObject j_obj = new JObject();
            JArray j_arry = Newtonsoft.Json.Linq.JArray.Parse(_page.PageParameter);
            for (int i = 0; i < j_arry.Count; i++)
            {
                j_obj.Add(j_arry[i]["key"].ToString(), j_arry[i]["value"]);
            }
            ViewData["PageId"] = id;
            ViewData["Data"] = j_obj;
            ViewData["FileName"] = _page.PageName;
            ResultModel _result = null;
            if (!System.IO.File.Exists(MyPath.AppPath + _page.TempPage.TempPath))
            {
                _result = new ResultModel
                {
                    msg = "创建失败,模板文件未生成",
                    pass = false
                };
                return Json(_result);
            }
            ViewEngineResult viewResult =ViewEngines.Engines.FindView(this.ControllerContext, _page.TempPage.TempPath, "");
            IView view = viewResult.View;
            ViewDataDictionary vd = this.ViewData;
            TempDataDictionary td = this.TempData;
            try
            {
                using (StringWriter writer = new StringWriter())
                {
                    ViewContext viewContext = new ViewContext(this.ControllerContext, view, vd, td, writer);
                    viewContext.View.Render(viewContext, writer);
                    string _t = Path.Combine(base.blogConfig.HtmlDir, base.blogConfig.PageDir);//下次改为配置的*|*
                    string _e = blogConfig.PageExtension;//*|*
                    string _f = _page.PageName + _e;
                    string _dir = Path.Combine(MyPath.AppPath, _t);
                    if (Directory.Exists(_dir) == false)
                    {
                        Directory.CreateDirectory(_dir);
                    }
                    string webUrl = "/" + blogConfig.HtmlDir + "/" + blogConfig.PageDir + "/" + _f;
                    string target_path = Path.Combine(MyPath.AppPath, _t, _f);
                    System.IO.File.WriteAllText(target_path, StringHelper.Compress(writer.ToString()), Encoding.UTF8);
                    _page.PagePath=webUrl;
                    this.staticPageService.UpdatePath(_page.Page_Id, _page.PagePath);
                    if (System.IO.File.Exists(target_path))
                    {
                        _result = new ResultModel
                        {
                            msg = "重新生成",
                            pass = true,
                            append = new { url = webUrl }
                        };
                    }
                    else
                    {
                        _result = new ResultModel
                        {
                            msg = "创建成功",
                            pass = true,
                            append = new { url = webUrl }
                        };
                    }
                }
            }
            catch(Exception e){
                _result = new ResultModel
                {
                    msg = "创建失败，" + e.Message,
                    pass = false
                };
            }
            return Json(_result);
        }
        [Power(ModuleName = "aloneManage", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult AloneHtmlHelper()
        {
            return View();
        }
        [Power(ModuleName = "templateManage",Title="单页模板管理", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult TemplateManage() {
            return View();
        }
        [HttpPost]
        [Power(ModuleName = "templateManage", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult TemplateManage(int? page)
        {
            if (!page.HasValue) { page = 1; }
            PageTurnModel pageturn = new PageTurnModel() { ItemSize = 100 };
            pageturn.PageIndex = page;
            int count = 0;
            var list = this.tempPageService.ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "CreateTime DESC", out  count).Select(t => new
            {
                t.TempPage_Id,
                t.TempName,
                t.TempPath,
                CreateTime=t.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                LastTime=t.LastTime.ToString("yyyy-MM-dd HH:mm:ss")
            });
            pageturn.CountSize = count;
            return Json(new RowResultModel { rows = list, pagination = null, pass = true }, JsonRequestBehavior.AllowGet);
        }
        [Power(ModuleName = "templateManage", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult TemplateList() {
            PageTurnModel pageturn = new PageTurnModel() { ItemSize = 100 };
            pageturn.PageIndex = 1;
            int count = 0;
            var list = this.tempPageService.ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "CreateTime DESC", out  count).Select(t => new
            {
                t.TempPage_Id,
                t.TempName,
                t.TempByname
            });
            pageturn.CountSize = count;
            return Json(new RowResultModel { rows = list, pagination = null, pass = true }, JsonRequestBehavior.AllowGet);
        }
        [Power(ModuleName = "templateManage", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult TemplateFind(int id)
        {
            Cactus.Model.CMS.TempPage t = this.tempPageService.Find(id);
            if (t != null)
            {
                return Json(new ObjResultModel {  pass = true, obj = new { TempPage_Id = t.TempPage_Id, TempParameter = t.TempParameter } }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("数据错误");
            }    
        }
        [HttpGet]
        [Power(ModuleName = "templateManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult TemplateAdd()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        [Power(ModuleName = "templateManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult TemplateAdd(string TempName, string TempByname, string TempParameter, string TempContent)
        {
            if (string.IsNullOrEmpty(TempName)) { return Json(new ResultModel { msg = "添加失败", pass = false }); }
            if (string.IsNullOrEmpty(TempParameter)) { return Json(new ResultModel { msg = "添加失败", pass = false }); }
            if (string.IsNullOrEmpty(TempContent)) { return Json(new ResultModel { msg = "添加失败", pass = false }); }
            TempName=TempName.Trim();
            if (this.tempPageService.IsUseTempName(TempName, 0)) {
                return Json(new ResultModel { msg = "该模板名已经使用", pass = false });
            }
            try {
                TempPage tempPage = new TempPage();
                tempPage.TempName = TempName;
                tempPage.TempByname = TempByname.Trim();
                tempPage.TempParameter = TempParameter;
                tempPage.TempContent = TempContent;
                tempPage.CreateTime = DateTime.Now;
                tempPage.LastTime = DateTime.Now;                
                string tempName="Template";
                string tempPath = Path.Combine(MyPath.AppPath, tempName);
                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }
                string _path = "/"+tempName+"/" + TempName+".cshtml";
                tempPage.TempPath = _path;
                string sysPath=HIO.SysPathParse(MyPath.AppPath, _path, true);
                System.IO.File.WriteAllText(sysPath, TempContent, Encoding.UTF8);
                this.tempPageService.Insert(tempPage);
                return Json(new ResultModel { msg = "添加成功", pass = true });
            }
            catch {
                return Json(new ResultModel { msg = "添加失败", pass = false });
            }
        }
        [HttpGet]
        [Power(ModuleName = "templateManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult TemplateUpdate(int id)
        {
            Cactus.Model.CMS.TempPage t = this.tempPageService.Find(id);
            if (t != null) {
                ViewData["TempPage"] = t;
                return View();
            } else {
                return Content("数据错误");
            }            
        }
        [ValidateInput(false)]
        [HttpPost]
        [Power(ModuleName = "templateManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult TemplateUpdate(int TempPage_Id,string TempByname, string TempParameter, string TempContent)
        {
            if (string.IsNullOrEmpty(TempByname)) { return Json(new ResultModel { msg = "修改失败", pass = false }); }
            if (string.IsNullOrEmpty(TempParameter)) { return Json(new ResultModel { msg = "修改失败", pass = false }); }
            if (string.IsNullOrEmpty(TempContent)) { return Json(new ResultModel { msg = "修改失败", pass = false }); }
            try
            {
                TempPage tempPage = tempPageService.Find(TempPage_Id);
                tempPage.TempByname = TempByname.Trim();
                tempPage.TempParameter = TempParameter;
                tempPage.TempContent = TempContent;
                tempPage.LastTime = DateTime.Now;
                string sysPath = HIO.SysPathParse(MyPath.AppPath, tempPage.TempPath, true);
                System.IO.File.WriteAllText(sysPath, TempContent, Encoding.UTF8);
                this.tempPageService.Update(tempPage);
                return Json(new ResultModel { msg = "修改成功", pass = true });
            }
            catch
            {
                return Json(new ResultModel { msg = "修改失败", pass = false });
            }
        }
        [Power(ModuleName = "templateManage", actionEnum = EnumsModel.ActionEnum.Delete)]
        public ActionResult TemplateDelete(string ids)
        {
            try
            {
                this.tempPageService.Delete(ids); 
                return Json(new ResultModel { msg = "删除成功", pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new ResultModel { msg = "删除失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(ModuleName = "templateManage", actionEnum = EnumsModel.ActionEnum.Build)]
        public ActionResult TemplateCSHtmlRender(int id) 
        {
            TempPage tempPage = this.tempPageService.Find(id);
            string sysPath = HIO.SysPathParse(MyPath.AppPath, tempPage.TempPath, true);
            if (System.IO.File.Exists(sysPath))
            {
                System.IO.File.WriteAllText(sysPath, tempPage.TempContent, Encoding.UTF8);
                return Json(new ResultModel { msg = "重新生成", pass = true });
            }
            else
            {
                System.IO.File.WriteAllText(sysPath, tempPage.TempContent, Encoding.UTF8);
                return Json(new ResultModel { msg = "生成成功", pass = true });
            }
        }
        #endregion

        #region 标签管理
        [Power(ModuleName = "tagManage",Title="标签管理", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult TagManage() { return View(); }
        [Power(ModuleName = "tagManage", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult TagList(int? page) {
            if (!page.HasValue) { page = 1; }
            PageTurnModel pageturn = new PageTurnModel() { ItemSize = 10 };
            pageturn.PageIndex = page;
            int count = 0;
            var list = this.tagService.ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "CreateTime DESC", out  count).Select(t => new
            {
                t.Tag_Id,
                t.TagName,
                t.TagDes,
                LastTime = t.LastTime.ToString("yyyy-MM-dd HH:mm:ss"),
                CreateTime = t.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")
            });
            pageturn.CountSize = count;
            return Json(new RowResultModel { rows = list, pagination = pageturn, pass = true }, JsonRequestBehavior.AllowGet);
        }
        [Power(ModuleName = "tagManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult TagDelete(string ids) {
            try
            {
                this.tagService.Delete(ids);
                return Json(new ResultModel { msg = "删除成功", pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new ResultModel { msg = "删除失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [Power(ModuleName = "tagManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult TagUpdate(Tag tag) {

            tag.TagName = tag.TagName.Trim();
            if (this.tagService.IsUseTagName(tag.TagName, tag.Tag_Id))
            {
                return Json(new ResultModel { msg = "标签名与其他标签相同", pass = false });
            }
            else
            {
                try
                {
                    Model.CMS.Tag model = this.tagService.Find(tag.Tag_Id);
                    model.LastTime = DateTime.Now;
                    model.TagName = tag.TagName;
                    model.TagDes = tag.TagDes;
                    this.tagService.Update(model);
                    return Json(new ResultModel { msg = "修改成功", pass = true });
                }
                catch {
                    return Json(new ResultModel { msg = "修改失败", pass = false });
                }
            }
        }
        [HttpGet]
        [Power(ModuleName = "tagManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult TagUpdate(int id) {
            ViewData["Tag"] = this.tagService.Find(id);
            return View();    
        }
        [HttpPost]
        [Power(ModuleName = "tagManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult TagAdd(Tag tag) {
            tag.TagName = tag.TagName.Trim();
            if (this.tagService.IsUseTagName(tag.TagName, 0))
            {
                return Json(new ResultModel { msg = "标签名与其他标签相同", pass = false });
            }
            else
            {
                tag.CreateTime = DateTime.Now;
                tag.LastTime = tag.CreateTime;
                tag.TagDes = tag.TagDes.Trim();
                var b = this.tagService.Insert(tag);
                if (b)
                {
                    return Json(new ResultModel { msg = "添加成功", pass = true });
                }
                else
                {
                    return Json(new ResultModel { msg = "添加失败", pass = false });
                }
            }
        }
        [HttpGet]
        [Power(ModuleName = "tagManage", actionEnum = EnumsModel.ActionEnum.Add)]
        public ActionResult TagAdd() { return View(); }
        #endregion

        #region 评论管理
        [Power(ModuleName = "commentManage", Title = "评论管理",IsShow=true, actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult CommentManage()
        {
            return View();
        }
        [Power(ModuleName = "commentManage", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult CommentList(int? page) 
        {
            if (!page.HasValue) { page = 1; }
            PageTurnModel pageturn = new PageTurnModel() { ItemSize = 10 };
            pageturn.PageIndex = page;
            int count = 0;
            var list = this.commentService.ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "CreateTime DESC", out  count).Select(t => new
            {
                t.Comment_Id,
                t.Content,
                t.Email,
                t.Nickname,
                CreateTime = t.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                t.Article.Title
            });
            pageturn.CountSize = count;
            return Json(new RowResultModel { rows = list, pagination = pageturn, pass = true }, JsonRequestBehavior.AllowGet);
        }
        [Power(ModuleName = "commentManage", actionEnum = EnumsModel.ActionEnum.Delete)]
        public ActionResult CommentDelete(string ids)
        {
            try
            {
                this.commentService.Delete(ids);
                return Json(new ResultModel { msg = "删除成功", pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new ResultModel { msg = "删除失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 界面管理
        [Power(ModuleName = "UIManage", IsShow = true, Title = "界面管理", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult ManageUI()
        {
            string pathBlogDir = Path.Combine(MyPath.AppPath, "Views", "Blog");
            string pathSharedDir = Path.Combine(MyPath.AppPath,  "Views", "Shared");
            string pathBlogCssdDir = Path.Combine(MyPath.AppPath, "Content", "Blog","css");
            string pathBlogJsdDir = Path.Combine(MyPath.AppPath, "Content", "Blog", "js");

            FileInfo[] blogFiles = new DirectoryInfo(pathBlogDir).GetFiles();
            FileInfo[] sharedFiles = new DirectoryInfo(pathSharedDir).GetFiles();
            FileInfo[] BlogCssFiles = new DirectoryInfo(pathBlogCssdDir).GetFiles();
            FileInfo[] BlogJsFiles = new DirectoryInfo(pathBlogJsdDir).GetFiles();

            ViewData["blogFiles"] = blogFiles;
            ViewData["sharedFiles"] = sharedFiles;
            ViewData["blogCssFiles"] = BlogCssFiles;
            ViewData["blogJsFiles"] = BlogJsFiles;

            return View();
        }
        [Power(ModuleName = "UIManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult UIEdit(string dir, string filename)
        {
            string pathDir = "";
            switch (dir)
            {
                case "Blog": pathDir = Path.Combine(MyPath.AppPath, "Views", "Blog"); break;
                case "Shared": pathDir = Path.Combine(MyPath.AppPath, "Views", "Shared"); break;
                case "Js": pathDir = Path.Combine(MyPath.AppPath, "Content", "Blog", "js"); break;
                case "Css": pathDir = Path.Combine(MyPath.AppPath, "Content", "Blog", "css"); break;
            }
            string filePath = Path.Combine(pathDir, filename);
            if (System.IO.File.Exists(filePath))
            {
                ViewData["dir"] = dir;
                ViewData["fileName"] = filename;
                ViewData["filePath"] = filePath;
                return View();
            }
            else
            {
                return Json(new ResultModel { msg = "文件不存在", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [ValidateInput(false)]
        [Power(ModuleName = "UIManage", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult UISave(string dir, string filename, string Content)
        {
            try
            {
                string pathDir = Path.Combine(MyPath.AppPath, "Views", dir);
                string targetFile_path = Path.Combine(pathDir, filename);
                if (System.IO.File.Exists(targetFile_path))
                {
                    System.IO.File.WriteAllText(targetFile_path, Content, Encoding.UTF8);
                    return Json(new ResultModel { msg = "修改成功", pass = true });
                }
                else
                {
                    return Json(new ResultModel { msg = "文件不存在", pass = true });
                }
            }
            catch
            {
                return Json(new ResultModel { msg = "修改失败", pass = false });
            }
        }

        #endregion

        #region 配置管理
        [HttpGet]
        [Power(ModuleName = "configManage",IsShow=true, Title="文章配置", actionEnum = EnumsModel.ActionEnum.Show)]
        public ActionResult BlogConfig()
        {
            ViewData["BlogConfig"] = this.blogConfigService.LoadConfig(Constant.BlogConfigPath);
            return View();
        }
        [HttpPost]
        [Power(ModuleName = "configManage", IsShow = true, Title = "文章配置", actionEnum = EnumsModel.ActionEnum.Edit)]
        public ActionResult BlogConfig(BlogConfig blog)
        {
            try
            {
                this.blogConfigService.SaveConfig(blog, Constant.BlogConfigPath);
                base.cacheService.Remove(Constant.CacheKey.BlogConfigCacheKey);
                return Json(new ResultModel
                {
                    pass = true,
                    msg = "操作成功"
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
        #endregion

    }
}
