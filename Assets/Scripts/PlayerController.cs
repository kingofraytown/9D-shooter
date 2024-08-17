using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Vector2 _movement;
    Animator animator;
    public ObjectPool bulletPool;
    public ObjectPool ammo1Pool;
    public ObjectPool ammo2Pool;
    public ObjectPool ammo3Pool;
    public ObjectPool ammo4Pool;
    public bool isFiringBullets = false;
    public Transform turret;
    public float fireRateInterval;
    public float fireRateTimer;
    public Breakable health;
    public bool isHurt = false;
    public float hurtTime;
    public float hurtTimer;
    public float damageBounce;
    public List<int> ammoCrate;


    public enum PlayerStates
    {
        Idle,
        Moving,
        Hurt,
        Dead
    }

    public PlayerStates currentState = PlayerStates.Idle;

    public enum AmmoStates
    {
        Normal,
        Ammo1,
        Ammo2,
        Ammo3,
        Ammo4
    }

    public AmmoStates currentAmmoState = AmmoStates.Normal;

    public float ammo1FireRate;
    public float ammo2FireRate;
    public float ammo3FireRate;
    public float ammo4FireRate;

    public float specialAmmoTime;
    public float specialAmmoTimer;

    public float meleeTime;
    public float meleeTimer;
    public Animator meleeAnimator;
    public bool meleeAttack;


    Rigidbody2D _rigidbody2D;


    public delegate void TakeDamageDelegate(int damage);
    public static event TakeDamageDelegate PlayerDamageEvent;

    public delegate void AddHealthDelegate(int health);
    public static event AddHealthDelegate AddHealthEvent;

    public delegate void TakeAmmoDelegate(int[] ammo);
    public static event TakeAmmoDelegate TakeAmmoEvent;

    public delegate void PlayerDeathDelegate();
    public static event PlayerDeathDelegate PlayerDeathEvent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(_movement);
        if (isFiringBullets)
        {
            Fire();
        }

        fireRateTimer += Time.deltaTime;

        if (isHurt)
        {
            hurtTimer -= Time.deltaTime;

            if(hurtTimer <= 0)
            {
                isHurt = false;
            }

        }

        if(currentAmmoState != AmmoStates.Normal)
        {
            specialAmmoTimer -= Time.deltaTime;

            if (specialAmmoTimer <= 0)
            {
                currentAmmoState = AmmoStates.Normal;
            }
        }

        if (meleeAttack)
        {
            meleeTimer -= Time.deltaTime;
            if(meleeTimer <= 0)
            {
                meleeAttack = false;
            }
        }


    }


    public void OnMove(InputAction.CallbackContext cc)
    {
        _movement = cc.ReadValue<Vector2>();
       // Debug.Log(_movement);
    }

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Move(Vector2 direction)
    {
        if (!isHurt)
        {
            Vector2 min = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ScreenToWorldPoint(new Vector2(1, 1));

            max.x = max.x - 0.14f;
            min.x = min.x + 0.14f;
            max.y = max.y - 0.28f;
            min.y = min.y + 0.28f;

            Vector2 pos = transform.position;
            pos += direction * (speed * Time.deltaTime);

            //pos.x = Mathf.Clamp(pos.x, min.x, max.x);
            //pos.y = Mathf.Clamp(pos.y, min.y, max.y);

            transform.position = pos;
        }



    }

    public void OnFire(InputAction.CallbackContext cc)
    {
        if (cc.started)
        {
            isFiringBullets = true;
        }
        if (cc.performed)
        {
            isFiringBullets = true;
        }
        if (cc.canceled)
        {
            isFiringBullets = false;
        }
    }

    public void OnSpecialAttack(InputAction.CallbackContext cc)
    {

        if (cc.performed && (currentAmmoState == AmmoStates.Normal))
        {
            if (ammoCrate.Count > 0)
            {
                Debug.Log("Special Attack");


                int newAmmo = ammoCrate[0];
                ammoCrate.RemoveAt(0);
                int[] tempArray = ammoCrate.ToArray();
                TakeAmmoEvent(tempArray);
                specialAmmoTimer = specialAmmoTime;

                switch (newAmmo)
                {
                    case 1:
                        currentAmmoState = AmmoStates.Ammo1;
                        break;
                    case 2:
                        currentAmmoState = AmmoStates.Ammo2;
                        break;
                    case 3:
                        currentAmmoState = AmmoStates.Ammo3;
                        break;
                    case 4:
                        currentAmmoState = AmmoStates.Ammo4;
                        break;
                }
            }
        }
    }

    public void OnMelee(InputAction.CallbackContext cc)
    {
        if (cc.performed && !meleeAttack)
        {
            Debug.Log("swing that sword");
            meleeAnimator.SetTrigger("attack");
            meleeTimer = meleeTime;
            meleeAttack = true;

        }
    }

    public ObjectPool currentBulletPool()
    {
        ObjectPool r = bulletPool;
        switch (currentAmmoState) {
            case AmmoStates.Ammo1:
                r = ammo1Pool;
                break;
            case AmmoStates.Ammo2:
                r = ammo2Pool;
                break;
            case AmmoStates.Ammo3:
                r = ammo3Pool;
                break;
            case AmmoStates.Ammo4:
                r = ammo4Pool;
                break;
            default:
                r = bulletPool;
                break;
        }


        return r;
    }

    void Fire() {
      if (fireRateTimer >= GetFireRate()) {
        GameObject bullet = currentBulletPool().GetPooledObject();
        if (bullet != null)
        {
            bullet.GetComponent<BaseBullet>().Restore();
            bullet.transform.position = turret.position;
            bullet.SetActive(true);
            fireRateTimer = 0;
         }

      }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag) {
            case "Breakable_Box":
                if (isHurt == false)
                {
                    Breakable b = collision.gameObject.GetComponent<Breakable>();

                    if (b != null)
                    {
                        b.TakeDamage(2);
                    }

                    PlayerHit();

                    PlayerDamageEvent(1);
                }
                break;
            case "Health":
                AddHealthEvent(1);
                collision.gameObject.SetActive(false);
                break;
            case "Ammo 1":
                if (ammoCrate.Count < 9)
                {
                    LoadAmmo(1);
                    collision.gameObject.SetActive(false);
                }
                break;
            case "Ammo 2":
                if (ammoCrate.Count < 9)
                {
                    LoadAmmo(2);
                    collision.gameObject.SetActive(false);
                }
                break;
            case "Ammo 3":
                if (ammoCrate.Count < 9)
                {
                    LoadAmmo(3);
                    collision.gameObject.SetActive(false);
                }
                break;
            case "Ammo 4":
                if (ammoCrate.Count < 9)
                {
                    LoadAmmo(4);
                    collision.gameObject.SetActive(false);
                }
                break;

        }
    }

    public void LoadAmmo(int a)
    {
        ammoCrate.Add(a);
        int[] tempArray = ammoCrate.ToArray();
        TakeAmmoEvent(tempArray);
    }

    public void PlayerHit()
    {
        if (isHurt == false)
        {
            isHurt = true;
            hurtTimer = hurtTime;
            gameObject.transform.Translate(new Vector3(-1 * damageBounce, 0));
        }

    }

    public float GetFireRate()
    {
        float rate;
        switch (currentAmmoState)
        {
            case AmmoStates.Ammo1:
                rate = ammo1FireRate;
                break;
            case AmmoStates.Ammo2:
                rate = ammo2FireRate;
                break;
            case AmmoStates.Ammo3:
                rate = ammo3FireRate;
                break;
            case AmmoStates.Ammo4:
                rate = ammo4FireRate;
                break;
            default:
                rate = fireRateInterval;
                break;
        }
        return rate;
    }
}
