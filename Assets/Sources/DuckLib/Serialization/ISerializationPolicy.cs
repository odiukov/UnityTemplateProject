using System;

namespace DuckLib.Serialization
{
    public interface ISerializationPolicy
    {
        string FileExtension { get; }
        string Serialize(object obj);
        T Deserialize<T>(string json);
        object Deserialize(string json, Type type);
    }
}