using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputField;

    [SerializeField]
    private List<TextMeshProUGUI> _textMeshProList = new();

    [SerializeField]
    private Button _button;

    [SerializeField]
    private Button _buttonLaw1;

    [SerializeField]
    private Button _buttonLaw2;

    private int _currentIndexOfPlayer = 0;

    private static Dictionary<string, int> _partyScores = new() { { "EG", 0 }, { "G", 0 }, { "C", 0 }, { "D", 0 }, { "ED", 0 } };

    public void RenamePlayer()
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
    }

    public void AddPuntos()
    {
        CibleLoi cibleLoi = GameRuleManager.RandomLaw;
        print($"test | parti 1 : {cibleLoi.Parti1} a gagné : {cibleLoi.Points1} (avant c'était : {_partyScores[cibleLoi.Parti1]})");
        _partyScores[cibleLoi.Parti1] += cibleLoi.Points1;
        _partyScores[cibleLoi.Parti2] += cibleLoi.Points2;
        _partyScores[cibleLoi.Parti3] += cibleLoi.Points3;
    } 
}
