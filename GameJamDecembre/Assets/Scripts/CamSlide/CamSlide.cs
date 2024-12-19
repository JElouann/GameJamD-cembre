using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CamSlide : MonoBehaviour
{
    [SerializeField]
    private List<CinemachineVirtualCamera> _slides = new List<CinemachineVirtualCamera>();

    [SerializeField]
    private GameObject _canvas;

    [SerializeField]
    private Negociation _negociation;

    [SerializeField]
    private bool _finish;

    public List<bool> _playerVote = new List<bool>();

    public static CamSlide Instance;

    private void Awake()
    {
        Instance = this;
    }

    public async void CamSlideForVote()
    {
        _negociation.CanNegociation = false;
        _negociation.NombreDePointMax = 0;
        _playerVote.Clear();
        _finish = false;
        _canvas.SetActive(false);
        _slides[0].enabled = true; //J1

        await Vote();
        await Task.Delay(500);

        _slides[1].enabled = true; //J2
        _slides[0].enabled = false; //J1

        await Vote();
        await Task.Delay(500);

        _slides[2].enabled = true; //J3
        _slides[1].enabled = false; //J2

        await Vote();
        await Task.Delay(500);

        _slides[3].enabled = true; //J4
        _slides[2].enabled = false; //J3

        await Vote();
        await Task.Delay(500);

        _slides[4].enabled = true; //J5
        _slides[3].enabled = false; //J4

        await Vote();
        await Task.Delay(500);

        _slides[5].enabled = true; //PM
        _slides[4].enabled = false; //J5
        _canvas.SetActive(true);
        _finish = true;
        await Task.Delay(1000);
        ScoreManager.Instance.AddPuntos();
    }

    public async Task Vote()
    {
        while (true)
        {
            if(Input.GetMouseButtonDown(0)) //gauche
            {
                _playerVote.Add(true);
                return;
            }
            if (Input.GetMouseButtonDown(1)) //droite
            {
                _playerVote.Add(false);
                return;
            }
            await Task.Yield();
        }
    }
}
