using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public Transform[] easySpawns;
    public Transform[] mediumSpawns;
    public Transform[] hardSpawns;
    public int numberOfEasyWaves = 4;
    public int numberOfMediumWaves = 6;
    public int numberOfHardWaves = 6;
    public float initialSpawnInterval = 6f; // Time in seconds between waves at start

    private int waveCounter = 0;
    private float waveTimer = 0f;
    private float spawnInterval;
    private int mediumWavesStart;
    private int hardWavesStart;
    

    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = initialSpawnInterval;
        mediumWavesStart = numberOfEasyWaves;
        hardWavesStart = numberOfEasyWaves + numberOfMediumWaves;
    }

    void spawnSomething()
    {
        var currentDifficulty = easySpawns;
        if (waveCounter >= mediumWavesStart) { currentDifficulty = mediumSpawns; }
        if (waveCounter >= hardWavesStart) { currentDifficulty = hardSpawns; }
        var index = UnityEngine.Random.Range(0, currentDifficulty.Length);
        var newSpawn = Instantiate(currentDifficulty[index]) as Transform;
        newSpawn.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        waveTimer += Time.deltaTime;
        if (waveTimer >= spawnInterval)
        {
            waveCounter++;
            spawnSomething();
            waveTimer = 0f;
        }
    }
}
