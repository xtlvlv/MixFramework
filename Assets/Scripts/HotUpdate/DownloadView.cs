using BaseFramework.Core;
using UnityEngine;
using UnityEngine.UI;

public class DownloadView : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    [SerializeField] private Text progressText;
    [SerializeField] private Text speedText;
    [SerializeField] private Button cancelDownLoad;
    [SerializeField] private Button reDownLoad;

    private void Reset()
    {
        progressSlider = transform.Find("ProgressSlider").GetComponent<Slider>();
        progressText = transform.Find("ProgressValue/Text").GetComponent<Text>();
        speedText = transform.Find("SpeedValue/Text").GetComponent<Text>();
        cancelDownLoad = transform.Find("Cancel").GetComponent<Button>();
        reDownLoad = transform.Find("ReDown").GetComponent<Button>();
    }

    private void Start()
    {
    }

    public void UpdateMessage(string msg)
    {
        progressText.text = msg;
    }

    public void UpdateProgress(float p)
    {
        progressSlider.value = p / 100.0f;
        progressText.text = p.ToString("#0.00") + "%";
    }

    public void UpdateSpeed(float s)
    {
        speedText.text = (s / 1024.0f).ToString("#0.00") + " kb/s";
    }

    // public void OnUpdateFinish()
    // {
    //     Destroy(gameObject);
    // }
}