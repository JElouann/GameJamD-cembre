using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BandeauUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private List<PathStep> _pathSteps = new();
    private int _index = 0;

    public void UpdateDisplayedText(string content)
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
