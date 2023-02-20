using BaseFramework.Core;
using UnityEngine;

namespace BaseFramework
{
    // 挂在主摄像机上
    public class CameraFollow: SingleObject<CameraFollow>
    {
        [SerializeField] private Transform _target;

        public                   float     smoothSpeed = 0.125f;
        public                   Vector3   offset;
        [SerializeField] private Transform _mainCamera;
        [SerializeField] private Transform _uiCamera;
        // [SerializeField] private Transform _mapCamera;


        public void SetTarget(Transform target, Transform uiCamera, Transform mapCamera)
        {
            _target     = target;
            _uiCamera   = uiCamera;
            _mainCamera = transform;
        }
        
        public void SetTarget(Transform target)
        {
            _target     = target;
        }

        void FixedUpdate ()
        {
            if (_target != null)
            {
                offset.x = 0;
                // Vector3 desiredPosition = _target.position + offset;
                Vector3 desiredPosition = new Vector3(transform.position.x, _target.position.y, -10);

                Vector3 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed);
                smoothedPosition.z   = -10;
                _mainCamera.position = smoothedPosition;
                _uiCamera.position   = smoothedPosition;
                // _mainCamera.position = desiredPosition;
                // _uiCamera.position   = desiredPosition;
            }
        }
    }
}