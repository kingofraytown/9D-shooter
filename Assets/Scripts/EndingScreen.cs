using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main Game");
    }
}
