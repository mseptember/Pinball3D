using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioManager audioManager;
    public Toggle toggle;

    private void Start()
    {
        audioManager = AudioManager.instance;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        
    }

    public void MuteAudio()
    {
        if (!toggle.isOn)
        {
            audioManager.Mute();
        }
        else
        {
            audioManager.Unmute();
            audioManager.Start();
        }
    }
}
