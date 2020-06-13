﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{


    public int hp = 1;

    public bool isEnemy = true;
    public bool active = false;
    public bool manualActivation = false; // Used for bosses etc. to prevent activation by the usual method
    public bool immuneToShots = false;
    public Sprite damagedSprite = null;

    public Transform deathExplosion;

    private Material matWhite;
    private Material matDefault;
    private SpriteRenderer sr;
    private float flashInterval = 0.1f;
    private float fadeLifetime = 0.01f; // Amount of time after going offscreen to continue existing
    private int maxHp = 1;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("flashWhite", typeof(Material)) as Material;
        matDefault = sr.material;
        maxHp = hp;
    }

    void fadeAway() // No death explosion
    {
        Destroy(gameObject);
    }

    /// Inflicts damage, flash sprite, and check if the object should be destroyed
    public void Damage(int damageCount)
    {
        if (!active)
        {
            return;
        }

        hp -= damageCount;
        sr.material = matWhite; //Flash white

        if (hp <= 0)
        {
            // Dead!
            if (deathExplosion != null)
            {
                var deathExplosionTransform = Instantiate(deathExplosion) as Transform;
                deathExplosionTransform.position = transform.position;
            }

            Destroy(gameObject);
        }
        else
        { //If not dead, return from white flash after interval
            Invoke("ResetMaterial", flashInterval);
            if(damagedSprite != null)
            {
                sr.sprite = damagedSprite;
            }

        }
    }

    void ResetMaterial()
    {
        sr.material = matDefault;
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Is this a shot?
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        if (shot != null && !immuneToShots)
        {
            // Avoid friendly fire
            if (shot.isEnemyShot != isEnemy)
            {
                Damage(shot.damage);
                shot.onImpact(gameObject.transform);
            }
        } else
        {
            //Is this the activator object?
            ActivatorScript activator = otherCollider.gameObject.GetComponent<ActivatorScript>();
            if (activator != null && !manualActivation && !active)
            {
                if (activator.isActivator)
                {
                    active = true;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D otherCollider)
    {
        ActivatorScript activator = otherCollider.gameObject.GetComponent<ActivatorScript>();
        if (activator != null && !manualActivation && active)
        {
            active = false;
            Invoke("fadeAway", fadeLifetime); // Vanish without explosion after a short time
        }
    }
}
