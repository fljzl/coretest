using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace Wikifx.BlockChain.Core
{
    public class JsonHelper
    {
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T DeserializeObject<T>(string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }

        public static T DeserializeObjectTime<T>(string obj, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return JsonConvert.DeserializeObject<T>(obj, new JsonSerializerSettings
            {
                DateFormatString = format
            });
        }
    }
}
