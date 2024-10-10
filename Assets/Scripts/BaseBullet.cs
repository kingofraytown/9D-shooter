using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float lifetime;
    public float lifeTimer;
    public float speed;
    public float deathTime;
    public float deathTimer;
    Collider2D bulletCollider;
    public Animator animator;
    public bool enemyBullet;
    public bool decreaseSpeedOverTime;
    public float decreaseSpeedRate;
    public bool increaseSpeedOverTime;
    public float increaseSpeedRate;
    public bool increaseSizeOverTime;
    public float increaseSizeRate;
    public float eSpeed;
    public Vector3 restoreScale;
    public bool bomb;
    public bool hasExplosion;
    public Animator explosionAnimator;

    public enum BulletStates
    {
        Active,
        Hit,
        Blocked,
        Dead
    }

    public BulletStates currentState = BulletStates.Active;

    // Start is called before the first frame update
    void Start()
    {
        restoreScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == BulletStates.Active)
        {
            if (decreaseSpeedOverTime)
            {
                eSpeed -= decreaseSpeedRate * Time.deltaTime;
                if (eSpeed < 0)
                {
                    eSpeed = 0;
                }
            }

            if (increaseSpeedOverTime)
            {
                eSpeed += increaseSpeedRate * Time.deltaTime;
                if (eSpeed > 30)
                {
                    eSpeed = 30;
                }
            }

            if (increaseSizeOverTime)
            {
                transform.localScale = new Vector3(transform.localScale.x + increaseSizeRate * Time.deltaTime, transform.localScale.y + increaseSizeRate * Time.deltaTime, transform.localScale.z + increaseSizeRate * Time.deltaTime);

            }

            transform.position += transform.right * Time.deltaTime * eSpeed;
            if (lifeTimer > 0)
            {
                lifeTimer -= Time.deltaTime;
                if (lifeTimer < 0)
                {
                    if (bomb)
                    {
                        ChangeState(BulletStates.Hit);
                        deathTimer = deathTime;
                    }
                    else
                    {
                        lifeTimer = 0;
                        Die();
                    }
                }
            }
        }

        if (currentState == BulletStates.Blocked || currentState == BulletStates.Hit)
        {
            if(deathTimer > 0)
            {
                deathTimer -= Time.deltaTime;
                if(deathTimer < 0)
                {
                    deathTimer = 0;
                    Die();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string source = "Player";

        if (enemyBullet)
        {
            source = "Enemy";
        }

        if (collision.collider.tag != source) {
            deathTimer = deathTime;
            if (collision.collider.tag == "Unbreakable")
            {
                ChangeState(BulletStates.Blocked);
            }
            else
            {
                ChangeState(BulletStates.Hit);
            }
        }
     
    }

    public void Restore()
    {
        lifeTimer = lifetime;
        eSpeed = speed;
        transform.localScale = restoreScale;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void Die()
    {
        ChangeState(BulletStates.Dead);
        gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Breakable b = collision.gameObject.GetComponent<Breakable>();

        string source = "Player";

        if (enemyBullet)
        {
            source = "Enemy";
        }

        

        if (collision.gameObject.tag != source)
        {
            if (b != null)
            {
                b.TakeDamage(1);
            }
            
            deathTimer = deathTime;
            if (collision.gameObject.tag == "Unbreakable")
            {
                ChangeState(BulletStates.Blocked);
            }
            else
            {
                ChangeState(BulletStates.Hit);
            }

        }

        //Die();
    }

    public void ChangeState(BulletStates bs)
    {
        if(bs != currentState)
        {
            currentState = bs;
            if (animator != null)
            {
                switch (currentState)
                {
                    case BulletStates.Active:
                        //animator.SetTrigger("activate");
                        animator.SetBool("active", true);
                        break;
                    case BulletStates.Hit:
                        animator.SetTrigger("hit_break");
                        animator.SetBool("active", false);
                        if (hasExplosion)
                        {
                            explosionAnimator.SetTrigger("explode");
                        }
                        break;
                    case BulletStates.Blocked:
                        animator.SetTrigger("hit_unbreak");
                        animator.SetBool("active", false);
                        if (hasExplosion)
                        {
                            explosionAnimator.SetTrigger("explode");
                        }
                        break;
                    case BulletStates.Dead:
                        Die();
                        break;

                }
            }
        }
    }
}
