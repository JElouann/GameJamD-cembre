using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class InputLoi : NetworkBehaviour
{
    public NetworkVariable<FixedString64Bytes> _valueLaws = new NetworkVariable<FixedString64Bytes>("test", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [SerializeField]
    private TextMeshProUGUI _loiText;


    public TextMeshProUGUI _newLaws;

    [SerializeField]
    private GameObject _panelLaws;

    [SerializeField]
    private PlayerController _playerControl;

    [SerializeField]
    private MouseLook _playerMouseControl;

    [SerializeField]
    public NetworkVariable<bool> _canTakeTablette = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public NetworkVariable<int> PlayerID;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn(); 
        if (!IsOwner) return;
        _panelLaws.SetActive(true);
        Transform zgeg = GameManager.Instance.SpawnPoints[Random.Range(0, GameManager.Instance.SpawnPoints.Count - 1)];
        this.transform.position = zgeg.position;
        this.transform.rotation = zgeg.rotation;
        // has to be in another script but MEH
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        if (!IsOwner) return;

        // has to be in another script but MEH
    }

    public void SetLaws()
    {
        if (!IsOwner) { return; } // ALL players will read this method, only player owner will execute past this line
        _playerControl.CanMove.Value = true;
        _playerMouseControl.CanMoveCamera.Value = true;

        _valueLaws.Value = _loiText.text;

        //GameManager.Instance.Laws.Add(new PlayerLaw(5, "g"));
        _newLaws.text = _valueLaws.Value.ToString();

        _panelLaws.SetActive(false);
        _canTakeTablette.Value = true;
    }
}