using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Audio
{
    public AudioClip clip;
    public string name;
    public bool loop;

    [HideInInspector]
    public AudioSource audioSource;
}
