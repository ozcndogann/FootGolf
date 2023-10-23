using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSounds : MonoBehaviour
{
    public GameObject MusicOn, MusicOff, SFXOn, SFXOff;
    AudioManager audiomanager;
    static bool SFXbool, Musicbool;

    // Start is called before the first frame update
    void Start()
    {
        audiomanager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        if(audiomanager.SFXSource.mute)
        {
            SFXOn.SetActive(false);
            SFXOff.SetActive(true);
        }
        else
        {
            SFXOn.SetActive(true);
            SFXOff.SetActive(false);
        }


        if (audiomanager.MusicSource.mute)
        {
            MusicOn.SetActive(false);
            MusicOff.SetActive(true);
        }
        else
        {
            MusicOn.SetActive(true);
            MusicOff.SetActive(false);
        }

    }

    public void ToggleSFXOff()
    {
        SFXOn.SetActive(false);
        SFXOff.SetActive(true);
        audiomanager.SFXSource.mute = true;
        SFXbool = false;
    }
    public void ToggleSFXOn()
    {
        SFXOn.SetActive(true);
        SFXOff.SetActive(false);
        audiomanager.SFXSource.mute = false;
        SFXbool = true;
    }

    public void ToggleMusicOff()
    {
        MusicOn.SetActive(false);
        MusicOff.SetActive(true);
        audiomanager.MusicSource.mute = true;
        Musicbool = false;
    }
    public void ToggleMusicOn()
    {
        MusicOn.SetActive(true);
        MusicOff.SetActive(false);
        audiomanager.MusicSource.mute = false;
        Musicbool = true;
    }
}
