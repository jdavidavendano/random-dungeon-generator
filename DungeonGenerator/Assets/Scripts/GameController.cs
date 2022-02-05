using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    AbstractDungeonGenerator generator;
    
    void Awake() {
        generator = FindObjectOfType<RoomFirstDungeonGenerator>();
        generator.tilemapVisualizer.Clear();
        generator.RunProceduralGeneration();
    }

    public void Restart() {
        SceneManager.LoadScene("Menu");
    }
}