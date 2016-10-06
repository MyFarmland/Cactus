using Cactus.Common;
using Cactus.Model.Sys.Enums;
using System;
using System.Diagnostics;
using System.IO;

namespace Cactus.Crond.ThreadTask
{
	public class DefaultBase:ITask {
        public void Execute(Plan plan)
        {
            Debug.WriteLine("Execute plan:"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
		}
	}
}