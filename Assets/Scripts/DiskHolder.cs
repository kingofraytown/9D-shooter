using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskHolder : MonoBehaviour
{
    public GameObject[] holder;

    public void ChooseDisk(int d)
    {
        switch (d)
        {
            case 1:
                holder[0].SetActive(true);
                holder[1].SetActive(false);
                holder[2].SetActive(false);
                holder[3].SetActive(false);
                break;
            case 2:
                holder[0].SetActive(false);
                holder[1].SetActive(true);
                holder[2].SetActive(false);
                holder[3].SetActive(false);
                break;
            case 3:
                holder[0].SetActive(false);
                holder[1].SetActive(false);
                holder[2].SetActive(true);
                holder[3].SetActive(false);
                break;
            case 4:
                holder[0].SetActive(false);
                holder[1].SetActive(false);
                holder[2].SetActive(false);
                holder[3].SetActive(true);
                break;
            default:
                holder[0].SetActive(false);
                holder[1].SetActive(false);
                holder[2].SetActive(false);
                holder[3].SetActive(false);
                break;
        }
    }
}
