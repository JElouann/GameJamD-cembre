using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float Timer { get; private set; }
    private Dictionary<PhaseEnum, Delegate> gr;
     
    public void LaunchTimer(float time)
    {
        Timer = time;
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
