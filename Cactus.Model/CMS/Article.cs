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
		/// 是否置顶
		/// </summary>
		/// <value><c>true</c> if this instance is top; otherwise, <c>false</c>.</value>
		public bool IsTop{ get; set; }
		/// <summary>
		/// 是否显示
		/// </summary>
		/// <value><c>true</c> if this instance is show; otherwise, <c>false</c>.</value>
		public bool IsShow{ get; set; }
	}
}

