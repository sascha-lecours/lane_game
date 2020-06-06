using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArrowScript : MonoBehaviour
{
    private Transform mySpawner = null;

    // Start is called before the first frame update
    void Start()
    {
        mySpawner = GameObject.Find("ShipSpawner").transform;
    }

    void faceAwayFromObject(Transform t)
    {
        var directionVector = new Vector2(0, 0);
        directionVector = t.position - transform.position;
        directionVector.Normalize();
        var angle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward); // Adds 180 assumes arrow sprite points to the right
    }

    // Update is called once per frame
    void Update()
    {
        faceAwayFromObject(mySpawner);
    }
}
