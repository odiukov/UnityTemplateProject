// Decompiled with JetBrains decompiler
// Type: DG.DOTweenEditor.UI.DOTweenUtilityWindowModules
// Assembly: DOTweenEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2794ED2-88CB-4A6A-996E-2EDA424C0531
// Assembly location: C:\Users\taras.lazoriv\Documents\Projs\Test2\Assets\#com.external.dotween\Editor\DOTweenEditor.dll

using DG.Tweening.Core;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor.UI
{
    public static class DOTweenUtilityWindowModules
    {
        private static readonly DOTweenUtilityWindowModules.ModuleInfo _audioModule = new DOTweenUtilityWindowModules.ModuleInfo("Modules/DOTweenModuleAudio.cs", "AUDIO");
        private static readonly DOTweenUtilityWindowModules.ModuleInfo _physicsModule = new DOTweenUtilityWindowModules.ModuleInfo("Modules/DOTweenModulePhysics.cs", "PHYSICS");
        private static readonly DOTweenUtilityWindowModules.ModuleInfo _physics2DModule = new DOTweenUtilityWindowModules.ModuleInfo("Modules/DOTweenModulePhysics2D.cs", "PHYSICS2D");
        private static readonly DOTweenUtilityWindowModules.ModuleInfo _spriteModule = new DOTweenUtilityWindowModules.ModuleInfo("Modules/DOTweenModuleSprite.cs", "SPRITE");
        private static readonly DOTweenUtilityWindowModules.ModuleInfo _uiModule = new DOTweenUtilityWindowModules.ModuleInfo("Modules/DOTweenModuleUI.cs", "UI");
        private static readonly DOTweenUtilityWindowModules.ModuleInfo _textMeshProModule = new DOTweenUtilityWindowModules.ModuleInfo("DOTweenTextMeshPro.cs", "TEXTMESHPRO");
        private static readonly DOTweenUtilityWindowModules.ModuleInfo _tk2DModule = new DOTweenUtilityWindowModules.ModuleInfo("DOTweenTk2D.cs", "TK2D");
        private static readonly string[] _ModuleDependentFiles = new string[4]
        {
      "DOTWEENDIR/Modules/DOTweenModuleUtils.cs",
      "DOTWEENPRODIR/DOTweenAnimation.cs",
      "DOTWEENPRODIR/DOTweenProShortcuts.cs",
      "DOTWEENPRODIR/Editor/DOTweenAnimationInspector.cs"
        };
        private static readonly List<int> _LinesToChange = new List<int>();
        private const string ModuleMarkerId = "MODULE_MARKER";
        private static EditorWindow _editor;
        private static DOTweenSettings _src;
        private static bool _refreshed;
        private static bool _isWaitingForCompilation;

        static DOTweenUtilityWindowModules()
        {
            for (int index = 0; index < DOTweenUtilityWindowModules._ModuleDependentFiles.Length; ++index)
            {
                DOTweenUtilityWindowModules._ModuleDependentFiles[index] = DOTweenUtilityWindowModules._ModuleDependentFiles[index].Replace("DOTWEENDIR/", EditorUtils.dotweenDir);
                DOTweenUtilityWindowModules._ModuleDependentFiles[index] = DOTweenUtilityWindowModules._ModuleDependentFiles[index].Replace("DOTWEENPRODIR/", EditorUtils.dotweenProDir);
            }
            DOTweenUtilityWindowModules._audioModule.filePath = EditorUtils.dotweenDir + DOTweenUtilityWindowModules._audioModule.filePath;
            DOTweenUtilityWindowModules._physicsModule.filePath = EditorUtils.dotweenDir + DOTweenUtilityWindowModules._physicsModule.filePath;
            DOTweenUtilityWindowModules._physics2DModule.filePath = EditorUtils.dotweenDir + DOTweenUtilityWindowModules._physics2DModule.filePath;
            DOTweenUtilityWindowModules._spriteModule.filePath = EditorUtils.dotweenDir + DOTweenUtilityWindowModules._spriteModule.filePath;
            DOTweenUtilityWindowModules._uiModule.filePath = EditorUtils.dotweenDir + DOTweenUtilityWindowModules._uiModule.filePath;
            DOTweenUtilityWindowModules._textMeshProModule.filePath = EditorUtils.dotweenProDir + DOTweenUtilityWindowModules._textMeshProModule.filePath;
            DOTweenUtilityWindowModules._tk2DModule.filePath = EditorUtils.dotweenProDir + DOTweenUtilityWindowModules._tk2DModule.filePath;
        }

        public static bool Draw(EditorWindow editor, DOTweenSettings src)
        {
            DOTweenUtilityWindowModules._editor = editor;
            DOTweenUtilityWindowModules._src = src;
            if (!DOTweenUtilityWindowModules._refreshed)
                DOTweenUtilityWindowModules.Refresh(DOTweenUtilityWindowModules._src, false);
            GUILayout.Label("Add/Remove Modules", EditorGUIUtils.titleStyle, new GUILayoutOption[0]);
            GUILayout.BeginVertical();
            EditorGUI.BeginDisabledGroup(EditorApplication.isCompiling);
            GUILayout.BeginVertical(GUI.skin.box, new GUILayoutOption[0]);
            GUILayout.Label("Unity", EditorGUIUtils.boldLabelStyle, new GUILayoutOption[0]);
            DOTweenUtilityWindowModules._audioModule.enabled = EditorGUILayout.Toggle("Audio", DOTweenUtilityWindowModules._audioModule.enabled, new GUILayoutOption[0]);
            DOTweenUtilityWindowModules._physicsModule.enabled = EditorGUILayout.Toggle("Physics", DOTweenUtilityWindowModules._physicsModule.enabled, new GUILayoutOption[0]);
            DOTweenUtilityWindowModules._physics2DModule.enabled = EditorGUILayout.Toggle("Physics2D", DOTweenUtilityWindowModules._physics2DModule.enabled, new GUILayoutOption[0]);
            DOTweenUtilityWindowModules._spriteModule.enabled = EditorGUILayout.Toggle("Sprites", DOTweenUtilityWindowModules._spriteModule.enabled, new GUILayoutOption[0]);
            DOTweenUtilityWindowModules._uiModule.enabled = EditorGUILayout.Toggle("UI", DOTweenUtilityWindowModules._uiModule.enabled, new GUILayoutOption[0]);
            EditorGUILayout.EndVertical();
            if (EditorUtils.hasPro)
            {
                GUILayout.BeginVertical(GUI.skin.box, new GUILayoutOption[0]);
                GUILayout.Label("External Assets (Pro)", EditorGUIUtils.boldLabelStyle, new GUILayoutOption[0]);
                DOTweenUtilityWindowModules._textMeshProModule.enabled = EditorGUILayout.Toggle("TextMesh Pro", DOTweenUtilityWindowModules._textMeshProModule.enabled, new GUILayoutOption[0]);
                DOTweenUtilityWindowModules._tk2DModule.enabled = EditorGUILayout.Toggle("2D Toolkit", DOTweenUtilityWindowModules._tk2DModule.enabled, new GUILayoutOption[0]);
                EditorGUILayout.EndVertical();
            }
            GUILayout.Space(2f);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Apply"))
            {
                DOTweenUtilityWindowModules.Apply();
                DOTweenUtilityWindowModules.Refresh(DOTweenUtilityWindowModules._src, false);
                return true;
            }
            if (GUILayout.Button("Cancel"))
                return true;
            GUILayout.EndHorizontal();
            EditorGUI.EndDisabledGroup();
            GUILayout.EndVertical();
            if (EditorApplication.isCompiling)
                DOTweenUtilityWindowModules.WaitForCompilation();
            return false;
        }

        private static void WaitForCompilation()
        {
            if (!DOTweenUtilityWindowModules._isWaitingForCompilation)
            {
                DOTweenUtilityWindowModules._isWaitingForCompilation = true;
                EditorApplication.update += new EditorApplication.CallbackFunction(DOTweenUtilityWindowModules.WaitForCompilation_Update);
                DOTweenUtilityWindowModules.WaitForCompilation_Update();
            }
            EditorGUILayout.HelpBox("Waiting for Unity to finish the compilation process...", MessageType.Info);
        }

        private static void WaitForCompilation_Update()
        {
            if (!EditorApplication.isCompiling)
            {
                EditorApplication.update -= new EditorApplication.CallbackFunction(DOTweenUtilityWindowModules.WaitForCompilation_Update);
                DOTweenUtilityWindowModules._isWaitingForCompilation = false;
                DOTweenUtilityWindowModules.Refresh(DOTweenUtilityWindowModules._src, false);
            }
            DOTweenUtilityWindowModules._editor.Repaint();
        }

        public static void ApplyModulesSettings()
        {
            DOTweenSettings doTweenSettings = DOTweenUtilityWindow.GetDOTweenSettings();
            if (!((Object)doTweenSettings != (Object)null))
                return;
            DOTweenUtilityWindowModules.Refresh(doTweenSettings, true);
        }

        public static void Refresh(DOTweenSettings src, bool applySrcSettings = false)
        {
            DOTweenUtilityWindowModules._src = src;
            DOTweenUtilityWindowModules._refreshed = true;
            AssetDatabase.StartAssetEditing();
            DOTweenUtilityWindowModules._audioModule.enabled = DOTweenUtilityWindowModules.ModuleIsEnabled(DOTweenUtilityWindowModules._audioModule);
            DOTweenUtilityWindowModules._physicsModule.enabled = DOTweenUtilityWindowModules.ModuleIsEnabled(DOTweenUtilityWindowModules._physicsModule);
            DOTweenUtilityWindowModules._physics2DModule.enabled = DOTweenUtilityWindowModules.ModuleIsEnabled(DOTweenUtilityWindowModules._physics2DModule);
            DOTweenUtilityWindowModules._spriteModule.enabled = DOTweenUtilityWindowModules.ModuleIsEnabled(DOTweenUtilityWindowModules._spriteModule);
            DOTweenUtilityWindowModules._uiModule.enabled = DOTweenUtilityWindowModules.ModuleIsEnabled(DOTweenUtilityWindowModules._uiModule);
            DOTweenUtilityWindowModules._textMeshProModule.enabled = DOTweenUtilityWindowModules.ModuleIsEnabled(DOTweenUtilityWindowModules._textMeshProModule);
            DOTweenUtilityWindowModules._tk2DModule.enabled = DOTweenUtilityWindowModules.ModuleIsEnabled(DOTweenUtilityWindowModules._tk2DModule);
            DOTweenUtilityWindowModules.CheckAutoModuleSettings(applySrcSettings, DOTweenUtilityWindowModules._audioModule, ref src.modules.audioEnabled);
            DOTweenUtilityWindowModules.CheckAutoModuleSettings(applySrcSettings, DOTweenUtilityWindowModules._physicsModule, ref src.modules.physicsEnabled);
            DOTweenUtilityWindowModules.CheckAutoModuleSettings(applySrcSettings, DOTweenUtilityWindowModules._physics2DModule, ref src.modules.physics2DEnabled);
            DOTweenUtilityWindowModules.CheckAutoModuleSettings(applySrcSettings, DOTweenUtilityWindowModules._spriteModule, ref src.modules.spriteEnabled);
            DOTweenUtilityWindowModules.CheckAutoModuleSettings(applySrcSettings, DOTweenUtilityWindowModules._uiModule, ref src.modules.uiEnabled);
            DOTweenUtilityWindowModules.CheckAutoModuleSettings(applySrcSettings, DOTweenUtilityWindowModules._textMeshProModule, ref src.modules.textMeshProEnabled);
            DOTweenUtilityWindowModules.CheckAutoModuleSettings(applySrcSettings, DOTweenUtilityWindowModules._tk2DModule, ref src.modules.tk2DEnabled);
            AssetDatabase.StopAssetEditing();
            EditorUtility.SetDirty((Object)DOTweenUtilityWindowModules._src);
        }

        private static void Apply()
        {
            AssetDatabase.StartAssetEditing();
            bool flag1 = DOTweenUtilityWindowModules.ToggleModule(DOTweenUtilityWindowModules._audioModule, ref DOTweenUtilityWindowModules._src.modules.audioEnabled);
            bool flag2 = DOTweenUtilityWindowModules.ToggleModule(DOTweenUtilityWindowModules._physicsModule, ref DOTweenUtilityWindowModules._src.modules.physicsEnabled);
            bool flag3 = DOTweenUtilityWindowModules.ToggleModule(DOTweenUtilityWindowModules._physics2DModule, ref DOTweenUtilityWindowModules._src.modules.physics2DEnabled);
            bool flag4 = DOTweenUtilityWindowModules.ToggleModule(DOTweenUtilityWindowModules._spriteModule, ref DOTweenUtilityWindowModules._src.modules.spriteEnabled);
            bool flag5 = DOTweenUtilityWindowModules.ToggleModule(DOTweenUtilityWindowModules._uiModule, ref DOTweenUtilityWindowModules._src.modules.uiEnabled);
            bool flag6 = false;
            bool flag7 = false;
            if (EditorUtils.hasPro)
            {
                flag6 = DOTweenUtilityWindowModules.ToggleModule(DOTweenUtilityWindowModules._textMeshProModule, ref DOTweenUtilityWindowModules._src.modules.textMeshProEnabled);
                flag7 = DOTweenUtilityWindowModules.ToggleModule(DOTweenUtilityWindowModules._tk2DModule, ref DOTweenUtilityWindowModules._src.modules.tk2DEnabled);
            }
            AssetDatabase.StopAssetEditing();
            EditorUtility.SetDirty((Object)DOTweenUtilityWindowModules._src);
            if (flag1 | flag2 | flag3 | flag4 | flag5 | flag6 | flag7)
            {
                StringBuilder strb = new StringBuilder();
                strb.Append("<b>DOTween module files modified ► </b>");
                if (flag1)
                    DOTweenUtilityWindowModules.Apply_AppendLog(strb, DOTweenUtilityWindowModules._src.modules.audioEnabled, "Audio");
                if (flag2)
                    DOTweenUtilityWindowModules.Apply_AppendLog(strb, DOTweenUtilityWindowModules._src.modules.physicsEnabled, "Physics");
                if (flag3)
                    DOTweenUtilityWindowModules.Apply_AppendLog(strb, DOTweenUtilityWindowModules._src.modules.physics2DEnabled, "Physics2D");
                if (flag4)
                    DOTweenUtilityWindowModules.Apply_AppendLog(strb, DOTweenUtilityWindowModules._src.modules.spriteEnabled, "Sprites");
                if (flag5)
                    DOTweenUtilityWindowModules.Apply_AppendLog(strb, DOTweenUtilityWindowModules._src.modules.uiEnabled, "UI");
                if (flag6)
                    DOTweenUtilityWindowModules.Apply_AppendLog(strb, DOTweenUtilityWindowModules._src.modules.textMeshProEnabled, "TextMesh Pro");
                if (flag7)
                    DOTweenUtilityWindowModules.Apply_AppendLog(strb, DOTweenUtilityWindowModules._src.modules.tk2DEnabled, "2D Toolkit");
                strb.Remove(strb.Length - 3, 3);
                Debug.Log((object)strb.ToString());
            }
            ASMDEFManager.RefreshExistingASMDEFFiles();
        }

        private static void Apply_AppendLog(StringBuilder strb, bool enabled, string id)
        {
            strb.Append("<color=#").Append(enabled ? "00ff00" : "ff0000").Append('>').Append(id).Append("</color>").Append(" - ");
        }

        private static bool ModuleIsEnabled(DOTweenUtilityWindowModules.ModuleInfo m)
        {
            if (!File.Exists(m.filePath))
                return false;
            using (StreamReader streamReader = new StreamReader(m.filePath))
            {
                for (string str = streamReader.ReadLine(); str != null; str = streamReader.ReadLine())
                {
                    if (str.EndsWith("MODULE_MARKER") && str.StartsWith("#if"))
                        return str.Contains("true");
                }
            }
            return true;
        }

        private static void CheckAutoModuleSettings(bool applySettings, DOTweenUtilityWindowModules.ModuleInfo m, ref bool srcModuleEnabled)
        {
            if (m.enabled == srcModuleEnabled)
                return;
            if (applySettings)
            {
                m.enabled = srcModuleEnabled;
                DOTweenUtilityWindowModules.ToggleModule(m, ref srcModuleEnabled);
            }
            else
            {
                srcModuleEnabled = m.enabled;
                EditorUtility.SetDirty((Object)DOTweenUtilityWindowModules._src);
            }
        }

        private static bool ToggleModule(DOTweenUtilityWindowModules.ModuleInfo m, ref bool srcSetting)
        {
            if (!File.Exists(m.filePath))
                return false;
            srcSetting = m.enabled;
            bool flag = false;
            DOTweenUtilityWindowModules._LinesToChange.Clear();
            string[] strArray = File.ReadAllLines(m.filePath);
            for (int index = 0; index < strArray.Length; ++index)
            {
                string str = strArray[index];
                if (str.EndsWith("MODULE_MARKER") && str.StartsWith("#if") && (m.enabled && str.Contains("false") || !m.enabled && str.Contains("true")))
                    DOTweenUtilityWindowModules._LinesToChange.Add(index);
            }
            if (DOTweenUtilityWindowModules._LinesToChange.Count > 0)
            {
                flag = true;
                using (StreamWriter streamWriter = new StreamWriter(m.filePath))
                {
                    for (int index = 0; index < strArray.Length; ++index)
                    {
                        string str = strArray[index];
                        if (DOTweenUtilityWindowModules._LinesToChange.Contains(index))
                            str = m.enabled ? str.Replace("false", "true") : str.Replace("true", "false");
                        streamWriter.WriteLine(str);
                    }
                }
                AssetDatabase.ImportAsset(EditorUtils.FullPathToADBPath(m.filePath), ImportAssetOptions.Default);
            }
            string marker = m.id + "_MARKER";
            for (int index = 0; index < DOTweenUtilityWindowModules._ModuleDependentFiles.Length; ++index)
            {
                if (DOTweenUtilityWindowModules.ToggleModuleInDependentFile(DOTweenUtilityWindowModules._ModuleDependentFiles[index], m.enabled, marker))
                    flag = true;
            }
            DOTweenUtilityWindowModules._LinesToChange.Clear();
            return flag;
        }

        private static bool ToggleModuleInDependentFile(string filePath, bool enable, string marker)
        {
            if (!File.Exists(filePath))
                return false;
            bool flag = false;
            DOTweenUtilityWindowModules._LinesToChange.Clear();
            string[] strArray = File.ReadAllLines(filePath);
            for (int index = 0; index < strArray.Length; ++index)
            {
                string str = strArray[index];
                if (str.EndsWith(marker) && str.StartsWith("#if") && (enable && str.Contains("false") || !enable && str.Contains("true")))
                    DOTweenUtilityWindowModules._LinesToChange.Add(index);
            }
            if (DOTweenUtilityWindowModules._LinesToChange.Count > 0)
            {
                flag = true;
                using (StreamWriter streamWriter = new StreamWriter(filePath))
                {
                    for (int index = 0; index < strArray.Length; ++index)
                    {
                        string str = strArray[index];
                        if (DOTweenUtilityWindowModules._LinesToChange.Contains(index))
                            str = enable ? str.Replace("false", "true") : str.Replace("true", "false");
                        streamWriter.WriteLine(str);
                    }
                }
                AssetDatabase.ImportAsset(EditorUtils.FullPathToADBPath(filePath), ImportAssetOptions.Default);
            }
            return flag;
        }

        private class ModuleInfo
        {
            public bool enabled;
            public string filePath;
            public readonly string id;

            public ModuleInfo(string filePath, string id)
            {
                this.filePath = filePath;
                this.id = id;
            }
        }
    }
}
