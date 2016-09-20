using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml;

namespace Cactus.Crond
{

	/// <summary>
	/// 需要执行的任务 从config 读取配置信息
	/// </summary>
	public partial class Task {

		#region Fields

		/// <summary>
		/// 任务接口
		/// </summary>
		private ITask _task;

		/// <summary>
		/// 是否开启任务
		/// </summary>
		private bool _enabled;

		/// <summary>
		/// 任务类型
		/// </summary>
		private Type _taskType;

		/// <summary>
		/// 任务名称
		/// </summary>
		private string _name;

		/// <summary>
		/// 遇见错误是否停止
		/// </summary>
		private bool _stopOnError;

		/// <summary>
		/// 单个plan
		/// </summary>
        private Plan _plan;

		/// <summary>
		/// 上次执行开始时间
		/// </summary>
		private DateTime _lastStarted;

		/// <summary>
		/// 上次执行成功时间
		/// </summary>
		private DateTime _lastSuccess;

		/// <summary>
		/// 上次执行结束时间
		/// </summary>
		private DateTime _lastEnd;

		/// <summary>
		/// 是否启动运行
		/// </summary>
		private bool _isRunning;

		#endregion

		#region .ctor
		/// <summary>
		/// 无参的私有构造方法
		/// </summary>
		private Task()
		{
			this._enabled = true;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="taskType"></param>
		/// <param name="_plan"></param>
        public Task(Type taskType, Plan _plan)
        {
            this._enabled = true;
            this._plan = _plan;
            this._taskType = taskType;
            this._enabled = _plan.Enabled;
            this._stopOnError = _plan.StopOnError;
            this._name = _plan.Name;
        }
		#endregion

		#region Methods

		public ITask createTask()
		{
			if (this.Enabled && (this._task == null))
			{
				if (this._taskType != null)
				{
					this._task = Activator.CreateInstance(this._taskType) as ITask;
				}
			}
			return this._task;
		}

		public void Execute()
		{
			this._isRunning = true;
			try
			{
				var task = this.createTask();
				if (task != null)
				{
					this._lastStarted = DateTime.Now;
					task.Execute(this._plan);
					this._lastEnd = this._lastSuccess = DateTime.Now;
				}
			}
			catch (Exception ex)
			{
				this._enabled = !this.StopOnError;
				this._lastEnd = DateTime.Now;
                Debug.WriteLine("Execute Error:"+ex.Message);
				//throw;
			}
			this._isRunning = false;
		}
		#endregion

		#region Properties

		/// <summary>
		/// 指示任务是否在执行
		/// </summary>
		public bool IsRunning
		{
			get
			{
				return this._isRunning;
			}
		}
		/// <summary>
		/// 上次开始时间
		/// </summary>
		public DateTime LastStarted
		{
			get
			{
				return this._lastStarted;
			}
		}

		/// <summary>
		/// 上次执行成功时间
		/// </summary>
		public DateTime LastSuccess
		{
			get
			{
				return this._lastStarted;
			}
		}

		/// <summary>
		/// 上次执行结束时间
		/// </summary>
		public DateTime LastEnd
		{
			get
			{
				return this._lastEnd;
			}
		}

		/// <summary>
		/// 任务类型
		/// </summary>
		public Type TaskType
		{
			get
			{
				return this._taskType;
			}
		}

		/// <summary>
		/// 是否停止当差生错误
		/// </summary>
		public bool StopOnError
		{
			get
			{
				return this._stopOnError;
			}
		}

		/// <summary>
		/// 名称
		/// </summary>
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>
		/// 是否启用
		/// </summary>
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
		}
		#endregion
	}
}