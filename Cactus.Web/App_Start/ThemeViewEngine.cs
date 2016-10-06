using Cactus.Common;
using Cactus.IService;
using Cactus.Model.Sys;
using Cactus.Model.Sys.Enums;
using HTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cactus.Web
{
    public class ThemeViewEngine : RazorViewEngine
    {
        internal Func<string, string> GetExtensionThunk;
        private readonly string[] _emptyLocations = null;

        public ThemeViewEngine()
            : this((IViewPageActivator)null)
        {
        }

        public ThemeViewEngine(IViewPageActivator viewPageActivator)
            : base(viewPageActivator)
        {
            /*0:name, 1:controllerName, 2:areaName, 3:theme*/
            this.AreaViewLocationFormats = new[]
              {
                "~/Themes/{3}/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Themes/{3}/Areas/{2}/Views/Shared/{0}.cshtml",

                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
              };
            this.AreaMasterLocationFormats = new[]
              {
                "~/Themes/{3}/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Themes/{3}/Areas/{2}/Views/Shared/{0}.cshtml",

                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
              };
            this.AreaPartialViewLocationFormats = new[]
              { "~/Themes/{3}/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Themes/{3}/Areas/{2}/Views/Shared/{0}.cshtml",

                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
              };
            this.ViewLocationFormats = new[]
              {
                "~/Themes/{2}/Views/{1}/{0}.cshtml",
                "~/Themes/{2}/Views/Shared/{0}.cshtml",

                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
              };
            this.MasterLocationFormats = new[]
              {
                "~/Themes/{2}/Views/{1}/{0}.cshtml",
                "~/Themes/{2}/Views/Shared/{0}.cshtml",

                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
              };
            this.PartialViewLocationFormats = new[]
              {
                "~/Themes/{2}/Views/{1}/{0}.cshtml",
                "~/Themes/{2}/Views/Shared/{0}.cshtml",

                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
              };
            this.FileExtensions = new[]
              {
                "cshtml"
              };

            GetExtensionThunk = new Func<string, string>(VirtualPathUtility.GetExtension);
        }

        #region Protected Methods

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return System.Web.Compilation.BuildManager.GetObjectFactory(virtualPath, false) != null;
        }
        public ICacheService cacheService = IocHelper.AutofacResolveNamed<ICacheService>("CacheService");
        public ISiteConfigService siteConfigService = IocHelper.AutofacResolveNamed<ISiteConfigService>("SiteConfigService");
        protected virtual string GetCurrentTheme()
        {
            string theme = "";
            //SiteConfig config = CacheHelper.GetCache(Constant.CacheKey.SiteConfigCacheKey) as SiteConfig;
            CacheObj obj = cacheService.Get(Constant.CacheKey.SiteConfigCacheKey);
            SiteConfig config = (obj != null && obj.value != null) ? (obj.value as SiteConfig) : null;

            if (config == null)
            {
                config = siteConfigService.LoadConfig(Constant.SiteConfigPath);
                cacheService.Add(Constant.CacheKey.SiteConfigCacheKey,
                    new CacheObj()
                    {
                        value = config,
                        AbsoluteExpiration = new DateTimeOffset(DateTime.Now).AddDays(1)
                    });
                //CacheHelper.SetCache(Constant.CacheKey.SiteConfigCacheKey, config);
            }
            theme = config.SiteTheme;
            return theme;
        }

        protected virtual string GetAreaName(RouteData routeData)
        {
            object obj2;
            if (routeData.DataTokens.TryGetValue("area", out obj2))
            {
                return (obj2 as string);
            }
            return GetAreaName(routeData.Route);
        }

        protected virtual string GetAreaName(RouteBase route)
        {
            var area = route as IRouteWithArea;
            if (area != null)
            {
                return area.Area;
            }
            var route2 = route as Route;
            if ((route2 != null) && (route2.DataTokens != null))
            {
                return (route2.DataTokens["area"] as string);
            }
            return null;
        }

        protected virtual List<ThemeViewLocation> GetViewLocations(string[] viewLocationFormats, string[] areaViewLocationFormats)
        {
            var list = new List<ThemeViewLocation>();
            if (areaViewLocationFormats != null)
            {
                list.AddRange(areaViewLocationFormats.Select(s => new ThemeAreaAwareViewLocation(s)).Cast<ThemeViewLocation>());
            }
            if (viewLocationFormats != null)
            {
                list.AddRange(viewLocationFormats.Select(s => new ThemeViewLocation(s)));
            }
            return list;
        }

        protected virtual bool IsSpecificPath(string name)
        {
            char ch = name[0];
            if (ch != '~')
            {
                return (ch == '/');
            }
            return true;
        }

        protected virtual string CreateCacheKey(string prefix, string name, string controllerName, string areaName, string theme)
        {
            return string.Format(CultureInfo.InvariantCulture, ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}:{5}", new object[] { base.GetType().AssemblyQualifiedName, prefix, name, controllerName, areaName, theme });
        }

        protected virtual bool FilePathIsSupported(string virtualPath)
        {
            if (this.FileExtensions == null)
            {
                return true;
            }
            string str = this.GetExtensionThunk(virtualPath).TrimStart(new char[] { '.' });
            return this.FileExtensions.Contains<string>(str, StringComparer.OrdinalIgnoreCase);
        }

        protected virtual string GetPathFromSpecificName(ControllerContext controllerContext, string name, string cacheKey, ref string[] searchedLocations)
        {
            string virtualPath = name;
            if (!this.FilePathIsSupported(name) || !this.FileExists(controllerContext, name))
            {
                virtualPath = string.Empty;
                searchedLocations = new string[] { name };
            }
            this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath);
            return virtualPath;
        }

        protected virtual string GetPathFromGeneralName(ControllerContext controllerContext, List<ThemeViewLocation> locations, string name, string controllerName, string areaName, string theme, string cacheKey, ref string[] searchedLocations)
        {
            string virtualPath = string.Empty;
            searchedLocations = new string[locations.Count];
            for (int i = 0; i < locations.Count; i++)
            {
                string str2 = locations[i].Format(name, controllerName, areaName, theme);
                if (this.FileExists(controllerContext, str2))
                {
                    searchedLocations = _emptyLocations;
                    virtualPath = str2;
                    this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath);
                    return virtualPath;
                }
                searchedLocations[i] = str2;
            }
            return virtualPath;
        }

        protected virtual string GetPath(ControllerContext controllerContext, string[] locations, string[] areaLocations, string locationsPropertyName, string name, string controllerName, string cacheKeyPrefix, bool useCache, out string[] searchedLocations)
        {
            string theme = GetCurrentTheme();
            searchedLocations = _emptyLocations;
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            string areaName = GetAreaName(controllerContext.RouteData);

            bool flag = !string.IsNullOrEmpty(areaName);
            List<ThemeViewLocation> viewLocations = GetViewLocations(locations, flag ? areaLocations : null);
            if (viewLocations.Count == 0)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Properties cannot be null or empty.", new object[] { locationsPropertyName }));
            }
            bool isSpecificPath = IsSpecificPath(name);
            string key = this.CreateCacheKey(cacheKeyPrefix, name, isSpecificPath ? string.Empty : controllerName, areaName, theme);
            if (useCache)
            {
                var cached = this.ViewLocationCache.GetViewLocation(controllerContext.HttpContext, key);
                if (cached != null)
                {
                    return cached;
                }
            }
            if (!isSpecificPath)
            {
                return this.GetPathFromGeneralName(controllerContext, viewLocations, name, controllerName, areaName, theme, key, ref searchedLocations);
            }
            return this.GetPathFromSpecificName(controllerContext, name, key, ref searchedLocations);
        }

        #endregion

        #region IViewEngine
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");
            if (string.IsNullOrEmpty(partialViewName))
                throw new ArgumentException("partialViewName");

            string requiredString = controllerContext.RouteData.GetRequiredString("controller");
            string[] searchedLocations;
            string path = this.GetPath(controllerContext, this.PartialViewLocationFormats, this.AreaPartialViewLocationFormats, "PartialViewLocationFormats", partialViewName, requiredString, "Partial", useCache, out searchedLocations);
            if (string.IsNullOrEmpty(path))
                return new ViewEngineResult((IEnumerable<string>)searchedLocations);
            else
                return new ViewEngineResult(this.CreatePartialView(controllerContext, path), (IViewEngine)this);
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");
            if (string.IsNullOrEmpty(viewName))
                throw new ArgumentException("viewName");
            string requiredString = controllerContext.RouteData.GetRequiredString("controller");
            string[] searchedLocations1;
            string path1 = this.GetPath(controllerContext, this.ViewLocationFormats, this.AreaViewLocationFormats, "ViewLocationFormats", viewName, requiredString, "View", useCache, out searchedLocations1);
            string[] searchedLocations2;
            string path2 = this.GetPath(controllerContext, this.MasterLocationFormats, this.AreaMasterLocationFormats, "MasterLocationFormats", masterName, requiredString, "Master", useCache, out searchedLocations2);
            if (string.IsNullOrEmpty(path1) || string.IsNullOrEmpty(path2) && !string.IsNullOrEmpty(masterName))
                return new ViewEngineResult(Enumerable.Union<string>((IEnumerable<string>)searchedLocations1, (IEnumerable<string>)searchedLocations2));
            else
                return new ViewEngineResult(this.CreateView(controllerContext, path1, path2), (IViewEngine)this);
        }
        #endregion

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            var runViewStartPages = false;
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, partialPath, null, runViewStartPages, fileExtensions, base.ViewPageActivator);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            string layoutPath = masterPath;
            var runViewStartPages = true;
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, viewPath, layoutPath, runViewStartPages, fileExtensions, base.ViewPageActivator);
        }
    }

    public class ThemeAreaAwareViewLocation : ThemeViewLocation
    {
        public ThemeAreaAwareViewLocation(string virtualPathFormatString)
            : base(virtualPathFormatString)
        {
        }

        public override string Format(string viewName, string controllerName, string areaName, string theme)
        {
            return string.Format(CultureInfo.InvariantCulture, _virtualPathFormatString, viewName, controllerName, areaName, theme);
        }
    }

    public class ThemeViewLocation
    {
        protected readonly string _virtualPathFormatString;

        public ThemeViewLocation(string virtualPathFormatString)
        {
            _virtualPathFormatString = virtualPathFormatString;
        }

        public virtual string Format(string viewName, string controllerName, string areaName, string theme)
        {
            return string.Format(CultureInfo.InvariantCulture, _virtualPathFormatString, viewName, controllerName, theme);
        }
    }
}