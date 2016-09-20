using System.Collections.Generic;

namespace Cactus.IService
{
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>添加后的数据实体</returns>
        bool Insert(T entity);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        bool InsertBatch(List<T> datas);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>是否成功</returns>
        void Update(T entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="expression">删除条件</param>
        /// <returns>是否成功</returns>
        void Delete(string ids);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<T> ToPagedList(int pageIndex, int pageSize, string keySelector,out int count);
        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="expression">查询条件</param>
        /// <returns>实体</returns>
        T Find(int id);
    }
}
