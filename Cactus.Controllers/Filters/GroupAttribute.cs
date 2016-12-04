using System;

namespace Cactus.Controllers.Filters
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class GroupAttribute : Attribute
    {
        public GroupAttribute() { }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool IsShow { get; set; }
        public string Des { get; set; }
        public string Icon { get; set; }

    }
}