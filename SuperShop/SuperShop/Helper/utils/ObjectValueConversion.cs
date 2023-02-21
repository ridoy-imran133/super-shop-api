using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Helper.utils
{
    public class ObjectValueConversion
    {
        public static T SingleValue<T>(string dataObjName, Object reqObject)
        {
            var businessData = Newtonsoft.Json.Linq.JObject.Parse(reqObject.ToString());

            string objData = businessData[dataObjName] != null ? businessData[dataObjName].ToString() : string.Empty;

            var res = JsonConvert.DeserializeObject<T>(objData);

            return res;
        }

        public static List<T> MultiValue<T>(string dataObjName, Object reqObject)
        {
            var businessData = Newtonsoft.Json.Linq.JObject.Parse(reqObject.ToString());

            string objData = businessData[dataObjName] != null ? businessData[dataObjName].ToString() : string.Empty;

            var res = JsonConvert.DeserializeObject<List<T>>(objData);

            return res;
        }
    }
}
