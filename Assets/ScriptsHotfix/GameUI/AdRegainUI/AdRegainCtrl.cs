using BaseFramework.Core;
using UnityEngine;

namespace ScriptsHotfix
{
    public class AdRegainCtrl : SingleClass<AdRegainCtrl>
    {
        private AdRegainView _view;

        public AdRegainCtrl()
        {
        }
        
        public void ShowAdView(bool a_win)
        {
            _view = UIManager.Instance.OpenUI(ViewId.AdRegainUI) as AdRegainView;
            if (_view!=null)
            {
                _view.SetData(a_win);
                _view.UpdateView();
            }
        }
        public void RemoveAdView()
        {
            UIManager.Instance.CloseUI(ViewId.AdRegainUI);
        }
    }
}
