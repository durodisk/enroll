using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace ENROLL.Helpers
{
    public static class HelperJson
    {
        public static string CamelCaseJson(this string vJsonEntrada)
        {
            JsonSerializerSettings jsonSerializerSetting = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(JsonConvert.DeserializeObject<ExpandoObject>(vJsonEntrada), jsonSerializerSetting);
        }
    }
}