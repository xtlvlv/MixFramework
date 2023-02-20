using System;
using BaseFramework.Core;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptsHotfix
{
    public class MainView : ViewBase
    {
        [SerializeField] private Button _musicBtn;
        [SerializeField] private Image  _musicImg;
        [SerializeField] private Sprite _soundOn;
        [SerializeField] private Sprite _soundOff;

        [SerializeField] private Button     _startBtn;
        [SerializeField] private GameObject _guideObj;
        [SerializeField] private Text       _scoreText;
        [SerializeField] private Slider       _scoreSlider;


        private MainData _data;
        private int      _lastScore;
        private void Reset()
        {
            BaseReset();
            _startBtn = transform.Find("Bg").GetComponent<Button>();
        }

        public override void OnLoad()
        {
            base.OnLoad();
            _startBtn.onClick.AddListener((() =>
            {
                _guideObj.SetActive(false);
                EventManager.Instance.Fire<LevelStartEvent>();
                _startBtn.gameObject.SetActive(false);
            }));
            
            _musicBtn.onClick.AddListener((() =>
            {
                if (AudioManager.Instance.Mute)
                {
                    AudioManager.Instance.Mute = false;
                    _musicImg.sprite           = _soundOn;
                }
                else
                {
                    AudioManager.Instance.Mute = true;
                    _musicImg.sprite           = _soundOff;
                }
            }));
            
            EventManager.Instance.Subscribe<LevelDeadEvent>((OnLevelDeadEvent));
            EventManager.Instance.Subscribe<LevelWalkEvent>((OnLevelWalkEvent));

        }

        public override void OnOpen()
        {
            base.OnOpen();
            _guideObj.SetActive(true);
            _startBtn.gameObject.SetActive(true);
            GetComponent<Animator>().Play("show");
            _lastScore      = 0;
            _scoreText.text = _lastScore.ToString();
        }

        private void Update()
        {
            if (_data==null)
            {
                return;
            }
            if (_lastScore<_data.Score)
            {
                _lastScore++;
                _scoreText.text = _lastScore.ToString();
            }
            else if (_lastScore>_data.Score)
            {
                _lastScore--;
                _scoreText.text = _lastScore.ToString();
            }
        }

        #region 数据

        public void SetData(MainData a_data)
        {
            _data = a_data;
        }
        

        #endregion

        #region 事件

        private void OnLevelDeadEvent(LevelDeadEvent obj)
        {
            if (obj.Win)
            {
                _scoreSlider.value = 1;
            }
            _startBtn.gameObject.SetActive(true);
            AdRegainCtrl.Instance.ShowAdView(obj.Win);
        }
        
        private void OnLevelWalkEvent(LevelWalkEvent obj)
        {
            _scoreSlider.value = obj.Percent;
        }
        
        #endregion

    }
}