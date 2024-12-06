using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public Animator animator;
    public int shieldLevel;

    public float damageTime;
    public float damageTimer;
    public bool isHurt;

    public delegate void TakeShieldDamageDelegate(int shieldDamage);
    public static event TakeShieldDamageDelegate PlayerShieldDamageEvent;

    private void OnEnable()
    {
        PlayerController.ActivateShieldEvent += ActivateShield;
    }

    private void OnDisable()
    {
        PlayerController.ActivateShieldEvent -= ActivateShield;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        damageTimer -= Time.deltaTime;

        if (damageTimer <= 0)
        {
            isHurt = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Breakable_Box":
                if (isHurt == false)
                {
                    Breakable b = collision.gameObject.GetComponent<Breakable>();

                    if (b != null)
                    {
                        b.TakeDamage(2);
                    }
                    if (PlayerShieldDamageEvent != null)
                    {
                        PlayerShieldDamageEvent(1);
                    }
                    ShieldHit(1); 
                }

                
                break;
            case "Enemy_Bullet":
                if (isHurt == false)
                {
                    Breakable b = collision.gameObject.GetComponent<Breakable>();

                    if (b != null)
                    {
                        b.TakeDamage(2);
                    }
                    if (PlayerShieldDamageEvent != null)
                    {
                        PlayerShieldDamageEvent(1);
                    }
                    ShieldHit(1);
                }
                break;
        }
    }

    public void ActivateShield()
    {
        shieldLevel = 5;
        animator.SetInteger("shield_level", shieldLevel);
    }

    public void ShieldHit(int d)
    {
        shieldLevel -= d;
        if(shieldLevel < 0)
        {
            shieldLevel = 0;
        }

        animator.SetInteger("shield_level", shieldLevel);
        animator.SetTrigger("hit");
        isHurt = true;
        damageTimer = damageTime;
    }
}
