using Cactus.Common;
using Cactus.Model.Sys.Enums;
using System.IO;

namespace Cactus.Model.CMS.Ext
{
    public class FInfo
    {
        public FInfo() { }
        public FInfo(FileInfo file) {
            this.FileName = file.Name;
            this.CreateTime = file.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.LastTime = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.Length = (file.Length / 1024);//kb单位
            this.WebUrl = HIO.WebPathParse(file.FullName, MyPath.AppPath, false);
        }
        public string FileName { get; set; }
        public string CreateTime { get; set; }
        public string LastTime { get; set; }
        public long Length { get; set; }
        public string WebUrl { get; set; }
    }
}
