using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BandeauUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private List<PathStep> _pathSteps = new();
    private int _index = 0;

    // Singleton
    #region Singleton
    private static BandeauUI _instance;

    public static BandeauUI Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("ça n'arrivera pas");
                _instance = go.AddComponent<BandeauUI>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            Debug.Log($"<b><color=#{UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 0f, 1f, 1f).ToHexString()}>{this.GetType()}</color> instance <color=#eb624d>destroyed</color></b>");
        }
        else
        {
            _instance = this;
            Debug.Log($"<b><color=#{UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f, 1f, 1f).ToHexString()}>{this.GetType()}</color> instance <color=#58ed7d>created</color></b>");
        }
    }
    #endregion

    public void ShowText(string content)
    {
        _text.text = content;
        Reset();
        Launch();
    }

    public void Reset()
    {
        this.transform.position = _pathSteps[0].Transform.position;
        _index = 0;
    }

    public void Launch()
    {
        StartCoroutine(Travel());
    }

    public IEnumerator Travel()
    {
        if (_index >= _pathSteps.Count) yield break;
        float duration = _pathSteps[_index].Time;
        this.transform.DOMove(_pathSteps[_index].Transform.position, duration).SetEase(_pathSteps[_index].Ease).onComplete = Launch;
        _index++;
    }
}
