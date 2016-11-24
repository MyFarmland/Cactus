using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.Model.Other
{
    public class EnumsModel
    {
        /// <summary>
        /// 统一管理操作枚举
        /// </summary>
        public enum ActionEnum
        {
            /// <summary>
            /// 显示
            /// </summary>
            Show,
            /// <summary>
            /// 查看
            /// </summary>
            View,
            /// <summary>
            /// 添加
            /// </summary>
            Add,
            /// <summary>
            /// 修改
            /// </summary>
            Edit,
            /// <summary>
            /// 删除
            /// </summary>
            Delete,
            /// <summary>
            /// 审核
            /// </summary>
            Audit,
            /// <summary>
            /// 回复
            /// </summary>
            Reply,
            /// <summary>
            /// 确认
            /// </summary>
            Confirm,
            /// <summary>
            /// 取消
            /// </summary>
            Cancel,
            /// <summary>
            /// 作废
            /// </summary>
            Invalid,
            /// <summary>
            /// 生成
            /// </summary>
            Build,
            /// <summary>
            /// 安装
            /// </summary>
            Instal,
            /// <summary>
            /// 卸载
            /// </summary>
            UnLoad,
            /// <summary>
            /// 登录
            /// </summary>
            Login,
            /// <summary>
            /// 备份
            /// </summary>
            Back,
            /// <summary>
            /// 还原
            /// </summary>
            Restore,
            /// <summary>
            /// 替换
            /// </summary>
            Replace,
            /// <summary>
            /// 复制
            /// </summary>
            Copy,
            /// <summary>
            /// 重置
            /// </summary>
            Reset,
            /// <summary>
            /// 上传
            /// </summary>
            Upload,
            /// <summary>
            /// 选择
            /// </summary>
            Select
        }

        public static Dictionary<string, string> ActionType()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Show", "显示");
            dic.Add("View", "查看");
            dic.Add("Add", "添加");
            dic.Add("Edit", "修改");
            dic.Add("Delete", "删除");
            dic.Add("Audit", "审核");
            dic.Add("Reply", "回复");
            dic.Add("Confirm", "确认");
            dic.Add("Cancel", "取消");
            dic.Add("Invalid", "作废");
            dic.Add("Build", "生成");
            dic.Add("Instal", "安装");
            dic.Add("Unload", "卸载");
            dic.Add("Back", "备份");
            dic.Add("Restore", "还原");
            dic.Add("Replace", "替换");
            dic.Add("Reset", "重置");
            dic.Add("Upload", "上传");
            dic.Add("Select", "选择");
            return dic;
        }
    }
}
