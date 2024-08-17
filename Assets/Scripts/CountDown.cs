using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDown : MonoBehaviour
{
    public TMP_Text countText;
    public int timer;
    public float realTime;

    // Start is called before the first frame update
    void Start()
    {
        realTime = timer;
    }

    // Update is called once per frame
    void Update()
    {
        realTime -= Time.deltaTime;

        int wholeTime = Mathf.CeilToInt(realTime);
        countText.text = wholeTime.ToString();
    }
}
