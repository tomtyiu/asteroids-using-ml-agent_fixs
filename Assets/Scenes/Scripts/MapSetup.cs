using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetup : MonoBehaviour
{
    public float margin = 2f;
    public float safeSquareSideLength = 3f;
    public GameObject asteroid;
    private Vector2 bounds;
    private float spawnRate = 3f;
    private float nextSpawn;
    private float minAsteroidSpeed = 7f;
    private float maxAsteroidSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        bounds.x = sr.bounds.extents.x - margin;
        bounds.y = sr.bounds.extents.y - margin;
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
            float randX = Random.Range(-bounds.x, bounds.x);
            float randY = Random.Range(-bounds.y, bounds.y);
            if (randX < safeSquareSideLength && randX > -safeSquareSideLength && randY < safeSquareSideLength && randY > -safeSquareSideLength)
            {
                if (Random.Range(0,1) == 0)
                {
                    randX = Random.Range(safeSquareSideLength, bounds.x);
                }
                else
                {
                    randX = Random.Range(-safeSquareSideLength, -bounds.x);
                }
            }
            GameObject f = Instantiate(type, new Vector2(randX, randY), Quaternion.identity);
            f.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-randX, 0), Random.Range(-randY, 0)) * Random.Range(minAsteroidSpeed, maxAsteroidSpeed));
        }
    }
}
