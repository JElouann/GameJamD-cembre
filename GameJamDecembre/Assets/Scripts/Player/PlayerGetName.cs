using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerGetName : NetworkBehaviour
{
    [SerializeField]
    private NetworkVariable<FixedString64Bytes> _nameToString = new NetworkVariable<FixedString64Bytes>("Undef", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [SerializeField]
    private TextMeshProUGUI _name;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsOwner) { return; }
        _nameToString.Value = ListRandomPseudo.Instance.PseudoInitial[0].ToString();
        _name.text = _nameToString.Value.ToString();
        ListRandomPseudo.Instance.PseudoInitial.RemoveAt(0);
    }
}