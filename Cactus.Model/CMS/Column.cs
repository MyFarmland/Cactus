using System;

namespace Cactus.Model.CMS
{
	public class Column
	{
		public Column ()
		{
			
		}
		/// <summary>
		/// 类目ID
		/// </summary>
		/// <value>The category identifier.</value>
        public int Column_Id { get; set; }
		/// <summary>
		/// 类目排序编号
		/// </summary>
		/// <value>The sort.</value>
		public int Sort{get;set;}
		/// <summary>
		/// 类目名
		/// </summary>
		/// <value>The name of the category.</value>
        public string ColumnName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Pid { get; set; }

        public int Lv { get; set; }
	}
}

