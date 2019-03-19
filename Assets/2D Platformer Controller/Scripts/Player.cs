﻿using System;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    public float maxJumpHeight = 4f;
    public float timeToJumpApex = .4f;
    public float maxJumpVelocity = 6f;
    public float gravity = -4f;
    public float rebounceStrenght = 4f;

    private Health health;
    private Controller2D controller;

    private float accelerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;
    private float moveSpeed = 6f;

    private Vector3 velocity;
    private Vector2 directionalInput;

    private float velocityXSmoothing;


    private void Start()
    {
        health = GetComponent<Health>();
        controller = GetComponent<Controller2D>();

        health.Die += OnDeath;
    }

    private void OnDeath()
    {
        Debug.Log("I equals dead.");
        Destroy(gameObject);
    }

    private void Update()
    {
        CalculateVelocity();

        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0f;
        }
    }

    public void SetDirectionalInput(Vector2 input)
    {
        input.y = 0;
        directionalInput = input;
    }
    
    public void OnJumpInputDown()
    {
        if (controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bug")
        {
            health.TakeDamage(10);

            velocity.y = maxJumpVelocity;
            velocity.x = (-Vector3.Normalize(collision.gameObject.transform.position - gameObject.transform.position) * rebounceStrenght).x;
        }
        else if (collision.gameObject.tag == "BugWeakness")
        {
            velocity.y = maxJumpVelocity / 2f;
            var bugHealth = collision.gameObject.GetComponent<Health>();
            bugHealth.TakeDamage(100);
        }
    }

    private void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne));
        velocity.y += gravity * Time.deltaTime;
    }
}
