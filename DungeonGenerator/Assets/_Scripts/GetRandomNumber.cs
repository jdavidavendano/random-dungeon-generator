using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRandomNumber : MonoBehaviour
{
    private int seed = 1234567890;

    void Start()
    {

        Random.InitState(seed);
    }

    public int getRandom()
    {

    }

}
