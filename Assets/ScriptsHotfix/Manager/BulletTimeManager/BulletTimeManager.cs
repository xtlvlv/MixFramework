using System;
using System.Collections;
using System.Collections.Generic;
using BaseFramework.Core;
using ScriptsHotfix;
using UnityEngine;

namespace ScriptsHotfix
{
    public class BulletTimeManager : Singleton<BulletTimeManager>, ISingletonAwake
    {
        private float _bulletTimeScale = 0.1f;

        private float _timeScaleBeforePause;
        private float _defaultFixedDeltaTime;
        private float _t;

        public void Awake()
        {
            _defaultFixedDeltaTime = Time.fixedDeltaTime;
            _timeScaleBeforePause = Time.timeScale;
        }

        public void SetBulletTime(float a_bulletTimeScale)
        {
            _bulletTimeScale = a_bulletTimeScale;
            Time.timeScale = _bulletTimeScale;
            Time.fixedDeltaTime = _defaultFixedDeltaTime * Time.timeScale;
        }
        public void Pause()
        {
            _timeScaleBeforePause = Time.timeScale;
            Time.timeScale = 0f;
        }
        
        public void Unpause()
        {
            Time.timeScale = _timeScaleBeforePause;
        }
        
        public void BulletTime(float duration)
        {
            Time.timeScale = _bulletTimeScale;
            // StartCoroutine(SlowOutCoroutine(duration));
            SingleObject<GameApp>.Instance.GameStartCoroutine(SlowOutCoroutine(duration));
        }
        
        IEnumerator SlowOutCoroutine(float duration)
        {
            _t = 0f;
            while (_t < 1f)
            {
                _t += Time.unscaledDeltaTime / duration;
                Time.timeScale = Mathf.Lerp(_bulletTimeScale, 1f, _t);
                Time.fixedDeltaTime = _defaultFixedDeltaTime * Time.timeScale;
                yield return null;
            }
        }
    }
}

