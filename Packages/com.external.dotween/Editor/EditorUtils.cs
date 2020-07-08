// Decompiled with JetBrains decompiler
// Type: DG.DOTweenEditor.EditorUtils
// Assembly: DOTweenEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2794ED2-88CB-4A6A-996E-2EDA424C0531
// Assembly location: C:\Users\taras.lazoriv\Documents\Projs\Test2\Assets\#com.external.dotween\Editor\DOTweenEditor.dll

using DG.Tweening;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor
{
    public static class EditorUtils
    {
        private static readonly StringBuilder _Strb = new StringBuilder();
        private static bool _hasPro;
        private static string _proVersion;
        private static bool _hasCheckedForPro;
        private static string _editorADBDir;
        private static string _demigiantDir;
        private static string _dotweenDir;
        private static string _dotweenProDir;
        private static string _dotweenModulesDir;

        public static string projectPath { get; private set; }

        public static string assetsPath { get; private set; }

        public static bool hasPro
        {
            get
            {
                if (!EditorUtils._hasCheckedForPro)
                    EditorUtils.CheckForPro();
                return EditorUtils._hasPro;
            }
        }

        public static string proVersion
        {
            get
            {
                if (!EditorUtils._hasCheckedForPro)
                    EditorUtils.CheckForPro();
                return EditorUtils._proVersion;
            }
        }

        public static string editorADBDir
        {
            get
            {
                if (string.IsNullOrEmpty(EditorUtils._editorADBDir))
                    EditorUtils.StoreEditorADBDir();
                return EditorUtils._editorADBDir;
            }
        }

        public static string demigiantDir
        {
            get
            {
                if (string.IsNullOrEmpty(EditorUtils._demigiantDir))
                    EditorUtils.StoreDOTweenDirs();
                return EditorUtils._demigiantDir;
            }
        }

        public static string dotweenDir
        {
            get
            {
                if (string.IsNullOrEmpty(EditorUtils._dotweenDir))
                    EditorUtils.StoreDOTweenDirs();
                return EditorUtils._dotweenDir;
            }
        }

        public static string dotweenProDir
        {
            get
            {
                if (string.IsNullOrEmpty(EditorUtils._dotweenProDir))
                    EditorUtils.StoreDOTweenDirs();
                return EditorUtils._dotweenProDir;
            }
        }

        public static string dotweenModulesDir
        {
            get
            {
                if (string.IsNullOrEmpty(EditorUtils._dotweenModulesDir))
                    EditorUtils.StoreDOTweenDirs();
                return EditorUtils._dotweenModulesDir;
            }
        }

        public static bool isOSXEditor { get; private set; }

        public static string pathSlash { get; private set; }

        public static string pathSlashToReplace { get; private set; }

        static EditorUtils()
        {
            EditorUtils.isOSXEditor = Application.platform == RuntimePlatform.OSXEditor;
            int num = Application.platform == RuntimePlatform.WindowsEditor ? 1 : 0;
            EditorUtils.pathSlash = num != 0 ? "\\" : "/";
            EditorUtils.pathSlashToReplace = num != 0 ? "/" : "\\";
            EditorUtils.projectPath = Application.dataPath;
            EditorUtils.projectPath = EditorUtils.projectPath.Substring(0, EditorUtils.projectPath.LastIndexOf("/"));
            EditorUtils.projectPath = EditorUtils.projectPath.Replace(EditorUtils.pathSlashToReplace, EditorUtils.pathSlash);
            EditorUtils.assetsPath = EditorUtils.projectPath + EditorUtils.pathSlash + "Assets";
        }

        public static void DelayedCall(float delay, Action callback)
        {
            DelayedCall delayedCall = new DelayedCall(delay, callback);
        }

     
        /// <summary>Returns TRUE if setup is required</summary>
        public static bool DOTweenSetupRequired()
        {
            if (!Directory.Exists(EditorUtils.dotweenDir))
                return false;
            return (uint)Directory.GetFiles(EditorUtils.dotweenDir + "Editor", "DOTweenUpgradeManager.*").Length > 0U;
        }

        public static void DeleteDOTweenUpgradeManagerFiles()
        {
            string adbPath = EditorUtils.FullPathToADBPath(EditorUtils.dotweenDir);
            AssetDatabase.StartAssetEditing();
            EditorUtils.DeleteAssetsIfExist(new string[4]
            {
        adbPath + "Editor/DOTweenUpgradeManager.dll",
        adbPath + "Editor/DOTweenUpgradeManager.xml",
        adbPath + "Editor/DOTweenUpgradeManager.pdb",
        adbPath + "Editor/DOTweenUpgradeManager.dll.mdb"
            });
            AssetDatabase.StopAssetEditing();
        }

        public static void DeleteLegacyNoModulesDOTweenFiles()
        {
            string adbPath = EditorUtils.FullPathToADBPath(EditorUtils.dotweenDir);
            AssetDatabase.StartAssetEditing();
            EditorUtils.DeleteAssetsIfExist(new string[21]
            {
        adbPath + "DOTween43.dll",
        adbPath + "DOTween43.xml",
        adbPath + "DOTween43.dll.mdb",
        adbPath + "DOTween43.dll.addon",
        adbPath + "DOTween43.xml.addon",
        adbPath + "DOTween43.dll.mdb.addon",
        adbPath + "DOTween46.dll",
        adbPath + "DOTween46.xml",
        adbPath + "DOTween46.dll.mdb",
        adbPath + "DOTween46.dll.addon",
        adbPath + "DOTween46.xml.addon",
        adbPath + "DOTween46.dll.mdb.addon",
        adbPath + "DOTween50.dll",
        adbPath + "DOTween50.xml",
        adbPath + "DOTween50.dll.mdb",
        adbPath + "DOTween50.dll.addon",
        adbPath + "DOTween50.xml.addon",
        adbPath + "DOTween50.dll.mdb.addon",
        adbPath + "DOTweenTextMeshPro.cs.addon",
        adbPath + "DOTweenTextMeshPro_mod.cs",
        adbPath + "DOTweenTk2d.cs.addon"
            });
            AssetDatabase.StopAssetEditing();
        }

        public static void DeleteOldDemiLibCore()
        {
            string assemblyFilePath = EditorUtils.GetAssemblyFilePath(typeof(DOTween).Assembly);
            string str1 = assemblyFilePath.IndexOf("/") != -1 ? "/" : "\\";
            string str2 = assemblyFilePath.Substring(0, assemblyFilePath.LastIndexOf(str1));
            string fullPath = str2.Substring(0, str2.LastIndexOf(str1)) + str1 + "DemiLib";
            string adbPath = EditorUtils.FullPathToADBPath(fullPath);
            if (!EditorUtils.AssetExists(adbPath))
                return;
            string str3 = adbPath + "/Core";
            if (!EditorUtils.AssetExists(str3))
                return;
            EditorUtils.DeleteAssetsIfExist(new string[7]
            {
        adbPath + "/DemiLib.dll",
        adbPath + "/DemiLib.xml",
        adbPath + "/DemiLib.dll.mdb",
        adbPath + "/Editor/DemiEditor.dll",
        adbPath + "/Editor/DemiEditor.xml",
        adbPath + "/Editor/DemiEditor.dll.mdb",
        adbPath + "/Editor/Imgs"
            });
            if (!EditorUtils.AssetExists(adbPath + "/Editor") || Directory.GetFiles(fullPath + str1 + "Editor").Length != 0)
                return;
            AssetDatabase.DeleteAsset(adbPath + "/Editor");
            AssetDatabase.ImportAsset(str3, ImportAssetOptions.ImportRecursive);
        }

        private static void DeleteAssetsIfExist(string[] adbFilePaths)
        {
            foreach (string adbFilePath in adbFilePaths)
            {
                if (EditorUtils.AssetExists(adbFilePath))
                    File.Delete(adbFilePath);
            }
        }

        /// <summary>
        /// Returns TRUE if the file/directory at the given path exists.
        /// </summary>
        /// <param name="adbPath">Path, relative to Unity's project folder</param>
        /// <returns></returns>
        public static bool AssetExists(string adbPath)
        {
            string fullPath = EditorUtils.ADBPathToFullPath(adbPath);
            if (!File.Exists(fullPath))
                return Directory.Exists(fullPath);
            return true;
        }

        /// <summary>
        /// Converts the given project-relative path to a full path,
        /// with backward (\) slashes).
        /// </summary>
        public static string ADBPathToFullPath(string adbPath)
        {
            adbPath = adbPath.Replace(EditorUtils.pathSlashToReplace, EditorUtils.pathSlash);
            return EditorUtils.projectPath + EditorUtils.pathSlash + adbPath;
        }

        /// <summary>
        /// Converts the given full path to a path usable with AssetDatabase methods
        /// (relative to Unity's project folder, and with the correct Unity forward (/) slashes).
        /// </summary>
        public static string FullPathToADBPath(string fullPath)
        {
            return fullPath.Substring(EditorUtils.projectPath.Length + 1).Replace("\\", "/");
        }

        /// <summary>
        /// Connects to a <see cref="T:UnityEngine.ScriptableObject" /> asset.
        /// If the asset already exists at the given path, loads it and returns it.
        /// Otherwise, either returns NULL or automatically creates it before loading and returning it
        /// (depending on the given parameters).
        /// </summary>
        /// <typeparam name="T">Asset type</typeparam>
        /// <param name="adbFilePath">File path (relative to Unity's project folder)</param>
        /// <param name="createIfMissing">If TRUE and the requested asset doesn't exist, forces its creation</param>
        public static T ConnectToSourceAsset<T>(string adbFilePath, bool createIfMissing = false) where T : ScriptableObject
        {
            if (!EditorUtils.AssetExists(adbFilePath))
            {
                if (!createIfMissing)
                    return default(T);
                EditorUtils.CreateScriptableAsset<T>(adbFilePath);
            }
            T obj = (T)AssetDatabase.LoadAssetAtPath(adbFilePath, typeof(T));
            if ((UnityEngine.Object)obj == (UnityEngine.Object)null)
            {
                EditorUtils.CreateScriptableAsset<T>(adbFilePath);
                obj = (T)AssetDatabase.LoadAssetAtPath(adbFilePath, typeof(T));
            }
            return obj;
        }

        /// <summary>
        /// Full path for the given loaded assembly, assembly file included
        /// </summary>
        public static string GetAssemblyFilePath(Assembly assembly)
        {
            string str = Uri.UnescapeDataString(new UriBuilder(assembly.CodeBase).Path);
            if (str.Substring(str.Length - 3) == "dll")
                return str;
            return Path.GetFullPath(assembly.Location);
        }

        /// <summary>
        /// Adds the given global define if it's not already present
        /// </summary>
        public static void AddGlobalDefine(string id)
        {
            bool flag = false;
            int num = 0;
            foreach (BuildTargetGroup buildTargetGroup in (BuildTargetGroup[])Enum.GetValues(typeof(BuildTargetGroup)))
            {
                if (EditorUtils.IsValidBuildTargetGroup(buildTargetGroup))
                {
                    string defineSymbolsForGroup = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
                    if (Array.IndexOf<string>(defineSymbolsForGroup.Split(';'), id) == -1)
                    {
                        flag = true;
                        ++num;
                        string defines = defineSymbolsForGroup + (defineSymbolsForGroup.Length > 0 ? ";" + id : id);
                        PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, defines);
                    }
                }
            }
            if (!flag)
                return;
            Debug.Log((object)string.Format("DOTween : added global define \"{0}\" to {1} BuildTargetGroups", (object)id, (object)num));
        }

        /// <summary>Removes the given global define if it's present</summary>
        public static void RemoveGlobalDefine(string id)
        {
            bool flag = false;
            int num = 0;
            foreach (BuildTargetGroup buildTargetGroup in (BuildTargetGroup[])Enum.GetValues(typeof(BuildTargetGroup)))
            {
                if (EditorUtils.IsValidBuildTargetGroup(buildTargetGroup))
                {
                    string[] array = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';');
                    if (Array.IndexOf<string>(array, id) != -1)
                    {
                        flag = true;
                        ++num;
                        EditorUtils._Strb.Length = 0;
                        for (int index = 0; index < array.Length; ++index)
                        {
                            if (!(array[index] == id))
                            {
                                if (EditorUtils._Strb.Length > 0)
                                    EditorUtils._Strb.Append(';');
                                EditorUtils._Strb.Append(array[index]);
                            }
                        }
                        PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, EditorUtils._Strb.ToString());
                    }
                }
            }
            EditorUtils._Strb.Length = 0;
            if (!flag)
                return;
            Debug.Log((object)string.Format("DOTween : removed global define \"{0}\" from {1} BuildTargetGroups", (object)id, (object)num));
        }

        /// <summary>
        /// Returns TRUE if the given global define is present in all the <see cref="T:UnityEditor.BuildTargetGroup" />
        /// or only in the given <see cref="T:UnityEditor.BuildTargetGroup" />, depending on passed parameters.<para />
        /// </summary>
        /// <param name="id"></param>
        /// <param name="buildTargetGroup"><see cref="T:UnityEditor.BuildTargetGroup" />to use. Leave NULL to check in all of them.</param>
        public static bool HasGlobalDefine(string id, BuildTargetGroup? buildTargetGroup = null)
        {
            BuildTargetGroup[] buildTargetGroupArray;
            if (buildTargetGroup.HasValue)
                buildTargetGroupArray = new BuildTargetGroup[1]
                {
          buildTargetGroup.Value
                };
            else
                buildTargetGroupArray = (BuildTargetGroup[])Enum.GetValues(typeof(BuildTargetGroup));
            foreach (BuildTargetGroup buildTargetGroup1 in buildTargetGroupArray)
            {
                if (EditorUtils.IsValidBuildTargetGroup(buildTargetGroup1))
                {
                    if (Array.IndexOf<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup1).Split(';'), id) != -1)
                        return true;
                }
            }
            return false;
        }

        private static void CheckForPro()
        {
            EditorUtils._hasCheckedForPro = true;
            try
            {
                EditorUtils._proVersion = Assembly.Load("DOTweenPro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null").GetType("DG.Tweening.DOTweenPro").GetField("Version", BindingFlags.Static | BindingFlags.Public).GetValue((object)null) as string;
                EditorUtils._hasPro = true;
            }
            catch
            {
                EditorUtils._hasPro = false;
                EditorUtils._proVersion = "-";
            }
        }

        private static void StoreEditorADBDir()
        {
            EditorUtils._editorADBDir = Path.GetDirectoryName(EditorUtils.GetAssemblyFilePath(Assembly.GetExecutingAssembly())).Substring(Application.dataPath.Length + 1).Replace("\\", "/") + "/";
        }

        public static void StoreDOTweenDirs()
        {
            string str = Path.DirectorySeparatorChar.ToString();
            EditorUtils._dotweenDir = Path.GetDirectoryName(EditorUtils.GetAssemblyFilePath(Assembly.GetAssembly(typeof(DOTween)))) + str;
            EditorUtils._dotweenDir = EditorUtils._dotweenDir.Substring(0, EditorUtils._dotweenDir.LastIndexOf(str) + 1);
            EditorUtils._dotweenProDir = EditorUtils._dotweenDir.Substring(0, EditorUtils._dotweenDir.LastIndexOf(str));
            EditorUtils._dotweenProDir = EditorUtils._dotweenProDir.Substring(0, EditorUtils._dotweenProDir.LastIndexOf(str) + 1) + "DOTweenPro" + str;
            EditorUtils._demigiantDir = EditorUtils._dotweenDir.Substring(0, EditorUtils._dotweenDir.LastIndexOf(str));
            EditorUtils._demigiantDir = EditorUtils._demigiantDir.Substring(0, EditorUtils._demigiantDir.LastIndexOf(str) + 1);
            if (EditorUtils._demigiantDir.Substring(EditorUtils._demigiantDir.Length - 10, 9) != "Demigiant")
                EditorUtils._demigiantDir = (string)null;
            EditorUtils._dotweenDir = EditorUtils._dotweenDir.Replace(EditorUtils.pathSlashToReplace, EditorUtils.pathSlash);
            EditorUtils._dotweenProDir = EditorUtils._dotweenProDir.Replace(EditorUtils.pathSlashToReplace, EditorUtils.pathSlash);
            EditorUtils._dotweenModulesDir = EditorUtils._dotweenDir + "Modules" + EditorUtils.pathSlash;
            if (EditorUtils._demigiantDir == null)
                return;
            EditorUtils._demigiantDir = EditorUtils._demigiantDir.Replace(EditorUtils.pathSlashToReplace, EditorUtils.pathSlash);
        }

        private static void CreateScriptableAsset<T>(string adbFilePath) where T : ScriptableObject
        {
            AssetDatabase.CreateAsset((UnityEngine.Object)ScriptableObject.CreateInstance<T>(), adbFilePath);
        }

        private static bool IsValidBuildTargetGroup(BuildTargetGroup group)
        {
            if (group == BuildTargetGroup.Unknown)
                return false;
            MethodInfo method1 = System.Type.GetType("UnityEditor.Modules.ModuleManager, UnityEditor.dll").GetMethod("GetTargetStringFromBuildTargetGroup", BindingFlags.Static | BindingFlags.NonPublic);
            MethodInfo method2 = typeof(PlayerSettings).GetMethod("GetPlatformName", BindingFlags.Static | BindingFlags.NonPublic);
            // ISSUE: variable of the null type
            //__Null local = null;
            object[] parameters = new object[1] { (object)group };
            string str1 = (string)method1.Invoke((object)null, parameters);
            string str2 = (string)method2.Invoke((object)null, new object[1]
            {
        (object) group
            });
            if (string.IsNullOrEmpty(str1))
                return !string.IsNullOrEmpty(str2);
            return true;
        }
    }
}
