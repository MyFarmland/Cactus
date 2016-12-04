using Cactus.Common;
using Cactus.IService;
using Cactus.Model.Sys;

namespace Cactus.BaseService
{
    public class PowerConfigService : IPowerConfigService
    {
        public PowerAdmin LoadConfig(string configPath)
        {
            return (PowerAdmin)SerializeHelper.DeSerialize(typeof(PowerAdmin), configPath);
        }

        private static readonly object LockHelper = new object();
        public void SaveConfig(PowerAdmin config, string configPath)
        {
            lock (LockHelper)
            {
                SerializeHelper.Serialize(config, configPath);
            }
        }
    }
}
