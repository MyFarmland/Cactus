using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cactus.Model.Other
{
    [Serializable()]
    public class ObjResultModel : ResultModel
    {
        public object obj { get; set; }
    }
}
