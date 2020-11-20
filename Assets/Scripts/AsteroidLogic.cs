using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLogic : MonoBehaviour
{
    private PilotAgent ship;
    //private MapSetup map;
    private int health;
    private void Start()
    {
        ship = transform.parent.parent.GetComponentInChildren<PilotAgent>();
        //map = transform.parent.parent.GetComponentInChildren<MapSetup>();
    }
    private void Update()
    {
        Vector2 bounds = new Vector2(MapSetup.dimensions.x / 2 + MapSetup.margin, MapSetup.dimensions.y / 2 + MapSetup.margin);
        if (transform.localPosition.x > bounds.x || transform.localPosition.x < -bounds.x || transform.localPosition.y > bounds.y || transform.localPosition.y < -bounds.y)
        {
            transform.position = new Vector3(0, 0, 500);
            gameObject.SetActive(false);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            if (health <= 0)
            {
                transform.position = new Vector3(0, 0, 500);
                gameObject.SetActive(false);
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                ship.GiveReward(0.50f);
            }
            else
            {
                ship.GiveReward(0.20f);
                health--;
            }
            Destroy(collision.gameObject);
        }
    }
    public void SpawnAsteroid(Vector3 _spawnPosition, Vector2 _forceDirection, float _minAsteroidSpeed, float _maxAsteroidSpeed)
    {
        gameObject.SetActive(true);
        transform.position = _spawnPosition;
        transform.rotation = Quaternion.identity;
        int asteroidSize = Random.Range(3, 6);
        transform.localScale = new Vector3(asteroidSize, asteroidSize, 1);
        health = (int)transform.localScale.x - 3;
        // Apply force between selected value and center of Arena
        GetComponent<Rigidbody2D>().AddForce(_forceDirection * Random.Range(_minAsteroidSpeed, _maxAsteroidSpeed) * -1);
    }
}
