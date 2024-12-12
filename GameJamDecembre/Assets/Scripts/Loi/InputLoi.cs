using TMPro;
using UnityEngine;

public class InputLoi : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _loiText;

    [SerializeField]
    private TextMeshProUGUI _newLaws;

    public void SetLaws()
    {
        _newLaws.text = _loiText.text;
    }
}
