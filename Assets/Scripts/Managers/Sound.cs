using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    public string name;

    [Range(0,1)]
    public float volume;
    [Range(0.1f, 3)]
    public float pitch;
    [Range(0, 1)]
    public float pitchRandomness;
    public bool loop = false;

    [HideInInspector]
    public AudioSource source;

}
