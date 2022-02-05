using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    
    [SerializeField] Sound[] _sounds;

    void Awake() {
        foreach(Sound s in _sounds) {
            s._source = gameObject.AddComponent<AudioSource>();
            s._source.clip = s._clip;

            s._source.volume = s._volume;
            s._source.pitch = s._pitch;
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(_sounds, sound => sound._name == name);
        s._source.Play();
    }
}