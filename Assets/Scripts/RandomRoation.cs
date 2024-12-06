using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoation : MonoBehaviour
{
    // Start is called before the first frame update
    public float minRotation;
    public float maxRotation;

    void OnEnable()
    {
        float angle = Random.Range(minRotation, maxRotation);
        Vector3 newAngle = new Vector3(0f, 0f, angle);
        gameObject.transform.eulerAngles = newAngle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
