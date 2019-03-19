using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Bug : MonoBehaviour
{
    public float gravity = -4f;
    public float moveSpeed;

    public float maxMoveSpeed = 4f;
    public float minMoveSpeed = 1f;

    public Health health;

    public List<Sprite> availableSprites = new List<Sprite>();
    public SpriteRenderer spriteRenderer;

    public AudioManager audioManager;

    private Controller2D controller; 
    private float velocityXSmoothing;
    private Vector2 velocity;
    private Vector2 direction = Vector2.left;

    private float accelerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;

    public string currentCollisionMask;

    private bool isFalling = false;

    void Start()
    {
        audioManager = GameManager.Instance.audioManager;
        controller = GetComponent<Controller2D>();

        health.Die += OnDeath;

        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    public void OnDeath()
    {
        audioManager.PlaySquashedSound();
        GameManager.Instance.EnemyDied();
        Destroy(gameObject);
    }

    private void Update()
    {
        CalculateVelocity();

        controller.Move(velocity * Time.deltaTime, direction);
    }

    private void LateUpdate()
    {
        if(controller.collisions.below)
        {
            velocity.y = 0;
        }

        if(controller.collisions.below == false && isFalling == false)
        {
            isFalling = true;
            SwitchDirection();
        }

        if(controller.collisions.below && isFalling)
        {
            isFalling = false;
        }
    }

    private void SwitchDirection()
    {
        direction.x = -direction.x;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private void CalculateVelocity()
    {
        float targetVelocityX = direction.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne));
        velocity.y += gravity * Time.deltaTime;
    }
}
