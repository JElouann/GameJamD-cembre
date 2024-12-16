using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int NumberOfPlayer;

    // list that contains every law
    public List<string> Laws = new();

    // TODO : liste de portes

    // event to move to debate phase (if conditions) (proc when player enter law)
    public event Action OnLawEntered;

    // event to move to vote phase ()
    public event Action OnChronoOver;

    private GameTimer _gameTimer;

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
}
 