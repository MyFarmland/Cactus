using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Linq;
using System.Drawing;

namespace Cactus.Common
{
    public static class WebHelper
    {
        /// <summary>
        /// 下载远程图片保存至本地
        /// </summary>
        /// <param name="remoteUrl"></param>
        /// <param name="savePath"></param>
        public static void DownRemoteImageToLocal(string remoteUrl, string savePath)
        {
            var request = (HttpWebRequest)WebRequest.Create(remoteUrl);
            var response = (HttpWebResponse)request.GetResponse();
            var stream = response.GetResponseStream();
            if (stream != null)
            {
                var img = Image.FromStream(stream);
                var newimg = new Bitmap(img);
                newimg.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="url">上传地址</param>
        /// <param name="path">文件地址</param>
        /// <returns></returns>
        public static string HttpUploadFile(string url, string path)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
            request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            int pos = path.LastIndexOf("\\");
            string fileName = path.Substring(pos + 1);
            //请求头部信息 
            StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] bArr = new byte[fs.Length];
            fs.Read(bArr, 0, bArr.Length);
            fs.Close();
            Stream postStream = request.GetRequestStream();
            postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            postStream.Write(bArr, 0, bArr.Length);
            postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            postStream.Close();
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            return content;
        }
        /// <summary>
        /// 提供本地文件下载，使用OutputStream.Write分块提供下载文件，参数为文件绝对路径
        /// </summary>
        /// <param name="FileName"></param>
        public static void DownLoadFileServer(string filePath)
        {
            //指定块大小
            long chunkSize = 204800;//1024*2*100 200kb
            //建立一个200K的缓冲区
            byte[] buffer = new byte[chunkSize];
            //已读的字节数
            long dataToRead = 0;
            FileStream stream = null;
            try
            {
                //打开文件
                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                dataToRead = stream.Length;
                //添加Http头
                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                string _fileName = HttpUtility.UrlEncode(Path.GetFileName(filePath), Encoding.UTF8);
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + _fileName);
                System.Web.HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());
                while (dataToRead > 0)
                {
                    if (System.Web.HttpContext.Current.Response.IsClientConnected)
                    {
                        int length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));//返回实际的流大小
                        System.Web.HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        try
                        {
                            System.Web.HttpContext.Current.Response.Flush();
                        }
                        catch {
                            //防止client失去连接
                            dataToRead = -1;
                        }
                        buffer = new Byte[chunkSize];
                        dataToRead = dataToRead - length;//减去实际传送的长度
                    }
                    else
                    {
                        //防止client失去连接
                        dataToRead = -1;
                    }
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
                System.Web.HttpContext.Current.Response.Close();
            }
        }
        /// <summary>
        /// 将文件保存到目录
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="savePath">保存地址</param>
        public static bool saveUploadFile(HttpPostedFileBase file, string savePath)
        {
            return saveUploadFile(file, savePath, null, 0);
        }
        /// <summary>
        /// 将文件保存到目录
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="savePath">保存地址</param>
        /// <param name="Extensions">文件类型</param>
        /// <returns></returns>
        public static bool saveUploadFile(HttpPostedFileBase file, string savePath, string[] Extensions)
        {
            return saveUploadFile(file, savePath, Extensions, 0);
        }
        /// <summary>
        /// 将文件保存到目录
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="savePath">保存地址</param>
        /// <param name="fileSize">文件大小（单位是kb，等于0是不检测）</param>
        /// <returns></returns>
        public static bool saveUploadFile(HttpPostedFileBase file, string savePath, int fileSize)
        {
            return saveUploadFile(file, savePath, null, fileSize);
        }
        /// <summary>
        /// 将文件保存到目录
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="savePath">保存地址</param>
        /// <param name="Extensions">文件类型</param>
        /// <param name="fileSize">文件大小（单位是kb，等于0是不检测）</param>
        /// <returns></returns>
        public static bool saveUploadFile(HttpPostedFileBase file, string savePath, string[] Extensions, int fileSize)
        {
            bool _b = true;
            if (fileSize != 0)
            {
                if (file.ContentLength <= fileSize * 1024)
                {
                    _b = true;
                }
                else
                {
                    _b = false;
                }
            }
            if (_b)
            {
                if (Extensions != null) {
                    var avatarName = file.FileName;
                    var avatarExt = Path.GetExtension(avatarName);
                    if (!String.IsNullOrEmpty(avatarExt) && Extensions.Length > 0 && Extensions.Contains(avatarExt))
                    {
                        _b = true;
                    }
                    else { _b = false; }
                }
                if (_b)
                {
                    try
                    {
                        file.SaveAs(savePath);
                        return true;
                    }
                    catch {
                        return false;
                    }
                    
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 蜘蛛程序列表
        /// </summary>
        private static readonly List<string> RobotUserAgentList =
            String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["RobotUserAgentList"])
                ? new List<string>()
                : ConfigurationManager.AppSettings["RobotUserAgentList"].Split(',').ToList();

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIPAddress()
        {
            return GetClientIPAddress(HttpContext.Current);
        }
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientIPAddress(HttpContext context)
        {
            var list = HttpContext.Current != null
                               ? new List<string>
                                 {
                                     context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"],
                                     context.Request.ServerVariables["REMOTE_ADDR"],
                                     context.Request.UserHostAddress,
                                     "127.0.0.1"
                                 }
                               : new List<string>
                                 {
                                     "127.0.0.1"
                                 };
            var clientIP = list.First(item => !String.IsNullOrEmpty(item) && item != "::1");

            IPAddress ip;
            if (!IPAddress.TryParse(clientIP, out ip))
            {
                clientIP = "127.0.0.1";
            }
            return clientIP;
        }


        /// <summary>
        /// 获取UserAgent
        /// </summary>
        /// <returns></returns>
        public static string GetUserAgent()
        {
            return HttpContext.Current == null ? String.Empty : HttpContext.Current.Request.UserAgent;
        }

        public static string GetUserHostAddress()
        {
            return HttpContext.Current == null ? String.Empty : HttpContext.Current.Request.UserHostAddress;
        }

        public static string GetCurrentUrl()
        {
            return GetCurrentUrl(true);
        }

        public static string GetCurrentUrl(bool queryStringRequired)
        {
            string currentUrl = string.Empty;
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                //url重写的状况
                string originalUrl = context.Request.ServerVariables["HTTP_X_ORIGINAL_URL"];
                currentUrl = String.IsNullOrWhiteSpace(originalUrl)
                                 ? HttpContext.Current.Request.Url.ToString()
                                 : new Uri(new Uri(string.Format("http://{0}/", context.Request.Url.Host)), originalUrl).ToString();
            }
            //如果不需要querystring，截掉
            if (!queryStringRequired && currentUrl.IndexOf('?') > -1)
            {
                currentUrl = currentUrl.Split('?')[0];
            }
            return currentUrl;
        }

        public static string GetUrlReferrer()
        {
            return (HttpContext.Current != null && HttpContext.Current.Request.UrlReferrer != null)
                       ? HttpContext.Current.Request.UrlReferrer.ToString()
                       : String.Empty;
        }

        public static string GetBrowser()
        {
            return HttpContext.Current == null
                       ? String.Empty
                       : string.Format("{0} {1}", HttpContext.Current.Request.Browser.Browser, HttpContext.Current.Request.Browser.Version);
        }

        /// <summary>
        /// 当前访问者是否为蜘蛛程序
        /// 注：可通过手动更改浏览器user-agent设置或者url后附加isrobot=1参数来伪造当前请求为蜘蛛程序
        /// </summary>
        /// <returns></returns>
        public static bool IsRobot()
        {
            var isRobot = false;
            if (HttpContext.Current != null)
            {
                var userAgent = HttpContext.Current.Request.UserAgent;
                isRobot = (!String.IsNullOrWhiteSpace(userAgent) && RobotUserAgentList.Any(item => userAgent.IndexOf(item, StringComparison.OrdinalIgnoreCase) >= 0)) ||
                          (HttpContext.Current.Request.QueryString.AllKeys.Contains("isrobot") && HttpContext.Current.Request.QueryString["isrobot"] == "1");
            }

            return isRobot;
        }

        public static string GetServerIPAddress()
        {
            return HttpContext.Current == null ? String.Empty : HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
        }

        public static string GetServerPort()
        {
            return HttpContext.Current == null ? String.Empty : HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
        }

        public static string GetPhysicalPath()
        {
            return HttpContext.Current == null ? String.Empty : HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"];
        }
    }
}
