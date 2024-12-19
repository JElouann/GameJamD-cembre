using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatsGraphic : MonoBehaviour
{
    [SerializeField] private Image _party1;
    [SerializeField] private Image _party2;
    [SerializeField] private Image _party3;
    [SerializeField] private Image _party4;
    [SerializeField] private Image _party5;

    // Singleton
    #region Singleton
    private static StatsGraphic _instance;

    public static StatsGraphic Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("je sers à faire des graphiques");
                _instance = go.AddComponent<StatsGraphic>();
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

    private void Start()
    {
        //UpdateGraphics();
        _party1.DOFillAmount(0, 0.5f);
        _party2.DOFillAmount(0, 0.5f);
        _party3.DOFillAmount(0, 0.5f);
        _party4.DOFillAmount(0, 0.5f);
        _party5.DOFillAmount(0, 0.5f);
    }

    public void UpdateGraphics()
    {
        float total = ScoreManager.PartyScores["EG"] + ScoreManager.PartyScores["G"] + ScoreManager.PartyScores["C"] + ScoreManager.PartyScores["D"] + ScoreManager.PartyScores["ED"];

        float proportion1 = Mathf.Clamp(ScoreManager.PartyScores["EG"] / total, 0, 9999);
        float proportion2 = Mathf.Clamp(ScoreManager.PartyScores["G"] / total, 0, 9999);
        float proportion3 = Mathf.Clamp(ScoreManager.PartyScores["C"] / total, 0, 9999);
        float proportion4 = Mathf.Clamp(ScoreManager.PartyScores["D"] / total, 0, 9999);
        float proportion5 = Mathf.Clamp(ScoreManager.PartyScores["ED"] / total, 0, 9999);

        #region Cimetière
        //float currentRot = 0;
        //float rotY = currentRot;

        //_party1.fillAmount = proportion1 / 2;
        ////currentRot += 180 * _party1.fillAmount;
        //currentRot = 0;
        ////_party1.transform.rotation = Quaternion.AngleAxis(currentRot, Vector3.forward);

        //_party2.fillAmount = proportion2 / 2;
        //currentRot += 180 * _party2.fillAmount;
        //_party2.transform.rotation = Quaternion.AngleAxis(currentRot, Vector3.forward);

        //_party3.fillAmount = proportion3 / 2;
        //currentRot += 180 * _party3.fillAmount;
        //_party3.transform.rotation = Quaternion.AngleAxis(currentRot, Vector3.forward);

        //_party4.fillAmount = proportion4 / 2;
        //currentRot += 180 * _party4.fillAmount;
        //_party4.transform.rotation = Quaternion.AngleAxis(currentRot, Vector3.forward);

        //_party5.fillAmount = proportion5 / 2;
        //currentRot += 180 * _party5.fillAmount;
        //_party5.transform.rotation = Quaternion.AngleAxis(currentRot, Vector3.forward);
        #endregion

        _party1.DOFillAmount(proportion1, 0.5f);
        _party2.DOFillAmount(proportion2, 0.5f);
        _party3.DOFillAmount(proportion3, 0.5f);
        _party4.DOFillAmount(proportion4, 0.5f);
        _party5.DOFillAmount(proportion5, 0.5f);
    }
}
