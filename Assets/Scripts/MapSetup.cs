using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetup : MonoBehaviour
{
    [System.NonSerialized] public static readonly Vector2 dimensions = new Vector2(32f, 18f);
    [System.NonSerialized] public static readonly float margin = 5f;
    private Vector2 safeSquareExtents = new Vector2(dimensions.x / 2, dimensions.y / 2);
    public Dictionary<int, GameObject> asteroids = new Dictionary<int, GameObject>();
    public GameObject asteroid;
    private Vector2 bounds;
    private readonly float spawnRate = 3f;
    private float nextSpawn;
    private readonly float minAsteroidSpeed = 10f;
    private readonly float maxAsteroidSpeed = 50f;
    private readonly int numberOfAsteroids = 3;
    public readonly int maxAsteroids = 200;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.size = dimensions;
        bounds.x = sr.bounds.extents.x + margin;
        bounds.y = sr.bounds.extents.y + margin;
        InitializeAsteroids();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            CreateAsteroid(numberOfAsteroids);
            nextSpawn = Time.time + spawnRate;
        }
    }
    void InitializeAsteroids()
    {
        for(int i = 0; i < maxAsteroids; i++)
        {
            GameObject f = Instantiate(asteroid, new Vector3(0,0,500), Quaternion.identity);
            asteroids.Add(f.GetHashCode(), f);
            // Group asteroids under Arena
            f.transform.parent = transform.parent.Find("Asteroids");
            f.SetActive(false);
        }
    }
    void CreateAsteroid(int num)
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

            
            Vector2 direction;
            if (Random.Range(0,2) == 0)
            {
                // Applies normalization, to convert vector magnitude to 1, so that coordinates wouldn't affect the stength of force
                direction = new Vector2(randX + Random.Range(-dimensions.x / 2, dimensions.x / 2), randY).normalized;
            }
            else
            {
                direction = new Vector2(randX, randY + Random.Range(-dimensions.y / 2, dimensions.y / 2)).normalized;
            }
            GameObject asteroidToSpawn = null;
            foreach(GameObject asteroid in asteroids.Values)
            {
                if (!asteroid.activeSelf)
                {
                    asteroidToSpawn = asteroid;
                    break;
                }
            }
            // Turn values from 0,0 centered to transform.position centered.
            if (asteroidToSpawn)
            {
                asteroidToSpawn.GetComponent<AsteroidLogic>().SpawnAsteroid(new Vector3(randX + transform.parent.position.x, randY + transform.parent.position.y, 0),
                    direction, minAsteroidSpeed, maxAsteroidSpeed);

            }
        }
    }
    public void ClearAsteroids()
    {
        foreach(GameObject asteroid in asteroids.Values)
        {
            asteroid.transform.position = new Vector3(0, 0, 500);
            asteroid.SetActive(false);
            asteroid.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
