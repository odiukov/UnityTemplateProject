// Decompiled with JetBrains decompiler
// Type: DG.DOTweenEditor.UI.DOTweenUtilityWindow
// Assembly: DOTweenEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2794ED2-88CB-4A6A-996E-2EDA424C0531
// Assembly location: C:\Users\taras.lazoriv\Documents\Projs\Test2\Assets\#com.external.dotween\Editor\DOTweenEditor.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor.UI
{
    public class DOTweenUtilityWindow : EditorWindow
    {
        private static readonly Vector2 _WinSize = new Vector2(370f, 510f);
        private static readonly float _HalfBtSize = (float)((double)DOTweenUtilityWindow._WinSize.x * 0.5 - 6.0);
        private string[] _tabLabels = new string[2]
        {
      "Setup",
      "Preferences"
        };
        private string[] _settingsLocation = new string[3]
        {
      "Assets > Resources",
      "DOTween > Resources",
      "Demigiant > Resources"
        };
        private const string _Title = "DOTween Utility Panel";
        public const string Id = "DOTweenVersion";
        public const string IdPro = "DOTweenProVersion";
        private bool _initialized;
        private DOTweenSettings _src;
        private Texture2D _headerImg;
        private Texture2D _footerImg;
        private Vector2 _headerSize;
        private Vector2 _footerSize;
        private string _innerTitle;
        private bool _setupRequired;
        private int _selectedTab;

        [MenuItem("Tools/Demigiant/DOTween Utility Panel")]
        private static void ShowWindow()
        {
            DOTweenUtilityWindow.Open();
        }

        public static void Open()
        {
            DOTweenUtilityWindow window = EditorWindow.GetWindow<DOTweenUtilityWindow>(true, "DOTween Utility Panel", true);
            window.minSize = DOTweenUtilityWindow._WinSize;
            window.maxSize = DOTweenUtilityWindow._WinSize;
            window.ShowUtility();
            EditorPrefs.SetString("DOTweenVersion", DOTween.Version);
            EditorPrefs.SetString("DOTweenProVersion", EditorUtils.proVersion);
        }

        public static Texture2D LoadImage(Vector2 size, string filePath)
        {
            if (!File.Exists(filePath))
            {
                EditorUtils.StoreDOTweenDirs();
            }
            byte[] bytes = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D((int)size.x, (int)size.y, TextureFormat.RGB24, false);
            texture.filterMode = FilterMode.Trilinear;
            texture.LoadImage(bytes);

            return texture;
        }

        private bool Init()
        {
            if (this._initialized)
                return true;
            if ((UnityEngine.Object)this._headerImg == (UnityEngine.Object)null)
            {
                this._headerImg = LoadImage(new Vector2(512, 164), EditorUtils.dotweenDir + "Imgs/Header.jpg");
                if ((UnityEngine.Object)this._headerImg == (UnityEngine.Object)null)
                    return false;
                this._headerSize.x = DOTweenUtilityWindow._WinSize.x;
                this._headerSize.y = (float)(int)((double)DOTweenUtilityWindow._WinSize.x * (double)this._headerImg.height / (double)this._headerImg.width);
                this._footerImg = LoadImage(new Vector2(194, 24), EditorUtils.dotweenDir + "Imgs/Footer.png");
                this._footerSize.x = DOTweenUtilityWindow._WinSize.x;
                this._footerSize.y = (float)(int)((double)DOTweenUtilityWindow._WinSize.x * (double)this._footerImg.height / (double)this._footerImg.width);
            }
            this._initialized = true;
            return true;
        }

        private void OnHierarchyChange()
        {
            this.Repaint();
        }

        private void OnEnable()
        {
            this._innerTitle = "DOTween v" + DOTween.Version + ("[Release build]");
            this._innerTitle = !EditorUtils.hasPro ? this._innerTitle + "\nDOTweenPro not installed" : this._innerTitle + "\nDOTweenPro v" + EditorUtils.proVersion;
            this.Init();
            this._setupRequired = EditorUtils.DOTweenSetupRequired();
        }

        private void OnDestroy()
        {
            if (!((UnityEngine.Object)this._src != (UnityEngine.Object)null))
                return;
            this._src.modules.showPanel = false;
            EditorUtility.SetDirty((UnityEngine.Object)this._src);
        }

        private void OnGUI()
        {
            if (!this.Init())
            {
                GUILayout.Space(8f);
                GUILayout.Label("Completing import process...");
            }
            else
            {
                this.Connect(false);
                EditorGUIUtils.SetGUIStyles(new Vector2?(this._footerSize));
                if (Application.isPlaying)
                {
                    GUILayout.Space(40f);
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(40f);
                    GUILayout.Label("DOTween Utility Panel\nis disabled while in Play Mode", EditorGUIUtils.wrapCenterLabelStyle, new GUILayoutOption[1]
                    {
            GUILayout.ExpandWidth(true)
                    });
                    GUILayout.Space(40f);
                    GUILayout.EndHorizontal();
                }
                else if (this._src.modules.showPanel)
                {
                    if (DOTweenUtilityWindowModules.Draw((EditorWindow)this, this._src))
                    {
                        this._setupRequired = EditorUtils.DOTweenSetupRequired();
                        this._src.modules.showPanel = false;
                        EditorUtility.SetDirty((UnityEngine.Object)this._src);
                    }
                }
                else
                {
                    this._selectedTab = GUI.Toolbar(new Rect(0.0f, 0.0f, this._headerSize.x, 30f), this._selectedTab, this._tabLabels);
                    if (this._selectedTab == 1)
                    {
                        double labelWidth = (double)EditorGUIUtility.labelWidth;
                        EditorGUIUtility.labelWidth = 160f;
                        this.DrawPreferencesGUI();
                        EditorGUIUtility.labelWidth = (float)labelWidth;
                    }
                    else
                        this.DrawSetupGUI();
                }
                if (!GUI.changed)
                    return;
                EditorUtility.SetDirty((UnityEngine.Object)this._src);
            }
        }

        private void DrawSetupGUI()
        {
            Rect position = new Rect(0.0f, 30f, this._headerSize.x, this._headerSize.y);
            GUI.DrawTexture(position, (Texture)this._headerImg, ScaleMode.StretchToFill, false);
            GUILayout.Space((float)((double)position.y + (double)this._headerSize.y + 2.0));
            GUILayout.Label(this._innerTitle, EditorGUIUtils.boldLabelStyle, new GUILayoutOption[0]);
            if (this._setupRequired)
            {
                GUI.backgroundColor = Color.red;
                GUILayout.BeginVertical(GUI.skin.box, new GUILayoutOption[0]);
                GUILayout.Label("DOTWEEN SETUP REQUIRED", EditorGUIUtils.setupLabelStyle, new GUILayoutOption[0]);
                GUILayout.EndVertical();
                GUI.backgroundColor = Color.white;
            }
            else
                GUILayout.Space(8f);
            GUI.color = Color.green;
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("<b>Setup DOTween...</b>\n(add/remove Modules)", EditorGUIUtils.btSetup, new GUILayoutOption[1]
            {
        GUILayout.Width(200f)
            }))
            {
                DOTweenUtilityWindowModules.ApplyModulesSettings();
                this._src.modules.showPanel = true;
                EditorUtility.SetDirty((UnityEngine.Object)this._src);
                EditorUtils.DeleteLegacyNoModulesDOTweenFiles();
                DOTweenDefines.RemoveAllLegacyDefines();
                EditorUtils.DeleteDOTweenUpgradeManagerFiles();
            }
            else
            {
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUI.color = Color.white;
                GUILayout.Space(4f);
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUI.color = ASMDEFManager.hasModulesASMDEF ? Color.yellow : Color.cyan;
                if (GUILayout.Button(ASMDEFManager.hasModulesASMDEF ? "Remove ASMDEF..." : "Create ASMDEF...", EditorGUIUtils.btSetup, new GUILayoutOption[1]
                {
          GUILayout.Width(200f)
                }))
                {
                    if (ASMDEFManager.hasModulesASMDEF)
                    {
                        if (EditorUtility.DisplayDialog("Remove ASMDEF", string.Format("This will remove the \"DOTween/Modules/DOTween.Modules.asmdef\" and \"DOTweenPro/DOTweenPro.Scripts.asmdef\" (if you have DOTween Pro) files."), "Ok", "Cancel"))
                            ASMDEFManager.RemoveAllASMDEF();
                    }
                    else if (EditorUtility.DisplayDialog("Create ASMDEF", string.Format("This will create the \"DOTween/Modules/DOTween.Modules.asmdef\" and \"DOTweenPro/DOTweenPro.Scripts.asmdef\" (if you have DOTween Pro) files."), "Ok", "Cancel"))
                        ASMDEFManager.CreateAllASMDEF();
                }
                GUI.color = Color.white;
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.Space(8f);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Website", EditorGUIUtils.btBigStyle, new GUILayoutOption[1]
                {
          GUILayout.Width(DOTweenUtilityWindow._HalfBtSize)
                }))
                    Application.OpenURL("http://dotween.demigiant.com/index.php");
                if (GUILayout.Button("Get Started", EditorGUIUtils.btBigStyle, new GUILayoutOption[1]
                {
          GUILayout.Width(DOTweenUtilityWindow._HalfBtSize)
                }))
                    Application.OpenURL("http://dotween.demigiant.com/getstarted.php");
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Documentation", EditorGUIUtils.btBigStyle, new GUILayoutOption[1]
                {
          GUILayout.Width(DOTweenUtilityWindow._HalfBtSize)
                }))
                    Application.OpenURL("http://dotween.demigiant.com/documentation.php");
                if (GUILayout.Button("Support", EditorGUIUtils.btBigStyle, new GUILayoutOption[1]
                {
          GUILayout.Width(DOTweenUtilityWindow._HalfBtSize)
                }))
                    Application.OpenURL("http://dotween.demigiant.com/support.php");
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Changelog", EditorGUIUtils.btBigStyle, new GUILayoutOption[1]
                {
          GUILayout.Width(DOTweenUtilityWindow._HalfBtSize)
                }))
                    Application.OpenURL("http://dotween.demigiant.com/download.php");
                if (GUILayout.Button("Check Updates", EditorGUIUtils.btBigStyle, new GUILayoutOption[1]
                {
          GUILayout.Width(DOTweenUtilityWindow._HalfBtSize)
                }))
                    Application.OpenURL("http://dotween.demigiant.com/download.php?v=" + DOTween.Version);
                GUILayout.EndHorizontal();
                GUILayout.Space(14f);
                if (!GUILayout.Button((Texture)this._footerImg, EditorGUIUtils.btImgStyle, new GUILayoutOption[0]))
                    return;
                Application.OpenURL("http://www.demigiant.com/");
            }
        }

        private void DrawPreferencesGUI()
        {
            GUILayout.Space(40f);
            if (GUILayout.Button("Reset", EditorGUIUtils.btBigStyle, new GUILayoutOption[0]))
            {
                this._src.useSafeMode = true;
                this._src.safeModeOptions.nestedTweenFailureBehaviour = NestedTweenFailureBehaviour.TryToPreserveSequence;
                this._src.showUnityEditorReport = false;
                this._src.timeScale = 1f;
                this._src.useSmoothDeltaTime = false;
                this._src.maxSmoothUnscaledTime = 0.15f;
                this._src.rewindCallbackMode = RewindCallbackMode.FireIfPositionChanged;
                this._src.logBehaviour = LogBehaviour.ErrorsOnly;
                this._src.drawGizmos = true;
                this._src.defaultRecyclable = false;
                this._src.defaultAutoPlay = AutoPlay.All;
                this._src.defaultUpdateType = UpdateType.Normal;
                this._src.defaultTimeScaleIndependent = false;
                this._src.defaultEaseType = Ease.OutQuad;
                this._src.defaultEaseOvershootOrAmplitude = 1.70158f;
                this._src.defaultEasePeriod = 0.0f;
                this._src.defaultAutoKill = true;
                this._src.defaultLoopType = LoopType.Restart;
                EditorUtility.SetDirty((UnityEngine.Object)this._src);
            }
            GUILayout.Space(8f);
            this._src.useSafeMode = EditorGUILayout.Toggle("Safe Mode", this._src.useSafeMode, new GUILayoutOption[0]);
            if (this._src.useSafeMode)
                this._src.safeModeOptions.nestedTweenFailureBehaviour = (NestedTweenFailureBehaviour)EditorGUILayout.EnumPopup(new GUIContent("└ On Nested Tween Failure", "Behaviour in case a tween inside a Sequence fails"), (Enum)this._src.safeModeOptions.nestedTweenFailureBehaviour, new GUILayoutOption[0]);
            this._src.timeScale = EditorGUILayout.FloatField("DOTween's TimeScale", this._src.timeScale, new GUILayoutOption[0]);
            this._src.useSmoothDeltaTime = EditorGUILayout.Toggle("Smooth DeltaTime", this._src.useSmoothDeltaTime, new GUILayoutOption[0]);
            this._src.maxSmoothUnscaledTime = EditorGUILayout.Slider("Max SmoothUnscaledTime", this._src.maxSmoothUnscaledTime, 0.01f, 1f, new GUILayoutOption[0]);
            this._src.rewindCallbackMode = (RewindCallbackMode)EditorGUILayout.EnumPopup("OnRewind Callback Mode", (Enum)this._src.rewindCallbackMode, new GUILayoutOption[0]);
            GUILayout.Space(-5f);
            GUILayout.BeginHorizontal();
            GUILayout.Space(EditorGUIUtility.labelWidth + 4f);
            EditorGUILayout.HelpBox(this._src.rewindCallbackMode == RewindCallbackMode.FireIfPositionChanged ? "When calling Rewind or PlayBackwards/SmoothRewind, OnRewind callbacks will be fired only if the tween isn't already rewinded" : (this._src.rewindCallbackMode == RewindCallbackMode.FireAlwaysWithRewind ? "When calling Rewind, OnRewind callbacks will always be fired, even if the tween is already rewinded." : "When calling Rewind or PlayBackwards/SmoothRewind, OnRewind callbacks will always be fired, even if the tween is already rewinded"), MessageType.None);
            GUILayout.EndHorizontal();
            this._src.showUnityEditorReport = EditorGUILayout.Toggle("Editor Report", this._src.showUnityEditorReport, new GUILayoutOption[0]);
            this._src.logBehaviour = (LogBehaviour)EditorGUILayout.EnumPopup("Log Behaviour", (Enum)this._src.logBehaviour, new GUILayoutOption[0]);
            this._src.drawGizmos = EditorGUILayout.Toggle("Draw Path Gizmos", this._src.drawGizmos, new GUILayoutOption[0]);
            DOTweenSettings.SettingsLocation settingsLocation = this._src.storeSettingsLocation;
            this._src.storeSettingsLocation = (DOTweenSettings.SettingsLocation)EditorGUILayout.Popup("Settings Location", (int)this._src.storeSettingsLocation, this._settingsLocation, new GUILayoutOption[0]);
            if (this._src.storeSettingsLocation != settingsLocation)
            {
                if (this._src.storeSettingsLocation == DOTweenSettings.SettingsLocation.DemigiantDirectory && EditorUtils.demigiantDir == null)
                {
                    EditorUtility.DisplayDialog("Change DOTween Settings Location", "Demigiant directory not present (must be the parent of DOTween's directory)", "Ok");
                    if (settingsLocation == DOTweenSettings.SettingsLocation.DemigiantDirectory)
                    {
                        this._src.storeSettingsLocation = DOTweenSettings.SettingsLocation.AssetsDirectory;
                        this.Connect(true);
                    }
                    else
                        this._src.storeSettingsLocation = settingsLocation;
                }
                else
                    this.Connect(true);
            }
            GUILayout.Space(8f);
            GUILayout.Label("DEFAULTS ▼");
            this._src.defaultRecyclable = EditorGUILayout.Toggle("Recycle Tweens", this._src.defaultRecyclable, new GUILayoutOption[0]);
            this._src.defaultAutoPlay = (AutoPlay)EditorGUILayout.EnumPopup("AutoPlay", (Enum)this._src.defaultAutoPlay, new GUILayoutOption[0]);
            this._src.defaultUpdateType = (UpdateType)EditorGUILayout.EnumPopup("Update Type", (Enum)this._src.defaultUpdateType, new GUILayoutOption[0]);
            this._src.defaultTimeScaleIndependent = EditorGUILayout.Toggle("TimeScale Independent", this._src.defaultTimeScaleIndependent, new GUILayoutOption[0]);
            this._src.defaultEaseType = (Ease)EditorGUILayout.EnumPopup("Ease", (Enum)this._src.defaultEaseType, new GUILayoutOption[0]);
            this._src.defaultEaseOvershootOrAmplitude = EditorGUILayout.FloatField("Ease Overshoot", this._src.defaultEaseOvershootOrAmplitude, new GUILayoutOption[0]);
            this._src.defaultEasePeriod = EditorGUILayout.FloatField("Ease Period", this._src.defaultEasePeriod, new GUILayoutOption[0]);
            this._src.defaultAutoKill = EditorGUILayout.Toggle("AutoKill", this._src.defaultAutoKill, new GUILayoutOption[0]);
            this._src.defaultLoopType = (LoopType)EditorGUILayout.EnumPopup("Loop Type", (Enum)this._src.defaultLoopType, new GUILayoutOption[0]);
        }

        public static DOTweenSettings GetDOTweenSettings()
        {
            return DOTweenUtilityWindow.ConnectToSource((DOTweenSettings)null, false, false);
        }

        private static DOTweenSettings ConnectToSource(DOTweenSettings src, bool createIfMissing, bool fullSetup)
        {
            DOTweenUtilityWindow.LocationData to1 = new DOTweenUtilityWindow.LocationData(EditorUtils.assetsPath + EditorUtils.pathSlash + "Resources");
            DOTweenUtilityWindow.LocationData to2 = new DOTweenUtilityWindow.LocationData(EditorUtils.dotweenDir + "Resources");
            bool flag = EditorUtils.demigiantDir != null;
            DOTweenUtilityWindow.LocationData to3 = flag ? new DOTweenUtilityWindow.LocationData(EditorUtils.demigiantDir + "Resources") : new DOTweenUtilityWindow.LocationData();
            if ((UnityEngine.Object)src == (UnityEngine.Object)null)
            {
                src = EditorUtils.ConnectToSourceAsset<DOTweenSettings>(to1.adbFilePath, false);
                if ((UnityEngine.Object)src == (UnityEngine.Object)null)
                    src = EditorUtils.ConnectToSourceAsset<DOTweenSettings>(to2.adbFilePath, false);
                if ((UnityEngine.Object)src == (UnityEngine.Object)null & flag)
                    src = EditorUtils.ConnectToSourceAsset<DOTweenSettings>(to3.adbFilePath, false);
            }
            if ((UnityEngine.Object)src == (UnityEngine.Object)null)
            {
                if (!createIfMissing)
                    return (DOTweenSettings)null;
                if (!Directory.Exists(to1.dir))
                    AssetDatabase.CreateFolder(to1.adbParentDir, "Resources");
                src = EditorUtils.ConnectToSourceAsset<DOTweenSettings>(to1.adbFilePath, true);
            }
            if (fullSetup)
            {
                switch (src.storeSettingsLocation)
                {
                    case DOTweenSettings.SettingsLocation.AssetsDirectory:
                        src = DOTweenUtilityWindow.MoveSrc(new DOTweenUtilityWindow.LocationData[2]
                        {
              to2,
              to3
                        }, to1);
                        break;
                    case DOTweenSettings.SettingsLocation.DOTweenDirectory:
                        src = DOTweenUtilityWindow.MoveSrc(new DOTweenUtilityWindow.LocationData[2]
                        {
              to1,
              to3
                        }, to2);
                        break;
                    case DOTweenSettings.SettingsLocation.DemigiantDirectory:
                        src = DOTweenUtilityWindow.MoveSrc(new DOTweenUtilityWindow.LocationData[2]
                        {
              to1,
              to2
                        }, to3);
                        break;
                }
            }
            return src;
        }

        private void Connect(bool forceReconnect = false)
        {
            if ((UnityEngine.Object)this._src != (UnityEngine.Object)null && !forceReconnect)
                return;
            this._src = DOTweenUtilityWindow.ConnectToSource(this._src, true, true);
        }

        private static DOTweenSettings MoveSrc(DOTweenUtilityWindow.LocationData[] from, DOTweenUtilityWindow.LocationData to)
        {
            if (!Directory.Exists(to.dir))
                AssetDatabase.CreateFolder(to.adbParentDir, "Resources");
            foreach (DOTweenUtilityWindow.LocationData locationData in from)
            {
                if (File.Exists(locationData.filePath))
                {
                    AssetDatabase.MoveAsset(locationData.adbFilePath, to.adbFilePath);
                    AssetDatabase.DeleteAsset(locationData.adbFilePath);
                    if (Directory.GetDirectories(locationData.dir).Length == 0 && Directory.GetFiles(locationData.dir).Length == 0)
                        AssetDatabase.DeleteAsset(EditorUtils.FullPathToADBPath(locationData.dir));
                }
            }
            return EditorUtils.ConnectToSourceAsset<DOTweenSettings>(to.adbFilePath, true);
        }

        private struct LocationData
        {
            public string dir;
            public string filePath;
            public string adbFilePath;
            public string adbParentDir;

            public LocationData(string srcDir)
            {
                this = new DOTweenUtilityWindow.LocationData();
                this.dir = srcDir;
                this.filePath = this.dir + EditorUtils.pathSlash + "DOTweenSettings.asset";
                this.adbFilePath = EditorUtils.FullPathToADBPath(this.filePath);
                this.adbParentDir = EditorUtils.FullPathToADBPath(this.dir.Substring(0, this.dir.LastIndexOf(EditorUtils.pathSlash)));
            }
        }
    }
}
