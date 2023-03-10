using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class BackgroundAdapter : MonoBehaviour
{
    private CanvasScaler _scaler;

    private void Start()
    {
        UpdateScale();
    }

    // 在不改变原图比例的情况下缩放。
    private void UpdateScale()
    {
        if (_scaler == null) _scaler = GetComponentInParent<CanvasScaler>();

        var resolution = _scaler.referenceResolution;
        var rt = _scaler.transform as RectTransform;
        if (rt == null) return;
        var screenSize = rt.sizeDelta;
        var factor = Mathf.Max(screenSize.x / resolution.x, screenSize.y / resolution.y);
        var scale = Vector3.one * factor;
        transform.localScale = scale;
    }

    [Conditional("UNITY_EDITOR")]
    private void Update()
    {
        UpdateScale();
    }
}