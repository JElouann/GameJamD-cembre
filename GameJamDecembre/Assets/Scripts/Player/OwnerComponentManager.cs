using UnityEngine;
using Unity.Netcode;

public class OwnerComponentManager : NetworkBehaviour
{
    [SerializeField] 
    private Camera _camera; // This is your camera, assign it in the prefab

    [SerializeField]
    private GameObject _mainPanel;

    [SerializeField] 
    private GameObject _panel;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsOwner) { return; } // ALL players will read this method, only player owner will execute past this line
        _camera.enabled = true; // only enable YOUR PLAYER'S camera, all others will stay disabled
        _mainPanel.SetActive(false);
        _panel.SetActive(true);
    }
}
