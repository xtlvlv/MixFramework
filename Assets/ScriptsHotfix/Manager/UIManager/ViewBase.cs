using System;
using BaseFramework.Core;
using UnityEngine;

namespace ScriptsHotfix
{
    public class ViewBase: MonoBehaviour
    {
        public Canvas canvas;

        public void BaseReset()
        {
            canvas = gameObject.GetComponent<Canvas>();
        }

        public virtual void OnLoad()
        {
            Log.Info($"Load UI: {this.GetType().Name}");
        }

        public virtual void OnOpen()
        {
            Log.Info($"Open UI: {this.GetType().Name}");
        }

        public virtual void OnClose()
        {
            Log.Info($"Close UI: {this.GetType().Name}");
        }
        
        public virtual void OnUnload()
        {
            Log.Info($"Unload UI: {this.GetType().Name}");
        }
    }
}