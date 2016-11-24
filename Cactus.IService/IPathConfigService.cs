using Cactus.Model.Sys.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.IService
{
    public interface IPathConfigService
    {
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="configPath"></param>
        void SaveConfig(PathConfig config, string configPath);
        /// <summary>
        /// 加载站点配置
        /// </summary>
        /// <param name="configPath"></param>
        /// <returns></returns>
        PathConfig LoadConfig(string configPath);
    }
}
