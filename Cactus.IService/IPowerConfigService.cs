using Cactus.Model.Sys;

namespace Cactus.IService
{
    public interface IPowerConfigService
    {
        /// <summary>
        /// 加载权限配置文件
        /// </summary>
        /// <param name="configPath"></param>
        /// <returns></returns>
        PowerAdmin LoadConfig(string configPath);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="config"></param>
        /// <param name="configPath"></param>
        void SaveConfig(PowerAdmin config, string configPath);
    }
}
