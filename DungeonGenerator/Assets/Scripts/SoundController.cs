using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    
    public static AudioClip _playerWalk, _playerHit, _playerDeath, _sword, _enemyLogHit, _bossHit, _bossDeath;
    static AudioSource _audioSource;

    void Start() {
        _playerHit = Resources.Load<AudioClip>("Sounds/PlayerHit");
        _playerWalk = Resources.Load<AudioClip>("Sounds/PlayerWalk");
        _playerDeath = Resources.Load<AudioClip>("Sounds/PlayerDeath");
        _sword = Resources.Load<AudioClip>("Sounds/Sword");
        _enemyLogHit = Resources.Load<AudioClip>("Sounds/LogHit");
        _bossHit = Resources.Load<AudioClip>("Sounds/BossHit");
        _bossDeath = Resources.Load<AudioClip>("Sounds/BossDeath");

        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip) {
        switch(clip) {
            case "PlayerHit":
                _audioSource.PlayOneShot(_playerHit);
                break;
            case "PlayerWalk":
                _audioSource.PlayOneShot(_playerWalk);
                break;
            case "PlayerDeath":
                _audioSource.PlayOneShot(_playerDeath);
                break;
            case "Sword":
                _audioSource.PlayOneShot(_sword);
                break;
            case "LogHit":
                _audioSource.PlayOneShot(_enemyLogHit);
                break;
            case "BossHit":
                _audioSource.PlayOneShot(_bossHit);
                break;
            case "BossDeath":
                _audioSource.PlayOneShot(_bossDeath);
                break;
        }
    }
}