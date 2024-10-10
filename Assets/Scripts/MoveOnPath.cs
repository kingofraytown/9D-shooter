using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MoveOnPath : MonoBehaviour
{
    // Start is called before the first frame update
    public SplineAnimate splineAnimate;
    public GameFloat triggerDistance;
    public float duration;
    public GameObject Camera;
    public float distance;
    public bool triggered = false;

    // Update is called once per frame
    void Update()
    {
        if (!triggered)
        {
            distance = Mathf.Abs(gameObject.transform.position.x - Camera.transform.position.x);
            if (distance <= triggerDistance.value())
            {
                splineAnimate.Duration = duration;
                splineAnimate.Play();
                triggered = true;
            }
        }
    }
}
