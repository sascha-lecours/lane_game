using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;
using UnityEngine;

public class PlayerSpawnableType
{
    public Transform UnitType { get; set; }

    public float SpawnInterval { get; set; }

    public float RebuildTime { get; set; }

    public int MaxSupply { get; set; }

    public int StartSupply { get; set; }

    public float SpawnCooldown { get; set; }

    public float RebuildTimer { get; set; }

    public int CurSupply { get; set; }

    public PlayerSpawnableType(Transform unitType, float spawnInterval, float rebuildTime, int maxSupply, int startSupply)
    {
        UnitType = unitType;
        SpawnInterval = spawnInterval;
        RebuildTime = rebuildTime;
        MaxSupply = maxSupply;
        StartSupply = startSupply;

        CurSupply = StartSupply;
        RebuildTimer = 0f;
        SpawnCooldown = 0f;
    }
}

public class FriendlySpawnerScript : MonoBehaviour
{
    private PlayerSpawnableType[] Spawnables = { null, null };

    public Transform unitType1;
    public Transform unitType2;

    public float spawnInterval1 = 0.45f;
    public float spawnInterval2 = 0.25f;

    public float rebuildTime1 = 2.5f;
    public float rebuildTime2 = 1.5f;

    public int maxSupply1 = 6;
    public int maxSupply2 = 4;

    public int startSupply1 = 4;
    public int startSupply2 = 2;

    private float spawnCooldown1;
    private float spawnCooldown2;

    private float rebuildTimer1;
    private float rebuildTimer2;

    public int curSupply1;
    public int curSupply2;





    // Start is called before the first frame update
    void Start()
    {
        Spawnables[0] = new PlayerSpawnableType(unitType1, spawnInterval1, rebuildTime1, maxSupply1, startSupply1);
        Spawnables[1] = new PlayerSpawnableType(unitType2, spawnInterval2, rebuildTime2, maxSupply2, startSupply2);
    }



    void spawnUnit(Transform s)
    {
        var newSpawn = Instantiate(s) as Transform;
        var fs = newSpawn.GetComponent<FriendlyShipScript>() as FriendlyShipScript;
        newSpawn.position = transform.position;


        // TODO: Set shooting and movement targets now? 
        //newSpawn.GetComponent<MoveScript>().direction = directionVector;
    }

    void TrytoSpawn(int index)
    {
        if(Spawnables[index].SpawnCooldown >= Spawnables[index].SpawnInterval)
        {
            spawnUnit(Spawnables[index].UnitType);
            Spawnables[index].SpawnCooldown = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(var i=0; i<Spawnables.Length; i++)
        {
            Spawnables[i].SpawnCooldown += Time.deltaTime;
            Spawnables[i].RebuildTimer += Time.deltaTime;
        }


        if (Input.GetKeyDown("1"))
        {
            TrytoSpawn(0);
        }
        if (Input.GetKeyDown("2"))
        {
            TrytoSpawn(1);
        }
    }
}
