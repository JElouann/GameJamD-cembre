using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector3 _scaleOnHover;
    [SerializeField] private float _scaleTimeOnHover;
    [SerializeField] private float _scaleTimeOnLeave;
    private Vector3 _baseScale;
    private bool _canReScale = true;

    private void Start()
    {
        _baseScale = this.transform.localScale;    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_canReScale) return;
        this.transform.DOScale(_scaleOnHover, _scaleTimeOnHover);
        print("enter " + gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ScaleTimer();
        this.transform.DOScale(_baseScale, _scaleTimeOnLeave);
        print("exit " + gameObject.name);
    }

    private async void ScaleTimer()
    {
        _canReScale = false;
        await Task.Delay(200);
        _canReScale = true;
    }
}
