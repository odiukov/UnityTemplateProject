// Decompiled with JetBrains decompiler
// Type: DG.DOTweenEditor.UI.EditorGUIUtils
// Assembly: DOTweenEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2794ED2-88CB-4A6A-996E-2EDA424C0531
// Assembly location: C:\Users\taras.lazoriv\Documents\Projs\Test2\Assets\#com.external.dotween\Editor\DOTweenEditor.dll

using DG.Tweening;
using System;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor.UI
{
    public static class EditorGUIUtils
    {
        internal static readonly string[] FilteredEaseTypes = new string[32]
        {
      "Linear",
      "InSine",
      "OutSine",
      "InOutSine",
      "InQuad",
      "OutQuad",
      "InOutQuad",
      "InCubic",
      "OutCubic",
      "InOutCubic",
      "InQuart",
      "OutQuart",
      "InOutQuart",
      "InQuint",
      "OutQuint",
      "InOutQuint",
      "InExpo",
      "OutExpo",
      "InOutExpo",
      "InCirc",
      "OutCirc",
      "InOutCirc",
      "InElastic",
      "OutElastic",
      "InOutElastic",
      "InBack",
      "OutBack",
      "InOutBack",
      "InBounce",
      "OutBounce",
      "InOutBounce",
      ":: AnimationCurve"
        };
        private static bool _stylesSet;
        private static bool _additionalStylesSet;
        public static GUIStyle boldLabelStyle;
        public static GUIStyle setupLabelStyle;
        public static GUIStyle redLabelStyle;
        public static GUIStyle btBigStyle;
        public static GUIStyle btSetup;
        public static GUIStyle btImgStyle;
        public static GUIStyle wrapCenterLabelStyle;
        public static GUIStyle handlelabelStyle;
        public static GUIStyle handleSelectedLabelStyle;
        public static GUIStyle wordWrapLabelStyle;
        public static GUIStyle wordWrapRichTextLabelStyle;
        public static GUIStyle wordWrapItalicLabelStyle;
        public static GUIStyle titleStyle;
        public static GUIStyle logoIconStyle;
        public static GUIStyle sideBtStyle;
        public static GUIStyle sideLogoIconBoldLabelStyle;
        public static GUIStyle wordWrapTextArea;
        public static GUIStyle popupButton;
        public static GUIStyle btIconStyle;
        public static GUIStyle infoboxStyle;
        private static Texture2D _logo;

        public static Texture2D logo
        {
            get
            {
                if ((UnityEngine.Object)EditorGUIUtils._logo == (UnityEngine.Object)null)
                {
                    EditorGUIUtils._logo = DOTweenUtilityWindow.LoadImage(new Vector2(93, 18), EditorUtils.dotweenDir + "Imgs/DOTweenIcon.png");
                }
                return EditorGUIUtils._logo;
            }
        }

        public static Ease FilteredEasePopup(Ease currEase)
        {
            int selectedIndex = currEase == Ease.INTERNAL_Custom ? EditorGUIUtils.FilteredEaseTypes.Length - 1 : Array.IndexOf<string>(EditorGUIUtils.FilteredEaseTypes, currEase.ToString());
            if (selectedIndex == -1)
                selectedIndex = 0;
            int index = EditorGUILayout.Popup("Ease", selectedIndex, EditorGUIUtils.FilteredEaseTypes, new GUILayoutOption[0]);
            if (index != EditorGUIUtils.FilteredEaseTypes.Length - 1)
                return (Ease)Enum.Parse(typeof(Ease), EditorGUIUtils.FilteredEaseTypes[index]);
            return Ease.INTERNAL_Custom;
        }

        public static void InspectorLogo()
        {
            GUILayout.Box((Texture)EditorGUIUtils.logo, EditorGUIUtils.logoIconStyle, new GUILayoutOption[0]);
        }

        public static bool ToggleButton(bool toggled, GUIContent content, GUIStyle guiStyle = null, params GUILayoutOption[] options)
        {
            Color backgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = toggled ? Color.green : Color.white;
            if ((guiStyle == null ? (GUILayout.Button(content, options) ? 1 : 0) : (GUILayout.Button(content, guiStyle, options) ? 1 : 0)) != 0)
            {
                toggled = !toggled;
                GUI.changed = true;
            }
            GUI.backgroundColor = backgroundColor;
            return toggled;
        }

        public static void SetGUIStyles(Vector2? footerSize = null)
        {
            if (!EditorGUIUtils._additionalStylesSet && footerSize.HasValue)
            {
                EditorGUIUtils._additionalStylesSet = true;
                Vector2 vector2 = footerSize.Value;
                EditorGUIUtils.btImgStyle = new GUIStyle(GUI.skin.button);
                EditorGUIUtils.btImgStyle.normal.background = (Texture2D)null;
                EditorGUIUtils.btImgStyle.imagePosition = ImagePosition.ImageOnly;
                EditorGUIUtils.btImgStyle.padding = new RectOffset(0, 0, 0, 0);
                EditorGUIUtils.btImgStyle.fixedWidth = vector2.x;
                EditorGUIUtils.btImgStyle.fixedHeight = vector2.y;
            }
            if (EditorGUIUtils._stylesSet)
                return;
            EditorGUIUtils._stylesSet = true;
            EditorGUIUtils.boldLabelStyle = new GUIStyle(GUI.skin.label);
            EditorGUIUtils.boldLabelStyle.fontStyle = FontStyle.Bold;
            EditorGUIUtils.redLabelStyle = new GUIStyle(GUI.skin.label);
            EditorGUIUtils.redLabelStyle.normal.textColor = Color.red;
            EditorGUIUtils.setupLabelStyle = new GUIStyle(EditorGUIUtils.boldLabelStyle);
            EditorGUIUtils.setupLabelStyle.alignment = TextAnchor.MiddleCenter;
            EditorGUIUtils.wrapCenterLabelStyle = new GUIStyle(GUI.skin.label);
            EditorGUIUtils.wrapCenterLabelStyle.wordWrap = true;
            EditorGUIUtils.wrapCenterLabelStyle.alignment = TextAnchor.MiddleCenter;
            EditorGUIUtils.btBigStyle = new GUIStyle(GUI.skin.button);
            EditorGUIUtils.btBigStyle.padding = new RectOffset(0, 0, 10, 10);
            EditorGUIUtils.btSetup = new GUIStyle(EditorGUIUtils.btBigStyle);
            EditorGUIUtils.btSetup.padding = new RectOffset(10, 10, 6, 6);
            EditorGUIUtils.btSetup.wordWrap = true;
            EditorGUIUtils.btSetup.richText = true;
            EditorGUIUtils.titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 12,
                fontStyle = FontStyle.Bold
            };
            EditorGUIUtils.handlelabelStyle = new GUIStyle(GUI.skin.label)
            {
                normal = {
          textColor = Color.white
        },
                alignment = TextAnchor.MiddleLeft
            };
            EditorGUIUtils.handleSelectedLabelStyle = new GUIStyle(EditorGUIUtils.handlelabelStyle)
            {
                normal = {
          textColor = Color.yellow
        },
                fontStyle = FontStyle.Bold
            };
            EditorGUIUtils.wordWrapLabelStyle = new GUIStyle(GUI.skin.label);
            EditorGUIUtils.wordWrapLabelStyle.wordWrap = true;
            EditorGUIUtils.wordWrapRichTextLabelStyle = new GUIStyle(GUI.skin.label);
            EditorGUIUtils.wordWrapRichTextLabelStyle.wordWrap = true;
            EditorGUIUtils.wordWrapRichTextLabelStyle.richText = true;
            EditorGUIUtils.wordWrapItalicLabelStyle = new GUIStyle(EditorGUIUtils.wordWrapLabelStyle);
            EditorGUIUtils.wordWrapItalicLabelStyle.fontStyle = FontStyle.Italic;
            EditorGUIUtils.logoIconStyle = new GUIStyle(GUI.skin.box);
            EditorGUIUtils.logoIconStyle.active.background = EditorGUIUtils.logoIconStyle.normal.background = (Texture2D)null;
            EditorGUIUtils.logoIconStyle.margin = new RectOffset(0, 0, 0, 0);
            EditorGUIUtils.logoIconStyle.padding = new RectOffset(0, 0, 0, 0);
            EditorGUIUtils.sideBtStyle = new GUIStyle(GUI.skin.button);
            EditorGUIUtils.sideBtStyle.margin.top = 1;
            EditorGUIUtils.sideBtStyle.padding = new RectOffset(0, 0, 2, 2);
            EditorGUIUtils.sideLogoIconBoldLabelStyle = new GUIStyle(EditorGUIUtils.boldLabelStyle);
            EditorGUIUtils.sideLogoIconBoldLabelStyle.alignment = TextAnchor.MiddleLeft;
            EditorGUIUtils.sideLogoIconBoldLabelStyle.padding.top = 2;
            EditorGUIUtils.wordWrapTextArea = new GUIStyle(GUI.skin.textArea);
            EditorGUIUtils.wordWrapTextArea.wordWrap = true;
            EditorGUIUtils.popupButton = new GUIStyle(EditorStyles.popup);
            EditorGUIUtils.popupButton.fixedHeight = 18f;
            ++EditorGUIUtils.popupButton.margin.top;
            EditorGUIUtils.btIconStyle = new GUIStyle(GUI.skin.button);
            EditorGUIUtils.btIconStyle.padding.left -= 2;
            EditorGUIUtils.btIconStyle.fixedWidth = 24f;
            EditorGUIUtils.btIconStyle.stretchWidth = false;
            EditorGUIUtils.infoboxStyle = new GUIStyle(GUI.skin.box)
            {
                alignment = TextAnchor.UpperLeft,
                richText = true,
                wordWrap = true,
                padding = new RectOffset(5, 5, 5, 6),
                normal = {
          textColor = Color.white,
          background = Texture2D.whiteTexture
        }
            };
        }
    }
}
