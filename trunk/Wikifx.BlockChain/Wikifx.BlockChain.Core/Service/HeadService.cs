using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Net;

namespace Wikifx.BlockChain.Core.Service
{
    public class HeadService : BaseWebService
    {
        public HttpClient Client { get; }
        public string _url { get; }
        public RedisHelper _redis { get; set; }
        public HeadService(HttpClient client, RedisHelper redis)
        {
            _url = "http://192.168.1.92:8080/";
            client.BaseAddress = new Uri(_url);
            client.Timeout = TimeSpan.FromSeconds(timeout);
            Client = client;
            _redis = redis;
        }

        public async Task<T> GetDate<T>() where T : class
        {
            var posturl = $"BarInfo/BarList?languageCode=zh-cn&countryCode=cn";
            return await base.Get<T>(Client, posturl, "获取头部");
        }

        public async Task<T> GetCache<T>() where T : class
        {
            var posturl = $"BarInfo/BarList?languageCode=zh-cn&countryCode=cn";
            return await base.GetCache<T>(Client, posturl, "aa", "aaa", 12, _redis);
        }
    }
}
