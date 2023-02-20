using UnityEngine;

namespace ScriptsHotfix
{
    public class ViewPortAdapter : MonoBehaviour
    {
        public Camera selfCamera;

        public float resultX;
        public float resultY;
        public float resultWidth = 1.0f;
        public float resultHeight = 1.0f;

        private void Reset()
        {
            selfCamera = GetComponent<Camera>();
        }

        void Awake()
        {
            CalculateViewPort();
        }

        void Start()
        {

        }

        //保证高度匹配屏幕高度
        public void CalculateViewPort()
        {
            float actualWidth = Screen.height * (1080.0f / 1920.0f);
            if (actualWidth < Screen.width)
            {
                resultWidth = actualWidth / Screen.width;
                resultX = (1.0f - resultWidth) / 2.0f;

                if (selfCamera != null)
                {
                    selfCamera.rect = new Rect(resultX, resultY, resultWidth, resultHeight);
                }
            }
        }
    }
}