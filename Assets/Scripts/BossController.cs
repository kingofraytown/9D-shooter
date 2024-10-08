using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float lookRange;
    public GameObject player;
    public GameObject gun;
    public GameObject otherGun;
    public GameObject otherGun2;
    public ItemDropper bag;
    public ObjectPool bullets;
    public ObjectPool otherBullets;
    public ObjectPool otherBullets2;
    public float fireRate;
    public float fireRateTimer;
    public float fireRate2;
    public float fireRateTimer2;
    public float fireRate3;
    public float fireRateTimer3;
    public float targetAngle;
    public float targetAngle2;
    public float targetAngl3;
    public Animator animator;
    public Breakable health;
    public float deathTime;
    public float deathTimer;
    public float distance;
    public bool shootStraight = false;

    public GameObject winPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //check for player distance
        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);

        fireRateTimer += Time.deltaTime;

        //if in range
        if (distance <= lookRange)
        {
            Vector3 direction = player.transform.position - gun.transform.position;
            targetAngle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg - 180;
            Vector3 look = gun.transform.InverseTransformPoint(player.transform.position);
            //targetAngle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
            //face toward player
            //gun.transform.Rotate(0, 0, targetAngle);
            Fire();
        }

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

        if (health.currentState == Breakable.healthState.None)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void Fire()
    {
        if (fireRateTimer >= fireRate)
        {
            GameObject bullet = bullets.GetPooledObject();
            if (bullet != null)
            {
                bullet.GetComponent<EnemyBullet>().Restore();
                bullet.transform.position = gun.transform.position;
                if (shootStraight)
                {
                    bullet.transform.Rotate(0, 0, -180);
                }
                else
                {
                    bullet.transform.Rotate(0, 0, targetAngle);
                }
                bullet.SetActive(true);
                fireRateTimer = 0;
            }

        }
    }

    public void Hit()
    {
        animator.SetTrigger("hit");
    }

    public void BreakCrate()
    {
        animator.SetBool("dead", true);
        bag.DropItems();
    }
}
