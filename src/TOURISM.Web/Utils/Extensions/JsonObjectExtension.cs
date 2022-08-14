using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TOURISM.Web.Utils.Extensions
{
    public static class JsonObjectExtension
    {
        public static string SerializeObjectToJson<T>(this List<T> input)
        {
            var result = JsonConvert.SerializeObject(input).Replace("'", "");
            return result;
        }
    }
}
