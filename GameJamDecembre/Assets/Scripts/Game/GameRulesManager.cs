using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameRuleManager : MonoBehaviour
{
    [SerializeField]
    private Negociation _negociation;

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
        AddLoi(false, "Nationalisation des trotinettes.", "EG", 4, "G", 2, "C", 1 , "OSEF", 0);
        AddLoi(false, "Augmentation du SMIC a 3000€.", "EG", 4, "G", 2, "C", 1, "OSEF", 0);
        AddLoi(false, "Requisition de villas en AirBNB.", "EG", 4, "G", 2, "C", 1, "OSEF", 0);
        AddLoi(false, "Depart a la retraite a 89 ans et demi.", "EG", 2, "G", 3, "C", 1, "OSEF", 0);
        AddLoi(false, "Installation de panneaux solaires sur les parkings.", "EG", 2, "G", 3, "C", 1, "OSEF", 0);
        AddLoi(false, "Pole emploi : Level Up Edition.", "EG", 2, "G", 3, "C", 1, "OSEF", 0);
        AddLoi(false, "Simplifions la paperasse (vraiment ?)", "G", 1, "C", 3, "D", 1, "OSEF", 0);
        AddLoi(false, "Dons de tablettes a tous les CM2.", "G", 1, "C", 3, "D", 1, "OSEF", 0);
        AddLoi(false, "Numerisation des buralistes.", "G", 1, "C", 3, "D", 1, "OSEF", 0);
        AddLoi(false, "Mise en place d'une taxe fixe pour les riches.", "C", 1, "D", 3, "ED", 2, "OSEF", 0);
        AddLoi(false, "Exonerations pour Tesla et Amazon.", "C", 1, "D", 3, "ED", 2, "OSEF", 0);
        AddLoi(false, "Renforcement des troupes de CRS.", "C", 1, "D", 3, "ED", 2, "OSEF", 0);
        AddLoi(false, "Expulsion des chiens de migrants.", "C", 0, "D", 2, "ED", 4, "OSEF", 0);
        AddLoi(false, "Retour des frontieres en papier.", "C", 0, "D", 2, "ED", 4, "OSEF", 0);
        AddLoi(false, "Candidatures reservees aux nationaux.", "C", 0, "D", 2, "ED", 4, "OSEF", 0);

        // ajout des lois un peu particulière jpp

        AddLoi(true, "Salaire XXL pour le PM.", "PM", 5, "DEP", -1, "PM", -3, "DEP", 1);
        AddLoi(true, "Budget du PM : on ferme les yeux.", "PM", 4, "DEP", -1, "PM", -2, "DEP", 1);
        AddLoi(true, "Prime pour faire semblant de bosser.", "DEP", 3, "PM", -2, "DEP", -1, "PM", 2);
        AddLoi(true, "Cumul de mandats : youpi !", "DEP", 2, "PM", -3, "DEP", -1, "PM", 1);
        AddLoi(true, "Aides pour les losers.", "LOS", 4, "AUTRES", -1, "OSEF", 0, "OSEF", 0);
        AddLoi(true, "Relance des losers éternels.", "LOS", 3, "AUTRES", 1, "OSEF", 0, "OSEF", 0);
        AddLoi(true, "Subventions pour vos galères.", "LOS", 5, "OSEF", 0, "OSEF", 0, "OSEF", 0);
        AddLoi(true, "Taxe sur les mecs blindés.", "WIN", -4, "AUTRES", 1, "OSEF", 0, "OSEF", 0);
        AddLoi(true, "Redistribution (mais pas trop).", "WIN", -3, "LOS", 3, "OSEF", 0, "OSEF", 0);
        AddLoi(true, "On coupe les privilèges.", "WIN", -5, "OSEF", 0, "OSEF", 0, "OSEF", 0);
    }

    public void AddLoi(bool IsSpecial, string law, string cible1, int points1, string cible2, int points2, string cible3, int points3, string cible4, int points4)
    {
        CibleLoi newCible = new CibleLoi(IsSpecial, law, cible1, points1, cible2, points2, cible3, points3, cible4, points4);
        _cibleLoi.Add(newCible);
    }

    public void TirerLoiAleatoire()
    {
        if (_cibleLoi.Count > 0)
        {
            _negociation.CanNegociation = true;
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
