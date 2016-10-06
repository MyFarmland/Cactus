
using System;
namespace Cactus.Model.Other
{
    /// <summary>
    /// 普通执行结果模型
    /// </summary>
    [Serializable()]
    public class ResultModel
    {
        /// <summary>
        /// 是否通过
        /// </summary>
        public bool pass { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 附加数据
        /// </summary>
        public object append { get; set; }
    }
}
