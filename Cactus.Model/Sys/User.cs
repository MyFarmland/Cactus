using System;

namespace Cactus.Model.Sys
{
    /// <summary>
    /// 管理员
    /// </summary>
    [Serializable()]
    public class User
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int User_Id { get; set; }

        public int RoleId { get; set; }
        /// <summary>
        /// 管理员角色
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string Qq { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 最近登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 最近登录IP
        /// </summary>
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsSuperUser { get; set; }

    }
}
