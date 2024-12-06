using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoMeter : MonoBehaviour 
{
    public DiscImage[] ammoCrate;
    public Color ammoColor1;
    public Color ammoColor2;
    public Color ammoColor3;
    public Color ammoColor4;
    public Color ammoColor5;
    public Color ammoColor6;
    public Color ammoColor7;
    public Color ammoColor8;
    public Color ammoColor9;
    public Color defaultColor;
    public Color noColor;

    public Image outline;
    public Image screen;
    public Image gauge;

    public GameFloat ammoTime;
    public float ammoTimer;
    public bool specialAmmoActive = false;
    public int[] ammoArray;
    public Animator animator;


    private void OnEnable()
    {
        PlayerController.TakeAmmoEvent += UpdateAmmoMeter;
        PlayerController.UseAmmoEvent += UseAmmo;
    }

    private void OnDisable()
    {
        PlayerController.TakeAmmoEvent -= UpdateAmmoMeter;
        PlayerController.UseAmmoEvent -= UseAmmo;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (specialAmmoActive)
        {
            ammoTimer -= Time.deltaTime;
            float gaugeAmount = ammoTimer / ammoTime.value();
            gauge.fillAmount = gaugeAmount;

            if(ammoTimer <= 0)
            {
                specialAmmoActive = false;
                animator.SetBool("active", false);
                UpdateAmmoMeter(ammoArray);
                gauge.fillAmount = 0;
                screen.color = defaultColor;
                outline.color = noColor;
                
            }
        }
    }

    public void UpdateAmmoMeter(int[] ammo)
    {
        ammo = AddZeros(ammo);
        ammoCrate[0].SetDiscType(ammo[0]);
        ammoCrate[1].SetDiscType(ammo[1]);
        ammoCrate[2].SetDiscType(ammo[2]);
        ammoCrate[3].SetDiscType(ammo[3]);
        ammoCrate[4].SetDiscType(ammo[4]);
        ammoCrate[5].SetDiscType(ammo[5]);
        ammoCrate[6].SetDiscType(ammo[6]);
        ammoCrate[7].SetDiscType(ammo[7]);
        ammoCrate[8].SetDiscType(ammo[8]);

        ammoArray = ammo;
    }

    public int[] AddZeros(int[] a)
    {
        int[] temp = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        //check size of array
        if (a.Length == 9)
        {
            return a;
        } else
        {
            for (int i = 0; i < a.Length; i++){
                temp[i] = a[i];
            }
        }

        return temp;

    }

    public void UseAmmo(int[] ammo)
    {
        if (specialAmmoActive == false)
        {
            specialAmmoActive = true;
            ammoTimer = ammoTime.value();
            ammoArray = ammo;
            //color the stuff
            DiscImage.discType dt = ammoCrate[0].currentType;
            Color newColor = noColor;
            switch (dt)
            {
                case DiscImage.discType.cyan:
                    newColor = ammoColor1;
                    break;
                case DiscImage.discType.orange:
                    newColor = ammoColor2;
                    break;
                case DiscImage.discType.green:
                    newColor = ammoColor3;
                    break;
                case DiscImage.discType.purple:
                    newColor = ammoColor4;
                    break;
                case DiscImage.discType.yellow:
                    newColor = ammoColor5;
                    break;
                case DiscImage.discType.red:
                    newColor = ammoColor6;
                    break;
                case DiscImage.discType.grey:
                    newColor = ammoColor7;
                    break;
                case DiscImage.discType.blue:
                    newColor = ammoColor8;
                    break;
                case DiscImage.discType.pink:
                    newColor = ammoColor9;
                    break;
            }

            screen.color = newColor;
            gauge.color = newColor;
            outline.color = newColor;

            gauge.fillAmount = 1f;

            //set animator 
            animator.SetBool("active", true);

        }
    }
}
