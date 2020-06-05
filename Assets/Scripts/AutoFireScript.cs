using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFireScript : MonoBehaviour
{
    private WeaponScript[] weapons;
    private HealthScript myHealthScript = null;
    private bool isEnemy = false;

    void Start()
    {
        weapons = GetComponentsInChildren<WeaponScript>();
        myHealthScript = GetComponent<HealthScript>();
        isEnemy = myHealthScript.isEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (WeaponScript weapon in weapons)
        {
            // Auto-fire
            if (weapon != null && weapon.CanAttack)
            {
                weapon.Attack(isEnemy);
            }
        }
    }
}
