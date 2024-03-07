using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PilotAgent : Agent
{
    public GameObject projectile;
    private MapSetup map;
    private readonly float turnSpeed = 250f;
    private readonly float moveSpeed = 25f;
    private Rigidbody2D rBody;
    private float nextDamageEvent;
    private readonly float attackInterval = 0.2f;
    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        map = transform.parent.GetComponentInChildren<MapSetup>();
    }
    private void Update()
    {
        Constraints();
    }
    public override void OnEpisodeBegin()
    {
            // Reset the map
            map.ClearAsteroids();
            // Reset the agent
            rBody.angularVelocity = 0;
            rBody.velocity = Vector2.zero;
            transform.localPosition = Vector2.zero;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        Vector2 localVelocity = rBody.transform.InverseTransformDirection(rBody.velocity);
        sensor.AddObservation(localVelocity.x);
        sensor.AddObservation(localVelocity.y);
        // Assuming rBody.velocity is a Vector3, normalize and add x, y components as observations
        sensor.AddObservation(localVelocity.normalized.x);
        sensor.AddObservation(localVelocity.normalized.y);
        // Correct way to add rotation as observation
        sensor.AddObservation(transform.localEulerAngles.x / 360.0f);
        sensor.AddObservation(transform.localEulerAngles.y / 360.0f);
        sensor.AddObservation(transform.localPosition.x / (MapSetup.dimensions.x / 2));
        sensor.AddObservation(transform.localPosition.y / (MapSetup.dimensions.y / 2));
        sensor.AddObservation(transform.localRotation.normalized);
        
        //foreach(GameObject asteroid in map.asteroids.Values)
        //{
        //    sensor.AddObservation(asteroid.activeSelf);
        //    Rigidbody2D arBody = asteroid.GetComponent<Rigidbody2D>();
        //    Vector2 alocalVelocity = arBody.transform.InverseTransformDirection(arBody.velocity);
        //    sensor.AddObservation(alocalVelocity.x);
        //    sensor.AddObservation(alocalVelocity.y);
        //    // If agent only knows a directinal vector, but doesnt know the velocity of the asteroid, it can't accurately shoot.
        //    //sensor.AddObservation(new Vector2(asteroid.transform.localPosition.x - transform.localPosition.x, asteroid.transform.localPosition.y - transform.localPosition.y));
        //    sensor.AddObservation(asteroid.transform.localPosition.x / (MapSetup.dimensions.x / 2));
        //    sensor.AddObservation(asteroid.transform.localPosition.y / (MapSetup.dimensions.y / 2));
        //    sensor.AddObservation(asteroid.transform.localScale);
        //}
    }
    public override void OnActionReceived(float[] actions)
    {
        // Rotate
        if (actions[0] == 1)
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * turnSpeed * -1);
        }
        else if (actions[0] == 2)
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * turnSpeed);
        }        
        // Move forward
        rBody.AddForce(transform.up * actions[1] * moveSpeed);
        // Shoot
        if ((int)actions[2] == 1)
        {
            GiveReward(-0.03f);
            Shoot();
        }
        GiveReward(0.01f);
    }
    public override void Heuristic(float[] actionsOut)
    {
        if (Input.GetKey(KeyCode.A))
        {
            actionsOut[0] = 2;
        } 
        else if (Input.GetKey(KeyCode.D))
        {
            actionsOut[0] = 1;
        }
        else
        {
            actionsOut[0] = 0;
        }
        actionsOut[1] = Input.GetKey(KeyCode.W) ? 1 : 0;
        actionsOut[2] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            GiveReward(-1f);
            map.ClearAsteroids();
            EndEpisode();
        }
    }
    // Constraints the model to the map. Out of bounds dimension is changed to its negative value.
    private void Constraints()
    {
        // divided by 2, to get the extents.
        Vector2 bounds = new Vector2(MapSetup.dimensions.x / 2, MapSetup.dimensions.y / 2);
        Vector2 ltpos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        if (ltpos.x > bounds.x || ltpos.x < -1 * (bounds.x))
        {
            GiveReward(-0.5f);
            transform.localPosition = new Vector3(bounds.x * Mathf.Sign(ltpos.x) * -1, ltpos.y, transform.localPosition.z);
        }
        if (ltpos.y > bounds.y || ltpos.y < -1 * (bounds.y))
        {
            GiveReward(-0.5f);
            transform.localPosition = new Vector3(ltpos.x, bounds.y * Mathf.Sign(ltpos.y) * -1, transform.localPosition.z);
        }
    }
    private void Shoot()
    {
        if (Time.time > nextDamageEvent)
        {
            GameObject p = Instantiate(projectile, transform.parent.Find("Projectiles"));
            p.transform.position = transform.position;
            p.transform.rotation = transform.rotation;
            p.GetComponent<Rigidbody2D>().AddForce(transform.up * 50f, ForceMode2D.Impulse);
            nextDamageEvent = Time.time + attackInterval;
        }
    }
    public void GiveReward(float amount)
    {
        AddReward(amount);
    }
}
