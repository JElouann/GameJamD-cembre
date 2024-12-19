using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public struct PathStep
{
    public Transform Transform;
    public Ease Ease;
    public float Time;
}