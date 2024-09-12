using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class PlayerTarget : ScriptableObject
{
    public PlayerController subject;

    public void RegisterPlayer(PlayerController player)
    {
        subject = player;
    }
    public PlayerController GetInstance()
    {
        return subject;
    }
}
