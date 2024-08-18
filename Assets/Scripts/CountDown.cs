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
    public GameObject GameOverPanel;

    public bool timesUp = false;

    //public delegate void CountDownDelegate();
    //public static event CountDownDelegate ZeroTimeEvent;

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

        if(wholeTime < 1)
        {
            wholeTime = 0;
            GameOverPanel.SetActive(true);
            //ZeroTimeEvent();
        }
        countText.text = wholeTime.ToString();
    }
}
