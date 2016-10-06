using System;
using System.Collections;
using System.Web;
using System.Text;
using System.IO;
using System.Runtime.Caching;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;
using System.Collections.Generic;

namespace Cactus.Common
{
    /// <summary>
    /// 缓存对象数据结构
    /// </summary>
    [Serializable()]
    public class CacheData{
        public object Value { get;set;}
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        public DateTimeOffset AbsoluteExpiration { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime FailureTime { get 
        { if (AbsoluteExpiration == System.Runtime.Caching.ObjectCache.InfiniteAbsoluteExpiration) 
            {
                return AbsoluteExpiration.DateTime;
            } 
            else { return CreateTime.AddTicks(AbsoluteExpiration.Ticks); } } 
        }
        public CacheItemPriority Priority { get; set; }
    }
    
    /// <summary>
    /// 缓存处理类(MemoryCache)
    /// </summary>
    public class CacheHelper
    {
        //在应用程序的同级目录(主要防止外部访问)
        public static string filePath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.ConnectionStrings["filecache"].ConnectionString);
        //文件扩展名
        public static string fileExt = ".cache";
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public static object GetCache(string cacheKey)
        {
            long i=System.Runtime.Caching.MemoryCache.Default.GetCount();
            CacheItem objCache=System.Runtime.Caching.MemoryCache.Default.GetCacheItem(cacheKey);
            if (objCache == null)
            {
                string _filepath = filePath + cacheKey + fileExt;
                if (File.Exists(_filepath))
                {
                    FileStream _file = File.OpenRead(_filepath);
                    if (_file.CanRead)
                    {
                        Debug.WriteLine("缓存反序列化获取数据：" + cacheKey);
                        object obj = CacheHelper.BinaryDeSerialize(_file);
                        CacheData _data = (CacheData)obj;
                        if (_data != null)
                        {
                            //判断是否过期
                            if (_data.FailureTime >= DateTime.Now)
                            {
                                //将数据添加到内存
                                Debug.WriteLine("数据未过期：" + cacheKey);
                                CacheHelper.SetCacheToMemory(cacheKey, _data);
                                return _data.Value;
                            }
                            else
                            {
                                Debug.WriteLine("数据过期：" + cacheKey);
                                File.Delete(_filepath);
                                //数据过期
                                return null;
                            }
                        }
                        else { return null; }
                    }
                    else { return null; }
                }
                else { return null; }
            }
            else {
                CacheData _data = (CacheData)objCache.Value;
                return _data.Value;
            }
        }
        /// <summary>
        /// 内存缓存数
        /// </summary>
        /// <returns></returns>
        public static object GetCacheCount()
        {
            return System.Runtime.Caching.MemoryCache.Default.GetCount();
        }
        /// <summary>
        /// 文件缓存数
        /// </summary>
        /// <returns></returns>
        public static object GetFileCacheCount()
        {
            DirectoryInfo di = new DirectoryInfo(filePath);
            return di.GetFiles().Length;
        }   
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static bool SetCache(string cacheKey, object objObject, CacheItemPolicy policy)
        {
            //System.Web.Caching.Cache objCache = HttpRuntime.Cache;            
            //objCache.Insert(cacheKey, objObject);
            string _filepath = filePath + cacheKey + fileExt;
            if (Directory.Exists(filePath)==false) {
                Directory.CreateDirectory(filePath);
            }
            //设置缓存数据的相关参数
            CacheData data = new CacheData() { Value = objObject, CreateTime = DateTime.Now, AbsoluteExpiration = policy.AbsoluteExpiration, Priority = policy.Priority };
            CacheItem objCache = new CacheItem(cacheKey, data);
            FileStream stream = null;
            if (File.Exists(_filepath) == false)
            {
                stream = new FileStream(_filepath, FileMode.CreateNew, FileAccess.Write, FileShare.Write);
            }
            else {
                stream = new FileStream(_filepath, FileMode.Create, FileAccess.Write, FileShare.Write);
            }
            CacheHelper.BinarySerialize(stream, data);
            return System.Runtime.Caching.MemoryCache.Default.Add(objCache, policy);
        }
        public static bool SetCacheToMemory(string cacheKey, CacheData data)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            CacheItem objCache = new CacheItem(cacheKey, data);
            policy.AbsoluteExpiration = data.AbsoluteExpiration;
            policy.Priority = CacheItemPriority.NotRemovable;
            return System.Runtime.Caching.MemoryCache.Default.Add(objCache, policy);
        }

