using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen18022021Naza.Helpers
{
    public class ToolKit
    {
        public static String SerializeJsonObject(object objeto)
        {
            String respuesta =
                JsonConvert.SerializeObject(objeto);
            return respuesta;
        }

        //METODO QUE RECIBIRA UN String Json Y DEVOLVERA EL OBJETO
        //QUE REPRESENTA DICHO JSON
        public static T DeserializeJsonObject<T>(String json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
