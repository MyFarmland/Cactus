using System;
namespace Cactus.Model.CMS
{
    /// <summary>
    /// blog配置
    /// </summary>
    [Serializable()] 
    public class BlogConfig
    {

        #region blog基本设置
        private string _blogname = "Cactus";

        /// <summary>
        /// 站点名称
        /// </summary>
        public string BlogName
        {
            get { return _blogname; }
            set { _blogname = value; }
        }
        private string _blogtheme = "Default";
        /// <summary>
        /// 站点主题
        /// </summary>
        public string BlogTheme
        {
            get { return _blogtheme; }
            set { _blogtheme = value; }
        }

        private string _pagedir = "html";
        /// <summary>
        /// 静态页存放的目录
        /// </summary>
        public string PageDir
        {
            get { return _pagedir; }
            set { _pagedir = value; }
        }

        private string _extension = ".html";
        /// <summary>
        /// 静态页后缀
        /// </summary>
        public string PageExtension
        {
            get { return _extension; }
            set { _extension = value; }
        }
        #endregion

        public override string ToString()
        {
            return this.BlogName;
        }
    }
}
