using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public float speed;
    public GameObject Camera;
    public float triggerDistance;
    public float distance;

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
