using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace Cactus.Common
{
    /// <summary>
    /// 常用Http操作类
    /// </summary>
    public static class HHttp
    {
        /// <summary>
        /// 超时时间
        /// </summary>
        public static int m_timeout = 5 * 1000;

        #region Async
        /// <summary>
        /// 异步post的http请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="val">post数据段</param>
        /// <param name="callback">回调</param>
        public static void AsyncPost(string url, string val, AsyncCallback callback)
        {
            NameValueCollection m_headers = new NameValueCollection();
            m_headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            m_headers.Add("Accept-Encoding", "gzip, deflate, sdch");
            m_headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            m_headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36");
            AsyncPost(url, val, m_headers, m_timeout, callback);
        }
        /// <summary>
        /// 异步post的http请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="val">post数据段</param>
        /// <param name="headers">头消息</param>
        /// <param name="callback">回调</param>
        public static void AsyncPost(string url, string val, NameValueCollection headers, AsyncCallback callback)
        {
            AsyncPost(url, val, headers, m_timeout, callback);
        }
        /// <summary>
        /// 异步post的http请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="val">post数据段</param>
        /// <param name="headers">头消息</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="callback">回调</param>
        public static void AsyncPost(string url, string val, NameValueCollection headers, int timeout, AsyncCallback callback)
        {
            AsyncPost(url, val, headers, timeout, Encoding.UTF8, callback);
        }
        /// <summary>
        /// 异步post的http请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="val">post数据段</param>
        /// <param name="headers">头消息</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="encoder">编码</param>
        /// <param name="callback">回调</param>
        public static void AsyncPost(string url, string val, NameValueCollection headers, int timeout, Encoding encoder, AsyncCallback callback)
        {
            AsyncHttp(url, "POST", val, headers, encoder, timeout, callback, null);
        }
        /// <summary>
        /// 异步get的http请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="callback">回调</param>
        public static void AsyncGet(string url, AsyncCallback callback)
        {
            NameValueCollection m_headers = new NameValueCollection();
            m_headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            m_headers.Add("Accept-Encoding", "gzip, deflate, sdch");
            m_headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            m_headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36");
            AsyncGet(url, m_headers, m_timeout, callback);
        }
        /// <summary>
        /// 异步get的http请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="headers">头消息</param>
        /// <param name="callback">回调</param>
        public static void AsyncGet(string url, NameValueCollection headers, AsyncCallback callback)
        {
            AsyncGet(url, headers, m_timeout, callback);
        }
        /// <summary>
        /// 异步get的http请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="headers">头消息</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="callback">回调</param>
        public static void AsyncGet(string url, NameValueCollection headers, int timeout, AsyncCallback callback)
        {
            AsyncGet(url, headers, timeout, Encoding.UTF8, callback);
        }
        /// <summary>
        /// 异步get的http请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="headers">头消息</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="encoder">编码</param>
        /// <param name="callback">回调</param>
        public static void AsyncGet(string url, NameValueCollection headers, int timeout, Encoding encoder, AsyncCallback callback)
        {
            AsyncHttp(url, "GET", "", headers, encoder, timeout, callback, null);
        }
        /// <summary>
        /// 异步http请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="method">http方式</param>
        /// <param name="val">追加的值（post）</param>
        /// <param name="headers">头消息</param>
        /// <param name="encoding">编码</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="callback">回调</param>
        /// <param name="point">端口协议</param>
        public static void AsyncHttp(string url, string method, string val, NameValueCollection headers, Encoding encoding, int timeout, AsyncCallback callback, BindIPEndPoint point)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = method;
            req.ServicePoint.Expect100Continue = false;
            req.ServicePoint.BindIPEndPointDelegate = point;
            for (int i = 0; i < headers.AllKeys.Length; i++)
            {
                SetHeaderValue(req.Headers, headers.AllKeys[i], headers[headers.AllKeys[i]]);
            }
            if (string.IsNullOrEmpty(val) == false)
            {
                byte[] byteArray = encoding.GetBytes(val);
                req.ContentLength = byteArray.Length;
                Stream stream = req.GetRequestStream();
                stream.Write(byteArray, 0, byteArray.Length);
                stream.Close();
            }
            req.Timeout = timeout;
            req.BeginGetResponse(callback, req);
        }
        #endregion

        #region Sync
        /// <summary>
        /// 同步http的post的请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="val">追加的值（post）</param>
        /// <returns></returns>
        public static HttpWebResponse SyncPost(string url, string val)
        {
            NameValueCollection m_headers = new NameValueCollection();
            m_headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            m_headers.Add("Accept-Encoding", "gzip, deflate, sdch");
            m_headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            m_headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36");
            return SyncPost(url, val, m_headers, m_timeout);
        }
        /// <summary>
        /// 同步http的post的请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="val">追加的值（post）</param>
        /// <param name="headers">头消息</param>
        /// <returns></returns>
        public static HttpWebResponse SyncPost(string url, string val, NameValueCollection headers)
        {
            return SyncPost(url, val, headers, m_timeout);
        }
        /// <summary>
        /// 同步http的post的请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="val">追加的值（post）</param>
        /// <param name="headers">头消息</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public static HttpWebResponse SyncPost(string url, string val, NameValueCollection headers, int timeout)
        {
            return SyncPost(url, val, headers, timeout, Encoding.UTF8);
        }
        /// <summary>
        ///  同步http的post的请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="val">追加的值（post）</param>
        /// <param name="headers">头消息</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static HttpWebResponse SyncPost(string url, string val, NameValueCollection headers, int timeout, Encoding encoder)
        {
            return SyncPost(url, val, headers, timeout, encoder);
        }
        /// <summary>
        /// 同步http的get的请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns></returns>
        public static HttpWebResponse SyncGet(string url)
        {
            NameValueCollection m_headers = new NameValueCollection();
            m_headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            m_headers.Add("Accept-Encoding", "gzip, deflate, sdch");
            m_headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            m_headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36");
            return SyncGet(url, m_headers);
        }
        /// <summary>
        /// 同步http的get的请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="headers">头消息</param>
        /// <returns></returns>
        public static HttpWebResponse SyncGet(string url,NameValueCollection headers)
        {
            return SyncGet(url, headers, m_timeout);
        }
        /// <summary>
        /// 同步http的get的请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="headers">头消息</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public static HttpWebResponse SyncGet(string url, NameValueCollection headers, int timeout)
        {
            return SyncGet(url, headers, timeout, Encoding.UTF8);
        }
        /// <summary>
        /// 同步http的get的请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="headers">头消息</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static HttpWebResponse SyncGet(string url,NameValueCollection headers, int timeout, Encoding encoder)
        {
            return SyncHttp(url, "GET", "", headers, encoder, timeout,null);
        }
        /// <summary>
        /// 同步http的请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="method">http方式</param>
        /// <param name="val">追加的值（post）</param>
        /// <param name="headers">头消息</param>
        /// <param name="encoding">编码</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="point">端口协议</param>
        /// <returns></returns>
        public static HttpWebResponse SyncHttp(string url, string method, string val, NameValueCollection headers, Encoding encoding, int timeout, BindIPEndPoint point)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = method;
            req.ServicePoint.Expect100Continue = false;
            req.ServicePoint.BindIPEndPointDelegate = point;
            for (int i = 0; i < headers.AllKeys.Length; i++)
            {
                SetHeaderValue(req.Headers, headers.AllKeys[i], headers[headers.AllKeys[i]]);
            }
            if (string.IsNullOrEmpty(val) == false)
            {
                byte[] byteArray = encoding.GetBytes(val);
                req.ContentLength = byteArray.Length;
                Stream stream = req.GetRequestStream();
                stream.Write(byteArray, 0, byteArray.Length);
                stream.Close();
            }
            req.Timeout = timeout;
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            return response;
        }
        #endregion
        /// <summary>
        /// 添加头
        /// </summary>
        /// <param name="header"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        private static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }
    }
}
