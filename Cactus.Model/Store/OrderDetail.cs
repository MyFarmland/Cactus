using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.Model.Store
{
    /// <summary>
    /// 订单明细
    /// </summary>
    public class OrderDetail
    {
        /// <summary>
        /// 主ID
        /// </summary>
        public int Od_Id { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        public int OId { get; set; }
        /// <summary>
        /// 商品名（会加上属性）
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
