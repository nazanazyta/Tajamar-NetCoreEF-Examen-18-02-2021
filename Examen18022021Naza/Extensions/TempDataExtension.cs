using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen18022021Naza.Extensions
{
    public static class TempDataExtension
    {
        public static void SetObject(this ITempDataDictionary tempdata, String key, Object objeto)
        {
            tempdata[key] = JsonConvert.SerializeObject(objeto);
        }

        public static T GetObject<T>(this ITempDataDictionary tempdata, String key)
        {
            if (tempdata[key] == null)
            {
                return default(T);
            }
            else
            {
                String data = tempdata[key].ToString();
                return JsonConvert.DeserializeObject<T>(data);
            }
        }
    }
}
