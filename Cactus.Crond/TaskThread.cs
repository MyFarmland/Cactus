using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Web;
using System.Xml;

namespace Cactus.Crond
{
	/// <summary>
	/// 目标任务线程
	/// </summary>
	public partial class TaskThread:IDisposable {
		#region Fields
		/// <summary>
		/// 定时器
		/// </summary>
		private Timer _timer;
		/// <summary>
		/// 是否释放
		/// </summary>
		private bool _disposed;

		/// <summary>
		/// 开始时间
		/// </summary>
		private DateTime _started;

		/// <summary>
		/// 是否在运行
		/// </summary>
		private bool _isRunning;

		/// <summary>
		/// 任务字典列表
		/// </summary>
		private Dictionary<string, Task> _tasks;
        
        /// <summary>
        /// 
        /// </summary>
        private int _interval;
		#endregion

		#region .ctor

		/// <summary>
		/// 私有无参构造函数
		/// </summary>
		private TaskThread()
		{
			this._tasks=new Dictionary<string, Task>();
            this._interval = 1000;
		}

		/// <summary>
		/// 当前程序集的构造函数
		/// </summary>
		/// <param name="node"></param>
        internal TaskThread(int interval)
        {
            this._tasks = new Dictionary<string, Task>();
            this._isRunning = false;
            if (interval >= 0)
            {
                this._interval = interval*1000;
            }
            else {
                this._interval = 1000;
            }
        }
		#endregion

		#region Methods

		/// <summary>
		/// 执行任务
		/// </summary>
		private void Run()
		{
			this._started = DateTime.Now;
			this._isRunning = true;
			foreach (var task in _tasks.Values)
			{
				task.Execute();
			}
			this._isRunning = false;
		}

		/// <summary>
		/// 计时器处控制器
		/// </summary>
		/// <param name="state"></param>
		private void TimerHandler(object state)
		{
			this._timer.Change(-1, -1);//计时器马上开始执行
			this.Run();;
			this._timer.Change(this.Interval, this.Interval);//执行完 再间隔
		}
		
		/// <summary>
		/// 释放计时器
		/// </summary>
		public void Dispose() {
			if ((this._timer != null) && !this._disposed)
			{
				lock (this)
				{
					this._timer.Dispose();
					this._timer = null;
					this._disposed = true;
				}
			}
		}

		/// <summary>
		/// 初始化一个计时器
		/// </summary>
		public void InitTimer()
		{
			if (this._timer == null)
			{
				this._timer=new Timer(new TimerCallback(TimerHandler),null,this.Interval,this.Interval);
			}
		}

		/// <summary>
		/// 添加任务到当前线程中
		/// </summary>
		/// <param name="task"></param>
		public void AddTask(Task task)
		{
			if (!this._tasks.ContainsKey(task.Name))
			{
				this._tasks.Add(task.Name,task);
			}
		}
		#endregion

		#region Properties

		/// <summary>
        /// 任务执行之间的间隔
		/// </summary>
		public int Interval
		{
			get
			{
                return this._interval;
			}
		}

		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime Started
		{
			get
			{
				return this._started;
			}
		}
		/// <summary>
		/// 获取当前是否在线程中运行
		/// </summary>
		public bool IsRunning
		{
			get
			{
				return this._isRunning;
			}
		}
		/// <summary>
		/// 获取当前任务列表
		/// </summary>
		public IList<Task> Tasks
		{
			get
			{
				var list = new List<Task>();
				foreach (var task in _tasks.Values)
				{
					list.Add(task);
				}
				return new ReadOnlyCollection<Task>(list);
			}
		}
		#endregion

	}
}