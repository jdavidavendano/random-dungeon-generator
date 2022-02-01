using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;

    [SerializeField]
    public int enemyNumber = 8;

    private GameObject floor;

    private Tilemap tilemap;

    private List<Vector3> availableTiles;

    void Start()
    {
        // Find the floor grid to spawn enemies in the avaliable tiles

        floor = GameObject.Find("Grid/Floor");
        tilemap = floor.GetComponent<Tilemap>();

        availableTiles = new List<Vector3>();

        for (int n = tilemap.cellBounds.xMin; n < tilemap.cellBounds.xMax; n++)
        {
            for (int p = tilemap.cellBounds.yMin; p < tilemap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tilemap.transform.position.y));
                Vector3 place = tilemap.CellToWorld(localPlace);
                if (tilemap.HasTile(localPlace))
                {
                    availableTiles.Add(place);
                }
            }
        }

        // Random instantiate enemies
        for (int newEnemy = 0; newEnemy < enemyNumber; newEnemy++)
        {
            int randomTileIndex = Random.Range(0, availableTiles.Count);
            Vector2 randomTilePosition = new Vector2(
                availableTiles[randomTileIndex].x + 0.5f,
                availableTiles[randomTileIndex].y + 0.5f
            );
            Instantiate(Enemy, randomTilePosition, Quaternion.identity);
        }
    }
}
