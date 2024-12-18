using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    // Liste des joueurs connect�s (synchronis�e c�t� serveur uniquement) ==> probl�me si build ?
    public static Dictionary<ulong, string> connectedPlayers = new Dictionary<ulong, string>();

    public static PlayerManager _instance;

    //Il faut penser au d�conection si polish

    private void Awake()
    {
        _instance = this;
    }

    public void OnClientConnected(ulong clientId)
    {
        AddPlayer(clientId, $"Player_{clientId}");
    }

    public void OnClientDisconnected(ulong clientId)
    {
        RemovePlayer(clientId);
    }

    public static void AddPlayer(ulong clientId, string playerName)
    {
        if (!connectedPlayers.ContainsKey(clientId))
        {
            connectedPlayers.Add(clientId, playerName);
            Debug.Log($"Player {playerName} (ID: {clientId}) has joined the game.");
        }
    }

    public static void RemovePlayer(ulong clientId)
    {
        if (connectedPlayers.ContainsKey(clientId))
        {
            string playerName = connectedPlayers[clientId];
            connectedPlayers.Remove(clientId);
            Debug.Log($"Player {playerName} (ID: {clientId}) has left the game.");
        }
    }

    public static Dictionary<ulong, string> GetConnectedPlayers()
    {
        return new Dictionary<ulong, string>(connectedPlayers); // Renvoie une copie de la liste
    }
}
