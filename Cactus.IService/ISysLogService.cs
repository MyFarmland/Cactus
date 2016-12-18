using System.Collections.Generic;
using Cactus.Model.Sys;

namespace Cactus.IService
{
    public interface ISysLogService
    {
        void WriteLog(int uid,string info);

        bool DeteleLog(string ids);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<SysLog> ToPagedList(int pageIndex, int pageSize, string keySelector, out int count);
    }
}
