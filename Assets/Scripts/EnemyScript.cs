using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{


    private MoveScript ms;
    private Transform myTransform;
    public int collisionDamage = 1;


    void Start()
    {
        ms = GetComponent<MoveScript>();
        myTransform = GetComponent<Transform>();
    }

}
