using Cactus.Common;
using Cactus.IService;
using Cactus.Model.Other;
using System;
using System.Configuration;
using System.IO;
using System.Runtime.Caching;
using System.Runtime.Serialization.Formatters.Binary;
using HTools;
using System.Collections.Generic;

namespace Cactus.BaseService
{
    /// <summary>
    /// 自制缓存组件
    /// </summary>
    public class CacheService : ICacheService
    {
        private static HTools.HMemoryCache m_mcache = new HTools.HMemoryCache();
        private static HTools.HDiskCache m_dcache = new HTools.HDiskCache(ConfigurationManager.ConnectionStrings["filecache"].ConnectionString);
        public HTools.CacheObj Get(string key)
        {
            //内存和磁盘双重获取
            if (string.IsNullOrEmpty(key)) { return null; }
            CacheObj obj=m_mcache.Get(key);
            if (obj == null)
            {
                obj=m_dcache.Get(key);
                if(obj!=null){
                    m_mcache.Add(key, obj);
                }
                return m_dcache.Get(key);
            }
            else {
                return obj;
            }
        }

        public System.Collections.Generic.IDictionary<string, HTools.CacheObj> GetValues(System.Collections.Generic.List<string> keys)
        {
            IDictionary<string, CacheObj> first = m_dcache.GetValues(keys);
            IDictionary<string, CacheObj> second = m_mcache.GetValues(keys);
            if (second.Count == 0) return first;
            foreach (string key in second.Keys)
            {
                if (!first.ContainsKey(key))
                    first.Add(key, second[key]);
            }
            return first;
        }

        public int GetCount()
        {
            //只返回内存的
            return m_mcache.GetCount();
        }

        public bool isExists(string key)
        {
            if (string.IsNullOrEmpty(key)) { return false; }
            return m_mcache.isExists(key) || m_dcache.isExists(key);
        }

        public bool Add(string key, HTools.CacheObj obj)
        {
            if (string.IsNullOrEmpty(key)) { return false; }
            if (obj==null) { return false; }
            if (m_mcache.Add(key, obj))
            {
                m_dcache.Add(key, obj);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(string key, HTools.CacheObj obj)
        {
            if (string.IsNullOrEmpty(key)) { return false; }
            if (obj == null) { return false; }
            if (m_mcache.Update(key, obj))
            {
                m_dcache.Update(key, obj);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key)) { return false; }
            m_mcache.Remove(key);
            m_dcache.Remove(key);
            return true;
        }

        public void RemoveByKeys(System.Collections.Generic.List<string> keys)
        {
            m_mcache.RemoveByKeys(keys);
            m_dcache.RemoveByKeys(keys);
        }

        public bool RemoveAll()
        {
            try
            {
                m_mcache.RemoveAll();
                m_dcache.RemoveAll();
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
