using Cactus.Common;
using Cactus.Model.Sys.Enums;
using System;
using System.Diagnostics;
using System.IO;

namespace Cactus.Crond.ThreadTask
{
	public class DefaultBase:ITask {

        public void Execute(Plan plan)
        {
            Debug.WriteLine("Execute plan:"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            //清除过期的缓存
            FileInfo[] files = new DirectoryInfo(CacheHelper.filePath).GetFiles();
            if (files.Length > 0) {
                foreach(var file in files){
                    if (file.Name.StartsWith(Constant.CacheKey.LoginAdminInfoCacheKey)) 
                    {
                        TimeSpan ts=DateTime.Now - file.LastWriteTime;
                        int h = ts.Days * 24 + ts.Hours;
                        //Debug.WriteLine(file.Name+":" + h);
                        //删除过期的缓存文件，后期通过配置的时间来判断
                        if (h >= 24)
                        {
                            File.Delete(file.FullName);
                        }
                    }
                }
            }
		}

	}
}