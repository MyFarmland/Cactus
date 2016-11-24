using System;
namespace Cactus.Model.Sys
{
    /// <summary>
    /// 站点配置
    /// </summary>
    [Serializable()] 
    public class SiteConfig
    {

        #region 站点基本设置

        private string _sitename = "Cactus";

        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName
        {
            get { return _sitename; }
            set { _sitename = value; }
        }

        private string _sitelogo = "/Content/Images/Logo.png";

        /// <summary>
        /// 站点Logo
        /// </summary>
        public string SiteLogo
        {
            get { return _sitelogo; }
            set { _sitelogo = value; }
        }

        private string _siteurl = "";

        /// <summary>
        /// 站点域名
        /// </summary>
        public string SiteUrl
        {
            get { return _siteurl; }
            set { _siteurl = value; }
        }


        private string _sitecrod = "";

        /// <summary>
        /// 站点备案号
        /// </summary>
        public string SiteCrod
        {
            get { return _sitecrod; }
            set { _sitecrod = value; }
        }

        private string _sitetitle = "Cactus";

        /// <summary>
        /// 站点标题
        /// </summary>
        public string SiteTitle
        {
            get { return _sitetitle; }
            set { _sitetitle = value; }
        }

        private string _sitekeywords = "Cactus";

        /// <summary>
        /// 站点关键字
        /// </summary>
        public string SiteKeywords
        {
            get { return _sitekeywords; }
            set { _sitekeywords = value; }
        }

        private string _sitedescr = "Cactus";

        /// <summary>
        /// 站点描述
        /// </summary>
        public string SiteDescr
        {
            get { return _sitedescr; }
            set { _sitedescr = value; }
        }

        private string _sitecopyright = "Copyright ©2015 漫漫洒洒";

        /// <summary>
        /// 站点版权信息
        /// </summary>
        public string SiteCopyRight
        {
            get { return _sitecopyright; }
            set { _sitecopyright = value; }
        }

        /// <summary>
        /// 是否关闭站点
        /// </summary>
        public bool SiteStatus { get; set; }

        private string _sitecountcode = "";

        /// <summary>
        /// 站点统计代码
        /// </summary>
        public string SiteCountCode
        {
            get { return _sitecountcode; }
            set { _sitecountcode = value; }
        }
        
        private string _sitestaticdir = "";
        /// <summary>
        /// 站点资源地址（目录或者域名,方便存放单独文件服务器或者CDN）
        /// </summary>
        public string SiteStaticDir
        {
            get { return _sitestaticdir; }
            set { _sitestaticdir = value; }
        }

        #endregion

        #region 图片上传设置

        private string _imgextensions = "*.jpg*.jpeg*.gif*.bmp*.png";

        /// <summary>
        /// 图片上传类型
        /// </summary>
        public string ImgExtensions
        {
            get { return _imgextensions; }
            set { _imgextensions = value; }
        }
        
        private string _defaultavatar = "/Upload/Avatars/avatar.jpg";

        /// <summary>
        /// 默认用户头像
        /// </summary>
        public string DefaultAvatar
        {
            get { return _defaultavatar; }
            set { _defaultavatar = value; }
        }

        public SiteConfig()
        {
            SiteStatus = true;
        }
        #endregion

        public override string ToString()
        {
            return this.SiteName;
        }
    }
}
