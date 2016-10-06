using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.Model.Store
{
    /// <summary>
    /// 订单
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 主ID
        /// </summary>
        public string Order_Id { get; set; }
        /// <summary>
        /// 商店Id
        /// </summary>
        public int StoreId { get; set; }
        /// <summary>
        /// 商店实体
        /// </summary>
        public StoreInfo Store { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CId { get; set; }
        /// <summary>
        /// 客户实体
        /// </summary>
        public Customer Customer { get; set; }
        /// <summary>
        /// 详细
        /// </summary>
        public string Des { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
