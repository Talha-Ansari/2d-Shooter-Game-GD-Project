using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public Sound[] sound;

    public static AudioManager instance;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        foreach (Sound s in sound)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;
            s.audioSource.pitch = s.pitch;
            s.audioSource.volume = s.volumn;
            s.audioSource.loop = s.loop;
        }
    }
    private void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + "  is not found");
            return;
        }
        s.audioSource.Play();
    }
}
