using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
    
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("Deleted instance of AudioManager");
            Destroy(gameObject);
            return;
        }
        
        // zapobiega zatrzymywania się main theme po przejściu między scenami
        DontDestroyOnLoad(gameObject);
        
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.mute = sound.mute;
            
            sound.source.outputAudioMixerGroup = sound.group;
        }
        
    }

    public void Start()
    {
        instance.Play("Theme");
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(instance.sounds, s => s.soundName == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        sound.source.Play();
    }

    public void Mute()
    {
        foreach (var sound in instance.sounds)
        {
            Debug.Log(sound.source);
            sound.source.mute = true;
        }
    }

    public void Unmute()
    {
        foreach (var sound in instance.sounds)
        {
            Debug.Log(sound.source);
            sound.source.mute = false;
        }
    }
}
