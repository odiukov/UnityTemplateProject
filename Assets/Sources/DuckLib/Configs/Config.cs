using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DuckLib.Configs.Location;
using DuckLib.Serialization;
using UnityEngine.Networking;

namespace DuckLib.Configs
{
    public abstract class Config<TConfig, TSerializationPolicy, TLocation>
        where TSerializationPolicy : ISerializationPolicy, new()
        where TLocation : ILocation, new()
        where TConfig : Config<TConfig, TSerializationPolicy, TLocation>
    {
        protected abstract string FileName { get; }

        private TSerializationPolicy _serializer = new TSerializationPolicy();
        private TLocation _location = new TLocation();

        public async Task<TConfig> Load()
        {
            var filepath =
                _location.GetPath(FileName,
                    _serializer.FileExtension);
            var op = await UnityWebRequest.Get(filepath).SendWebRequest();
            return _serializer.Deserialize<TConfig>(op.downloadHandler.text);
        }
    }
}