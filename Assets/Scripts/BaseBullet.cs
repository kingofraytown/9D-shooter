using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
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
        if (collision.collider.tag != "Player")
        {
            Die();
        }
    }

    public void Restore()
    {
        lifeTimer = lifetime;
    }

    public void Die()
    {
        gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Breakable b = collision.gameObject.GetComponent<Breakable>();

        if (b != null)
        {
            b.TakeDamage(1);
        }

        Die();
    }
}
