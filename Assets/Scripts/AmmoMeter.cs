using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMeter : MonoBehaviour
{
    public DiskHolder[] ammoCrate;

    private void OnEnable()
    {
        PlayerController.TakeAmmoEvent += UpdateAmmoMeter;
    }

    private void OnDisable()
    {
        PlayerController.TakeAmmoEvent -= UpdateAmmoMeter;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAmmoMeter(int[] ammo)
    {
        ammo = AddZeros(ammo);
        ammoCrate[0].ChooseDisk(ammo[0]);
        ammoCrate[1].ChooseDisk(ammo[1]);
        ammoCrate[2].ChooseDisk(ammo[2]);
        ammoCrate[3].ChooseDisk(ammo[3]);
        ammoCrate[4].ChooseDisk(ammo[4]);
        ammoCrate[5].ChooseDisk(ammo[5]);
        ammoCrate[6].ChooseDisk(ammo[6]);
        ammoCrate[7].ChooseDisk(ammo[7]);
        ammoCrate[8].ChooseDisk(ammo[8]);
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
}
