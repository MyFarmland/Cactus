using Newtonsoft.Json;

namespace Cactus.Common
{
    public static class JSONExtension
    {
        public static string ToJSON(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T ParseJSON<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
        public static T ParseJSON<T>(this object str)
        {
            return JsonConvert.DeserializeObject<T>(str.ToString());
        }
    }  
}
