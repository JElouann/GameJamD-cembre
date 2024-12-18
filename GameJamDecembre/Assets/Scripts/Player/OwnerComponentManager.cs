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
        _mainPanel.SetActive(true);
        _panel.SetActive(true);
        if (IsHost)
        {
            gameObject.transform.position = new Vector3(0,5000,0);
        }

        PlayerManager._instance.OnClientConnected(NetworkManager.Singleton.LocalClientId); ///balance l'ID du joueur
    }
}
