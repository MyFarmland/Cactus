using System;
using System.Text.RegularExpressions;

namespace HTools
{
    public class HValidate
    {
        /// <summary>
        /// 添加默认超时（.net 4.5.2）,防止cpu突然暴涨100%
        /// </summary>
        public static TimeSpan _default = TimeSpan.FromSeconds(1);
        /// <summary>
        /// 验证是否为用户名（大写字母，小写字母，数字，下划线，中文）
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidUserName(string strIn)
        {
            return Regex.IsMatch(strIn, @"^[A-Za-z0-9_\-\u4e00-\u9fa5]+$", RegexOptions.Compiled, _default);
        }

        /// <summary>
        /// 验证是否为密码（大写字母，小写字母，数字，下划线，短横线）
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidPassword(string strIn)
        {
            return Regex.IsMatch(strIn, @"^[A-Za-z0-9_-]+$");
        }

        /// <summary>
        /// 验证是否为Email地址
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^\w+((-w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", RegexOptions.Compiled, _default);
        }

        /// <summary>
        /// 验证是否为正数
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidUInt(string strIn)
        {
            return Regex.IsMatch(strIn, @"^[1-9]\d*|0$", RegexOptions.Compiled, _default);
        }

        /// <summary>
        /// 验证是否为整数
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidInt(string strIn)
        {
            return Regex.IsMatch(strIn, @"^-?[1-9]\d*$", RegexOptions.Compiled, _default);
        }

        /// <summary>
        /// 验证是否为小数
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidFloat(string strIn)
        {
            return Regex.IsMatch(strIn, @"^-?([1-9]\d*|0(?!\.0+$))\.\d+?$", RegexOptions.Compiled, _default);
        }

        /// <summary>
        /// 验证是否为正的小数
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidUFloat(string strIn)
        {
            return Regex.IsMatch(strIn, @"^[1-9]\d*.\d*|0.\d*[1-9]\d*$", RegexOptions.Compiled, _default);
        }

        /// <summary>
        /// 验证是否为日期（匹配规则为：2013.12.23）
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidDate(string strIn)
        {
            return Regex.IsMatch(strIn, @"^\d{4}(\-|\/|.)\d{1,2}\1\d{1,2}$", RegexOptions.Compiled, _default);
        }

        /// <summary>
        /// 验证是否为大写字母
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidUpperStr(string strIn)
        {
            return Regex.IsMatch(strIn, @"^[A-Z]+$", RegexOptions.Compiled, _default);
        }

        /// <summary>
        /// 验证是否为小写字母
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidLowerStr(string strIn)
        {
            return Regex.IsMatch(strIn, @"^[a-z]+$", RegexOptions.Compiled, _default);
        }

        /// <summary>
        /// 验证是否为空
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static  bool IsNull(string strIn)
        {
            return Regex.IsMatch(strIn, @"^\S+$", RegexOptions.Compiled, _default);
        }

        /// <summary>
        /// 验证是否为色码值
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidColorCode(string strIn)
        {
            return Regex.IsMatch(strIn, @"^#[a-fA-F0-9]{6}$", RegexOptions.Compiled, _default);
        }

        /// <summary>
        /// 验证是否为正确的手机号码
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsCellPhone(string strIn)
        {
            return Regex.IsMatch(strIn, @"^0?(13|14|15|18)[0-9]{9}/$", RegexOptions.Compiled, _default);
        }

        /// <summary>
        /// 验证后缀名
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidPostfix(string strIn)
        {
            return Regex.IsMatch(strIn, @"\.(?i:gif|jpg)$", RegexOptions.Compiled, _default);
        }
        
        /// <summary>
        /// 验证是否为IP地址
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidIp(string strIn)
        {
            return Regex.IsMatch(strIn, @"^((?:(?:25[0-5]|2[0-4]\d|[01]?\d?\d)\.){3}(?:25[0-5]|2[0-4]\d|[01]?\d?\d))$", RegexOptions.Compiled, _default);
        }

        /// <summary>
        /// 验证是否为中文
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsZhongWen(string strIn)
        {
            return Regex.IsMatch(strIn, @"^[\u4e00-\u9fa5]+$", RegexOptions.Compiled, _default);
        }

        /// <summary>
        /// 从字符串中提取数字
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int GetNumber(string source)
        {
            source = Regex.Replace(source, @"[^\d]*", "", RegexOptions.Compiled, _default);
            int result;
            if (!int.TryParse(source, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 过滤字符串中的换行和空格
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string FilterWordwrapAndBlank(string source)
        {
            if (!String.IsNullOrEmpty(source))
            {
                source = source.Replace("\n", "").Replace(" ", "");
            }
            return source;
        }

        /// <summary>
        /// 检查危险字符
        /// </summary>
        /// <param name="sInput"></param>
        /// <returns></returns>
        public static string SqlFilter(string sInput)
        {
            if (string.IsNullOrEmpty(sInput))
            {
                return null;
            }
            var sInputLower = sInput.ToLower();
            
            const string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
            if (Regex.Match(sInputLower, Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase,_default).Success)
            {
                return null;
            }

            sInput = sInput.Replace("'", "''");
            return sInput;
        }
    }
}
