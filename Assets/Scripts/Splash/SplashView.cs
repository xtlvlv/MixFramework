using System;
using System.Collections;
using BaseFramework.Core;
using UnityEngine;
using UnityEngine.UI;

public class SplashView : MonoBehaviour
{
    [SerializeField] private Image _logoImg;
    private void Reset()
    {
    }

    private void Awake()
    {
        _logoImg.color = new Color(_logoImg.color.r, _logoImg.color.g, _logoImg.color.b, 0);
    }

    private void Start()
    {
        Log.Warning("开始");
        StartCoroutine(showLogo());
    }

    IEnumerator showLogo()
    {
        float curTime = 0;
        while (curTime<1f)
        {
            _logoImg.color =  new Color(_logoImg.color.r, _logoImg.color.g, _logoImg.color.b, curTime);
            curTime        += Time.deltaTime;
            yield return null;
        }
        _logoImg.color = new Color(_logoImg.color.r, _logoImg.color.g, _logoImg.color.b, 1);
        curTime = 1f;
        while (curTime>0.1f)
        {
            _logoImg.color =  new Color(_logoImg.color.r, _logoImg.color.g, _logoImg.color.b, curTime);
            curTime        -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);        
    }
    
}