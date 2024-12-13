using System.Collections;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class TestNetworkVariableSynchronization : NetworkBehaviour
{
    //[SerializeField]
    private NetworkVariable<FixedString64Bytes> _valueLaws = new NetworkVariable<FixedString64Bytes>("test", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

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
        _valueLaws.Value = _loiText.text;
        _newLaws.text = _valueLaws.Value.ToString();
        _panelLaws.SetActive(false);
    }
}