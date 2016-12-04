using System.Collections.Generic;
namespace HTools
{
    /// <summary>
    /// 公共缓存调用方法接口(读)
    /// </summary>
    public interface ICacheService
    {
        #region 读
        
        /// <summary>
        /// 返回指定key的对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        CacheObj Get(string key);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        IDictionary<string, CacheObj> GetValues(List<string> keys);
        /// <summary>
        /// 现有缓存数量
        /// </summary>
        /// <returns></returns>
        int GetCount();
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool isExists(string key);
        
        #endregion

        #region 写

        /// <summary>
        /// 添加指定key的对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        bool Add(string key, CacheObj obj);

        /// <summary>
        /// 修改指定key的对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Update(string key, CacheObj obj);

        /// <summary>
        /// 移除指定key的对象
        /// </summary>
        /// <param name="key"></param>
        bool Remove(string key);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        void RemoveByKeys(List<string> keys);
        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <returns></returns>
        bool RemoveAll();
        #endregion
    }

}
