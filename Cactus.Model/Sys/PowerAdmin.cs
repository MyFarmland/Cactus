using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Cactus.Model.Sys
{
    [Serializable()]
    [XmlRoot("Admin")]
    public class PowerAdmin {
        public List<PowerGroup> list { get; set; }
        /// <summary>
        /// 权限判断
        /// </summary>
        /// <param name="Name">组名</param>
        /// <param name="ModuleName">模块名</param>
        /// <param name="action">操作</param>
        /// <returns></returns>
        public bool IsPower(string UserActions, string Name, string ModuleName, string action)
        {
            List<string> actions = new List<string>(UserActions.Split(','));
            return actions.Contains(Name + "|" + ModuleName +"|"+ action);
        }
    }
    [Serializable()]
    [XmlRoot("group")]
    public class PowerGroup {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlAttribute("Title")]
        public string Title { get; set; }
        [XmlAttribute("Des")]
        public string Des { get; set; }

        [XmlAttribute("IsShow")]
        public bool IsShow { get; set; }

        private string icon;
        [XmlAttribute("Icon")]
        public string Icon
        {
            get
            {
                if (string.IsNullOrEmpty(icon))
                {
                    return "fa-bars";
                }
                else
                {
                    return icon;
                }
            }
            set { icon = value; }
        }

        public List<PowerModule> module { get; set; }
    }
    [Serializable()]
    [XmlRoot("module")]
    public class PowerModule {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlAttribute("Title")]
        public string Title { get; set; }

        [XmlAttribute("ParamStr")]
        public string ParamStr { get; set; }
        
        [XmlAttribute("Des")]
        public string Des { get; set; }
        [XmlAttribute("IsShow")]
        public bool IsShow { get; set; }

        private string icon;
        [XmlAttribute("Icon")]
        public string Icon
        {
            get
            {
                if (string.IsNullOrEmpty(icon))
                {
                    return "fa-bars";
                }
                else
                {
                    return icon;
                }
            }
            set { icon = value; }
        }
        [XmlAttribute("ActionType")]
        public string Action_Type { get; set; }
    }
}
