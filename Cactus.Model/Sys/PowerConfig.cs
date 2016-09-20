using System;
using System.Collections.Generic;

namespace Cactus.Model.Sys
{
    [Serializable()]
    public class PowerConfig
    {
        public List<PowerGroup> PowerGroupList { get; set; }
    }
    [Serializable()]
    public class Power {
        public string ParamStr { get; set; }
        public string PowerName { get; set; }
        public string PowerDes { get; set; }
        public bool IsShow { get; set; }
        
        private string icon;
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
        public string NoGroupId { get; set; }
        public string NoPowerId { get; set; }
    }
    [Serializable()]
    public class PowerGroup
    {
        public bool IsShow { get; set; }
        public string GroupName { get; set; }
        public string NoGroupId { get; set; }
        public List<Power> PowerList { get; set; }

        private string icon;
        public string Icon
        {
            get {
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
    }
}
