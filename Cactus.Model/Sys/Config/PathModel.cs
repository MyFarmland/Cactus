using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cactus.Model.Sys.Config
{
    [Serializable()]
    [XmlRoot("path")]
    public class PathModel
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlAttribute("DirPath")]
        public string DirPath { get;set;}
        [XmlAttribute("WebPath")]
        public string WebPath { get; set; }
        [XmlAttribute("Des")]
        public string Des { get; set; }
        [XmlAttribute("FileSize")]
        public int FileSize { get; set; }
                 
    }
}
