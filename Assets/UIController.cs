using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider MusicSlider, SFXSlider;

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleFSX();

    }
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(MusicSlider.value);
    }
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(SFXSlider.value);
    }
}
