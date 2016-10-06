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
            if (CreateTime == null)
            {
                CreateTime = DateTime.Now;
            }
            if (AbsoluteExpiration == null) {
                AbsoluteExpiration = new DateTimeOffset().AddMinutes(5);
            }
        }
        public object value { get; set; }
        /// <summary>
        /// 绝对过期时间
        /// </summary>
        public DateTimeOffset AbsoluteExpiration { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime FailureTime
        {
            get
            {
                if (AbsoluteExpiration == DateTime.MaxValue)
                {
                    return DateTime.MaxValue;
                }
                else { return CreateTime.AddTicks(AbsoluteExpiration.Ticks); }
            }
        }
        public DateTime CreateTime { get; set; }
    }
}
