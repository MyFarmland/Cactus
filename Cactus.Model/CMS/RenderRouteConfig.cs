using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.Model.CMS
{
    public class RenderRouteConfig
    {
        List<RouteRule> RouteList { get; set; }
    }
    public class RouteRule
    {
        /// <summary>
        /// 路径规则
        /// </summary>
        public string PathRule { get;set; }
        /// <summary>
        /// 视图名
        /// </summary>
        public string ViewName { get; set; }
        /// <summary>
        /// 视图介绍
        /// </summary>
        public string ViewDes { get; set; }
        /// <summary>
        /// 视图文件全地址（不包含Theme的文件地址）
        /// </summary>
        public string ViewFullPath { get; set; }

    }
}
