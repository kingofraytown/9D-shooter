using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLaser : MonoBehaviour
{
    Collider2D bulletCollider;
    public Animator animator;
    public bool enemyLaser;
    public GameFloat lifeTime;
    public float lifeTimer;

    public enum LaserStates
    {
        Active,
        Inactive
    }

    public LaserStates currentState = LaserStates.Inactive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == LaserStates.Active)
        {
            lifeTimer += Time.deltaTime;
        
            if (lifeTimer >= lifeTime.value())
            {
                EndLaser();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Breakable b = collision.gameObject.GetComponent<Breakable>();

        string source = "Player";

        if (enemyLaser)
        {
            source = "Enemy";
        }



        if (collision.gameObject.tag != source)
        {
            if (b != null)
            {
                b.TakeDamage(1);
            }

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Breakable b = collision.gameObject.GetComponent<Breakable>();

        string source = "Player";

        if (enemyLaser)
        {
            source = "Enemy";
        }



        if (collision.gameObject.tag != source)
        {
            if (b != null)
            {
                b.TakeDamage(1);
            }

        }

    }

    public void StartLaser()
    {
        if (currentState != LaserStates.Active)
        {
            animator.SetTrigger("start");
            currentState = LaserStates.Active;
            lifeTimer = 0;
        }
    }

    public void EndLaser()
    {
        if (currentState != LaserStates.Inactive)
        {
            animator.SetTrigger("end");
            currentState = LaserStates.Inactive;
        }
    }
}
