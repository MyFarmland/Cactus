using Cactus.Common;
using Cactus.IService;
using Cactus.Model.Sys.Config;

namespace Cactus.BaseService
{
    public class PathConfigService : IPathConfigService
    {
        private static readonly object LockHelper = new object();
        public void SaveConfig(Model.Sys.Config.PathConfig config, string configPath)
        {
            lock (LockHelper)
            {
                SerializeHelper.Serialize(config, configPath);
            }
        }

        public Model.Sys.Config.PathConfig LoadConfig(string configPath)
        {
            return (PathConfig)SerializeHelper.DeSerialize(typeof(PathConfig), configPath);
        }
    }
}
