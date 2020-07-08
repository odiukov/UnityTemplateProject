// Decompiled with JetBrains decompiler
// Type: DG.DOTweenEditor.ASMDEFManager
// Assembly: DOTweenEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2794ED2-88CB-4A6A-996E-2EDA424C0531
// Assembly location: C:\Users\taras.lazoriv\Documents\Projs\Test2\Assets\#com.external.dotween\Editor\DOTweenEditor.dll

using DG.DOTweenEditor.UI;
using DG.Tweening.Core;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor
{
    internal static class ASMDEFManager
    {
        private const string _ModulesId = "DOTween.Modules";
        private const string _ProId = "DOTweenPro.Scripts";
        private const string _ModulesASMDEFFile = "DOTween.Modules.asmdef";
        private const string _ProASMDEFFile = "DOTweenPro.Scripts.asmdef";
        private const string _RefTextMeshPro = "Unity.TextMeshPro";

        public static bool hasModulesASMDEF { get; private set; }

        public static bool hasProASMDEF { get; private set; }

        static ASMDEFManager()
        {
            ASMDEFManager.Refresh();
        }

        public static void Refresh()
        {
            ASMDEFManager.hasModulesASMDEF = File.Exists(EditorUtils.dotweenModulesDir + "DOTween.Modules.asmdef");
            ASMDEFManager.hasProASMDEF = File.Exists(EditorUtils.dotweenProDir + "DOTweenPro.Scripts.asmdef");
        }

        public static void RefreshExistingASMDEFFiles()
        {
            ASMDEFManager.Refresh();
            if (!ASMDEFManager.hasModulesASMDEF)
            {
                if (!ASMDEFManager.hasProASMDEF)
                    return;
                ASMDEFManager.RemoveASMDEF(ASMDEFManager.ASMDEFType.DOTweenPro);
            }
            else if (EditorUtils.hasPro && !ASMDEFManager.hasProASMDEF)
            {
                ASMDEFManager.CreateASMDEF(ASMDEFManager.ASMDEFType.DOTweenPro, false);
            }
            else if(EditorUtils.hasPro)
            {
                DOTweenSettings doTweenSettings = DOTweenUtilityWindow.GetDOTweenSettings();
                if ((Object)doTweenSettings == (Object)null)
                    return;
                bool flag = false;
                using (StreamReader streamReader = new StreamReader(EditorUtils.dotweenProDir + "DOTweenPro.Scripts.asmdef"))
                {
                    string str;
                    while ((str = streamReader.ReadLine()) != null)
                    {
                        if (str.Contains("Unity.TextMeshPro"))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (flag == doTweenSettings.modules.textMeshProEnabled)
                    return;
                ASMDEFManager.CreateASMDEF(ASMDEFManager.ASMDEFType.DOTweenPro, true);
            }
        }

        public static void CreateAllASMDEF()
        {
            ASMDEFManager.CreateASMDEF(ASMDEFManager.ASMDEFType.Modules, false);
            ASMDEFManager.CreateASMDEF(ASMDEFManager.ASMDEFType.DOTweenPro, false);
        }

        public static void RemoveAllASMDEF()
        {
            ASMDEFManager.RemoveASMDEF(ASMDEFManager.ASMDEFType.Modules);
            ASMDEFManager.RemoveASMDEF(ASMDEFManager.ASMDEFType.DOTweenPro);
        }

        private static void LogASMDEFChange(ASMDEFManager.ASMDEFType asmdefType, ASMDEFManager.ChangeType changeType)
        {
            string format = "<b>DOTween ASMDEF file <color=#{0}>{1}</color></b> ► {2}";
            string str1;
            switch (changeType)
            {
                case ASMDEFManager.ChangeType.Deleted:
                    str1 = "ff0000";
                    break;
                case ASMDEFManager.ChangeType.Created:
                    str1 = "00ff00";
                    break;
                default:
                    str1 = "ff6600";
                    break;
            }
            string str2;
            switch (changeType)
            {
                case ASMDEFManager.ChangeType.Deleted:
                    str2 = "removed";
                    break;
                case ASMDEFManager.ChangeType.Created:
                    str2 = "created";
                    break;
                default:
                    str2 = "changed";
                    break;
            }
            string str3 = asmdefType == ASMDEFManager.ASMDEFType.Modules ? "DOTween/Modules/DOTween.Modules.asmdef" : "DOTweenPro/DOTweenPro.Scripts.asmdef";
            Debug.Log((object)string.Format(format, (object)str1, (object)str2, (object)str3));
        }

        private static void CreateASMDEF(ASMDEFManager.ASMDEFType type, bool forceOverwrite = false)
        {
            ASMDEFManager.Refresh();
            bool flag = false;
            string str1 = (string)null;
            string str2 = (string)null;
            string path = (string)null;
            switch (type)
            {
                case ASMDEFManager.ASMDEFType.Modules:
                    flag = ASMDEFManager.hasModulesASMDEF;
                    str1 = "DOTween.Modules";
                    str2 = "DOTween.Modules.asmdef";
                    path = EditorUtils.dotweenModulesDir;
                    break;
                case ASMDEFManager.ASMDEFType.DOTweenPro:
                    flag = ASMDEFManager.hasProASMDEF;
                    str1 = "DOTweenPro.Scripts";
                    str2 = "DOTweenPro.Scripts.asmdef";
                    path = EditorUtils.dotweenProDir;
                    break;
            }
            if (flag && !forceOverwrite)
                EditorUtility.DisplayDialog("Create ASMDEF", str2 + " already exists", "Ok");
            else if (!Directory.Exists(path))
            {
                EditorUtility.DisplayDialog("Create ASMDEF", string.Format("Directory not found\n({0})", (object)path), "Ok");
            }
            else
            {
                string str3 = path + str2;
                using (StreamWriter text = File.CreateText(str3))
                {
                    text.WriteLine("{");
                    switch (type)
                    {
                        case ASMDEFManager.ASMDEFType.Modules:
                            text.WriteLine("\t\"name\": \"{0}\"", (object)str1);
                            break;
                        case ASMDEFManager.ASMDEFType.DOTweenPro:
                            text.WriteLine("\t\"name\": \"{0}\",", (object)str1);
                            text.WriteLine("\t\"references\": [");
                            DOTweenSettings doTweenSettings = DOTweenUtilityWindow.GetDOTweenSettings();
                            if ((Object)doTweenSettings != (Object)null && doTweenSettings.modules.textMeshProEnabled)
                                text.WriteLine("\t\t\"{0}\",", (object)"Unity.TextMeshPro");
                            text.WriteLine("\t\t\"{0}\"", (object)"DOTween.Modules");
                            text.WriteLine("\t]");
                            break;
                    }
                    text.WriteLine("}");
                }
                AssetDatabase.ImportAsset(EditorUtils.FullPathToADBPath(str3), ImportAssetOptions.ForceUpdate);
                ASMDEFManager.Refresh();
                ASMDEFManager.LogASMDEFChange(type, flag ? ASMDEFManager.ChangeType.Overwritten : ASMDEFManager.ChangeType.Created);
            }
        }

        private static void RemoveASMDEF(ASMDEFManager.ASMDEFType type)
        {
            bool flag = false;
            string str1 = (string)null;
            string str2 = (string)null;
            switch (type)
            {
                case ASMDEFManager.ASMDEFType.Modules:
                    flag = ASMDEFManager.hasModulesASMDEF;
                    str2 = EditorUtils.dotweenModulesDir;
                    str1 = "DOTween.Modules.asmdef";
                    break;
                case ASMDEFManager.ASMDEFType.DOTweenPro:
                    flag = ASMDEFManager.hasProASMDEF;
                    str1 = "DOTweenPro.Scripts.asmdef";
                    str2 = EditorUtils.dotweenProDir;
                    break;
            }
            ASMDEFManager.Refresh();
            if (!flag)
            {
                EditorUtility.DisplayDialog("Remove ASMDEF", str1 + " not present", "Ok");
            }
            else
            {
                AssetDatabase.DeleteAsset(EditorUtils.FullPathToADBPath(str2 + str1));
                ASMDEFManager.Refresh();
                ASMDEFManager.LogASMDEFChange(type, ASMDEFManager.ChangeType.Deleted);
            }
        }

        public enum ASMDEFType
        {
            Modules,
            DOTweenPro,
        }

        private enum ChangeType
        {
            Deleted,
            Created,
            Overwritten,
        }
    }
}
