using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public music musicManager;
    void Start()
    {
        musicManager = GameObject.FindGameObjectWithTag("Music").GetComponent<music>();
        if (!musicManager.bgm.IsPlaying())
        {
            musicManager.bgm.Play();
        }
        //musicManager.ChangeMusic(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //change to instructions screen 
        SceneManager.LoadScene("Instructions");
    }

    public void EndGame()
    {
        //Exit Game
        Application.Quit();
    }
}
