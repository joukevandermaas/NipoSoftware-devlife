using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameManager gameManager;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(collision.gameObject);

            if (collision.gameObject.tag == "Enemy")
            {
                gameManager.BugReachedDoor();
            }
        }
    }
}
