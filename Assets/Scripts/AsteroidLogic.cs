using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLogic : MonoBehaviour
{
    private int health;
    private void Start()
    {
        health = (int)transform.localScale.x - 3;
    }
    private void Update()
    {
        Vector2 bounds = new Vector2(MapSetup.dimensions.x / 2 + MapSetup.margin, MapSetup.dimensions.y / 2 + MapSetup.margin);
        if (transform.localPosition.x > bounds.x || transform.localPosition.x < -bounds.x || transform.localPosition.y > bounds.y || transform.localPosition.y < -bounds.y)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                health--;
            }
            Destroy(collision.gameObject);
        }
    }
}
