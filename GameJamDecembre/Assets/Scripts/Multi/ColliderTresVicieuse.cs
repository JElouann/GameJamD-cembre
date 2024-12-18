using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ColliderTresVicieuse : NetworkBehaviour
{
    private const int PlayerLayer = 7;

    [SerializeField] private List<ulong> _idPlayersInGame = new List<ulong>();
    private Dictionary<ulong, NetworkVariable<int>> _playerScores = new Dictionary<ulong, NetworkVariable<int>>();

    [SerializeField] private TextMeshProUGUI _loiEnCours;

    private void OnTriggerEnter(Collider other)
    {
        if (IsOwner && other.gameObject.layer == PlayerLayer)
        {
            var playerComponent = other.gameObject.GetComponent<OwnerComponentManager>();
            if (playerComponent != null)
            {
                RegisterPlayerId(playerComponent.IdPlayer);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == PlayerLayer)
        {
            _loiEnCours.text = "Prenez place pour parler de votre loi";
        }
    }

    private void RegisterPlayerId(ulong playerId)
    {
        if (!_idPlayersInGame.Contains(playerId))
        {
            print("ajout");
            _idPlayersInGame.Add(playerId);
            if (!_playerScores.ContainsKey(playerId))
            {
                _playerScores[playerId] = new NetworkVariable<int>(0);
            }
        }
        DisplayPlayerLaw(playerId);
    }

    private void DisplayPlayerLaw(ulong playerId)
    {
        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(playerId, out var client))
        {
            GameObject playerObject = client.PlayerObject.gameObject;
            var inputLoi = playerObject.GetComponent<InputLoi>();
            if (inputLoi != null)
            {
                _loiEnCours.text = inputLoi.ValueLaws.Value.ToString();
            }
        }
        else
        {
            Debug.LogWarning($"Player ID {playerId} not found.");
        }
    }
}
