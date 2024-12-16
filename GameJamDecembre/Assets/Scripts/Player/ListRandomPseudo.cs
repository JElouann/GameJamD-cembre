using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListRandomPseudo : MonoBehaviour
{
    public List<string> PseudoInitial;

    public static ListRandomPseudo Instance;

    private void Awake()
    {
        Instance = this;
    }
}