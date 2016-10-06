 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.Model.Store
{
    /// <summary>
    /// 商店信息
    /// </summary>
    public class StoreInfo
    {
        /// <summary>
        /// 商店ID
        /// </summary>
        public int Store_Id { get; set; }
        /// <summary>
        /// 店铺名
        /// </summary>
        public int StoreName { get; set; }
        /// <summary>
        /// 店铺Logo
        /// </summary>
        public int StoreLogoPath { get; set; }
        /// <summary>
        /// 商店描述
        /// </summary>
        public int StoreDes { get; set; }
        /// <summary>
        /// 开关，表明店铺是否营业
        /// </summary>
        public bool StoreSwitch { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        public DateTime LastTime { get; set; }
    }
}
