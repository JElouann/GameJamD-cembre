using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public List<Transform> SpawnPoints;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
