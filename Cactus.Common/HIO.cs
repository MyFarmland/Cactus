using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Cactus.Common
{
    /// <summary>
    /// 常用IO操作类
    /// </summary>
    public class HIO
    {
        /// <summary>
        /// 控制台数据错误
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteConsole(Exception ex)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine("Data:" + ex.Data + Environment.NewLine
            + " InnerException:" + ex.InnerException + Environment.NewLine
            + " Message:" + ex.Message + Environment.NewLine
            + " Source:" + ex.Source + Environment.NewLine
            + " StackTrace:" + ex.StackTrace + Environment.NewLine
            + " TargetSite:" + ex.TargetSite);
        }
        /// <summary>
        /// 控制台数据错误
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteDebug(Exception ex)
        {
            Debug.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            Debug.WriteLine("Data:" + ex.Data + Environment.NewLine
            + " InnerException:" + ex.InnerException + Environment.NewLine
            + " Message:" + ex.Message + Environment.NewLine
            + " Source:" + ex.Source + Environment.NewLine
            + " StackTrace:" + ex.StackTrace + Environment.NewLine
            + " TargetSite:" + ex.TargetSite);
        }
        /// <summary>
        /// 应用根目录
        /// </summary>
        public static string AppRootPath = Environment.CurrentDirectory;
        /// <summary>
        /// 日志目录
        /// </summary>
        public static string logDir = "Log";
        /// <summary>
        /// 日志目录全路径
        /// </summary>
        public static string logDirPath = AppRootPath + Path.DirectorySeparatorChar + logDir;
        /// <summary>
        /// 错误记录
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteLog(Exception ex)
        {
            WriteLog("Data:" + ex.Data + Environment.NewLine
                + " InnerException:" + ex.InnerException + Environment.NewLine
                + " Message:" + ex.Message + Environment.NewLine
                + " Source:" + ex.Source + Environment.NewLine
                + " StackTrace:" + ex.StackTrace + Environment.NewLine
                + " TargetSite:" + ex.TargetSite);
        }
        /// <summary>
        /// 写log
        /// </summary>
        /// <param name="InfoStr"></param>
        public static void WriteLog(string info)
        {
            WriteLog(info, logDirPath);
        }
        /// <summary>
        /// 写log(自动时间log)
        /// </summary>
        /// <param name="InfoStr">内容</param>
        /// <param name="FilePath">目录地址</param>
        public static void WriteLog(string info, string DirPath)
        {
            FileStream stream = null;
            System.IO.StreamWriter writer = null;
            try
            {
                if (Directory.Exists(DirPath) == false)
                {
                    Directory.CreateDirectory(DirPath);
                }
                string dateDir = DateTime.Now.ToString("yyyyMMdd");
                if (!Directory.Exists(DirPath + Path.DirectorySeparatorChar + dateDir)) { Directory.CreateDirectory(DirPath + Path.DirectorySeparatorChar + dateDir); }
                string logFilePath = DirPath + Path.DirectorySeparatorChar + dateDir
                    + Path.DirectorySeparatorChar + "SyncLog_" + DateTime.Now.ToString("yyyyMMddHH") + ".txt";
                if (File.Exists(logFilePath) == false)
                {
                    stream = new FileStream(logFilePath, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
                }
                else
                {
                    stream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                }
                writer = new System.IO.StreamWriter(stream);
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                writer.WriteLine(info);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }
        /// <summary>
        /// 异步写log
        /// </summary>
        /// <param name="info"></param>
        public static void AsyncWriteLog(string info)
        {
            AsyncWriteLog(info, Encoding.UTF8, logDirPath);
        }
        /// <summary>
        /// 异步写log
        /// </summary>
        /// <param name="ex"></param>
        public static void AsyncWriteLog(Exception ex)
        {
            AsyncWriteLog("Data:" + ex.Data + Environment.NewLine
                + " InnerException:" + ex.InnerException + Environment.NewLine
                + " Message:" + ex.Message + Environment.NewLine
                + " Source:" + ex.Source + Environment.NewLine
                + " StackTrace:" + ex.StackTrace + Environment.NewLine
                + " TargetSite:" + ex.TargetSite, Encoding.UTF8, logDirPath);
        }
        /// <summary>
        /// 异步写log
        /// </summary>
        /// <param name="info"></param>
        /// <param name="encode"></param>
        /// <param name="FilePath"></param>
        public static void AsyncWriteLog(string info, Encoding encode, string FileDirPath)
        {
            AsyncWriteLog(encode.GetBytes(info), FileDirPath);
        }
        /// <summary>
        /// 异步写log
        /// </summary>
        /// <param name="datagram">要写入当前流的数据的缓冲区</param>
        /// <param name="FilePath"></param>
        public static void AsyncWriteLog(byte[] datagram, string FileDirPath)
        {
            AsyncWriteLog(datagram, datagram.Length, FileDirPath, (obj) =>
            {
                AsyncResult ar = obj as AsyncResult;
                FileStream stream = ar.AsyncState as FileStream;
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            });
        }
        /// <summary>
        /// 异步写log
        /// </summary>
        /// <param name="datagram">要写入当前流的数据的缓冲区</param>
        /// <param name="numBytes">最多写入的字节数</param>
        /// <param name="FilePath"></param>
        /// <param name="callback"></param>
        public static void AsyncWriteLog(byte[] datagram, int numBytes, string FileDirPath, AsyncCallback callback)
        {
            if (datagram.Length == 0) { throw new Exception("数据为0"); }
            if (numBytes == 0) { throw new Exception("写入数为0"); }
            if (string.IsNullOrEmpty(FileDirPath)) { throw new Exception("文件地址为空"); }
            if (!Directory.Exists(FileDirPath)) { Directory.CreateDirectory(FileDirPath); }
            string dateDir = DateTime.Now.ToString("yyyyMMdd");
            if (!Directory.Exists(FileDirPath + Path.DirectorySeparatorChar + dateDir)) { Directory.CreateDirectory(FileDirPath + Path.DirectorySeparatorChar + dateDir); }
            string logFilePath = FileDirPath + Path.DirectorySeparatorChar + dateDir
                + Path.DirectorySeparatorChar + "AsyncLog_" + DateTime.Now.ToString("yyyyMMddHH") + ".txt";
            FileStream stream = null;
            try
            {
                if (File.Exists(logFilePath) == false)
                {
                    stream = new FileStream(logFilePath, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
                }
                else
                {
                    stream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                }

                if (stream.CanWrite)
                {
                    stream.BeginWrite(datagram, 0, numBytes, callback, stream);
                }
                else
                {
                    throw new Exception("文件无法写入，文件或只读！");
                }
            }
            catch (Exception ex)
            {
                WriteDebug(ex);
            }
        }
        /// <summary>
        /// 文本转义（方便讲文本转换成C#变量代码）
        /// 例子 " 转化成 string str="\"";
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToEscape(string str, string m_var)
        {
            /*
                "           \"
                \           \\
            */
            str = str.Trim();
            str = str.Replace("\\", "\\\\");
            str = str.Replace("\"", "\\\"");
            return "string " + m_var + "=\"" + str + "\";";
        }

        /// <summary>
        /// (递归)去掉所有首部出现的字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="headStr">首部出现的字符串</param>
        /// <returns></returns>
        public static string RemoveHead(string str, string headStr)
        {
            if (str.StartsWith(headStr))
            {
                str = str.Remove(0, headStr.Length);
                return RemoveHead(str, headStr);
            }
            else
            {
                return str;
            }
        }
        /// <summary>
        /// 路径解析转换,转化成符合当前系统的路径符号
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="flag">（路径的类型）1：windows \ 2：linux /（linux和web网页的分隔相符）</param>
        /// <param name="dHear">是否去掉首部的路径分隔符</param>
        /// <returns></returns>
        public static string PathParse(string path, int flag, bool dHear)
        {
            string win = @"\";
            string linux = @"/";
            string sys = Path.DirectorySeparatorChar.ToString();
            if (flag == 1)
            {
                path = path.Replace(win, sys);
            }
            else if (flag == 2)
            {
                path = path.Replace(linux, sys);
            }
            if (dHear)
            {
                path = RemoveHead(path, sys);
            }
            return path;
        }
        /// <summary>
        /// web路径地址转换为系统路径
        /// </summary>
        /// <param name="appPath">应用路径</param>
        /// <param name="webPath">web路径</param>
        /// <param name="dHear">是否去掉首部的路径分隔符</param>
        /// <returns></returns>
        public static string SysPathParse(string appPath, string webPath, bool dHear)
        {
            string sys = Path.DirectorySeparatorChar.ToString();
            string web = @"/";//web的分隔符
            webPath = webPath.Replace(web, sys);
            if (dHear)
            {
                webPath = RemoveHead(webPath, sys);
            }
            string result = "";
            if (appPath.EndsWith(sys))
            {
                if (webPath.StartsWith(sys))
                {
                    result = appPath + webPath.Remove(0, 1);
                }
                else
                {
                    result = appPath + webPath;
                }
            }
            else
            {
                if (webPath.StartsWith(sys))
                {
                    result = appPath + webPath;
                }
                else
                {
                    result = appPath + sys + webPath;
                }
            }
            return result;
        }
        public static string WebPathParse(string fullPath, string appPath, bool dHear)
        {
            string sys = Path.DirectorySeparatorChar.ToString();
            string web = @"/";//web的分隔符
            if (fullPath.StartsWith(appPath))
            {
                string webPath = fullPath.Remove(0, appPath.Length);
                webPath = webPath.Replace(sys, web);
                if (webPath.StartsWith(web) == false)
                {
                    webPath = web + webPath;
                }
                if (dHear)
                {
                    webPath = RemoveHead(webPath, web);
                }
                return webPath;
            }
            else
            {
                return "";
            }
        }
    }
}
