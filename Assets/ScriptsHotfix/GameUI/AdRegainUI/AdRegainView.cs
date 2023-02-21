using BaseFramework.Core;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace ScriptsHotfix
{
    public class AdRegainView : ViewBase
    {
    
        [SerializeField] private Button _regainBtn; // 复活  用于死亡后复活
        [SerializeField] private Button _resetBtn;  // 重来  用于通关后重来
        [SerializeField] private Button _adBtn;  // 广告

        private bool _winRes;
        
        private void Reset()
        {
            BaseReset();
        }

        public override void OnLoad()
        {
            base.OnLoad();
            _regainBtn.onClick.AddListener((() =>
            {
                GameWorld.Instance.Regain();
            }));
            
            _resetBtn.onClick.AddListener((() =>
            {
                GameWorld.Instance.Reset();
            }));
            _adBtn.onClick.AddListener((() =>
            {
                Log.Warning("ad");
                InterstitialAdExample.Instance.LoadAd();
                InterstitialAdExample.Instance.ShowAd();
            }));
        }
        void HandleShowResult(ShowResult result)
        {
            if (result == ShowResult.Finished)
            {
                // 用户成功观看完整的广告
            }
            else if (result == ShowResult.Skipped)
            {
                // 用户跳过了广告
            }
            else if (result == ShowResult.Failed)
            {
                // 广告展示失败
            }
        }

        
      

        #region 数据

        public void SetData(bool a_win)
        {
            _winRes = a_win;
        }

        public void UpdateView()
        {
            if (_winRes)
            {
                _regainBtn.gameObject.SetActive(false);
                _resetBtn.gameObject.SetActive(true);
            }
            else
            {
                _regainBtn.gameObject.SetActive(true);
                _resetBtn.gameObject.SetActive(false);
            }
        }

        #endregion

        #region 事件

        
        #endregion

    }
}