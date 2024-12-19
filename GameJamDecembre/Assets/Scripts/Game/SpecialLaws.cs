using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpecialLaws : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string, int> _dicoSpecialLaws = new();

    [SerializeField]
    private List<GamesRules> _specialRules = new List<GamesRules>();

    void Start()
    {
        //ajout des lois
        AddLoi("Nationalisation des trotinettes.", "EG", 4, "G", 2, "C", 1);
        AddLoi("Augmentation du SMIC a 3000€.", "EG", 4, "G", 2, "C", 1);
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
        //CibleLoi newCible = new CibleLoi(law, cible1, points1, cible2, points2, cible3, points3);
        //_specialRules.Add(newCible);
    }
}
