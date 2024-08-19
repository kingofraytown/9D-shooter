using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{

    public PlayerController player;
    public GameObject hp1;
    public GameObject hp2;
    public GameObject hp3;
    public GameObject hp4;
    public GameObject hp5;

    public int health;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PlayerHealth.PlayerDamageEvent += TakeDamage;
        PlayerController.AddHealthEvent += TakeHealth;
        PlayerController.PlayerDeathEvent += Die;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerDamageEvent -= TakeDamage;
        PlayerController.AddHealthEvent -= TakeHealth;
        PlayerController.PlayerDeathEvent -= Die;
    }

    public void Die()
    {
        TakeDamage(10);
    }

    public void TakeHealth(int h)
    {
        health += h;
        switch (health)
        {
            case 5:
                hp1.SetActive(true);
                hp2.SetActive(true);
                hp3.SetActive(true);
                hp4.SetActive(true);
                hp5.SetActive(true);
                break;
            case 4:
                hp1.SetActive(true);
                hp2.SetActive(true);
                hp3.SetActive(true);
                hp4.SetActive(true);
                hp5.SetActive(false);
                break;
            case 3:
                hp1.SetActive(true);
                hp2.SetActive(true);
                hp3.SetActive(true);
                hp4.SetActive(false);
                hp5.SetActive(false);
                break;
            case 2:
                hp1.SetActive(true);
                hp2.SetActive(true);
                hp3.SetActive(false);
                hp4.SetActive(false);
                hp5.SetActive(false);
                break;
            case 1:
                hp1.SetActive(true);
                hp2.SetActive(false);
                hp3.SetActive(false);
                hp4.SetActive(false);
                hp5.SetActive(false);
                break;
            default:
                hp1.SetActive(true);
                hp2.SetActive(true);
                hp3.SetActive(true);
                hp4.SetActive(true);
                hp5.SetActive(true);
                break;
        }
    }

    public void TakeDamage(int d)
    {
        health -= d;
        switch (health)
        {
            case 5:
                hp1.SetActive(true);
                hp2.SetActive(true);
                hp3.SetActive(true);
                hp4.SetActive(true);
                hp5.SetActive(true);
                break;
            case 4:
                hp1.SetActive(true);
                hp2.SetActive(true);
                hp3.SetActive(true);
                hp4.SetActive(true);
                hp5.SetActive(false);
                break;
            case 3:
                hp1.SetActive(true);
                hp2.SetActive(true);
                hp3.SetActive(true);
                hp4.SetActive(false);
                hp5.SetActive(false);
                break;
            case 2:
                hp1.SetActive(true);
                hp2.SetActive(true);
                hp3.SetActive(false);
                hp4.SetActive(false);
                hp5.SetActive(false);
                break;
            case 1:
                hp1.SetActive(true);
                hp2.SetActive(false);
                hp3.SetActive(false);
                hp4.SetActive(false);
                hp5.SetActive(false);
                break;
            default:
                hp1.SetActive(false);
                hp2.SetActive(false);
                hp3.SetActive(false);
                hp4.SetActive(false);
                hp5.SetActive(false);

                break;
        }

        
    }
}
