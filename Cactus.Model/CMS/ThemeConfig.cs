
using System.Collections.Generic;
namespace Cactus.Model.CMS
{
    public class ThemeConfig
    {
        /// <summary>
        /// 主题列
        /// </summary>
        public List<Theme> ThemeList { get; set; }
    }
    public class Theme
    {
        /// <summary>
        /// 主题名
        /// </summary>
        public string ThemeName { get; set; }
        /// <summary>
        /// 主题别名
        /// </summary>
        public string ThemeAnotherName { get; set; }
        /// <summary>
        /// 主题介绍
        /// </summary>
        public string ThemeDes { get; set; }
        /// <summary>
        /// 主题预览图
        /// </summary>
        public string ThemeImgUrl { get; set; }
    }
}
