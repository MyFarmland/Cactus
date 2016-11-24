using System;

namespace HTools
{
    [Serializable]
    public class CacheObj
    {
        /// <summary>
        /// 缓存对象，创建时间默认是当前时间，过期时间是默认是创建后五分钟
        /// </summary>
        public CacheObj()
        {
             _CreateTime = DateTime.Now;
            if (AbsoluteExpiration == null) {
                AbsoluteExpiration = new TimeSpan(0, 0, 5, 0);
            }
        }

        public DateTime _CreateTime = DateTime.MinValue;
        public object value { get; set; }
        /// <summary>
        /// 绝对过期时间
        /// </summary>
        public TimeSpan AbsoluteExpiration { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime FailureTime
        {
            get
            {
                if (AbsoluteExpiration == TimeSpan.MaxValue)
                {
                    return DateTime.MaxValue;
                }
                else { return _CreateTime.AddTicks(AbsoluteExpiration.Ticks); }
            }
        }
        public DateTime CreateTime { get{ return _CreateTime;}  }
    }
}
