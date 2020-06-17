using System.IO;
using UnityEngine;

namespace DuckLib.Configs.Location
{
    public class StreamingAssetsLocation : LocalLocation
    {
        public override string GetPath(string file, string extension)
        {
            return GetStreamingAssetsPath(ConfigsFolder) + GetNameWithExtension(file, extension);
        }

        private string GetStreamingAssetsPath(string fileName)
        {
#if UNITY_ANDROID
            return Path.Combine (Application.streamingAssetsPath, fileName);
#else
            return "file://" + Path.Combine(Application.streamingAssetsPath, fileName);
#endif
        }
    }
}