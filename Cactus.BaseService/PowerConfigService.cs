using Cactus.Common;
using Cactus.IService;
using Cactus.Model.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cactus.BaseService
{
    public class PowerConfigService : IPowerConfigService
    {
        public PowerConfig LoadConfig(string configPath)
        {
            return (PowerConfig)SerializeHelper.DeSerialize(typeof(PowerConfig), configPath);
        }

        private static readonly object LockHelper = new object();
        public void SaveConfig(PowerConfig config, string configPath)
        {
            lock (LockHelper)
            {
                SerializeHelper.Serialize(config, configPath);
            }
        }
    }
}
