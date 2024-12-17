using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public int NumberOfPlayer;

    // list that contains every law
    public List<PlayerLaw> Laws = new();

    // TODO : liste de portes

    // event to move to debate phase (if conditions) (proc when player enter law)
    public event Action OnLawEntered;

    // event to move to vote phase ()
    public event Action OnChronoOver;

    private GameTimer _gameTimer;
    [SerializeField] private List<Transform> _spawnPoints;

    public NetworkVariable<int> PlayerCount = new();
    public override void OnNetworkSpawn()
    {
        if (IsHost)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;
        }
    }

    private void OnClientDisconnectCallback(ulong obj)
    {
        PlayerCount.Value--;
    }

    private void OnClientConnectedCallback(ulong obj)
    {
        PlayerCount.Value++;
    }

    // Singleton
    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Game Manager");
                _instance = go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            Debug.Log($"<b><color=#{UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 0f, 1f, 1f).ToHexString()}>{this.GetType()}</color> instance <color=#eb624d>destroyed</color></b>");
        }
        else
        {
            _instance = this;
            Debug.Log($"<b><color=#{UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f, 1f, 1f).ToHexString()}>{this.GetType()}</color> instance <color=#58ed7d>created</color></b>");
        }
        #endregion
        // end Awake
        _gameTimer = GetComponent<GameTimer>();
    }

    private void Start()
    {
        OnLawEntered += MoveToDebatePhase;
        OnChronoOver += MoveToVotePhase;
    }

    #region AGAGAGAGA
    private void Initialize()
    {
        // assign the number of player
    }

    public void MoveToDebatePhase()
    {
        if (!HasEveryPlayerEnteredALaw()) return;
    }

    public void MoveToVotePhase()
    {
        if (_gameTimer.Timer > 0) return;
    }

    private bool HasEveryPlayerEnteredALaw()
    {
        return (Laws.Count <= 0 && Laws.Count < NumberOfPlayer);
    }

    private void UpdateScreen()
    {
        // update the world pos canva
    }
    #endregion

}

public struct PlayerLaw
{
    public int PlayerID;
    public string Law;

    public PlayerLaw(int playerID, string law)
    {
        PlayerID = playerID;
        Law = law;
    }
}
 