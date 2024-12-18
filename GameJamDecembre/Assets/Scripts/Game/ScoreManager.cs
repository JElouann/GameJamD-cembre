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
}
