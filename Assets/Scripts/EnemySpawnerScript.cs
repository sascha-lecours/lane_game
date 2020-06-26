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
    public float shrinkTimeInterval = 0.15f; // Amount of time wave timer shortens once it starts to
    public int wavesPerIntervalShrink = 6; // Once hard waves start, the interval will shrink each time this many waves pass

    private int waveCounter = 0;
    private float waveTimer = 0f;
    private float spawnInterval;
    private int mediumWavesStart;
    private int hardWavesStart;
    private int intervalCounter = 0;
    private float minimumInterval = 3.5f; // Won't shrink any smaller than this.
    

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
        if (waveCounter >= hardWavesStart) {
            currentDifficulty = hardSpawns;
            intervalCounter += 1;
        }
        var index = UnityEngine.Random.Range(0, currentDifficulty.Length);
        var newSpawn = Instantiate(currentDifficulty[index]) as Transform;
        newSpawn.position = transform.position;

        if (intervalCounter >= wavesPerIntervalShrink)
        {
            // Shrink the timer interval each time until it reaches the minimum
            spawnInterval = Math.Max(minimumInterval, spawnInterval - shrinkTimeInterval);
            intervalCounter = 0;
        }
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
