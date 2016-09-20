using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cactus.Controllers.Filters
{
    public class GroupAttribute : Attribute
    {
        public GroupAttribute() { }
        public string NoGroupId { get; set; }
        public string GroupName { get; set; }
        public bool IsShow { get; set; }

        public string Icon { get; set; }
    }
}