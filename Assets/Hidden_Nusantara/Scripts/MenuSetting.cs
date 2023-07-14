using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuSetting : MonoBehaviour
{
    public AudioMixer MusicAudioMixer;
    public AudioMixer SfxAudioMixer;
    public AudioMixer UIAudioMixer;
    public GameObject toogleMusicOff;
    public GameObject toogleMusicOn;
    public GameObject toogleAudioOff;
    public GameObject toogleAudioOn;

    private void Start()
    {
        float musicVolume;

        if(MusicAudioMixer.GetFloat("MusicVolume", out musicVolume ) == true)
        {
            if(musicVolume == -18)
            {
                toogleMusicOff.SetActive(true);
                toogleMusicOn.SetActive(false);
            }
            else
            {
                toogleMusicOff.SetActive(false);
                toogleMusicOn.SetActive(true);
            }
        }

        float audioVolume;
        float uiVolume;

        if (SfxAudioMixer.GetFloat("SfxVolume", out audioVolume) == true && UIAudioMixer.GetFloat("UIVolume", out uiVolume) == true)
        {
            if (audioVolume == 5 && uiVolume == -20)
            {
                toogleAudioOff.SetActive(true);
                toogleAudioOn.SetActive(false);
            }
            else
            {
                toogleAudioOff.SetActive(false);
                toogleAudioOn.SetActive(true);
            }
        }
    }

    public void SetMusicVolume(float volume)
    {
        MusicAudioMixer.SetFloat("MusicVolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        SfxAudioMixer.SetFloat("SfxVolume", volume);
    }

    public void setSoundOff(bool choice)
    {
        if (choice == true)
        {
            SfxAudioMixer.SetFloat("SfxVolume", -80);
            UIAudioMixer.SetFloat("UIVolume", -80);
        }
        else
        {
            SfxAudioMixer.SetFloat("SfxVolume", 5);
            UIAudioMixer.SetFloat("UIVolume", -20);
        }
    }
    public void setMusicOff(bool choice)
    {
        if(choice == true)
        {
            MusicAudioMixer.SetFloat("MusicVolume", -80);
        }
        else
        {
            MusicAudioMixer.SetFloat("MusicVolume", -18);
        }
        
    }
}
