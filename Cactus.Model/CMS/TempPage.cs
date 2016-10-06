using System;

namespace Cactus.Model.CMS
{
    public class TempPage
    {
        public TempPage()
		{
		}
        public int TempPage_Id { get; set; }
        public string TempName { get; set; }
        public string TempByname { get; set; }
        public string TempParameter { get; set; }
        public string TempPath { get; set; }
        public string TempContent { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastTime { get; set; }
    }
}
