using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int shield;
    public float damageTime;
    public float damageTimer;

    public delegate void TakeDamageDelegate(int damage);
    public static event TakeDamageDelegate PlayerDamageEvent;

    private void OnEnable()
    {
        ShieldController.PlayerShieldDamageEvent += ShieldTakeDamage;
        PlayerController.ActivateShieldEvent += ShieldActivate;

    }

    private void OnDisable()
    {
        ShieldController.PlayerShieldDamageEvent -= ShieldTakeDamage;
        PlayerController.ActivateShieldEvent -= ShieldActivate;
    }

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
        if (currentState == healthState.Full)
        {
            if (shield <= 0)
            {
                PlayerDamageEvent(d);
                health -= d;
            }
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

    public void ShieldTakeDamage(int d)
    {
        shield -= d;

        if (currentState == healthState.Full)
        {
            currentState = healthState.Damaged;
            damageTimer = 0;
        }

        if (shield < 0)
        {
            shield = 0;
        }
    }
    
    public void ShieldActivate()
    {
        shield = 5;
    }

    void Update()
    {
        if (currentState == healthState.Damaged)
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