        public static bool SetCache(string cacheKey, object objObject, DateTimeOffset AbsoluteExpiration)
        {
            //System.Web.Caching.Cache objCache = HttpRuntime.Cache;            
            //objCache.Insert(cacheKey, objObject);
            CacheItemPolicy _priority = new CacheItemPolicy();
            _priority.Priority = CacheItemPriority.NotRemovable;
            _priority.AbsoluteExpiration = AbsoluteExpiration;
            return SetCache(cacheKey, objObject, _priority);
        }

        public static bool SetCache(string cacheKey, object objObject, CacheItemPriority priority)
        {
            //System.Web.Caching.Cache objCache = HttpRuntime.Cache;            
            //objCache.Insert(cacheKey, objObject);
            CacheItemPolicy _priority = new CacheItemPolicy();
            _priority.Priority = priority;
            _priority.AbsoluteExpiration = System.Runtime.Caching.ObjectCache.InfiniteAbsoluteExpiration;
            return SetCache(cacheKey, objObject, _priority);
        }
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static bool SetCache(string cacheKey, object objObject)
        {
            //System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            //objCache.Insert(cacheKey, objObject, null, DateTime.MaxValue, timeout, System.Web.Caching.CacheItemPriority.NotRemovable, null);
            return CacheHelper.SetCache(cacheKey, objObject, System.Runtime.Caching.CacheItemPriority.NotRemovable);
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public static void RemoveCache(string cacheKey)
        {
            //System.Web.Caching.Cache cache = HttpRuntime.Cache;
            //cache.Remove(cacheKey);
            System.Runtime.Caching.MemoryCache.Default.Remove(cacheKey);
            string _filepath = filePath + cacheKey + fileExt;
            File.Delete(_filepath);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            MemoryCache _cache = System.Runtime.Caching.MemoryCache.Default;
            foreach (var _c in _cache)
            {
                _cache.Remove(_c.Key);
            }
            if (Directory.Exists(filePath))
            {
                Directory.Delete(filePath, true);
                Directory.CreateDirectory(filePath);
            }
        }
        /// <summary>
        /// 删除创建时间小于指定时间的所有缓存
        /// </summary>
        /// <param name="time"></param>
        /// <param name="type"></param>
        public static void RemoveAllCache(DateTime time,int type) {
            if (type == 1)
            {
                MemoryCache _cache = System.Runtime.Caching.MemoryCache.Default;
                foreach (var _c in _cache)
                {
                    CacheItem objCache = _cache.GetCacheItem(_c.Key);
                    CacheData _data = (CacheData)objCache.Value;
                    if (_data.CreateTime <= time)
                    {
                        _cache.Remove(_c.Key);
                    }
                }
            }
            else if (type == 2)
            {
                if (Directory.Exists(filePath))
                {
                    FileInfo[] files = new DirectoryInfo(filePath).GetFiles();
                    foreach (var _f in files)
                    {
                        if (_f.CreationTime <= time)
                        {
                            string _filepath = filePath + _f.Name;
                            File.Delete(_filepath);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 清除指定缓存
        /// </summary>
        /// <param name="type">1:内存 2:文件</param>
        public static void RemoveAllCache(int type)
        {
            if (type == 1) 
            {
                MemoryCache _cache = System.Runtime.Caching.MemoryCache.Default;
                foreach (var _c in _cache)
                {
                    _cache.Remove(_c.Key);
                }
            } 
            else if (type == 2)
            {
                DirectoryInfo di = new DirectoryInfo(filePath);
                di.Delete(true);
            } 
        }

        #region 流序列化
        public static void BinarySerialize(Stream stream, object obj)
        {
            try
            {
                stream.Seek(0, SeekOrigin.Begin);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
            }
            catch (Exception e)
            {
                HIO.WriteDebug(e);
            }
            finally
            {
                //stream.Close();
                stream.Dispose();
            }
        }

        public static object BinaryDeSerialize(Stream stream)
        {
            object obj = null;
            stream.Seek(0, SeekOrigin.Begin);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                obj = formatter.Deserialize(stream);
            }
            catch (Exception e)
            {
                HIO.WriteDebug(e);
            }
            finally
            {
                //stream.Close();
                stream.Dispose();
            }
            return obj;
        }
        #endregion
    }
}
