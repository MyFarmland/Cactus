using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.Model.Store
{
    public class Category
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Cat_Id { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>
        public int StoreId { get; set; }
        /// <summary>
        /// 店铺实体
        /// </summary>
        public StoreInfo Store { get; set; }
        /// <summary>
        /// 类别名
        /// </summary>
        public string CatName { get; set; }
        /// <summary>
        /// 类别描述
        /// </summary>
        public string CatDes { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        public int PId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastTime { get; set; }
    }
}
