using JsonSerialization;
using UnityEngine;

namespace ConfigLoaders.UnityResources
{
    public class ConfigLoader<T> : IConfigLoader<T>
    {
        readonly string filePath;

        public ConfigLoader(string filePath)
        {
            this.filePath = filePath;
        }

        public T Load()
        {
            var textAsset = Resources.Load<TextAsset>(filePath);
            return JsonSerializer.Deserialize<T>(textAsset.text);
        }
    }
}
