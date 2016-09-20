using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.Model.CMS
{
    public class Tag
    {
        public int Tag_Id { get; set; }
        public string TagName { get; set; }
        public string TagDes { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastTime { get; set; }
    }
}
