using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSlide : MonoBehaviour
{
    [SerializeField]
    private List<CinemachineVirtualCamera> _slides = new List<CinemachineVirtualCamera>();

    [SerializeField]
    private GameObject _canvas;

    public void CamSlideForVote()
    {
        StartCoroutine(DelayBeforeSlide());
    }

    IEnumerator DelayBeforeSlide()
    {
        _canvas.SetActive(false);
        _slides[0].enabled = true; //J1
        yield return new WaitForSeconds(1.5f);
        _slides[1].enabled = true; //J2
        _slides[0].enabled = false; //J1
        yield return new WaitForSeconds(1.5f);
        _slides[2].enabled = true; //J3
        _slides[1].enabled = false; //J2
        yield return new WaitForSeconds(1.5f);
        _slides[3].enabled = true; //J4
        _slides[2].enabled = false; //J3
        yield return new WaitForSeconds(1.5f);
        _slides[4].enabled = true; //J5
        _slides[3].enabled = false; //J4
        yield return new WaitForSeconds(1.5f);
        _slides[5].enabled = true; //PM
        _slides[4].enabled = false; //J5
        _canvas.SetActive(true);
        yield return new WaitForSeconds(1.5f);
    }
}
