using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Classes
{
    public static class HelperCookies
    {
        /// <summary>  
        /// Get the cookie  
        /// </summary>  
        /// <param name="key">Key </param>  
        /// <returns>string value</returns>  
        public static string GetCookie(string key, IHttpContextAccessor _httpAccessor)
        {
            return _httpAccessor.HttpContext.Request.Cookies[key];
        }
        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        public static void SetCookie(string key, string value, IHttpContextAccessor _httpAccessor, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            _httpAccessor.HttpContext.Response.Cookies.Append(key, value, option);
        }
        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        public static void RemoveCookie(string key, IHttpContextAccessor _httpAccessor)
        {
            _httpAccessor.HttpContext.Response.Cookies.Delete(key);
        }
    }
}
