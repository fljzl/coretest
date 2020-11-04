using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Wikifx.BlockChain.Core.Cookie
{
    public class UserCookie
    {
        public static string cookieName = ConfigUtil.GetSection("Config:CookieName");

        public static int expire = 999;

        public static CurUserShort UserInfo
        {
            get
            {
                try
                {
                    if (CommonHelper.IsChina())
                    {
                        cookieName = ConfigUtil.GetSection("Config:FxeyeCookieName");
                    }
                    else
                    {
                        cookieName = ConfigUtil.GetSection("Config:CookieName");
                    }
                    DateTime createTime = DateTime.Now;
                    string uid = "";
                    string cookieValue = string.Empty;

                    if (MvcContext.GetContext().Request.Cookies[cookieName] != null)
                    {

                        cookieValue = WebUtility.UrlDecode(MvcContext.GetContext().Request.Cookies[cookieName]);
                        //cookieValue = HttpUtility.UrlDecode(MvcContext.GetContext().Request.Cookies.TryGetValue(cookieName,out cookieValue));
                        MvcContext.GetContext().Request.Cookies.TryGetValue(cookieName, out cookieValue);
                        if (!string.IsNullOrWhiteSpace(cookieValue) && cookieValue.Contains("|"))
                        {
                            uid = DesHelper.DesDecode(cookieValue.Split('|')[0]);
                        }
                    }

                    long ck = 0;

                    if (string.IsNullOrWhiteSpace(uid) || !long.TryParse(uid, out ck))
                    {
                        return null;
                    }

                    int numZoneIndex = Convert.ToInt32(uid.Substring(uid.Length - 1));

                    CurUserShort user = UserCookieFramework<CurUserShort>.ValidateCookie(cookieValue, numZoneIndex, out createTime);

                    //cookie验证失败
                    if (user == null || string.IsNullOrWhiteSpace(user.UserId) || createTime > DateTime.Now.AddMinutes(5) || createTime.AddDays(expire) < DateTime.Now)
                    {
                        Logger.Info("user" + user);
                        Logger.Info("UserId" + string.IsNullOrWhiteSpace(user.UserId));
                        Logger.Info("createTime" + (createTime > DateTime.Now));
                        Logger.Info("createTime.AddDays" + (createTime.AddDays(expire) < DateTime.Now));
                        return null;
                    }

                    user.UserId = DesHelper.DesDecode(user.UserId);

                    return user;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }



    }
}
