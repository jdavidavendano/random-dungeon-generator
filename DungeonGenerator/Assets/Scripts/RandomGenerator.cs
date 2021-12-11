using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomGenerator {

    [SerializeField] private static int seed = 0;

    static void Awake() {
        Random.InitState(seed);
    }

    public static int GenerateRandom(int min, int max) {
        return Random.Range(min, max);
    }
}