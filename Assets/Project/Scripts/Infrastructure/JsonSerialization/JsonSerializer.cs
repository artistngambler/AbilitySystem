using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JsonSerialization
{
    public static class JsonSerializer
    {
        public static void Initialize()
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

            settings.Converters.Add(new StringEnumConverter());

            JsonConvert.DefaultSettings = () => settings;
        }

        public static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
