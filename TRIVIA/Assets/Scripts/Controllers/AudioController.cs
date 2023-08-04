using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Slider slider;
    void Start()
    {
        LoadAudio();
    }
    public void SetAudio(float volume)
    {
        AudioListener.volume = volume;
        SaveAudio();
    }
    public void SaveAudio()
    {
        PlayerPrefs.SetFloat("audioVolume",AudioListener.volume);
    }
    
    public void LoadAudio()
    {
        if (PlayerPrefs.HasKey("audioVolume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("audioVolume");
            slider.value = PlayerPrefs.GetFloat("audioVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("audioVolume",0.1f);
            AudioListener.volume = PlayerPrefs.GetFloat("audioVolume");
            slider.value = PlayerPrefs.GetFloat("audioVolume");
        }
    }
  
}
