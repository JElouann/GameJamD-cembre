using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Negociation : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerMask;

    public void Update()
    {
        if (!Input.GetMouseButtonDown(0)) { return; }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.Log(hit.transform.name);
            ToiTesMonAmi(hit.transform.name);
        }
    }

    public void ToiTesMonAmi(string PartiName)
    {
        switch (PartiName)
        {
            case "RN":
                ScoreManager.PartyScores["ED"] ++;
                ScoreManager._partiScoreText["ED"].text = ScoreManager.PartyScores["ED"].ToString();
                break;
            case "S":
                ScoreManager.PartyScores["G"] ++;
                ScoreManager._partiScoreText["G"].text = ScoreManager.PartyScores["G"].ToString();
                break;
            case "C":
                ScoreManager.PartyScores["C"] ++;
                ScoreManager._partiScoreText["C"].text = ScoreManager.PartyScores["C"].ToString();
                break;
            case "LR":
                ScoreManager.PartyScores["D"] ++;
                ScoreManager._partiScoreText["D"].text = ScoreManager.PartyScores["D"].ToString();
                break;
            case "LFI":
                ScoreManager.PartyScores["EG"] ++;
                ScoreManager._partiScoreText["EG"].text = ScoreManager.PartyScores["EG"].ToString();
                break;
        }
    }
}
