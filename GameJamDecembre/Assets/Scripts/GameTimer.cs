using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float Timer;

    [SerializeField] private float _debateTime;
    [SerializeField] private float _voteTime;

    public void LaunchTimer(PhaseEnum whichPhase)
    {
        switch (whichPhase)
        {
            case PhaseEnum.Debate:
                Timer = _debateTime;
                break;

            case PhaseEnum.Vote:
                Timer = _voteTime;
                break;
        }
    }

    private void Update()
    {
        Timer -= 0.01f;
    }
}

public enum PhaseEnum
{
    Start,
    Debate,
    Vote
}
