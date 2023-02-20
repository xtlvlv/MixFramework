using System;
using BaseFramework.Core;
using UnityEngine;

namespace ScriptsHotfix
{
    public class EnemyController: MonoBehaviour
    {
        private bool        _start = false;
        private void Awake()
        {
            EventManager.Instance.Subscribe<LevelStartEvent>(OnLevelStartEvent);
        }

        private void Update()
        {
            if (!_start)
            {
                return;
            }
           
        }

        private void FixedUpdate()
        {
            if (!_start)
            {
                return;
            }
        }

        #region 碰撞

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("TagPlayer"))
            {
                AudioManager.Instance.StopSingleSound();
                AudioManager.Instance.StopMusic();
                AudioManager.Instance.PlaySingleSound("Assets/ResHotfix/MainBundle/Audio/dead.wav");
                EventManager.Instance.Fire<LevelDeadEvent>(new LevelDeadEvent(false));
            }
        }

        #endregion
      

        #region 事件处理

        private void OnLevelStartEvent(LevelStartEvent obj)
        {
            _start = true;
        }

        #endregion
    }
}