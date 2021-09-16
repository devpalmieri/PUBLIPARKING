using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Publiparking.Web.Classes.Extensions
{
    public static class SessionExtensions
    {
        //public static void Set<T>(this ISession session, string key, T value)
        //{
        //    session.Set(key, JsonSerializer.SerializeToUtf8Bytes(value));
        //}

        //public static T Get<T>(this ISession session, string key)
        //{
        //    var value = session.Get(key);

        //    return value == null ? default(T) :
        //        JsonSerializer.Deserialize<T>(value);
        //}
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
    }
}
