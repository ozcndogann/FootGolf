using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource MusicSource, SFXSource;
    public Sound[] MusicSounds, SFXSounds;
    //public GameObject MusicOn, MusicOff, SFXOn, SFXOff;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(MusicSounds, x => x.name == name);

        if(s == null)
        {

            Debug.Log("müzik bulunamadý");

        }
        else
        {
            MusicSource.clip = s.clip;
            MusicSource.Play();
        }
    }
    public void PlaySFX (string name)
    {
        Sound s = Array.Find(SFXSounds, x => x.name == name);

        if (s == null)
        {

            Debug.Log("müzik bulunamadý");

        }
        else
        {
            SFXSource.PlayOneShot(s.clip);
        }

    }
}
