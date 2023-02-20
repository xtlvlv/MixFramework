using BaseFramework.Core;
using UnityEngine;

namespace ScriptsHotfix
{
    public class MainData
    {
        public int Score = 0;     // 分数
        public int Distance;      // 距离终点距离
        public int TotalDistance; // 总距离
    }
    public class MainCtrl : SingleClass<MainCtrl>
    {
        private MainView _view;
        private MainData _data;
        public MainCtrl()
        {
            _data = new MainData();
        }
        
        public void ShowMainView()
        {
            _view = UIManager.Instance.OpenUI(ViewId.MainUI) as MainView;
            if (_view!=null)
            {
                _view.SetData(_data);
            }
        }
        public void RemoveMainView()
        {
            UIManager.Instance.CloseUI(ViewId.MainUI);
        }

        public void AddScore()
        {
            _data.Score += 10;
        }

        public void ResetData()
        {
            _data.Score    = 0;
            _data.Distance = 0;
        }

    }
}
