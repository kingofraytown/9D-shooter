using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public int health;
    public float damageTime;
    public float damageTimer;


    public enum healthState
    {
        Full,
        Damaged,
        Destroyed,
        None
    }

    public healthState currentState = healthState.Full;

    public void TakeDamage(int d)
    {
        if (currentState != healthState.Destroyed)
        {
            health -= d;
            if (health <= 0)
            {
                currentState = healthState.Destroyed;
            }
            if (currentState == healthState.Full)
            {
                currentState = healthState.Damaged;
                damageTimer = 0;
            }
        }
    }

    void Update()
    {
        if(currentState == healthState.Damaged)
        {
            if (damageTimer >= damageTime)
            {
                currentState = healthState.Full;
            }
            else
            {
                damageTimer += Time.deltaTime;
            }
        }
    }
}
