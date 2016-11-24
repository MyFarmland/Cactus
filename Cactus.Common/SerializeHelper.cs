using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Cactus.Common
{
    public class SerializeHelper
    {
        #region XML序列化
        /// <summary>
        /// 文件XML序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件路径</param>
        public static void Serialize(object obj, string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                var serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
            }
            catch(Exception ex)
            {
                throw new Exception("文件XML序列化失败", ex);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// 文件XML反序列化
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="filename">文件路径</param>
        public static object DeSerialize(Type type, string filename)
        {
            FileStream fs = null;
            try
            {
                if (!File.Exists(filename)) { return null; }
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            catch
            {
                throw new Exception("文件XML反序列化失败");
                //return null;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// 文本化XML序列化
        /// </summary>
        /// <param name="item">对象</param>
        public string ToXml<T>(T item)
        {
            var serializer = new XmlSerializer(item.GetType());
            var sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                serializer.Serialize(writer, item);
                return sb.ToString();
            }
        }

        /// <summary>
        /// 文本化XML反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        public T FromXml<T>(string str)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (XmlReader reader = new XmlTextReader(new StringReader(str)))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
        #endregion
    }
}
