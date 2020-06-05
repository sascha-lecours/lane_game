using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFireScript : MonoBehaviour
{
    private WeaponScript[] weapons;

    void Start()
    {
        weapons = GetComponentsInChildren<WeaponScript>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (WeaponScript weapon in weapons)
        {
            // Auto-fire
            if (weapon != null && weapon.CanAttack)
            {
                weapon.Attack(true);
            }
        }
    }
}
