using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter bgm;
    private void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("Music").Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
    }

    public void ChangeMusic(int m)
    {
        //bgm.SetParameter("Music", m);
    }

    public void Stop()
    {
        bgm.Stop();
    }
}
