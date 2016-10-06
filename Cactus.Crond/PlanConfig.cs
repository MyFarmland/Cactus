using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.Crond
{
    public class PlanConfig
    {
        public bool IsCache { get; set; }
        public List<PlanGroup> planGroupList { get; set; }
    }
    /// <summary>
    /// 计划组
    /// </summary>
    public class PlanGroup
    {
        public int interval { get; set; }
        public List<Plan> planList { get; set; }
    }
    /// <summary>
    /// 单个计划
    /// </summary>
    public class Plan
    {
        public string Name { get; set; }
        public string PlanType { get; set; }
        public bool Enabled { get; set; }
        public bool StopOnError { get; set; }

    }

}
