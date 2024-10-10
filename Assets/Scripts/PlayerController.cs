using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float smoothness = 0.1f;
    public Vector2 _movement;
    Animator animator;

    public Gun[] guns; 
    public bool isFiringBullets = false;
    public Transform turret;

    public PlayerHealth health;
    public bool isHurt = false;
    public float hurtTime;
    public float hurtTimer;
    public float damageBounce;
    public List<int> ammoCrate;
    public GameObject GameOverPanel;
    public GameObject WinPanel;

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
        Ammo4,
        Ammo5,
        Ammo6,
        Ammo7,
        Ammo8,
        Ammo9
    }

    public AmmoStates currentAmmoState = AmmoStates.Normal;

    public float specialAmmoTime;
    public float specialAmmoTimer;

    public float meleeTime;
    public float meleeTimer;
    public Animator meleeAnimator;
    public bool meleeAttack;
    public float deathTime;
    public float deathTimer;

    [Header("Camera Bounds")]
    public Transform cameraFollow;
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -4f;
    public float maxY = 4f;
    public PlayerTarget playerTarget;

    Vector3 currentVelocity;
    Vector3 targetPosition;




    Rigidbody2D _rigidbody2D;


    //public delegate void TakeDamageDelegate(int damage);
    //public static event TakeDamageDelegate PlayerDamageEvent;

    public delegate void AddHealthDelegate(int health);
    public static event AddHealthDelegate AddHealthEvent;

    public delegate void TakeAmmoDelegate(int[] ammo);
    public static event TakeAmmoDelegate TakeAmmoEvent;

    public delegate void PlayerDeathDelegate();
    public static event PlayerDeathDelegate PlayerDeathEvent;

    private void OnEnable()
    {
        PlayerHealth.PlayerDamageEvent += PlayerHit;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerDamageEvent -= PlayerHit;
    }


    // Start is called before the first frame update
    void Start()
    {
        playerTarget.RegisterPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();

        Move(_movement);

        if (isFiringBullets)
        {
            Fire();
        }

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
                CurrentGun().StopFiring();
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

    public void CheckHealth()
    {
        if (health.currentState == PlayerHealth.healthState.Damaged)
        {
            if (isHurt == false)
            {
                //PlayerHit();
                //PlayerDamageEvent(1);
            }
        }

        if (health.currentState == PlayerHealth.healthState.Destroyed)
        {
            //BreakCrate();
            if (PlayerDeathEvent != null)
            {
                PlayerDeathEvent();
            }
           
            GameOverPanel.SetActive(true);
            deathTimer = deathTime;
            health.currentState = PlayerHealth.healthState.None;
        }

        if (health.currentState == PlayerHealth.healthState.None)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }


    public void OnMove(InputAction.CallbackContext cc)
    {
        _movement = cc.ReadValue<Vector2>();
        //Debug.Log(_movement);
    }

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Move(Vector2 direction)
    {
        if (!isHurt)
        {
            targetPosition += new Vector3(direction.x, direction.y, 0) * (speed * Time.deltaTime);

            //calculate the movement bounds
            float minPlayerX = cameraFollow.position.x + minX;
            float maxPlayerX = cameraFollow.position.x + maxX;
            float minPlayerY = cameraFollow.position.y + minY;
            float maxPlayerY = cameraFollow.position.y + maxY;

            //clamp the player position to the camera view
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPlayerX, maxPlayerX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPlayerY, maxPlayerY);

            //lerp the positions
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);

            //Vector2 pos = transform.position;
            //pos += direction * (speed * Time.deltaTime);

            //pos.x = Mathf.Clamp(pos.x, min.x, max.x);
            //pos.y = Mathf.Clamp(pos.y, min.y, max.y);

           // transform.position = pos;
        }



    }

    public void OnFire(InputAction.CallbackContext cc)
    {
        Debug.Log(cc);
        if (cc.started)
        {
            isFiringBullets = true;
        }
        if (cc.performed)
        {

            isFiringBullets = true;
        }

        //if(_movement == Vector2.zero)
        //{
            if (cc.canceled)
            {
                Debug.Log("Fire Action Canceled");
                isFiringBullets = false;
                CurrentGun().StopFiring();
            }
        //}

        
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
                if(TakeAmmoEvent != null)
                {
                    TakeAmmoEvent(tempArray);
                }
                
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
                    case 5:
                        currentAmmoState = AmmoStates.Ammo5;
                        break;
                    case 6:
                        currentAmmoState = AmmoStates.Ammo6;
                        break;
                    case 7:
                        currentAmmoState = AmmoStates.Ammo7;
                        break;
                    case 8:
                        currentAmmoState = AmmoStates.Ammo8;
                        break;
                    case 9:
                        currentAmmoState = AmmoStates.Ammo9;
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

    public void OnFireRelease(InputAction.CallbackContext cc)
    {
       /* if (cc.started)
        {
            //Debug.Log("Fire Action Release Started");
            //isFiringBullets = false;
            //CurrentGun().StopFiring();
        }
        if (cc.performed)
        {
            Debug.Log("Fire Action Release Performed");
            isFiringBullets = false;
            CurrentGun().StopFiring();
        }

        if (cc.canceled)
        {
            Debug.Log("Fire Action Release Canceled");
            //isFiringBullets = false;
            //CurrentGun().StopFiring();
        }*/
    }

    public Gun CurrentGun()
    {
        Gun g;
        switch (currentAmmoState)
        {
            case AmmoStates.Ammo1:
                g = guns[1];
                break;
            case AmmoStates.Ammo2:
                g = guns[2];
                break;
            case AmmoStates.Ammo3:
                g = guns[3];
                break;
            case AmmoStates.Ammo4:
                g = guns[4];
                break;
            case AmmoStates.Ammo5:
                g = guns[5];
                break;
            case AmmoStates.Ammo6:
                g = guns[6];
                break;
            case AmmoStates.Ammo7:
                g = guns[7];
                break;
            case AmmoStates.Ammo8:
                g = guns[8];
                break;
            case AmmoStates.Ammo9:
                g = guns[9];
                break;
            default:
                g = guns[0];
                break;
        }
        return g;
    }

    void Fire() {
        CurrentGun().Fire();
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

                    health.TakeDamage(1);

                    //PlayerHit();

                    //PlayerDamageEvent(1);
                }
                break;
            case "Health":
                if (AddHealthEvent != null) {
                    AddHealthEvent(1);
                }
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
            case "Ammo 5":
                if (ammoCrate.Count < 9)
                {
                    LoadAmmo(5);
                    collision.gameObject.SetActive(false);
                }
                break;
            case "Ammo 6":
                if (ammoCrate.Count < 9)
                {
                    LoadAmmo(6);
                    collision.gameObject.SetActive(false);
                }
                break;
            case "Ammo 7":
                if (ammoCrate.Count < 9)
                {
                    LoadAmmo(7);
                    collision.gameObject.SetActive(false);
                }
                break;
            case "Ammo 8":
                if (ammoCrate.Count < 9)
                {
                    LoadAmmo(8);
                    collision.gameObject.SetActive(false);
                }
                break;
            case "Ammo 9":
                if (ammoCrate.Count < 9)
                {
                    LoadAmmo(9);
                    collision.gameObject.SetActive(false);
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

                    //PlayerHit();

                    //PlayerDamageEvent(1);
                }
                break;
            case "Goal":
                WinPanel.SetActive(true);
                break;

        }
    }

    public void LoadAmmo(int a)
    {
        ammoCrate.Add(a);
        int[] tempArray = ammoCrate.ToArray();
        if(TakeAmmoEvent != null)
        {
            TakeAmmoEvent(tempArray);
        }
        
    }

    public void PlayerHit(int d)
    {
        if (isHurt == false)
        {
            isHurt = true;
            hurtTimer = hurtTime;
            gameObject.transform.Translate(new Vector3(-1 * damageBounce, 0));
        }

    }
}
