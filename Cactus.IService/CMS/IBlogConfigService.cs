using Cactus.Model.CMS;
using Cactus.Model.Sys;

namespace Cactus.IService.CMS
{
    public interface IBlogConfigService
    {
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="configPath"></param>
        void SaveConfig(BlogConfig config, string configPath);
        /// <summary>
        /// 加载站点配置
        /// </summary>
        /// <param name="configPath"></param>
        /// <returns></returns>
        BlogConfig LoadConfig(string configPath);
    }
}
