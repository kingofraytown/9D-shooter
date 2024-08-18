using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverController : MonoBehaviour
{

    public GameObject GameOverPanel;

    private void OnEnable()
    {
        //CountDown.ZeroTimeEvent += ShowPanel;
    }

    private void OnDisable()
    {
        //CountDown.ZeroTimeEvent -= ShowPanel;
    }

    public void ShowPanel()
    {
        GameOverPanel.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
