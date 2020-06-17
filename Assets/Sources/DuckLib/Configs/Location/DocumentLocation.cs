using System.IO;
using UnityEngine;

namespace DuckLib.Configs.Location
{
    public class DocumentLocation : LocalLocation
    {
        private string _documentsPath;

        public override string GetPath(string file, string extension)
        {
            var documentsPath = GetDocumentsPath();
            if (!Directory.Exists(documentsPath))
                Directory.CreateDirectory(documentsPath);
            return "file://" + documentsPath + GetNameWithExtension(file, extension);
        }

        private string GetDocumentsPath()
        {
            if (!string.IsNullOrEmpty(_documentsPath))
                return _documentsPath;

#if UNITY_EDITOR
            _documentsPath = Directory.GetParent(Application.dataPath).FullName;
            _documentsPath = Path.Combine(_documentsPath, ConfigsFolder);

#else
            _documentsPath = Application.persistentDataPath;
#endif
            return _documentsPath;
        }
    }
}