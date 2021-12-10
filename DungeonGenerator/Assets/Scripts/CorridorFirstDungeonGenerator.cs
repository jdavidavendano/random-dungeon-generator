using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator {

    [SerializeField] private int corridorLength = 14, corridorCount = 5;
    [SerializeField] [Range(0.1f, 1)] private float roomPercent = 0.8f;

    protected override void RunProceduralGeneration() {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration() {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomsPositions = new HashSet<Vector2Int>();
        CreateCorridors(floorPositions, potentialRoomsPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomsPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors) {
        foreach (var position in deadEnds) {
            if (!roomFloors.Contains(position)) {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions) {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions) {
            int neighborsCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList) {
                if (floorPositions.Contains(position + direction)) {
                    neighborsCount++;
                }
            }
            if (neighborsCount == 1) {
                deadEnds.Add(position);
            }
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomsPositions) {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomsPositions.Count * roomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomsPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomsToCreate) {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomsPositions) {
        var currentPosition = startPosition;
        potentialRoomsPositions.Add(currentPosition);

        for (int i = 0; i < corridorCount; i++) {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomsPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
    }
}