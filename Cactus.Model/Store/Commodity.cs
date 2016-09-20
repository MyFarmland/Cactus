using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.Model.Store
{
    /// <summary>
    /// 商品
    /// </summary>
    public class Commodity
    {
        /// <summary>
        /// 主ID
        /// </summary>
        public int C_Id { get; set; }
        /// <summary>
        /// 商店ID
        /// </summary>
        public int StoreId { get; set; }
        /// <summary>
        /// 商店实体
        /// </summary>
        public StoreInfo Store { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// 商品图（允许多个的话就用英文逗号分隔）
        /// </summary>
        public string PicPath { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }
        /// <summary>
        /// 商品详细
        /// </summary>
        public string Des { get; set; }
        /// <summary>
        /// 属性名（比如颜色，尺寸），非必要使用，使用后库存为属性中的库存数之和
        /// </summary>
        public string ProName { get; set; }
        /// <summary>
        /// 属性参数（属性名+库存逐个分隔）
        /// </summary>
        public string ProDetail { get; set; }
        /// <summary>
        /// 类目
        /// </summary>
        public int CatId { get; set; }
        /// <summary>
        /// 类目实体
        /// </summary>
        public Category Category { get; set; }
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
