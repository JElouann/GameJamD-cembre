using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LePetitMouvementDuHautVersLeBasPourLUI : MonoBehaviour
{
    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _endPos;

    [SerializeField] private float _speed;

    public bool IsOut;

    private void Start()
    {
        //_startPos = transform.position;
    }

    // arnaque
    private void FalseActiveSet()
    {
        this.gameObject.SetActive(false);
    }

    public void MoveUp() 
    {
        IsOut = false;
        this.transform.DOMoveY(_startPos.position.y, _speed).onComplete = FalseActiveSet;
    }

    public void MoveDown()
    {
        IsOut = true;
        this.gameObject.SetActive(true);
        this.transform.DOMoveY(_endPos.position.y, _speed);
    }
}
