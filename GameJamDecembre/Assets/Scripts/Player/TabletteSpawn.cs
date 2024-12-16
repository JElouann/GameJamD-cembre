using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class TabletteSpawn : NetworkBehaviour
{
    [SerializeField]
    private GameObject _canvasTablette;

    [SerializeField]
    private MouseLook _playerMouseControl;

    [SerializeField]
    private PlayerController _playerControl;

    [SerializeField]
    private InputLoi _inputLoiRef;

    [SerializeField]
    private NetworkVariable<bool> _panelOpen = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public void OnTabletteSpawn(InputAction.CallbackContext context)
    {
        if (!IsOwner) { return; } // ALL players will read this method, only player owner will execute past this line
        if (context.performed)
        {
            if (!_panelOpen.Value && _inputLoiRef._canTakeTablette.Value)
            {
                _playerControl.CanMove.Value = false;
                _playerMouseControl.CanMoveCamera.Value = false;
                _panelOpen.Value = true;
                _canvasTablette.SetActive(true);
            }
            else
            {
                _playerControl.CanMove.Value = true;
                _playerMouseControl.CanMoveCamera.Value = true;
                _panelOpen.Value = false;
                _canvasTablette.SetActive(false);
            }
        }
    }
}
