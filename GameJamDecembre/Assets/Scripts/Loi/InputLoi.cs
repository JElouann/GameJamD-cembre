using TMPro;
using Unity.Netcode;
using UnityEngine;

public class InputLoi : NetworkBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _inputFieldText;

    [SerializeField]
    private TextMeshProUGUI _newLaws;

    [SerializeField]
    private GameObject _panelLaws;

    [SerializeField]
    private NetworkVariable<string> _loiName = new NetworkVariable<string>("Loi", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public void SetLaws()
    {
        base.OnNetworkSpawn();
        if (!IsOwner) { return; } // ALL players will read this method, only player owner will execute past this line
        _loiName.Value = _inputFieldText.ToString();
        _newLaws.text = _loiName.ToString();
        _panelLaws.SetActive(false);
    }
}
