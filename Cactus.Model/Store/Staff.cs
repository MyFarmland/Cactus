using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.Model.Store
{
    /// <summary>
    /// 工作员工
    /// </summary>
    public class Staff
    {
        /// <summary>
        /// 主Id
        /// </summary>
        public int Staff_Id { get; set; }
        /// <summary>
        /// 店铺id
        /// </summary>
        public int StoreId { get; set; }
        /// <summary>
        /// 店铺
        /// </summary>
        public StoreInfo Storeinfo { get; set; }
        /// <summary>
        /// 账户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string AvatarPath { get; set; }
        /// <summary>
        /// 目标ip
        /// </summary>
        public string TargetIp { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsUsable { get; set; }
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
