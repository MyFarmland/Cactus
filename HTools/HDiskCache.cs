using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace HTools
{
    public class HDiskCache: ICacheService
    {
        #region 字段
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
        private static int PatrolInterval = 60;
        /// <summary>
        /// 是否监视缓存集合
        /// </summary>
        private static bool IsSurveillant = true;
        /// <summary>
        /// 存放目录
        /// </summary>
        private static string FileDir =@"..\filecache\";
        /// <summary>
        /// 文件后缀
        /// </summary>
        private static string Extension = ".cache";
        /// <summary>
        /// 全路径
        /// </summary>
        private static string CachePath="";
        #endregion

        static object lock_obj = new object();

        /// <summary>
        /// 本地内存缓存集合
        /// </summary>
        public HDiskCache(string fileDir=null, int maxCount = 100000, bool isSurveillant = true)
        {
            Debug.WriteLine("HCache Init");
            IsSurveillant = isSurveillant;
            Instance(maxCount);
            if (fileDir == null) {
                fileDir = FileDir;
            }
            CachePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileDir);
            if (!Directory.Exists(CachePath)) {
                Directory.CreateDirectory(CachePath);
            }
            Debug.WriteLine("CachePath:" + CachePath);
            Debug.WriteLine("Extension:" + Extension);
            Debug.WriteLine("IsSurveillant:" + IsSurveillant);
            Debug.WriteLine("MaxCount:" + MaxCount);
        }

        #region private

        private static byte[] Serialize(object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream ms=new MemoryStream();
            formatter.Serialize(ms, obj);
            return ms.GetBuffer();
        }
        private static string SerializeToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private static CacheObj Deserialization(byte[] bts)
        {
            IFormatter formatter = new BinaryFormatter();
            try
            {
                return (CacheObj)formatter.Deserialize(new MemoryStream(bts));
            }
            catch(Exception ex){
                Debug.WriteLine("Message:" + ex.Message);
                return null;
            }
        }

        private static CacheObj DeserializationByJson(string strJson)
        {
            return JsonConvert.DeserializeObject<CacheObj>(strJson);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        private static void Instance(int maxCount)
        {
            if (maxCount > Int32.MaxValue)
            {
                MaxCount = Int32.MaxValue;
            }
            else
            {
                MaxCount = maxCount;
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
                Debug.WriteLine("监视序列化的文件："+DateTime.Now.ToString());
                try
                {
                    if (Directory.GetFiles(CachePath).Length > 0)
                    {
                        string[] key_arr = Directory.GetFiles(CachePath);//临时的key
                        for (int j = 0; j < key_arr.Length; j++)
                        {
                            string key = key_arr[j];
                            if (string.IsNullOrEmpty(key)) { continue; }
                            CacheObj tempobj=new CacheObj();
                            string filepath = key;
                            if (File.Exists(filepath)){
                                tempobj = DeserializationByJson(File.ReadAllText(filepath));
                                if (DateTime.Now > tempobj.FailureTime)
                                {
                                    try
                                    {
                                        File.Delete(filepath);
                                        Debug.WriteLine("监控线程移除key:" + key);
                                    }
                                    catch(Exception ex){
                                        Debug.WriteLine("监控线程 Error:"+ex.Message);
                                    }
                                }
                            }
                        }
                    }
                }
                catch(Exception ex){
                    Debug.WriteLine("SurveillantTask Message:" + ex.Message);
                }
                if (PatrolInterval < 10)
                {
                    PatrolInterval = 10;//不要间隔太短
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
            Debug.WriteLine("Count:" + Directory.GetFiles(CachePath).Length);
        }
        public CacheObj Get(string key)
        {
            if (string.IsNullOrEmpty(key)) { return null; }
            string path = Path.Combine(CachePath, key.Trim() + Extension);
            if (File.Exists(path))
            {
                CacheObj obj = DeserializationByJson(File.ReadAllText(path));
                if (IsOverdue(obj))
                {
                    this.Remove(key);
                    return null;
                }
                else
                {
                    return obj;
                }
            }
            else {
                return null;
            }
        }
        public IDictionary<string, CacheObj> GetValues(List<string> keys) {
            
            Dictionary<string, CacheObj> dic_t = new Dictionary<string, CacheObj>();
            foreach (var key in keys)
            {
                if (!string.IsNullOrEmpty(key)) {
                    CacheObj obj = Get(key);
                    if (obj != null)
                    {
                        if (!this.IsOverdue(obj))
                        {
                            dic_t.Add(key, obj);
                        }
                    }
                }
            }
            return dic_t;
        }
        public int GetCount() 
        {
            return Directory.GetFiles(CachePath).Length;
        }
        public bool isExists(string key)
        {
            if (string.IsNullOrEmpty(key)) { return false; }
            return File.Exists(Path.Combine(CachePath, key.Trim() + Extension));
        }
        public bool Add(string key, CacheObj obj)
        {
            if (string.IsNullOrEmpty(key)) { return false; }
            if (GetCount() > MaxCount) { return false; }
            string path = Path.Combine(CachePath, key.Trim() + Extension);
            if (!File.Exists(path) && obj != null)
            {
                try
                {
                    File.WriteAllText(path, SerializeToJson(obj));
                    return true;
                }
                catch(Exception ex){
                    Debug.WriteLine("Add Error 未命中:" + ex.Message);
                    return false; }
            }
            else {
                return false;
            }
        }
        public bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key)) { return false; }
            string path = Path.Combine(CachePath, key.Trim() + Extension);
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                    return true;
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("Remove Error 未命中:" + ex.Message);
                    return false;
                }
            }
            else { return false; }
        }
        public bool Update(string key, CacheObj obj)
        {
            try
            {
                if (string.IsNullOrEmpty(key)) { return false; }
                string path = Path.Combine(CachePath, key.Trim() + Extension);
                if (File.Exists(path))
                {
                    File.WriteAllText(path,SerializeToJson(obj));
                    return true;
                }
                else { return false; }
            }
            catch(Exception ex){
                Debug.WriteLine("Update Error 未命中:" + ex.Message);
                return false; }
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
                Directory.Delete(CachePath, true);
                Directory.CreateDirectory(CachePath);
                return true;
            }
            catch { return false; }
        }
    }
}
