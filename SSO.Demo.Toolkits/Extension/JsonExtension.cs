using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SSO.Demo.Toolkits.Extension
{
    public static class JsonExtension
    {
        public static string ToJson(this object obj)
        {
            return obj == null ? null : JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });
        }

        public static T FromJson<T>(this string inputStr) where T : class
        {
            return inputStr == null ? null : JsonConvert.DeserializeObject<T>(inputStr);
        }
    }
}
