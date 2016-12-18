using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.Model.Sys
{
    public class SysLog
    {
        public int Log_Id { get; set; }

        public int UserId { get; set; }
        
        public virtual User User { get; set; }

        public string LogInfo { get; set; }

        public long CreateTs { get; set; }
    }
}
