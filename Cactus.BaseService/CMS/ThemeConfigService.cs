using Cactus.Common;
using Cactus.IService.CMS;
using Cactus.Model.CMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.BaseService.CMS
{
    public class ThemeConfigService : IThemeConfigService
    {
        private static readonly object LockHelper = new object();
        public Cactus.Model.CMS.ThemeConfig LoadConfig(string configPath)
        {
            return (Cactus.Model.CMS.ThemeConfig)SerializeHelper.DeSerialize(typeof(Cactus.Model.CMS.ThemeConfig), configPath);
        }

        public void SaveConfig(ThemeConfig config, string configPath)
        {
            lock (LockHelper)
            {
                SerializeHelper.Serialize(config, configPath);
            }
        }
    }
}
