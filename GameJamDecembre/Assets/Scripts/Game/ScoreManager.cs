using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

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
    public static Dictionary<string, int> PartyScores = new() { { "EG", 0 }, { "G", 0 }, { "C", 0 }, { "D", 0 }, { "ED", 0 }, { "PM", 0 }, { "WIN", 0 }, { "LOS", 0 }, { "DEP", 0 }, { "OSEF", 0 }, { "AUTRES", 0 } };
    public static Dictionary<string, TextMeshProUGUI> _partiScoreText = new();

    private List<string> _toIgnore = new() { "WIN", "LOS", "DEP", "OSEF", "AUTRES"};

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
        _partiScoreText.Add("PM", _textMeshProList[5]);
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

        CibleLoi cibleLoi = GameRuleManager.RandomLaw;
        if (_nombreDeVoixPour >= 3)
        {
            if (cibleLoi.IsSpecial)
            {
                SpecialPuntos(cibleLoi, true);
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
            StatsGraphic.Instance.UpdateGraphics();
        }
        else
        {
            if (cibleLoi.IsSpecial)
            {
                SpecialPuntos(cibleLoi, false);
            }
            else
            {
                RemovePuntos();
            }
        }

    }

    private void SpecialPuntos(CibleLoi loi, bool isAccepted)
    {
        List<string> party = new();
        List<int> puntos = new();
        List<string> lesAutres = new() { "EG", "G", "C", "D", "ED", "PM"};

        if (isAccepted)
        {
            party.Add(loi.Parti1);
            party.Add(loi.Parti2);
            puntos.Add(loi.Points1);
            puntos.Add(loi.Points2);
        }
        else
        {
            party.Add(loi.Parti3);
            party.Add(loi.Parti4);
            puntos.Add(loi.Points3);
            puntos.Add(loi.Points4);
        }

        for (int i = 0; i < party.Count; i++)
        {
            switch (party[i])
            {
                case "OSEF":
                    break; // osef 

                case "PM":
                    Mathf.Clamp(_scoreMinistre += puntos[i], 0, 9999);
                    _premierMinistre.text = _scoreMinistre.ToString();
                    lesAutres.Remove("PM");
                    break;

                case "WIN":
                    foreach(string key in GetWinners())
                    {
                        Mathf.Clamp(PartyScores[key] += puntos[i], 0, 9999);
                        _partiScoreText[key].text = PartyScores[key].ToString();
                        lesAutres.Remove(key);
                        print($"{key} : {puntos[i]}");
                    }
                    break;

                case "LOS":
                    foreach (string key in GetLosers())
                    {
                        Mathf.Clamp(PartyScores[key] += puntos[i], 0, 9999); ;
                        lesAutres.Remove(key);
                        _partiScoreText[key].text = PartyScores[key].ToString();
                        print($"{key} : {puntos[i]}");
                    }
                    break;

                case "DEP":
                    foreach(string key in GetDeputesDuSenatDeLaRepubliqueDemocratique())
                    {
                        Mathf.Clamp(PartyScores[key] += puntos[i], 0, 9999); ;
                        lesAutres.Remove(key);
                        _partiScoreText[key].text = PartyScores[key].ToString();
                        print($"{key} : {puntos[i]}");
                    }
                    break;

                case "AUTRES":
                    foreach (string key in lesAutres)
                    {
                        Mathf.Clamp(PartyScores[key] += puntos[i], 0, 9999); ;
                        _partiScoreText[key].text = PartyScores[key].ToString();
                        print($"{key} : {puntos[i]}");
                    }
                    break;
            }
        }
        StatsGraphic.Instance.UpdateGraphics();
    }

    public void RemovePuntos()
    {
        _scoreMinistre -= 1;
        _premierMinistre.text = _scoreMinistre.ToString();
        StatsGraphic.Instance.UpdateGraphics();
    }

    private List<string> GetWinners()
    {
        List<string> winningParties = new();

        foreach (KeyValuePair<string, int> item in PartyScores)
        {
            if(item.Value >= PartyScores.Values.Max() && !_toIgnore.Contains(item.Key))
            {
                winningParties.Add(item.Key);
            }
        }

        return winningParties;
    }

    private List<string> GetLosers()
    {
        List<string> losingParties = new();

        foreach (KeyValuePair<string, int> item in PartyScores)
        {
            if (item.Value <= PartyScores.Values.Min() && !_toIgnore.Contains(item.Key))
            {
                losingParties.Add(item.Key);
            }
        }

        return losingParties;
    }

    private List<string> GetDeputesDuSenatDeLaRepubliqueDemocratique()
    {
        List<string> deputes = new();

        foreach (KeyValuePair<string, int> item in PartyScores)
        {
            if (item.Key == "EG" || item.Key == "G" || item.Key == "C" || item.Key == "D" || item.Key == "ED")
            {
                deputes.Add(item.Key);
            }
        }

        return deputes;
    }
}
