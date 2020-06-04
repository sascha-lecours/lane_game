﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simply moves the current game object
/// </summary>
public class MoveScript : MonoBehaviour
{
    public Vector2 startSpeed = new Vector2(0f, 0f);

    public Vector2 speed = new Vector2(10f, 10f);
    public Vector2 direction = new Vector2(-1, 0);
    public float acceleration = 1f;
    public float rotationSpeed = 0.01f;
    private Vector2 curSpeed = new Vector2(0f, 0f);
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    private void Start()
    {
        curSpeed.x = startSpeed.x * direction.x;
        curSpeed.y = startSpeed.y * direction.y;
    }

    void Update()
    {
        direction.Normalize();
        curSpeed.x = Mathf.Lerp(curSpeed.x, speed.x * direction.x, (acceleration * Time.deltaTime));
        curSpeed.y = Mathf.Lerp(curSpeed.y, speed.y * direction.y, (acceleration * Time.deltaTime));

        movement = new Vector2(curSpeed.x, curSpeed.y);
        float angle = (Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg) - 90;
        //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        // Apply movement to the rigidbody
        rigidbodyComponent.velocity = movement;
    }
}
