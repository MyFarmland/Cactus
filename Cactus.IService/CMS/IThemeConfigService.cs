using Cactus.Model.CMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.IService.CMS
{
    public interface IThemeConfigService
    {
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="configPath"></param>
        /// <returns></returns>
        ThemeConfig LoadConfig(string configPath);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="config"></param>
        /// <param name="configPath"></param>
        void SaveConfig(ThemeConfig config, string configPath);
    }
}
