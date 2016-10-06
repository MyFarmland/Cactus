using Cactus.Common;
using Cactus.IService.CMS;
using Cactus.Model.CMS;

namespace Cactus.BaseService.CMS
{
    public class BlogConfigService : IBlogConfigService
    {
        private static readonly object LockHelper = new object();
        public Cactus.Model.CMS.BlogConfig LoadConfig(string configPath)
        {
            return (Cactus.Model.CMS.BlogConfig)SerializeHelper.DeSerialize(typeof(Cactus.Model.CMS.BlogConfig), configPath);
        }

        public void SaveConfig(BlogConfig config, string configPath)
        {
            lock (LockHelper)
            {
                SerializeHelper.Serialize(config, configPath);
            }
        }
    }
}
