using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameFloat : ScriptableObject
{
    [SerializeField]
    float gameFloat;

    public float value()
    {
        return gameFloat;
    }
}
