using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Cactus.Web
{
    /// <summary>
    /// 依赖注入Controller、FilterAtrribute
    /// </summary>
    public class IocConfig
    {

        /// <summary>
        /// 创建 MVC容器（包含Filter）
        /// </summary>
        public static void BuildMvcContainer()
        {
            var builder = new ContainerBuilder();
            Assembly[] asm = GetAllAssembly("*.Controllers.dll").ToArray();
            builder.RegisterAssemblyTypes(asm);
            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        #region 加载程序集
        private static List<Assembly> GetAllAssembly(string dllName)
        {
            List<string> pluginpath = FindPlugin(dllName);
            var list = new List<Assembly>();
            foreach (string filename in pluginpath)
            {
                try
                {
                    string asmname = Path.GetFileNameWithoutExtension(filename);
                    if (asmname != string.Empty)
                    {
                        Assembly asm = Assembly.LoadFrom(filename);
                        list.Add(asm);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            return list;
        }
        //查找所有插件的路径
        private static List<string> FindPlugin(string dllName)
        {
            List<string> pluginpath = new List<string>();
           
                string path = AppDomain.CurrentDomain.BaseDirectory;
                string dir = Path.Combine(path, "bin");
                string[] dllList = Directory.GetFiles(dir, dllName);
                if (dllList.Length > 0)
                {
                    pluginpath.AddRange(dllList.Select(item => Path.Combine(dir, item.Substring(dir.Length + 1))));
                }
            return pluginpath;
        }
        #endregion

    }
}