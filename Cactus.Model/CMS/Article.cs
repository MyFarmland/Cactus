using System;

namespace Cactus.Model.CMS
{
	public class Article
	{
		public Article ()
		{
		}
		/// <summary>
		/// 文章ID
		/// </summary>
		/// <value>The article identifier.</value>
		public int Article_Id{ get; set;}
		/// <summary>
		/// 文章类目ID
		/// </summary>
		/// <value>The category identifier.</value>
        public int ColumnId { get; set; }
		/// <summary>
		/// 类目
		/// </summary>
		/// <value>The category.</value>
		public Column Column{ get; set;}
		/// <summary>
		/// 文章标签
		/// </summary>
		/// <value>The tags.</value>
		public string Tags{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TagIds { get; set; }
		/// <summary>
		/// 文章内容
		/// </summary>
		/// <value>The content.</value>
        public string ArticleContent { get; set; }
		/// <summary>
		/// 文章标题
		/// </summary>
		/// <value>The title.</value>
		public string Title{ get; set; }
		/// <summary>
		/// 文章创建时间
		/// </summary>
		/// <value>The create time.</value>
		public DateTime CreateTime{ get; set; }
		/// <summary>
		/// 文章最后一次修改时间
		/// </summary>
		/// <value>The last time.</value>
		public DateTime LastTime{ get; set; }
		/// <summary>
		/// 浏览数
		/// </summary>
		/// <value>The browse.</value>
		public int Browse{ get; set;}
		/// <summary>
		/// 文章作者
		/// </summary>
		/// <value>The author.</value>
		public string Author{ get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Digest { get; set; }
        /// <summary>
        /// SEO标题
        /// </summary>
        public string SEO_Title { get; set; }
        /// <summary>
        /// SEO关键词
        /// </summary>
        public string SEO_Keywords { get; set; }
        /// <summary>
        /// SEO描述
        /// </summary>
        public string SEO_DES { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        public string ImgUrl { get; set; }
		/// <summary>
		/// 是否置顶
		/// </summary>
		public bool IsTop{ get; set; }
		/// <summary>
		/// 是否显示
		/// </summary>
		public bool IsShow{ get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source{get;set;}
        /// <summary>
        /// 来源地址
        /// </summary>
        public string SourceLink{get;set;}
        /// <summary>
        /// 赞成数
        /// </summary>
        public int Like { get; set; }
	}
}

