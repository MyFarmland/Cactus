using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Cactus.Crond
{

	/// <summary>
	/// 任务管理器-单利实例
	/// </summary>
	public partial class TaskManager {
		#region Fields
		private static readonly TaskManager _taskManager = new TaskManager();
		private List<TaskThread> _taskThreads = new List<TaskThread>();
		#endregion

		#region .ctor
		/// <summary>
		/// 无参私有构造函数
		/// </summary>
		private TaskManager() {

		}
		#endregion

		#region Methods

		/// <summary>
		/// 线程出错异常处理
		/// </summary>
		/// <param name="task"></param>
		/// <param name="exception"></param>
		internal void ProcessException(Task task, Exception exception) {
			try {
				//Process exception code here
			} catch {

			}
		}

        public void Initialize(string configPath) 
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(configPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var serializer = new XmlSerializer(typeof(PlanConfig));
                PlanConfig _planConfig= (PlanConfig)serializer.Deserialize(fs);
                this._taskThreads.Clear();
                foreach (var _planGroup in _planConfig.planGroupList)
                {
                    var taskThread = new TaskThread(_planGroup.interval);
                    this._taskThreads.Add(taskThread);
                    foreach (var _plan in _planGroup.planList) {
                        var taskType = Type.GetType(_plan.PlanType);
                        if (taskType != null)
                        {
                            var task = new Task(taskType, _plan);
                            taskThread.AddTask(task);
                        }
                    }
                }
                //var taskType = Type.GetType(attribute.Value);

            }
            catch
            {
                throw new Exception("文件XML反序列化失败");
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
		/// <summary>
		/// 开始任务管理的所有定时任务
		/// </summary>
		public void Start() {
			foreach (var taskThread in _taskThreads) {
				//开始执行任务的计时器
				taskThread.InitTimer();
			}
		}

		/// <summary>
		/// 清理任务管理器所有的定时任务
		/// </summary>
		public void Stop() {
			foreach (var taskThread in _taskThreads) {
				//停止计时器
				taskThread.Dispose();
			}
		}

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="_planConfig"></param>
        /// <param name="savePath"></param>
        public void Create(PlanConfig _planConfig, string savePath) {
            FileStream fs = null;
            try
            {
                fs = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                var serializer = new XmlSerializer(_planConfig.GetType());
                serializer.Serialize(fs, _planConfig);
            }
            catch
            {
                throw new Exception("文件XML序列化失败,创建PlanConfig失败");
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
		#endregion

		#region Properties

		/// <summary>
		/// 获取任务管理实例 形成单列实例
		/// </summary>
		public static TaskManager Instance {
			get {
				return _taskManager;
			}
		}

		/// <summary>
		/// 获取当前任务管理所有的线程任务
		/// </summary>
		public IList<TaskThread> TaskThreads {
			get {
				return new ReadOnlyCollection<TaskThread>(_taskThreads);
			}
		}
		#endregion
	}
}