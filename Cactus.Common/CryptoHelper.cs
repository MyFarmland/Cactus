using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
namespace Cactus.Common
{
    public class CryptoHelper
    {
        #region MD5加密

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns></returns>
        public static string MD5Encrypt(string plainText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.Unicode.GetBytes(plainText);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }
            return byte2String;

            //以下方法亦可
            //string MD5encryptStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(
            //    plainText, "MD5");
            //return MD5encryptStr;
        }

        #endregion

        #region DES加密
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="sourceString">明文</param>
        /// <param name="key">秘钥</param>
        /// <param name="iv">8位向量</param>
        /// <returns></returns>
        public string DESEncrypt(string sourceString, string key, byte[] iv)
        {
            byte[] btKey = Encoding.UTF8.GetBytes(key);
            byte[] btIv = iv;
            var des = new DESCryptoServiceProvider();
            using (var ms = new MemoryStream())
            {
                byte[] inData = Encoding.UTF8.GetBytes(sourceString);
                try
                {
                    using (var cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIv), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        #endregion

        #region DES解密

        /// <summary>
        /// 对DES加密后的字符串进行解密
        /// </summary>
        /// <param name="encryptedString">密文</param>
        /// <param name="key">秘钥</param>
        /// <param name="iv">8位向量</param>
        /// <returns></returns>
        public string DESDecrypt(string encryptedString, string key, byte[] iv)
        {
            byte[] btKey = Encoding.UTF8.GetBytes(key);
            byte[] btIv = iv;
            var des = new DESCryptoServiceProvider();

            using (var ms = new MemoryStream())
            {
                try
                {
                    byte[] inData = Convert.FromBase64String(encryptedString);
                    using (var cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIv), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region TripleDES加密

        /// <summary>
        /// TripleDES加密
        /// </summary>
        public static string TripleDESEncrypting(string strSource)
        {
            try
            {
                byte[] bytIn = Encoding.Default.GetBytes(strSource);
                byte[] key = {
                                 42, 16, 93, 156, 78, 4, 218, 32, 15, 167, 44, 80, 26, 20, 155, 112, 2, 94, 11, 204, 119
                                 ,
                                 35, 184, 197
                             }; //定义密钥
                byte[] IV = {55, 103, 246, 79, 36, 99, 167, 3}; //定义偏移量
                var TripleDES = new TripleDESCryptoServiceProvider {IV = IV, Key = key};
                ICryptoTransform encrypto = TripleDES.CreateEncryptor();
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();
                byte[] bytOut = ms.ToArray();
                return Convert.ToBase64String(bytOut);
            }
            catch (Exception ex)
            {
                throw new Exception("加密时候出现错误!错误提示:\n" + ex.Message);
            }
        }

        #endregion

        #region TripleDES解密

        /// <summary>
        /// TripleDES解密
        /// </summary>
        public static string TripleDESDecrypting(string Source)
        {
            try
            {
                byte[] bytIn = Convert.FromBase64String(Source);
                byte[] key = {
                                 42, 16, 93, 156, 78, 4, 218, 32, 15, 167, 44, 80, 26, 20, 155, 112, 2, 94, 11, 204, 119
                                 ,
                                 35, 184, 197
                             }; //定义密钥
                byte[] IV = {55, 103, 246, 79, 36, 99, 167, 3}; //定义偏移量
                var TripleDES = new TripleDESCryptoServiceProvider {IV = IV, Key = key};
                ICryptoTransform encrypto = TripleDES.CreateDecryptor();
                var ms = new MemoryStream(bytIn, 0, bytIn.Length);
                var cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
                var strd = new StreamReader(cs, Encoding.Default);
                return strd.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception("解密时候出现错误!错误提示:\n" + ex.Message);
            }
        }

        #endregion

    }
}