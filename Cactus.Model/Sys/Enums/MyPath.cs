using System;
using System.Collections.Generic;

namespace Cactus.Model.Sys.Enums
{
    public static class MyPath
    {
        //System.IO.Path.DirectorySeparatorChar
        //Windows用"\"，Mac OS用"/"。

        public static string AppPath = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 文件大小（kb）
        /// </summary>
        public static int defaultFileSize = 200;

        /// <summary>
        /// 临时文件本机路径
        /// </summary>
        public static string TempPath = AppPath + "Upload" + System.IO.Path.DirectorySeparatorChar + "Temp";

    }
}
