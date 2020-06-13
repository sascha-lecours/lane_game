using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FriendlyShipScript : MonoBehaviour
{
    public Transform myDestination = null;
    public bool cargoShip = false;

    private MoveScript myMoveScript = null;

    

    // Start is called before the first frame update
    void Start()
    {
        if (!cargoShip)
        {
             myDestination = GameObject.Find("DestinationPoint").transform;
        }
        else
        {
            myDestination = GameObject.Find("CargoShipDestinationPoint").transform;
        }
        myMoveScript = GetComponent<MoveScript>();

        if (myDestination != null && myMoveScript != null)
        {
            myMoveScript.direction = GetVector2PointingAtTarget(myDestination);
            FaceTowardObject(myDestination);
        }

    }

    void FaceTowardObject(Transform t)
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        var tookDamage = false;
        var damageAmount = 0;

        // Collision with enemy
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            // Kill the enemy
            HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
            if (enemyHealth != null) enemyHealth.Damage(100);

            tookDamage = true;
            damageAmount = enemy.collisionDamage;
        }

        if (tookDamage)
        {
            HealthScript myHealth = this.GetComponent<HealthScript>();
            if (myHealth != null) myHealth.Damage(damageAmount);
        }
    }
}
