using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StatsGraphic : MonoBehaviour
{
    [SerializeField] private Image _party1;
    [SerializeField] private Image _party2;
    [SerializeField] private Image _party3;
    [SerializeField] private Image _party4;
    [SerializeField] private Image _party5;

    private void Start()
    {
        UpdateGraphics();    
    }

    public void UpdateGraphics()
    {
        float total = ScoreManager.PartyScores["EG"] + ScoreManager.PartyScores["G"] + ScoreManager.PartyScores["C"] + ScoreManager.PartyScores["D"] + ScoreManager.PartyScores["ED"];

        float proportion1 = ScoreManager.PartyScores["EG"] / total;
        float proportion2 = (ScoreManager.PartyScores["G"] == 0) ? 0 : proportion1 + ScoreManager.PartyScores["G"] / total;
        float proportion3 = (ScoreManager.PartyScores["C"] == 0) ? 0 : proportion2 + ScoreManager.PartyScores["C"] / total;
        float proportion4 = (ScoreManager.PartyScores["D"] == 0) ? 0 : proportion3 + ScoreManager.PartyScores["D"] / total;
        float proportion5 = (ScoreManager.PartyScores["ED"] == 0) ? 0 : proportion4 + ScoreManager.PartyScores["ED"] / total;

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
