using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Start()
    {
        InitAudio();
    }

    private void InitAudio()
    {
        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        if (sound == null) {
            Debug.LogWarning("Sound \"" + name + "\" not found!");
            return;
        }
        sound.source.Play();
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume;
    [Range(1f, 3f)] public float pitch;
    public bool loop;
    [HideInInspector] public AudioSource source;
}
