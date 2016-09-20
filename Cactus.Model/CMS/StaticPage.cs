using System;

namespace Cactus.Model.CMS
{
    public class StaticPage
    {
        public StaticPage()
		{
		}
        public int Page_Id { get; set; }
        public string PagePath { get; set; }
        public string PageName { get; set; }
        public int TempPageId { get; set; }
        /// <summary>
        /// 类目
        /// </summary>
        /// <value>The category.</value>
        public TempPage TempPage { get; set; }
        /// <summary>
        /// 保存静态页的数据（方便修改再生成）
        /// </summary>
        public string PageParameter { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastTime { get; set; }
    }
}
