using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameRuleManager : MonoBehaviour
{
    public int LeDelaiDeLaMortQuiTueEtNeRespecteMemePasLesConventionsTellementIlEstPuissantAuMoinsAutantQueUnCoupDBouleDePhillipeEtchebest;

    [SerializeField]
    private Negociation _negociation;

    [SerializeField]
    private List<CibleLoi> _cibleLoi = new List<CibleLoi>();

    // UI

    [SerializeField] private TMP_Text _lawDisplayer;

    // Pour loi CLASSIQUE
    [SerializeField]
    private TMP_Text _lawInfosDisplayText;
    [SerializeField]
    private LePetitMouvementDuHautVersLeBasPourLUI _infoPanelClassic;

    // Pour loi SPECIALE
    [SerializeField]
    private TMP_Text _lawInfosDisplayTextSpecialYes;
    [SerializeField]
    private TMP_Text _lawInfosDisplayTextSpecialNo;
    [SerializeField]
    private LePetitMouvementDuHautVersLeBasPourLUI _infoPanelSpecial;

    // Pour ecran de fin
    [SerializeField] private GameObject _score;
    [SerializeField] private GameObject _endGame;

    [SerializeField]
    private Button _randomLawButton;

    public static CibleLoi RandomLaw;

    // Singleton
    #region Singleton
    private static GameRuleManager _instance;

    public static GameRuleManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("ça n'arrivera pas");
                _instance = go.AddComponent<GameRuleManager>();
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

    void Start()
    {
        _infoPanelClassic.MoveUp();
        _infoPanelSpecial.MoveUp();

        //ajout des lois
        AddLoi(false, "Nationalisation des trotinettes.", "EG", 4, "G", 2, "C", 1, "OSEF", 0);
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

        AddLoi(true, "Prime pour faire semblant de bosser.", "DEP", 3, "PM", -2, "DEP", -1, "PM", 2);
        AddLoi(true, "Cumul de mandats : youpi !", "DEP", 2, "PM", -3, "DEP", -1, "PM", 1);
        AddLoi(true, "Aides pour les losers.", "LOS", 4, "AUTRES", -1, "OSEF", 0, "OSEF", 0);
        AddLoi(true, "Relance des losers eternels.", "LOS", 3, "AUTRES", 1, "OSEF", 0, "OSEF", 0);
        AddLoi(true, "Subventions pour vos galeres.", "LOS", 5, "OSEF", 0, "OSEF", 0, "OSEF", 0);
        AddLoi(true, "Taxe sur les mecs blindes.", "WIN", -4, "AUTRES", 1, "OSEF", 0, "OSEF", 0);
        AddLoi(true, "Redistribution (mais pas trop).", "WIN", -3, "LOS", 3, "OSEF", 0, "OSEF", 0);
        AddLoi(true, "On coupe les privileges.", "WIN", -5, "OSEF", 0, "OSEF", 0, "OSEF", 0);
    }

    public void AddLoi(bool IsSpecial, string law, string cible1, int points1, string cible2, int points2, string cible3, int points3, string cible4, int points4)
    {
        CibleLoi newCible = new CibleLoi(IsSpecial, law, cible1, points1, cible2, points2, cible3, points3, cible4, points4);
        _cibleLoi.Add(newCible);
    }

    public void TirerLoiAleatoire()
    {
        if (LeDelaiDeLaMortQuiTueEtNeRespecteMemePasLesConventionsTellementIlEstPuissantAuMoinsAutantQueUnCoupDBouleDePhillipeEtchebest >= 10) return;
        LeDelaiDeLaMortQuiTueEtNeRespecteMemePasLesConventionsTellementIlEstPuissantAuMoinsAutantQueUnCoupDBouleDePhillipeEtchebest++;
        if (_cibleLoi.Count > 0)
        {
            _negociation.CanNegociation = true;
            int randomIndex = Random.Range(0, _cibleLoi.Count);
            RandomLaw = _cibleLoi[randomIndex];

            _lawDisplayer.text = RandomLaw.Law;

            if (RandomLaw.IsSpecial)
            {
                DisplaySpecialLaw();
            }
            else
            {
                DisplayClassicalLaw();
            }

            _cibleLoi.Remove(RandomLaw);
            // afficher sur le bandeau la nouvelle loi
            BandeauUI.Instance.ShowText("Nouvelle loi : " + RandomLaw.Law);
        }
        else
        {
            _lawInfosDisplayText.text = "Aucune loi disponible.";
            // afficher sur le bandeau le manque de loi
            BandeauUI.Instance.ShowText("Plus de propositions de lois disponibles.");
        }
    }

    private void DisplayClassicalLaw()
    {
        print("classic");
        _infoPanelSpecial.MoveUp();
        _infoPanelClassic.MoveDown();
        _lawInfosDisplayText.text = null;
        _lawInfosDisplayText.text += $"{RandomLaw.Parti1}: {RandomLaw.Points1} points\n";
        _lawInfosDisplayText.text += $"{RandomLaw.Parti2}: {RandomLaw.Points2} points\n";
        _lawInfosDisplayText.text += $"{RandomLaw.Parti3}: {RandomLaw.Points3} points\n";
    }

    private void DisplaySpecialLaw()
    {
        print("special");
        _infoPanelClassic.MoveUp();
        _infoPanelSpecial.MoveDown();
        string yes = null;
        string no = null;
        // mais on est ou la
        switch (RandomLaw.Parti1)
        {
            case "WIN":
                yes += $"Plus haut score: {RandomLaw.Points1} points\n";
                break;
            case "LOS":
                yes += $"Plus bas score: {RandomLaw.Points1} points\n";
                break;
            case "DEP":
                yes += $"Deputes: {RandomLaw.Points1} points\n";
                break;
            case "AUTRES":
                yes += $"Autres: {RandomLaw.Points1} points\n";
                break;
            case "OSEF":
                break;
            default:
                yes += $"{RandomLaw.Parti1}: {RandomLaw.Points1} points\n";
                break;
        }
        // MAIS ON EST OU
        switch (RandomLaw.Parti2)
        {
            case "WIN":
                yes += $"Plus haut score: {RandomLaw.Points2} points\n";
                break;
            case "LOS":
                yes += $"Plus bas score: {RandomLaw.Points2} points\n";
                break;
            case "DEP":
                yes += $"Deputes: {RandomLaw.Points2} points\n";
                break;
            case "AUTRES":
                yes += $"Autres: {RandomLaw.Points2} points\n";
                break;
            case "OSEF":
                break;
            default:
                yes += $"{RandomLaw.Parti2}: {RandomLaw.Points2} points\n";
                break;
        }
        switch (RandomLaw.Parti3)
        {
            case "WIN":
                no += $"Plus haut score: {RandomLaw.Points3} points\n";
                break;
            case "LOS":
                no += $"Plus bas score: {RandomLaw.Points3} points\n";
                break;
            case "DEP":
                no += $"Deputes: {RandomLaw.Points3} points\n";
                break;
            case "AUTRES":
                no += $"Autres: {RandomLaw.Points3} points\n";
                break;
            case "OSEF":
                break;
            default:
                no += $"{RandomLaw.Parti3}: {RandomLaw.Points3} points\n";
                break;
        }
        switch (RandomLaw.Parti4)
        {
            case "WIN":
                no += $"Plus haut score: {RandomLaw.Points4} points\n";
                break;
            case "LOS":
                no += $"Plus bas score: {RandomLaw.Points4} points\n";
                break;
            case "DEP":
                no += $"Deputes: {RandomLaw.Points4} points\n";
                break;
            case "AUTRES":
                no += $"Autres: {RandomLaw.Points4} points\n";
                break;
            case "OSEF":
                break;
            default:
                no += $"{RandomLaw.Parti4}: {RandomLaw.Points4} points\n";
                break;
        }

        _lawInfosDisplayTextSpecialYes.text = yes;
        _lawInfosDisplayTextSpecialNo.text = no;
    }

    public void GIGASHUTDOWNARMAGGEDON()
    {
        _endGame.SetActive(true);
        _score.transform.DOScale(Vector3.one * 1.5f, 0.6f);
        _score.transform.DOMove(_endGame.transform.position, 0.6f);
        _score.transform.SetAsLastSibling();
        GameObject text = _endGame.transform.GetChild(0).gameObject;
        text.transform.parent = _endGame.transform.parent;
        text.transform.SetAsLastSibling();
    }
}
