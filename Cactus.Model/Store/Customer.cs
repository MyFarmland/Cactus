using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.Model.Store
{
    /// <summary>
    /// 客户
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// 客户Id
        /// </summary>
        public int U_Id { get; set; }
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
        public string Phone { get;set;}
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
