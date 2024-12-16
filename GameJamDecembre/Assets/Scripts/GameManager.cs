using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int NumberOfPlayer;

    // list that contains every law
    public List<string> Laws = new();

    // event to move to debate phase ()
    public event Action OnAllLawEntered;

    // event to move to vote phase ()
    public event Action OnChronoOver;

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
    }
    #endregion

    public void MoveToDebatePhase()
    {
        if (!HasEveryPlayerEnteredALaw()) return;
        OnAllLawEntered.Invoke();
    }

    private bool HasEveryPlayerEnteredALaw()
    {
        return (Laws.Count <= 0 && Laws.Count < NumberOfPlayer);
    }
}