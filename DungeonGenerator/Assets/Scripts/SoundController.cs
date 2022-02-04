using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    
    static AudioClip _playerHit, _sword, _enemyLogHit, _bossHit;
    static AudioSource _audioSource;

    void Start() {
        _playerHit = Resources.Load<AudioClip>("Sounds/PlayerHit");
        _sword = Resources.Load<AudioClip>("Sounds/Sword2");
        _enemyLogHit = Resources.Load<AudioClip>("Sounds/LogHit");
        _bossHit = Resources.Load<AudioClip>("Sounds/BossHit");

        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip) {
        switch(clip) {
            case "PlayerHit":
                _audioSource.PlayOneShot(_playerHit);
                break;
            case "Sword2":
                _audioSource.PlayOneShot(_sword);
                break;
            case "LogHit":
                _audioSource.PlayOneShot(_enemyLogHit);
                break;
            case "BossHit":
                _audioSource.PlayOneShot(_bossHit);
                break;
        }
    }
}