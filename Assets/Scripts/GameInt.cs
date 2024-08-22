using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameInt : ScriptableObject
{
    [SerializeField]
    float gameInt;

    public float value()
    {
        return gameInt;
    }
}
