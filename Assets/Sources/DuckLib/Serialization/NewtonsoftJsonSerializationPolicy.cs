using System;
using Newtonsoft.Json;

namespace DuckLib.Serialization
{
    public sealed class NewtonsoftJsonSerializationPolicy : ISerializationPolicy
    {
        public string FileExtension => "json";
        
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }
    }
}