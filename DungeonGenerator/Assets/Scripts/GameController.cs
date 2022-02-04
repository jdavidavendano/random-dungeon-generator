using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    AbstractDungeonGenerator generator;
    
    void Awake()
    {
        generator = FindObjectOfType<RoomFirstDungeonGenerator>();
        generator.tilemapVisualizer.Clear();
        generator.RunProceduralGeneration();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
