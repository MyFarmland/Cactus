using System;
using System.Text;

namespace HTools
{
    /// <summary>
    /// 高效string类
    /// </summary>
    public class HString
    {
        private StringBuilder m_InnerBuilder = new StringBuilder();

        public static implicit operator HString(string value)
        {
            HString text = new HString();
            text.InnerBuilder.Append(value);
            return text;
        }
        public static implicit operator string(HString value)
        {
            return value.ToString();
        }

        public StringBuilder InnerBuilder
        {
            get { return m_InnerBuilder; }
        }

        public int Length
        {
            get
            {
                return m_InnerBuilder.Length;
            }
        }

        public HString()
        {
            m_InnerBuilder = new StringBuilder();
        }

        public HString(int capacity)
        {
            m_InnerBuilder = new StringBuilder(capacity);
        }

        public HString(string value)
        {
            m_InnerBuilder = new StringBuilder(value);
        }

        public HString(StringBuilder innerBuilder)
        {
            m_InnerBuilder = innerBuilder;
        }
        public static HString operator +(HString buffer, char value)
        {
            buffer.InnerBuilder.Append(value);

            return buffer;
        }
        public static HString operator +(HString buffer, char[] value)
        {
            buffer.InnerBuilder.Append(value);

            return buffer;
        }
        public static HString operator +(HString buffer, string value)
        {
            buffer.InnerBuilder.Append(value);

            return buffer;
        }
        public static HString operator +(HString buffer, string[] value)
        {
            foreach (var s in value) {
                buffer.InnerBuilder.Append(s);
            }
            return buffer;
        }
        public static HString operator +(HString buffer, bool value)
        {
            buffer.InnerBuilder.Append(value);

            return buffer;
        }
        public static HString operator +(HString buffer, byte value)
        {
            buffer.InnerBuilder.Append(value);

            return buffer;
        }

        public static HString operator +(HString buffer, decimal value)
        {
            buffer.InnerBuilder.Append(value);

            return buffer;
        }
        public static HString operator +(HString buffer, double value)
        {
            buffer.InnerBuilder.Append(value);

            return buffer;
        }
        public static HString operator +(HString buffer, float value)
        {
            buffer.InnerBuilder.Append(value);

            return buffer;
        }
        public static HString operator +(HString buffer, int value)
        {
            buffer.InnerBuilder.Append(value);

            return buffer;
        }
        public static HString operator +(HString buffer, long value)
        {
            buffer.InnerBuilder.Append(value);

            return buffer;
        }
        public static HString operator +(HString buffer, object value)
        {
            buffer.InnerBuilder.Append(value);

            return buffer;
        }
        public static HString operator +(HString buffer, sbyte value)
        {
            buffer.InnerBuilder.Append(value);

            return buffer;
        }
        public static HString operator +(HString buffer, short value)
        {
            buffer.InnerBuilder.Append(value);

            return buffer;
        }
        public static HString operator +(HString buffer, uint value)
        {
            buffer.InnerBuilder.Append(value);

            return buffer;
        }
        public static HString operator +(HString buffer, ulong value)
        {
            buffer.InnerBuilder.Append(value);

            return buffer;
        }
        public static HString operator +(HString buffer, ushort value)
        {
            buffer.InnerBuilder.Append(value);
            return buffer;
        }

        #region api

        public HString Append(bool value) {
            this.m_InnerBuilder.Append(value);
            return this;
        }

        public HString Append(byte value)
        {
            this.m_InnerBuilder.Append(value);
            return this;
        }

        public HString Append(char value) {
            this.m_InnerBuilder.Append(value);
            return this;
        }

        public HString Append(char[] value) {
            this.m_InnerBuilder.Append(value);
            return this;
        }

        public HString Append(decimal value) {
            this.m_InnerBuilder.Append(value);
            return this;
        }

        public HString Append(double value) {
            this.m_InnerBuilder.Append(value);
            return this;
        }

        public HString Append(float value) {
            this.m_InnerBuilder.Append(value);
            return this;
        }

        public HString Append(int value) {
            this.m_InnerBuilder.Append(value);
            return this;
        }

        public HString Append(long value) {
            this.m_InnerBuilder.Append(value);
            return this;
        }

        public HString Append(object value) {
            this.m_InnerBuilder.Append(value);
            return this;
        }

        public HString Append(sbyte value) {
            this.m_InnerBuilder.Append(value);
            return this;
        }

        public HString Append(short value) {
            this.m_InnerBuilder.Append(value);
            return this;
        }

        public HString Append(string value) {
            this.m_InnerBuilder.Append(value);
            return this;
        }

        public HString Append(uint value) {
            this.m_InnerBuilder.Append(value);
            return this;
        }

