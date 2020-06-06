using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class FriendlySpawnerScript : MonoBehaviour
{
    public float spawnInterval = 0.25f;
    private float spawnCooldown;
    

    public Transform UnitType1;

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
        spawnCooldown += Time.deltaTime;
        if (Input.GetKeyDown("1"))
        {
            if(spawnCooldown >= spawnInterval)
            {
                spawnUnit(UnitType1);
            }
        }
    }
}
