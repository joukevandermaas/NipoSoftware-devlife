using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public GameManager gameManager;
    
    public float force = 1000f;
    public float cooldown = 1f;

    private float currentCooldown;

    private void Start()
    {
        currentCooldown = cooldown;
    }

    private void Update()
    {
        if (currentCooldown >= 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    public void Pew(Vector3 position, Vector2 direction)
    {
        if (currentCooldown < 0)
        {
            gameManager.ShotsFired();

            var projectileInstance = Instantiate(projectile, position, Quaternion.identity) as GameObject;
            projectileInstance.GetComponent<Rigidbody2D>().AddForce(direction * force);

            currentCooldown = cooldown;
        }
    }
}
