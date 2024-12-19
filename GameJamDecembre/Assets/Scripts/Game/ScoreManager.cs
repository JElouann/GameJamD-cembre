using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> _textMeshProList = new();

    [SerializeField]
    private TextMeshProUGUI _premierMinistre;

    [SerializeField]
    private int _nombreDeVoixPour;

    [SerializeField]
    private int _scoreMinistre = 0;

    // les différents partis + le premier ministre + WIN et LOS pour désigner les gagnants et les perdants + DEP pour désigner tous les députés + OSEF pour tu c quoi + AUTRES pour désigner tout ceux qui ne sont pas concernés
    public static Dictionary<string, int> PartyScores = new() { { "EG", 0 }, { "G", 0 }, { "C", 0 }, { "D", 0 }, { "ED", 0 }, { "PM", 0 }, { "WIN", 0 }, { "LOS", 0 }, { "DEP", 0 }, { "OSEF", 0 }, { "AUTRES", 0} };
    public static Dictionary<string, TextMeshProUGUI> _partiScoreText = new();

    public static ScoreManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    /*public void RenamePlayer()
    {
        if (_inputField.text == "") { return; } //Si fausse manip
        _textMeshProList[_currentIndexOfPlayer].text = _inputField.text;
        _currentIndexOfPlayer++;
        _inputField.text = "";
        if (_textMeshProList.Count <= _currentIndexOfPlayer) //Si c'était le dernier nom
        {
            _button.gameObject.SetActive(false);
            _inputField.gameObject.SetActive(false);
            _buttonLaw1.gameObject.SetActive(true);
            _buttonLaw2.gameObject.SetActive(true);
        } 
    }*/

    private void Start()
    {
        _partiScoreText.Clear();
        _partiScoreText.Add("EG", _textMeshProList[4]);
        _partiScoreText.Add("G", _textMeshProList[3]);
        _partiScoreText.Add("C", _textMeshProList[2]);
        _partiScoreText.Add("D", _textMeshProList[1]);
        _partiScoreText.Add("ED", _textMeshProList[0]);
    }

    public void AddPuntos()
    {
        _nombreDeVoixPour = 0;

        foreach (bool vote in CamSlide.Instance._playerVote)
        {
            if (vote)
            {
                _nombreDeVoixPour++;
            }
        }

        if (_nombreDeVoixPour >= 3)
        {
            CibleLoi cibleLoi = GameRuleManager.RandomLaw;
            if (cibleLoi.IsSpecial)
            {
                SpecialPuntos();
                return;
            }
            PartyScores[cibleLoi.Parti1] += cibleLoi.Points1;
            PartyScores[cibleLoi.Parti2] += cibleLoi.Points2;
            PartyScores[cibleLoi.Parti3] += cibleLoi.Points3;

            _partiScoreText[cibleLoi.Parti1].text = PartyScores[cibleLoi.Parti1].ToString();
            _partiScoreText[cibleLoi.Parti2].text = PartyScores[cibleLoi.Parti2].ToString();
            _partiScoreText[cibleLoi.Parti3].text = PartyScores[cibleLoi.Parti3].ToString();

            _scoreMinistre += 3;
            _premierMinistre.text = _scoreMinistre.ToString();
        }
        else
        {
            RemovePuntos();
        }

    }

    private void SpecialPuntos()
    {

    }

    public void RemovePuntos()
    {
        _scoreMinistre -= 1;
        _premierMinistre.text = _scoreMinistre.ToString();
    }
}
