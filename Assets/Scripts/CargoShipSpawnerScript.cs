using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoShipSpawnerScript : MonoBehaviour
{
    public Transform[] possibleSpawns;
    public float initialSpawnInterval = 2f; // Time in seconds between waves at start
    public float horizontalVariance = 0.4f;

    private float waveTimer = 0f;
    private float spawnInterval;
     


    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = initialSpawnInterval;
    }

    void spawnSomething()
    {
        var index = UnityEngine.Random.Range(0, possibleSpawns.Length);
        var newSpawn = Instantiate(possibleSpawns[index]) as Transform;
        var tempX = transform.position.x + UnityEngine.Random.Range(-horizontalVariance, horizontalVariance);
        var tempY = transform.position.y;
        newSpawn.position = new Vector3(tempX, tempY, 0);
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
