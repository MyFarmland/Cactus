using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

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
