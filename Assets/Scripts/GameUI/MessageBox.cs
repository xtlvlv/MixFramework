
using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class MessageBox
{
    private static readonly GameObject _prefab = Resources.Load<GameObject>("MessageBox");
    private Text _content;
    private Text _textNo;
    private Text _textOk;
    private Text _title;

    private GameObject gameObject { get; set; }
    
    public static MessageBox Show(string title, string content,Action onOkClick, Action onNoClick, string ok = "确定", string no = "取消")
    {
        return new MessageBox(title, content,onOkClick,onNoClick, ok, no);
    }

    private MessageBox(string title, string content, Action onOkClick, Action onNoClick, string ok, string no)
    {
        gameObject = Object.Instantiate(_prefab);
        gameObject.name = title;

        _title = GetComponent<Text>("Title");
        _content = GetComponent<Text>("Content/Text");
        _textOk = GetComponent<Text>("Buttons/Ok/Text");
        _textNo = GetComponent<Text>("Buttons/No/Text");

        var ok1 = GetComponent<Button>("Buttons/Ok");
        var no1 = GetComponent<Button>("Buttons/No");
        ok1.onClick.AddListener(() =>
        {
            onOkClick?.Invoke();

            Object.DestroyImmediate(gameObject);
        });
        no1.onClick.AddListener(() =>
        {
            onNoClick?.Invoke();
            Object.DestroyImmediate(gameObject);
        });

        Init(title, content, ok, no);
    }

    private void Init(string title, string content, string ok, string no)
    {
        _title.text = title;
        _content.text = content;
        _textOk.text = ok;
        _textNo.text = no;
    }

    private T GetComponent<T>(string path) where T : Component
    {
        var trans = gameObject.transform.Find(path);
        return trans.GetComponent<T>();
    }

}