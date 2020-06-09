using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnOneEnemyWaveScript : MonoBehaviour
{
    public Transform[] enemies;
    public float yAxisVariance = 5f; // Units above and below Y axis for spawn range.
    public float xSpacing = 2f; // Distance to move "back" before placing each item



    // Start is called before the first frame update
    void Start()
    {
        for(var index=0; index < enemies.Length; index++)
        {
            var newSpawn = Instantiate(enemies[index]) as Transform;
            var newY = transform.position.y + UnityEngine.Random.Range(-yAxisVariance, yAxisVariance);
            var newX = transform.position.x + index * xSpacing;
            newSpawn.position = new Vector3 (newX, newY, 0);
        }

        Destroy(gameObject);
    }
}
