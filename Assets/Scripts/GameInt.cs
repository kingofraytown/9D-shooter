using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameInt : ScriptableObject
{
    [SerializeField]
    int gameInt;

    public int value()
    {
        return gameInt;
    }
}
