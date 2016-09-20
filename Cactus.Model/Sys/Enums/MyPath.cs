using System;

namespace Cactus.Model.Sys.Enums
{
    public class MyPath
    {
        //System.IO.Path.DirectorySeparatorChar
        //Windows用"\"，Mac OS用"/"。

        public static string AppPath = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 用户头像本机存储路径
        /// </summary>
        public static string AvatarPath = AppPath + "Upload"+System.IO.Path.DirectorySeparatorChar+"Avatar";
        /// <summary>
        /// 头像网络展示路径
        /// </summary>
        public static string Web_AvatarPath = "/Upload/Avatar";
        /// <summary>
        /// 网站系统本机文件路径
        /// </summary>
        public static string SysPath = AppPath + "Upload" + System.IO.Path.DirectorySeparatorChar + "Sys";
        /// <summary>
        /// 网站系统展示路径,存放系统文件
        /// </summary>
        public static string Web_SysPath = "/Upload/Sys";

        /// <summary>
        /// 文件上传本机路径
        /// </summary>
        public static string EditUploadPath = AppPath + "Upload" + System.IO.Path.DirectorySeparatorChar + "Edit";
        /// <summary>
        /// 文件上传展示路径
        /// </summary>
        public static string Web_EditUploadPath = "/Upload/Edit";

        /// <summary>
        /// 文件上传本机路径
        /// </summary>
        public static string UploadFilePath = AppPath + "Upload" + System.IO.Path.DirectorySeparatorChar + "UploadFile";
        /// <summary>
        /// 文件上传展示路径
        /// </summary>
        public static string Web_UploadFilePath = "/Upload/UploadFile";
        /// <summary>
        /// 临时文件本机路径
        /// </summary>
        public static string TempPath = AppPath + "Upload" + System.IO.Path.DirectorySeparatorChar + "Temp";
        /// <summary>
        /// 文件大小（kb）
        /// </summary>
        public static int fileSize = 200;

        /// <summary>
        /// 宝贝主图本机路径
        /// </summary>
        public static string ItemPath = AppPath + "Upload" + System.IO.Path.DirectorySeparatorChar + "Item";
        /// <summary>
        /// 宝贝主图展示路径
        /// </summary>
        public static string Web_ItemPath = "Upload/Item";
        /// <summary>
        /// 宝贝内容介绍本机路径
        /// </summary>
        public static string ItemContentPath = AppPath + "Upload" + System.IO.Path.DirectorySeparatorChar + "ItemContent";
        /// <summary>
        /// 宝贝内容介绍展示路径
        /// </summary>
        public static string Web_ItemContentPath = "Upload/ItemContent";

    }
}
