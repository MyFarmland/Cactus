
using System;
namespace Cactus.Model.Other
{
    /// <summary>
    /// 列表数据模型
    /// </summary>
    [Serializable()]
    public class RowResultModel : ResultModel
    {
        public PageTurnModel pagination { get; set; }
        public object rows { get; set; }
    }
}
