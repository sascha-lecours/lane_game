using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FriendlyShipScript : MonoBehaviour
{
    public Transform myDestination = null;
    private MoveScript myMoveScript = null;

    // Start is called before the first frame update
    void Start()
    {
        myDestination = GameObject.Find("DestinationPoint").transform;
        myMoveScript = GetComponent<MoveScript>();

        if (myDestination != null && myMoveScript != null)
        {
            myMoveScript.direction = GetVector2PointingAtTarget(myDestination);
            faceTowardObject(myDestination);
        }
        
    }

    void faceTowardObject(Transform t)
    {
        var directionVector = new Vector2(0, 0);
        directionVector = t.position - transform.position;
        directionVector.Normalize();
        var angle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    Vector2 GetVector2PointingAtTarget(Transform t)
    {
        var directionVector = new Vector2(0, 0);
        directionVector = t.position - transform.position;
        directionVector.Normalize();
        return directionVector;
    }
}
