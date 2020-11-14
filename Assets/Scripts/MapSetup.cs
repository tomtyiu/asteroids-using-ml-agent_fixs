using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetup : MonoBehaviour
{
    public float margin = 2f;
    public float safeSquareExtent = 3f;
    public GameObject asteroid;
    private Vector2 bounds;
    private readonly float spawnRate = 3f;
    private float nextSpawn;
    private readonly float minAsteroidSpeed = 35f;
    private readonly float maxAsteroidSpeed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetComponentInParent<Transform>().position);
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
            // Pick random values for x and y, that are within the map
            float randX = Random.Range(-bounds.x, bounds.x);
            float randY = Random.Range(-bounds.y, bounds.y);
            // If both X and Y values are within safe zone
            if (randX < safeSquareExtent && randX > -safeSquareExtent && randY < safeSquareExtent && randY > -safeSquareExtent)
            {
                // Flip a coin
                if (Random.Range(0,2) == 0)
                {
                    // Reasign 
                    randX = Random.Range(safeSquareExtent, bounds.x);
                }
                else
                {
                    // Reasign to the other side.
                    randX = Random.Range(-bounds.x , -safeSquareExtent);
                }
            }
            // Applies normalization, to convert vector magnitude to 1, so that coordinates wouldn't affect the stength of force
            Vector2 direction = new Vector2(randX, randY).normalized;
            // Turn values from 0,0 centered to transform.position centered.
            GameObject f = Instantiate(type, new Vector2(randX + transform.parent.position.x, randY + transform.parent.position.y), Quaternion.identity);
            // Group asteroids under Arena
            f.transform.parent = transform.parent;
            // Apply force between selected value and center of Arena
            //f.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-randX, transform.parent.position.x), Random.Range(-randY, transform.parent.position.y)).normalized * Random.Range(minAsteroidSpeed, maxAsteroidSpeed));
            f.GetComponent<Rigidbody2D>().AddForce(direction * Random.Range(minAsteroidSpeed, maxAsteroidSpeed) * -1);
        }
    }
    public void ResetSpace(GameObject[] agents)
    {
        foreach (GameObject agent in agents)
        {
            if (agent.transform.parent == gameObject.transform)
            {
                agent.transform.position = gameObject.transform.position;
                agent.transform.rotation = Quaternion.identity;
            }
        }

        //CreateFood(numFood, food);
        //CreateFood(numBadFood, badFood);
    }
}
