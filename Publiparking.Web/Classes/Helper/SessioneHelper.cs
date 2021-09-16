using Microsoft.AspNetCore.Http;
using Publiparking.Web.Classes.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace Publiparking.Web.Classes.Helper
{
    public static class SessioneHelper
    {
        private static IHttpContextAccessor _httpContextAccessor = null;
        public static void storeValue(string key, object obj)
        {
            _httpContextAccessor = new HttpContextAccessor();
            _httpContextAccessor.HttpContext.Session.SetComplexData(key, obj);

        }
        //public static object getValue(string key)
        //{
        //    _httpContextAccessor = new HttpContextAccessor();
        //    return _httpContextAccessor.HttpContext.Session.GetComplexData<object>(key);
        //}
        public static T getValue<T>(string key)
        {
            _httpContextAccessor = new HttpContextAccessor();
            var data = _httpContextAccessor.HttpContext.Session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
        }
        public static void deleteValue(string key)
        {
            _httpContextAccessor = new HttpContextAccessor();
            var data = _httpContextAccessor.HttpContext.Session.GetString(key);
            if (data != null)
            {
                _httpContextAccessor.HttpContext.Session.Remove(key);
            }

        }
    }
}
