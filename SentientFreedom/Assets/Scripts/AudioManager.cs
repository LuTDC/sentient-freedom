using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Audio[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Audio s in sounds){
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.loop = s.loop;
        }
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Audio s = Array.Find(sounds, sound => sound.name == name);
        s.audioSource.Play();
    }

    public void Stop(string name)
    {
        Audio s = Array.Find(sounds, sound => sound.name == name);
        s.audioSource.Stop();
    }

    public bool isPlaying(string name){
        Audio s = Array.Find(sounds, sound => sound.name == name);

        return s.audioSource.isPlaying;
    }

}
