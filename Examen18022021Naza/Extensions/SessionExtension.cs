using Examen18022021Naza.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen18022021Naza.Extensions
{
    public static class SessionExtension
    {
        public static void SetObject(this ISession session, String key, object value)
        {
            String data = ToolKit.SerializeJsonObject(value);
            session.SetString(key, data);
        }

        public static T GetObject<T>(this ISession session, String key)
        {
            String data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return ToolKit.DeserializeJsonObject<T>(data);
        }
    }
}
