using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenuScript : MonoBehaviour {

    public AudioSource menuMusic;
    public static AudioMenuScript instance = null;

    void Awake()
    {
        if (instance == null)

            instance = this;

        else if (instance != this)

            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayMenuMusic()
    {
        if (menuMusic.isPlaying) return;
        menuMusic.Play();
    }

    public void StopMenuMusic()
    {
        menuMusic.Stop();
    }

}