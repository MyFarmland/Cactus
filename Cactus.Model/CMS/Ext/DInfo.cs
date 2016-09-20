using System.IO;

namespace Cactus.Model.CMS.Ext
{
    public class DInfo
    {
        public DInfo() { }
        public DInfo(DirectoryInfo dir) {
            this.DirName = dir.Name;
            this.LastTime = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.CreateTime = dir.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public string DirName { get; set; }
        public string LastTime { get; set; }
        public string CreateTime { get; set; }
    }
}
