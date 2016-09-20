using Cactus.Model.Sys;

namespace Cactus.IService
{
    public interface ISiteConfigService
    {
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="configPath"></param>
        void SaveConfig(SiteConfig config, string configPath);
        /// <summary>
        /// 加载站点配置
        /// </summary>
        /// <param name="configPath"></param>
        /// <returns></returns>
        SiteConfig LoadConfig(string configPath);
    }
}
