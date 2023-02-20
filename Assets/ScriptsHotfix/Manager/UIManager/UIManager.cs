using System;
using System.Collections.Generic;
using BaseFramework;
using BaseFramework.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ScriptsHotfix
{
    public class UIManager : Singleton<UIManager>, ISingletonAwake
    {
        public Camera m_UICamera;
        private GameObject _uiRootGo;
        
        private Dictionary<int, ViewBase>   _uiCacheDic;
        private Dictionary<int, ViewConfig> _viewConfigDic;
        
        public void Awake()
        {
            _uiCacheDic    = new Dictionary<int, ViewBase>();
            _viewConfigDic = new Dictionary<int, ViewConfig>();

            var configs = EDTable_ViewConfig.GetAllItem();
            foreach (var config in configs)
            {
                var viewConfig = new ViewConfig();
                viewConfig.ViewId       = config.Value.ID;
                viewConfig.SortingOrder = config.Value.SortingOrder;
                viewConfig.PrefabPath   = config.Value.PrefabPath;
                _viewConfigDic.Add(config.Value.ID, viewConfig);
            }
            
            _uiRootGo = GameObject.Find("UIRoot");
            if (_uiRootGo==null)
            {
                _uiRootGo = new GameObject("UIRoot");
                _uiRootGo.transform.LocalReset();

            }
            Object.DontDestroyOnLoad(_uiRootGo);
            
            m_UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
            if (m_UICamera==null)
            {
                Log.Error("Without ui camera!");
            }
            Object.DontDestroyOnLoad(m_UICamera);
        }

        // public void AddConfig(ViewId viewId, ViewConfig a_viewConfig)
        // {
        //     if (_uiPathConfig.ContainsKey(viewId))
        //     {
        //         Log.Warning("exist view, viewId={0}", viewId);
        //     }
        //     else
        //     {
        //         _uiPathConfig.Add(viewId, a_viewConfig);
        //     }
        // }

        public ViewBase OpenUI(int viewId)
        {
            if (_uiCacheDic.TryGetValue(viewId, out var uiView))
            {
                uiView.gameObject.SetActive(true);
            }
            else
            {
                if (_viewConfigDic.TryGetValue(viewId, out var config))
                {
                    GameObject uiGo = Object.Instantiate(AssetMgr.Load<GameObject>(config.PrefabPath), _uiRootGo.transform);
                    uiView = uiGo.GetComponent<ViewBase>();
                    uiView.transform.SetParent(_uiRootGo.transform, false);
                    uiView.transform.LocalReset();
                    Canvas canvas = uiGo.GetComponent<Canvas>();
                    canvas.worldCamera  = m_UICamera;
                    canvas.sortingOrder = config.SortingOrder;
                    _uiCacheDic.Add(viewId, uiView);
                    uiView.OnLoad();
                }
                else
                {
                    Log.Error("Without this UI in config, ViewId={0}", viewId);
                }
            }

            if (uiView!=null)
            {
                uiView.OnOpen();
            }

            return uiView;
        }


        public void CloseUI(int viewId)
        {
            if (_uiCacheDic.TryGetValue(viewId, out var uiView))
            {
                uiView.OnClose();
                uiView.gameObject.SetActive(false);
            }
        }

        // 锁定UI 不让操作
        public void LockUI()
        {
        }

        public void UnlockUI()
        {
            
        }

        
    }
}