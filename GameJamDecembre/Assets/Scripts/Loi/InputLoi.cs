using TMPro;
using Unity.Netcode;
using UnityEngine;

public class InputLoi : NetworkBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _loiText;

    [SerializeField]
    private TextMeshProUGUI _newLaws;

    [SerializeField]
    private GameObject _panelLaws;

    public void SetLaws()
    {
        base.OnNetworkSpawn();
        if (!IsOwner) { return; } // ALL players will read this method, only player owner will execute past this line
        _newLaws.text = _loiText.text;
        _panelLaws.SetActive(false);
    }
}