        public HString Append(ulong value) {
            this.m_InnerBuilder.Append(value);
            return this;
        }

        public HString Append(ushort value) {
            this.m_InnerBuilder.Append(value);
            return this;
        }

        public HString AppendFormat(string format, object arg0) {
            this.m_InnerBuilder.AppendFormat(format,arg0);
            return this;
        }

        public HString AppendFormat(string format, params object[] args) {
            this.m_InnerBuilder.AppendFormat(format, args);
            return this;
        }

        public HString AppendFormat(IFormatProvider provider, string format, params object[] args) {
            this.m_InnerBuilder.AppendFormat(provider,format, args);
            return this;
        }

        public HString AppendFormat(string format, object arg0, object arg1) {
            this.m_InnerBuilder.AppendFormat(format, arg0,arg1);
            return this;
        }

        public HString AppendFormat(string format, object arg0, object arg1, object arg2) {
            this.m_InnerBuilder.AppendFormat(format, arg0, arg1);
            return this;
        }

        public HString AppendLine() {
            this.m_InnerBuilder.AppendLine();
            return this;
        }

        public HString AppendLine(string value) {
            this.m_InnerBuilder.AppendLine(value);
            return this;
        }

        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count) {
            this.m_InnerBuilder.CopyTo(sourceIndex, destination, destinationIndex,count);
        }

        public int EnsureCapacity(int capacity) {
            return this.m_InnerBuilder.EnsureCapacity(capacity);
        }

        public bool Equals(StringBuilder sb) {
            return this.m_InnerBuilder.Equals(sb);
        }

        public bool Equals(HString hs)
        {
            HString p = hs as HString;
            if (p == null)
            {
                return false;
            }
            return (this.m_InnerBuilder.Length == hs.m_InnerBuilder.Length) && (this.m_InnerBuilder.Equals(hs.m_InnerBuilder));
        }

        public HString Insert(int index, bool value) {
            this.m_InnerBuilder.Insert(index, value);
            return this;
        }

        public HString Insert(int index, byte value) {
            this.m_InnerBuilder.Insert(index, value);
            return this;
        }

        public HString Insert(int index, char value) {
            this.m_InnerBuilder.Insert(index, value);
            return this;
        }

        public HString Insert(int index, char[] value) {
            this.m_InnerBuilder.Insert(index, value);
            return this;
        }

        public HString Insert(int index, double value) {
            this.m_InnerBuilder.Insert(index, value);
            return this;
        }

        public HString Insert(int index, float value) {
            this.m_InnerBuilder.Insert(index, value);
            return this;
        }

        public HString Insert(int index, int value) {
            this.m_InnerBuilder.Insert(index, value);
            return this;
        }

        public HString Insert(int index, long value) {
            this.m_InnerBuilder.Insert(index, value);
            return this;
        }

        public HString Insert(int index, object value) {
            this.m_InnerBuilder.Insert(index, value);
            return this;
        }

        public HString Insert(int index, sbyte value) {
            this.m_InnerBuilder.Insert(index, value);
            return this;
        }

        public HString Insert(int index, short value) {
            this.m_InnerBuilder.Insert(index, value);
            return this;
        }

        public HString Insert(int index, string value) {
            this.m_InnerBuilder.Insert(index, value);
            return this;
        }

        public HString Insert(int index, uint value) {
            this.m_InnerBuilder.Insert(index, value);
            return this;
        }

        public HString Insert(int index, ulong value) {
            this.m_InnerBuilder.Insert(index, value);
            return this;
        }

        public HString Insert(int index, ushort value) {
            this.m_InnerBuilder.Insert(index, value);
            return this;
        }

        public HString Insert(int index, string value, int count) {
            this.m_InnerBuilder.Insert(index, value);
            return this;
        }

        public HString Insert(int index, char[] value, int startIndex, int charCount) {
            this.m_InnerBuilder.Insert(index, value, startIndex, charCount);
            return this;
        }

        public HString Remove(int startIndex, int length) {
            this.m_InnerBuilder.Remove(startIndex, length);
            return this;
        }

        public HString Replace(char oldChar, char newChar) {
            this.m_InnerBuilder.Replace(oldChar, newChar);
            return this;
        }

        public HString Replace(string oldValue, string newValue) {
            this.m_InnerBuilder.Replace(oldValue, newValue);
            return this;
        }

        public HString Replace(char oldChar, char newChar, int startIndex, int count) {
            this.m_InnerBuilder.Replace(oldChar, newChar,startIndex,count);
            return this;
        }

        public HString Replace(string oldValue, string newValue, int startIndex, int count) {
            this.m_InnerBuilder.Replace(oldValue, newValue, startIndex, count);
            return this;
        }
        #endregion

        public override string ToString()
        {
            return InnerBuilder.ToString();
        }
    }

}
