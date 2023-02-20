using System;
using System.Collections;
using System.Collections.Generic;
using BaseFramework.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ScriptsHotfix
{
    public class PlayerController: MonoBehaviour
    {
        public Vector3 Dir;
        public float   Speed;
        public float   ClickSpeed =3f;
        public float   PressSpeed =5f;

        public float   ClickXAngle=0.5f;
        public float   PressXAngle= 1f;
        
        private Rigidbody2D _rigidbody2D;
        private Vector3     _dirNormal;
        private bool        _start = false;
        private float       _lastAddTime = 0f;

        private bool _isPress    = false;
        private int  _pressFrame = 0;
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _dirNormal   = Dir.normalized;
            
            EventManager.Instance.Subscribe<LevelStartEvent>(OnLevelStartEvent);
            EventManager.Instance.Subscribe<LevelDeadEvent>(OnLevelDeadEvent);
        }

        private void Start()
        {
            Dir.x = ClickXAngle;
        }

        private void Update()
        {
            if (CheckGuiRaycastObjects())
            {
                return;
            }
            if (!_start)
            {
                return;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.Instance.PlaySingleSound("Assets/ResHotfix/MainBundle/Audio/ski.wav");
                Dir.x = Dir.x > 0 ? -ClickXAngle : ClickXAngle;
                Log.Warning("down");
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                Log.Warning("up");
                _pressFrame = 0;
                Dir.x       = Dir.x > 0 ? ClickXAngle : -ClickXAngle;
                AudioManager.Instance.StopSingleSound();
                Speed = ClickSpeed;
            }

            if (Input.GetMouseButton(0))
            {
                _pressFrame++;
                if (_pressFrame>5) // 固定60帧
                {
                    Speed = PressSpeed;
                    Log.Warning("press");
                    AudioManager.Instance.PlaySingleSoundNotStop("Assets/ResHotfix/MainBundle/Audio/ski.wav");
                    Dir.x = Dir.x > 0 ? PressXAngle : -PressXAngle;
                }
            }

            if (Time.realtimeSinceStartup- _lastAddTime>1f)
            {
                _lastAddTime = Time.realtimeSinceStartup;
                MainCtrl.Instance.AddScore();
            }
        }
        
        private void FixedUpdate()
        {
            if (!_start)
            {
                return;
            }
            _rigidbody2D.MovePosition(transform.position + Dir * Time.deltaTime * Speed);


            float hasWalkPercent = (5 - transform.position.y)/35f;
            EventManager.Instance.Fire<LevelWalkEvent>(new LevelWalkEvent(hasWalkPercent));            
        }

        private void OnDrawGizmos()
        {
            GizmosExtensions.DrawArrow(transform.position, transform.position+Dir);
        }
        
        bool CheckGuiRaycastObjects()
        {
            // PointerEventData eventData = new PointerEventData(Main.Instance.eventSystem);
 
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.pressPosition = Input.mousePosition;
            eventData.position      = Input.mousePosition;
 
            List<RaycastResult> list = new List<RaycastResult>();
            // Main.Instance.graphicRaycaster.Raycast(eventData, list);
            EventSystem.current.RaycastAll(eventData, list); // 看有没有UI 事件
            //Debug.Log(list.Count);
            return list.Count > 0;
        }

        #region 事件处理

        private void OnLevelStartEvent(LevelStartEvent obj)
        {
            _start = true;
            BulletTimeManager.Instance.Unpause();
        }
        
        private void OnLevelDeadEvent(LevelDeadEvent obj)
        {
            _start = false;
            if (!obj.Win)
            {
                BulletTimeManager.Instance.Pause();
            }
            else
            {
                CameraFollow.Instance.SetTarget(null);
            }
        }

        #endregion
    }
}