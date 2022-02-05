using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {

    public string _name;
    public AudioClip _clip;
    [Range(0f, 1f)] public float _volume;
    [Range(0.1f, 3)] public float _pitch;
    [HideInInspector] public AudioSource _source;
}