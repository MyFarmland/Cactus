using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Cactus.Crond
{

	/// <summary>
	/// 执行任务接口 
	/// </summary>
	public partial interface ITask
	{
		/// <summary>
		/// 执行webconfig 配置的节点
		/// </summary>
		/// <param name="node"></param>
		void Execute(Plan n_plan);
	}
}
