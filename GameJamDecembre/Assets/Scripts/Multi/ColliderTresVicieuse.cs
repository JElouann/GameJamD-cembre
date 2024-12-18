using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ColliderTresVicieuse : NetworkBehaviour
{
    //Les 6 id de joueur
    [SerializeField] private List<ulong> _idPlayersInGame;

    //Les scoreds des 6 joueurs
    [SerializeField] private NetworkVariable<int> _scoreJ1 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    [SerializeField] private NetworkVariable<int> _scoreJ2 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    [SerializeField] private NetworkVariable<int> _scoreJ3 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    [SerializeField] private NetworkVariable<int> _scoreJ4 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    [SerializeField] private NetworkVariable<int> _scoreJ5 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    [SerializeField] private NetworkVariable<int> _scoreJ6 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [SerializeField] private TextMeshProUGUI _loiEnCours;

    /// <summary>
    /// Trigger très coquin qui pirate les données du joueur
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (IsOwner)
        {
            if (other.gameObject.layer == 7) //Si c'est un player
            {
                DownloadId(other.gameObject.GetComponent<OwnerComponentManager>().IdPlayer);
                print(other.gameObject.GetComponent<OwnerComponentManager>().IdPlayer);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7) //Si c'est un player
        {
            _loiEnCours.text = "Prenez place pour parler de votre loi"; //On invite un autre joueur à venir
        }
    }

    /// <summary>
    /// On enregistre potentiellement un nouvelle id
    /// </summary>
    /// <param name="_idPotential"></param>
    private void DownloadId(ulong _idPotential)
    {
        foreach (ulong _id in _idPlayersInGame) //parmis tout les id
        {
            if (_idPotential == _id) //Si je l'ai déjà enregistré
            {
                AfficheLoiPlayer(_idPotential);
                return;
            }
        }
        _idPlayersInGame.Add(_idPotential); //On enregister l'id du joueur car on ne le connaît pas
        AfficheLoiPlayer(_idPotential);
        print(_idPotential);
    }

    /// <summary>
    /// On récupert les infos liés au l'id
    /// </summary>
    /// <param name="_idPlayer"></param>
    private void AfficheLoiPlayer(ulong _idPlayer)
    {
        GameObject _playerDownload = NetworkManager.Singleton.ConnectedClients[_idPlayer].PlayerObject.gameObject;
        _loiEnCours.text = _playerDownload.GetComponent<InputLoi>().ValueLaws.Value.ToString();

    }
}
