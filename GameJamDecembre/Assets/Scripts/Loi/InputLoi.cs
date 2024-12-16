using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class InputLoi : NetworkBehaviour
{
    //[SerializeField]
    private NetworkVariable<FixedString64Bytes> _valueLaws = new NetworkVariable<FixedString64Bytes>("test", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [SerializeField]
    private TextMeshProUGUI _loiText;

    [SerializeField]
    private TextMeshProUGUI _newLaws;

    [SerializeField]
    private GameObject _panelLaws;

    [SerializeField]
    private PlayerController _playerControl;

    [SerializeField]
    private MouseLook _playerMouseControl;

    [SerializeField]
    public NetworkVariable<bool> _canTakeTablette = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public void SetLaws()
    {
        if (!IsOwner) { return; } // ALL players will read this method, only player owner will execute past this line
        _playerControl.CanMove.Value = true;
        _playerMouseControl.CanMoveCamera.Value = true;
        _valueLaws.Value = _loiText.text;
        _newLaws.text = _valueLaws.Value.ToString();
        _panelLaws.SetActive(false);
        _canTakeTablette.Value = true;
    }
}