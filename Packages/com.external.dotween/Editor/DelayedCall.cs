// Decompiled with JetBrains decompiler
// Type: DG.DOTweenEditor.DelayedCall
// Assembly: DOTweenEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2794ED2-88CB-4A6A-996E-2EDA424C0531
// Assembly location: C:\Users\taras.lazoriv\Documents\Projs\Test2\Assets\#com.external.dotween\Editor\DOTweenEditor.dll

using System;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor
{
    public class DelayedCall
    {
        public float delay;
        public Action callback;
        private float _startupTime;

        public DelayedCall(float delay, Action callback)
        {
            this.delay = delay;
            this.callback = callback;
            this._startupTime = Time.realtimeSinceStartup;
            EditorApplication.update += new EditorApplication.CallbackFunction(this.Update);
        }

        private void Update()
        {
            if ((double)Time.realtimeSinceStartup - (double)this._startupTime < (double)this.delay)
                return;
            if (EditorApplication.update != null)
                EditorApplication.update -= new EditorApplication.CallbackFunction(this.Update);
            if (this.callback == null)
                return;
            this.callback();
        }
    }
}
