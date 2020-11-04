using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Wikifx.BlockChain.Core.Service
{
    public class BaseWebService
    {
        protected int timeout = 100;

        protected Task<T> Get<T>(HttpClient client, string url, string msg)
        {
            try
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                var content = client.GetAsync(url).Result;
                var httpresult = content.Content.ReadAsStringAsync().Result;
                watch.Stop();
                var alltime = watch.ElapsedMilliseconds;
                LogHelper.Info($"{msg},result={httpresult},time_hm={alltime}");
                if (!string.IsNullOrWhiteSpace(httpresult))
                {
                    return Task.Run(() =>
                    {
                        return JsonHelper.DeserializeObject<T>(httpresult);
                    });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(msg, ex);
                return Task.Run(() => { return default(T); });
            }
        }

        protected Task<T> GetCache<T>(HttpClient client, string url, string msg, string cachekey, int time, RedisHelper redis) where T : class
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cachekey))
                {
                    return Task.Run(() => { return default(T); });
                }
                if (redis.Exists(cachekey))
                {
                    var redisvalue = redis.GetCache<T>(cachekey);
                    if (redisvalue != default(T))
                    {
                        return Task.Run(() => { return redisvalue; });
                    }
                }

                Stopwatch watch = new Stopwatch();
                watch.Start();
                var content = client.GetAsync(url).Result;
                var httpresult = content.Content.ReadAsStringAsync().Result;
                watch.Stop();
                var alltime = watch.ElapsedMilliseconds;
                LogHelper.Info($"{msg},result={httpresult},time_hm={alltime}");
                if (!string.IsNullOrWhiteSpace(httpresult))
                {
                    return Task.Run(() =>
                    {
                        redis.SetCache(cachekey, content);
                        return JsonHelper.DeserializeObject<T>(httpresult);
                    });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(msg, ex);
                return Task.Run(() => { return default(T); });
            }
        }
    }
}
