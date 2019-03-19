using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            var bugHealth = collision.gameObject.GetComponentInChildren<Health>();
            bugHealth.TakeDamage(1000);
        }

        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
