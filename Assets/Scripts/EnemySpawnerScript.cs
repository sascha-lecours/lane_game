using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public Transform[] possibleSpawns;
    public float initialSpawnInterval = 6f; // Time in seconds between waves at start

    private float waveTimer = 0f;
    private float spawnInterval;
    

    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = initialSpawnInterval;
    }

    void spawnSomething()
    {
        // ##TODO: right now this just spawns completely at random. Later this will be more sophisticated
        var index = UnityEngine.Random.Range(0, possibleSpawns.Length);
        var newSpawn = Instantiate(possibleSpawns[index]) as Transform;
        newSpawn.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        waveTimer += Time.deltaTime;
        if (waveTimer >= spawnInterval)
        {
            spawnSomething();
            waveTimer = 0f;
        }
    }
}
