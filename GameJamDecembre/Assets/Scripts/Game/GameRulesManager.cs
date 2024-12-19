using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameRuleManager : MonoBehaviour
{

    [SerializeField]
    private List<CibleLoi> _cibleLoi = new List<CibleLoi>();

    [SerializeField]
    private TMP_Text _lawDisplayText;

    [SerializeField]
    private Button _randomLawButton;

    public static CibleLoi RandomLaw;

    void Start()
    {
        //ajout des lois
        AddLoi("Nationalisation des trotinettes.", "EG", 4, "G", 2, "C", 1);
        AddLoi("Augmentation du SMIC a 3000�.", "EG", 4, "G", 2, "C", 1);
        AddLoi("Requisition de villas en AirBNB.", "EG", 4, "G", 2, "C", 1);
        AddLoi("Depart a la retraite a 89 ans et demi.", "EG", 2, "G", 3, "C", 1);
        AddLoi("Installation de panneaux solaires sur les parkings.", "EG", 2, "G", 3, "C", 1);
        AddLoi("Pole emploi : Level Up Edition.", "EG", 2, "G", 3, "C", 1);
        AddLoi("Simplifions la paperasse (vraiment ?)", "G", 1, "C", 3, "D", 1);
        AddLoi("Dons de tablettes a tous les CM2.", "G", 1, "C", 3, "D", 1);
        AddLoi("Numerisation des buralistes.", "G", 1, "C", 3, "D", 1);
        AddLoi("Mise en place d'une taxe fixe pour les riches.", "C", 1, "D", 3, "ED", 2);
        AddLoi("Exonerations pour Tesla et Amazon.", "C", 1, "D", 3, "ED", 2);
        AddLoi("Renforcement des troupes de CRS.", "C", 1, "D", 3, "ED", 2);
        AddLoi("Expulsion des chiens de migrants.", "C", 0, "D", 2, "ED", 4);
        AddLoi("Retour des frontieres en papier.", "C", 0, "D", 2, "ED", 4);
        AddLoi("Candidatures reservees aux nationaux.", "C", 0, "D", 2, "ED", 4);
    }

    public void AddLoi(string law, string cible1, int points1, string cible2, int points2, string cible3, int points3)
    {
        CibleLoi newCible = new CibleLoi(law, cible1, points1, cible2, points2, cible3, points3);
        _cibleLoi.Add(newCible);
    }

    public void TirerLoiAleatoire()
    {
        if (_cibleLoi.Count > 0)
        {
            int randomIndex = Random.Range(0, _cibleLoi.Count);
            RandomLaw = _cibleLoi[randomIndex];

            _lawDisplayText.text = $"Loi tiree : {RandomLaw.Law}\n";
            _lawDisplayText.text += $"{RandomLaw.Parti1}: {RandomLaw.Points1} points\n";
            _lawDisplayText.text += $"{RandomLaw.Parti2}: {RandomLaw.Points2} points\n";
            _lawDisplayText.text += $"{RandomLaw.Parti3}: {RandomLaw.Points3} points\n";

            _cibleLoi.Remove(RandomLaw);
            // afficher sur le bandeau la nouvelle loi
            BandeauUI.Instance.ShowText("Nouvelle loi : " + RandomLaw.Law);
        }
        else
        {
            _lawDisplayText.text = "Aucune loi disponible.";
            // afficher sur le bandeau le manque de loi
            BandeauUI.Instance.ShowText("Plus de propositions de lois disponibles.");
        }
    }
}
