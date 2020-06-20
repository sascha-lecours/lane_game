using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    
    public Transform shotPrefab; // Projectile prefab for shooting
    public float shootingRate = 0.25f; // Cooldown in seconds between two shots
    public bool shotless = false;
    public bool randomizeShotStart = false;
    public Vector3 shotOriginOffset = new Vector3(0, 0, 0);
    public float bulletSpread = 0f; // max range to change vectors when shooting
    public float initialShotDelay = 0.01f;
    public AudioClip shotSound = null;
    public bool aimAtTargetObject = false;
    public Transform myTarget = null;

    private HealthScript myHealthscript = null;
    private Vector2 tempShotDirection = new Vector2(0, -1);
    private Vector2 facing = new Vector2(0, 1);

    private float shootCooldown;
    private float maxRandomizationCooldownIncrease = 0.7f;

    void Start()
    {
        shootCooldown = 0f;
        myHealthscript = GetComponentInParent<HealthScript>();

        if (aimAtTargetObject) //TODO: replace this ## Sets it to just be any old target object during testing. Should get it from... healthscript?
        {
            myTarget = GameObject.Find("TargetPoint").transform;
            if(myTarget) faceTowardObject(myTarget);
        }

        if (randomizeShotStart)
        {
            shootCooldown = Random.Range(0f, (shootingRate * maxRandomizationCooldownIncrease));
        }
        shootCooldown += initialShotDelay;
    }

    void faceTowardObject(Transform t)
    {
        var directionVector = new Vector2(0, 0);
        directionVector = t.position - transform.position;
        directionVector.Normalize();
        var angle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    void Update()
    {
        if (aimAtTargetObject && myTarget)
        {
            faceTowardObject(myTarget);
        }
            if (shootCooldown > 0 && myHealthscript.active)
        {
            shootCooldown -= Time.deltaTime;
        }

    }

    public Vector2 Vector2FromAngle(float a)
    {
        a *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    }


    /// Create a new projectile if possible
    public void Attack(bool isEnemy)
    {
        if (!shotless & CanAttack)
        {
            if (aimAtTargetObject && myTarget)
            {
                faceTowardObject(myTarget);
            }
            facing = new Vector2(transform.up.x, transform.up.y) * -1; // Assumes sprite faces downward
            facing.Normalize();
            shootCooldown = shootingRate;
            {
                // Create a new shot
                var shotTransform = Instantiate(shotPrefab) as Transform;

                // Assign position
                shotTransform.position = transform.position + shotOriginOffset;

                // Assign direction to match turret
                tempShotDirection = facing;

                ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
                if (shot != null)
                {
                    shot.isEnemyShot = isEnemy;
                }

                MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
                if (move != null)
                {
                    {
                        move.direction.x = tempShotDirection.x + (Random.Range(-bulletSpread, bulletSpread));
                        move.direction.y = tempShotDirection.y + (Random.Range(-bulletSpread, bulletSpread));
                    }

                }
            }




            if (shotSound != null)
            {
                SoundEffectsHelper.Instance.MakePassedInSound(shotSound);
            } else
            {
                SoundEffectsHelper.Instance.MakeShipFiringSound();
            }
        }
    }

    /// <summary>
    /// Is the weapon ready to create a new projectile?
    /// </summary>
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}
