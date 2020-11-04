using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wikifx.BlockChain.Core
{
    public class CountryAndLanguageHelper
    {

        /// <summary>
        /// 获取国家
        /// </summary>
        /// <param name="routeData"></param>
        /// <returns></returns>
        public static string GetCountry()
        {
            try
            {
                return GetCurrentCountryAndLang().Country.ToLower();
            }
            catch (Exception ex)
            {
                LogHelper.Error("获取地址栏国家错误" + ex.Message + ex.StackTrace);
                return "us";
            }
        }

        /// <summary>
        /// 获取语言
        /// </summary>
        /// <param name="routeData"></param>
        /// <returns></returns>
        public static string GetLanguage()
        {
            try
            {
                return GetCurrentCountryAndLang().Language.ToLower();

            }
            catch (Exception ex)
            {
                LogHelper.Error("获取地址栏语言错误" + ex.Message + ex.StackTrace);
                return "en";
            }
        }

        /// <summary>
        /// 获取(国家_语言)
        /// </summary>
        /// <returns></returns>
        public static string GetCountry_Language()
        {
            return GetCurrentCountryAndLang().Country.ToLower() + "_" + GetCurrentCountryAndLang().Language.ToLower();
        }

        public static dynamic GetCurrentCountryAndLang()
        {
            try
            {
                if (MvcContext.GetContext().Request.Path.Value.ToLower().ToString().Contains("/home/error"))
                {
                    return new { Country = "us", Language = "en" };
                }
                //string regex = @"(?<=^/)([x]{1}|[a-z]{2,})_(([\w-]+(?=/))|[\w-]{5})";
                string regex = @"(?<=^/)([x]{1}|[a-z]{2,})_(([\w-]{2}-[\w-]{2})|([\w-]{2,3}))";
                Match lac = Regex.Match(MvcContext.GetContext().Request.Path, regex);//获取到国家和语言
                string regexalc = @"[^_]+";
                var lacarr = Regex.Matches(lac.ToString(), regexalc);
                return new { Country = lacarr[0].ToString().ToLower(), Language = lacarr[1].ToString().ToLower() };

            }
            catch (Exception ex)
            {
                LogHelper.Error("获取地址栏国家和语言出错" + ex.Message + ex.StackTrace + "当前地址:" + MvcContext.GetContext().Request.Host + MvcContext.GetContext().Request.Path);
                return new { Country = "us", Language = "en" };
            }
        }
    }
}
