using System;
using System.Collections.Generic;
using System.Text;

namespace HTools
{
    /// <summary>
    /// 定义解析模式（即状态）。
    /// </summary>
    public enum ParserMode
    {
        /// <summary>
        /// 无状态。
        /// </summary>
        None = 0,

        /// <summary>
        /// 进入标签处理。
        /// </summary>
        EnterLabel = 1,

        /// <summary>
        /// 退出标签处理。
        /// </summary>
        LeaveLabel = 2
    }
    /// <summary>
    /// 令牌置换类
    /// </summary>
    public class HTemplate
    {
        private static char _LABEL_OPEN_CHAR = '{';
        private static char _LABEL_CLOSE_CHAR = '}';

        public HTemplate() { 
            _LABEL_OPEN_CHAR = '{';
            _LABEL_CLOSE_CHAR = '}';
        }
        public HTemplate(char OPEN_CHAR, char CLOSE_CHAR)
        {
            _LABEL_OPEN_CHAR = OPEN_CHAR;
            _LABEL_CLOSE_CHAR = CLOSE_CHAR;
        }
        /// <summary>
        /// 表示 Token 顺序集合（Token流）。
        /// </summary>
        public List<string> _tokens = new List<string>();
        // 为有限状态机定义一个寄存器
        // 注意：有限状态机的理解在物理层的电路上和在编程概念上是相通的
        private StringBuilder _temp = new StringBuilder();
        /// <summary>
        /// 表示当前状态。
        /// </summary>
        private ParserMode _currentMode;
        /// <summary>
        /// 表示上一状态。
        /// </summary>
        /// <remarks>
        /// 如果状态多余两个的话，我们总不能再定义一个"_last_last_Mode"吧！
        /// 在状态有多个的时候，需要使用 <see cref="Stack{T}"/> 来保存历史
        /// 状态，这个我们将在解释型模版引擎中用到。
        /// </remarks>
        private ParserMode _lastMode;
        /// <summary>
        /// 进入模式。
        /// </summary>
        /// <param name="mode"><see cref="ParserMode"/> 枚举值之一。</param>
        private void _EnterMode(ParserMode mode)
        {
            // 当状态改变的时候应当保存之前已处理的寄存器中的内容
            if (this._temp.Length > 0)
            {
                this._tokens.Add(this._temp.ToString());

                //this._temp.Clear();
                this._temp.Remove(0, this._temp.Length);
            }

            this._lastMode = this._currentMode;
            this._currentMode = mode;
        }
        /// <summary>
        /// 离开模式。
        /// </summary>
        private void _LeaveMode()
        {
            // 当状态改变的时候应当保存之前已处理的寄存器中的内容
            // 当状态超过2个的时候，实际上这里的代码应该是不一样的
            // 虽然现在我们只需要考虑两种状态，但为了更加直观的演示，我特意在这里又写了一遍
            if (this._temp.Length > 0)
            {
                this._tokens.Add(this._temp.ToString());
                //this._temp.Clear();
                this._temp.Remove(0,this._temp.Length);
            }

            // 因为只有两个状态，因此
            this._currentMode = this._lastMode;
        }
        /// <summary>
        /// 解析模板
        /// </summary>
        /// <param name="template"></param>
        public void ParseTemplate(string template)
        {
            if (String.IsNullOrEmpty(template)) return;
            var templateLength = template.Length;
            // 为了模拟光标的定位移动
            for (var index = 0; index < templateLength; index++)
            {
                var c = template[index];
                if (c == _LABEL_OPEN_CHAR) {
                    this._EnterMode(ParserMode.EnterLabel);
                    this._temp.Append(c);
                }
                else if (c == _LABEL_CLOSE_CHAR)
                {
                    this._temp.Append(c);
                    this._LeaveMode();
                }
                else {
                    this._temp.Append(c);
                }
                //switch (c)
                //{
                //    case _LABEL_OPEN_CHAR:
                //        this._EnterMode(ParserMode.EnterLabel);
                //        this._temp.Append(c);
                //        break;
                //    case _LABEL_CLOSE_CHAR:
                //        this._temp.Append(c);
                //        this._LeaveMode();
                //        break;
                //    default:
                //        this._temp.Append(c);
                //        break;
                //}
            }
            // 到达结尾的时候也需要处理寄存器中的内容
            // 这就是之前提到的硬编码解决开始和结束两个状态
            if (this._temp.Length <= 0) return;
            this._tokens.Add(this._temp.ToString());
            //this._temp.Clear();
            this._temp.Remove(0, this._temp.Length);
        }
        /// <summary>
        /// 令牌搜索置换
        /// </summary>
        /// <param name="Template">模板内容</param>
        /// <param name="TokenItems">置换的字典</param>
        /// <returns></returns>
        public static string TokenSearch(string Template, Dictionary<string, string> TokenItems)
        {
            StringBuilder sb = new StringBuilder();
            HTemplate t = new HTemplate();
            t.ParseTemplate(Template);//解析成令牌流
            foreach (var s in t._tokens)
            {
                bool b = false;
                if (s.StartsWith(_LABEL_OPEN_CHAR.ToString()) && s.EndsWith(_LABEL_CLOSE_CHAR.ToString()))
                {
                    foreach (var d in TokenItems)
                    {
                        if (d.Key == s)
                        {
                            sb.Append(d.Value);
                            b = true;
                            break;
                        }
                    }
                }
                if (!b)
                {
                    sb.Append(s);
                }
            }
            return sb.ToString();
        }
    }
}
