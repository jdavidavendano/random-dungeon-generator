using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour {
    
    [SerializeField] Image[] _hearts;
    [SerializeField] Sprite _fullHeart;
    [SerializeField] Sprite _halfHeart;
    [SerializeField] Sprite _emptyHeart;
    [SerializeField] FloatValue _heartContainers;
    [SerializeField] FloatValue _playerCurrentHealth;

    void Start() {
        InitHearts();
    }

    public void InitHearts() {
        for(int i = 0; i < _heartContainers._initialValue; i++) {
            _hearts[i].gameObject.SetActive(true);
            _hearts[i].sprite = _fullHeart;
        }
    }

    public void UpdateHearts() {
        float tempHealth = _playerCurrentHealth._runTimeValue / 2;

        for( int i = 0; i < _heartContainers._initialValue; i++) {
            if(i <= tempHealth - 1) {
                // Full
                _hearts[i].sprite = _fullHeart;
            }
            else if(i >= tempHealth) {
                // Vacío
                _hearts[i].sprite = _emptyHeart;
            }
            else {
                // Mitad
                _hearts[i].sprite = _halfHeart;
            }
        }
    }
}