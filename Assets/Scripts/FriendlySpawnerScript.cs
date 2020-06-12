using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class FriendlySpawnerScript : MonoBehaviour
{
    public float spawnInterval1 = 0.25f;
    public float spawnInterval2 = 0.25f;

    private float spawnCooldown1;
    private float spawnCooldown2;


    public Transform UnitType1;
    public Transform UnitType2;

    // Start is called before the first frame update
    void Start()
    {
        
    }



    void spawnUnit(Transform s)
    {
        var newSpawn = Instantiate(s) as Transform;
        var fs = newSpawn.GetComponent<FriendlyShipScript>() as FriendlyShipScript;
        newSpawn.position = transform.position;


        // TODO: Set shooting and movement targets now? 
        //newSpawn.GetComponent<MoveScript>().direction = directionVector;
    }



    // Update is called once per frame
    void Update()
    {
        spawnCooldown1 += Time.deltaTime;
        spawnCooldown2 += Time.deltaTime;

        if (Input.GetKeyDown("1"))
        {
            if(spawnCooldown1 >= spawnInterval1)
            {
                spawnUnit(UnitType1);
            }
        }
        if (Input.GetKeyDown("2"))
        {
            if (spawnCooldown2 >= spawnInterval2)
            {
                spawnUnit(UnitType2);
            }
        }
    }
}
