using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float lifetime;
    public float lifeTimer;
    public float speed;
    Collider2D bulletCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
        if (lifeTimer > 0)
        {
            lifeTimer -= Time.deltaTime;
            if (lifeTimer < 0)
            {
                lifeTimer = 0;
                Die();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Enemy")
        {
            Die();
        }
    }

    public void Restore()
    {
        lifeTimer = lifetime;
        transform.localEulerAngles = Vector3.zero;
    }

    public void Die()
    {
        gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            Breakable b = collision.gameObject.GetComponent<Breakable>();

            if (b != null)
            {
                b.TakeDamage(1);
            }

            PlayerHealth p = collision.gameObject.GetComponent<PlayerHealth>();

            if (p != null)
            {
                p.TakeDamage(1);
            }
            Die();
        }
    }
}
