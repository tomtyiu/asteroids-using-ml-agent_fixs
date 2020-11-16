using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetup : MonoBehaviour
{
    [System.NonSerialized] public static readonly Vector2 dimensions = new Vector2(32f, 18f);
    [System.NonSerialized] public static readonly float margin = 5f;
    private Vector2 safeSquareExtents = new Vector2(dimensions.x / 2, dimensions.y / 2);
    public GameObject asteroid;
    private Vector2 bounds;
    private readonly float spawnRate = 3f;
    private float nextSpawn;
    private readonly float minAsteroidSpeed = 35f;
    private readonly float maxAsteroidSpeed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.size = dimensions;
        bounds.x = sr.bounds.extents.x + margin;
        bounds.y = sr.bounds.extents.y + margin;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            CreateAsteroid(5, asteroid);
            nextSpawn = Time.time + spawnRate;
        }
    }
    void CreateAsteroid(int num, GameObject type)
    {
        for (int i = 0; i < num; i++)
        {
            float randX;
            float randY;
            // spawn x or y?
            if (Random.Range(0,2) ==0)
            {
                // Spawn x
                // Y is any value within bounds
                randY = Random.Range(-bounds.y, bounds.y);
                // Flip a coin for X, negative or positive?
                if (Random.Range(0, 2) == 0)
                {
                    // From safe zone end to bounds end
                    randX = Random.Range(safeSquareExtents.x, bounds.x);
                }
                else
                {
                    randX = Random.Range(-bounds.x, -safeSquareExtents.x);
                }
            }
            else
            {
                // Spawn y
                randX = Random.Range(-bounds.x, bounds.x);
                if (Random.Range(0, 2) == 0)
                {
                    randY = Random.Range(safeSquareExtents.y, bounds.y);
                }
                else
                {
                    randY = Random.Range(-bounds.y, -safeSquareExtents.y);
                }
            }
            // Pick random values for x and y, that are within the map

            // Applies normalization, to convert vector magnitude to 1, so that coordinates wouldn't affect the stength of force
            Vector2 direction = new Vector2(randX, randY).normalized;
            // Turn values from 0,0 centered to transform.position centered.
            GameObject f = Instantiate(type, new Vector2(randX + transform.parent.position.x, randY + transform.parent.position.y), Quaternion.identity);
            int asteroidSize = Random.Range(3, 6);
            f.transform.localScale = new Vector3(asteroidSize, asteroidSize, 1);
            // Group asteroids under Arena
            f.transform.parent = transform.parent.Find("Asteroids");
            // Apply force between selected value and center of Arena
            f.GetComponent<Rigidbody2D>().AddForce(direction * Random.Range(minAsteroidSpeed, maxAsteroidSpeed) * -1);
        }
    }
    public void ClearAsteroids()
    {
        Transform asteroidTransform = transform.parent.Find("Asteroids");
        foreach (Transform asteroid in asteroidTransform)
        {
            Destroy(asteroid.gameObject);
        }
    }
}
