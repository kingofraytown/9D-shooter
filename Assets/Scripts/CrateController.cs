using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : MonoBehaviour
{
    // Start is called before the first frame update
    public Breakable health;
    public Animator animator;
    public GameObject[] drops;
    public float deathTime;
    public float deathTimer;
    public bool isBroken = false;
    public ItemDropper bag;
    public VFXController explosion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health.currentState == Breakable.healthState.Damaged)
        {
            Hit();
        }

        if (health.currentState == Breakable.healthState.Destroyed)
        {
            BreakCrate();
            deathTimer = deathTime;
            health.currentState = Breakable.healthState.None;
        }

        if(health.currentState == Breakable.healthState.None)
        {
            deathTimer -= Time.deltaTime;
            if(deathTimer <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        
    }

    public void Hit()
    {
        animator.SetTrigger("Hit");
    }

    public void BreakCrate()
    {
        explosion.TriggerVFX();
        animator.SetBool("Broken", true);
        bag.DropItems();
    }
}
