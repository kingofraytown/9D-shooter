using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoss : MonoBehaviour
{
    public float speed;
    public GameObject Camera;
    public float triggerDistance;
    public float distance;
    public float phase1Time;
    public float phase2Time;
    public float phaseTimer;
    public bool phase1 = false;
    public bool phase2 = false;


    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Abs(gameObject.transform.position.x - Camera.transform.position.x);

        if (distance <= triggerDistance)
        {
            gameObject.transform.position = transform.position += transform.right * Time.deltaTime * speed;
        }
    }
}
