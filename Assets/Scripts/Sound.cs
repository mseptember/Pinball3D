using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
    public string soundName;
    
    public AudioClip clip;
    
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;
    public bool mute;

    [HideInInspector]
    public AudioSource source;

    public AudioMixerGroup group;
}
