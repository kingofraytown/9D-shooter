using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    public Animator animator;
    public bool randomRotation;

    public void TriggerVFX()
    {
        if (randomRotation)
        {
            float r = Random.Range(0f, 359f);
            transform.rotation = Quaternion.Euler(0, 0, r);
        } else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        animator.SetTrigger("explode");
    }



}
