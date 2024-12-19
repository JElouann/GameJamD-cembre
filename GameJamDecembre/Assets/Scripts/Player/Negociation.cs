using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Negociation : MonoBehaviour
{
    public bool CanNegociation;

    public int NombreDePointMax;

    public void Update()
    {
        if (!Input.GetMouseButtonDown(0)) { return; }
        {
            if (CanNegociation)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    Debug.Log(hit.transform.name);
                    ToiTesMonAmi(hit.transform.name);
                }
            }
        }
    }

    public void ToiTesMonAmi(string PartiName)
    {
        if (NombreDePointMax >= 2) { return; }
        switch (PartiName)
        {
            case "RN":
                ScoreManager.PartyScores["ED"] ++;
                ScoreManager._partiScoreText["ED"].text = ScoreManager.PartyScores["ED"].ToString();
                NombreDePointMax++;
                break;
            case "S":
                ScoreManager.PartyScores["G"] ++;
                ScoreManager._partiScoreText["G"].text = ScoreManager.PartyScores["G"].ToString();
                NombreDePointMax++;
                break;
            case "C":
                ScoreManager.PartyScores["C"] ++;
                ScoreManager._partiScoreText["C"].text = ScoreManager.PartyScores["C"].ToString();
                NombreDePointMax++;
                break;
            case "LR":
                ScoreManager.PartyScores["D"] ++;
                ScoreManager._partiScoreText["D"].text = ScoreManager.PartyScores["D"].ToString();
                NombreDePointMax++;
                break;
            case "LFI":
                ScoreManager.PartyScores["EG"] ++;
                ScoreManager._partiScoreText["EG"].text = ScoreManager.PartyScores["EG"].ToString();
                NombreDePointMax++;
                break;
        }
    }
}
