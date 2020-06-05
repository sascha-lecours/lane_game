using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public bool hunt_player = false;
    private Transform playerTransform = null;
    private MoveScript ms;
    private Vector2 tempDirection;
    private Transform myTransform;

    void Awake()
    {
        // Retrieve the weapon only once
        
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Start()
    {
        ms = GetComponent<MoveScript>();
        myTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (hunt_player && playerTransform != null)
        {
            tempDirection = playerTransform.position - myTransform.position;
            tempDirection.Normalize();
            ms.direction = tempDirection;
        }
        
    }
}
