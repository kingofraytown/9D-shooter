using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinGame()
    {
        SceneManager.LoadScene("Ending");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void Title()
    {
        SceneManager.LoadScene("Title");
    }
}
