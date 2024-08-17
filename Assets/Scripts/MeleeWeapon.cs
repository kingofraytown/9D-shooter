using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Breakable b = collision.gameObject.GetComponent<Breakable>();

        if (b != null)
        {
            b.TakeDamage(1);
        }

    }
}
