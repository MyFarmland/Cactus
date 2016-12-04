using Cactus.Common;
using Cactus.IService;
using Cactus.Model.Sys;

namespace Cactus.BaseService
{
    public class SiteConfigService : ISiteConfigService
    {

        private static readonly object LockHelper = new object();
        public void SaveConfig(SiteConfig config, string configPath)
        {
            lock (LockHelper)
            {
                SerializeHelper.Serialize(config, configPath);
            }
        }

        public SiteConfig LoadConfig(string configPath)
        {
            return (SiteConfig)SerializeHelper.DeSerialize(typeof(SiteConfig), configPath);
        }
    }
}
