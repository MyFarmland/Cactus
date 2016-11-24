using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace HTools
{
    public class HMemoryCache: ICacheService
    {
        #region 字段
        /// <summary>
        /// 缓存集合
        /// </summary>
        private static ConcurrentDictionary<string, CacheObj> _cacheList;
        /// <summary>
        /// 时间集合
        /// </summary>
        private static ConcurrentDictionary<string, DateTime> _cacheTimeList;
        /// <summary>
        /// 缓存集合最大值
        /// </summary>
        private static int MaxCount;
        /// <summary>
        /// 监视线程
        /// </summary>
        private static Thread SurveillantThread;
        /// <summary>
        /// 巡逻一次的间隔（s）
        /// </summary>
        private static int PatrolInterval = 10;
        /// <summary>
        /// 是否监视缓存集合
        /// </summary>
        private static bool IsSurveillant = true;
        #endregion

        static object lock_obj = new object();

        /// <summary>
        /// 本地内存缓存集合
        /// </summary>
        public HMemoryCache(int maxCount = 100000, bool isSurveillant = true)
        {
            Debug.WriteLine("HCache Init");
            IsSurveillant = isSurveillant;
            Instance(maxCount);
            Debug.WriteLine("IsSurveillant:" + IsSurveillant);
            Debug.WriteLine("MaxCount:" + MaxCount);
        }

        #region private
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        private static void Instance(int maxCount)
        {
            //这里可以保证只实例化一次
            //即在第一次调用时实例化
            //以后调用便不会再实例化
            //采用双重锁定
            if (_cacheList == null)
            {
                lock (lock_obj)
                {
                    ConcurrentDictionary<string, CacheObj> temp = _cacheList;
                    if (temp == null) {
                        lock (lock_obj)
                        {
                            temp = new ConcurrentDictionary<string, CacheObj>();
                        }
                        _cacheList = temp;
                    }
                }
                if (maxCount > Int32.MaxValue)
                {
                    MaxCount = Int32.MaxValue;
                }
                else
                {
                    MaxCount = maxCount;
                }
            }
            else { 
                if (_cacheList.Count < maxCount) 
                {
                    if (maxCount > Int32.MaxValue)
                    {
                        MaxCount = Int32.MaxValue;
                    }
                    else
                    {
                        MaxCount = maxCount;
                    }
                }
            }
            if (_cacheTimeList == null) {
                _cacheTimeList = new ConcurrentDictionary<string, DateTime>();
                lock (lock_obj)
                {
                    ConcurrentDictionary<string, DateTime> temp = _cacheTimeList;
                    if (temp == null)
                    {
                        lock (lock_obj)
                        {
                            temp = new ConcurrentDictionary<string, DateTime>();
                        }
                        _cacheTimeList = temp;
                    }
                }
            }
            if (SurveillantThread == null) {
                lock (lock_obj)
                {
                    SurveillantThread = new Thread(SurveillantTask);
                    SurveillantThread.Name = "SurveillantThread";
                    SurveillantThread.IsBackground = true;
                    if (IsSurveillant)
                    {
                        SurveillantThread.Start();
                    }
                }
            }
        }
        /// <summary>
        /// 监控主体函数
        /// </summary>
        /// <param name="obj"></param>
        private static void SurveillantTask(object obj) {
            while (true) {
                try
                {
                    if (_cacheTimeList.Keys.Count > 0)
                    {
                        string[] key_arr = new string[_cacheTimeList.Keys.Count];//临时的key
                        _cacheTimeList.Keys.CopyTo(key_arr, 0);
                        for (int j = 0; j < key_arr.Length; j++)
                        {
                            DateTime dt = new DateTime();
                            string key = key_arr[j];
                            if (string.IsNullOrEmpty(key)) { continue; }
                            if (_cacheTimeList.TryGetValue(key, out dt)) {
                                if (dt != null)
                                {
                                    if (DateTime.Now > dt)
                                    {
                                        CacheObj c_obj = new CacheObj();
                                        _cacheTimeList.TryRemove(key, out dt);
                                        _cacheList.TryRemove(key, out c_obj);
                                        Debug.WriteLine("监控线程移除key:" + key);
                                    }
                                }
                            }
                        }
                    }
                }
                catch(Exception ex){
                    Debug.WriteLine("Message:"+ex.Message);
                }
                if (PatrolInterval < 5)
                {
                    PatrolInterval = 5;//不要间隔太短
                }
                Thread.Sleep(PatrolInterval * 1000);
            }
        }
        /// <summary>
        /// 过期判断
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool IsOverdue(CacheObj obj) 
        {
            if (obj == null) { return false; }
            if (obj.FailureTime == null) { return false; }
            if (obj.FailureTime == DateTime.MaxValue)
            {
                return false;
            }
            else {
                return DateTime.Now > obj.FailureTime;
            }
        }
        #endregion

        public void Info() {
            Console.WriteLine("HCache Info");
            Debug.WriteLine("MaxCount:" + MaxCount);
            Debug.WriteLine("Count:" + _cacheList.Count);
        }
        public CacheObj Get(string key)
        {
            if (string.IsNullOrEmpty(key)) { return null; }
            CacheObj obj=new CacheObj();
             if (_cacheList.TryGetValue(key, out obj)) {
                if (IsOverdue(obj))
                {
                    this.Remove(key);
                    DateTime dt=new DateTime();
                    _cacheTimeList.TryRemove(key,out dt);
                    return null;
                }
                else {
                    return obj;
                }
            } else { return null; }
        }
        public IDictionary<string, CacheObj> GetValues(List<string> keys) {
            
            Dictionary<string, CacheObj> dic_t = new Dictionary<string, CacheObj>();
            foreach (var key in keys)
            {
                if (!string.IsNullOrEmpty(key)) {
                    CacheObj obj = new CacheObj();
                    if (_cacheList.TryGetValue(key, out obj))
                    {
                        if (!this.IsOverdue(obj))
                        {
                            dic_t.Add(key, obj);
                        }
                        else {
                            DateTime dt = new DateTime();
                            _cacheTimeList.TryRemove(key, out dt);
                        }
                    }
                }
            }
            return dic_t;
        }
        public int GetCount() 
        {
            return _cacheList.Count;
        }
        public bool isExists(string key)
        {
            if (string.IsNullOrEmpty(key)) { return false; }
            return _cacheList.ContainsKey(key);
        }
        public bool Add(string key, CacheObj obj)
        {
            if (_cacheList.Count > MaxCount) { return false; }
            else
            {
                try {
                    _cacheTimeList.TryAdd(key, obj.FailureTime);
                    return _cacheList.TryAdd(key, obj);
                }
                catch {
                    return false;
                }
            }
        }
        public bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key)) { return false; }
            CacheObj obj = new CacheObj();
            DateTime dt = new DateTime();
            _cacheTimeList.TryRemove(key, out dt);
            return _cacheList.TryRemove(key, out obj);
        }
        public bool Update(string key, CacheObj obj)
        {
            try
            {
                CacheObj temp = new CacheObj();
                if (_cacheList.TryGetValue(key, out temp))
                {
                    if (this.IsOverdue(obj))
                    {
                        return false;
                    }
                    else
                    {
                        _cacheTimeList.TryUpdate(key, temp.FailureTime, obj.FailureTime);
                        return _cacheList.TryUpdate(key, temp, obj);
                    }
                }
                else { return false; }
            }
            catch { return false; }
        }
        public void RemoveByKeys(List<string> keys)
        {
            foreach (var key in keys)
            {
                this.Remove(key);
            }
        }
        public bool RemoveAll()
        {
            try
            {
                _cacheList.Clear();
                _cacheTimeList.Clear();
                return true;
            }
            catch { return false; }
        }
    }
}
