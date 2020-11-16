using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PilotAgent : Agent
{
    private MapSetup map;
    private SpriteRenderer mapSr;
    private float turnSpeed = 500f;
    private float moveSpeed = 5f;
    private Rigidbody2D rBody;
    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        map = transform.parent.Find("Map(Clone)").GetComponent<MapSetup>();
        mapSr = map.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Constraints();
    }
    public override void OnEpisodeBegin()
    {
            // Reset the agent
            rBody.angularVelocity = 0;
            rBody.velocity = Vector2.zero;
            transform.localPosition = Vector2.zero;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
            Vector2 localVelocity = transform.InverseTransformDirection(rBody.velocity);
            sensor.AddObservation(localVelocity.x);
            sensor.AddObservation(localVelocity.y);
            sensor.AddObservation(transform.rotation.z);
    }
    public override void OnActionReceived(float[] actions)
    {
        float rotateAxis = actions[0];
        float accelerateAxis = Mathf.Abs(actions[1]);
        transform.Rotate(Vector3.forward, rotateAxis * Time.deltaTime * turnSpeed * -1);
        rBody.AddForce(transform.up * accelerateAxis * moveSpeed);
        AddReward(0.1f);
    }
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
        //actionsOut[2] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        map.ClearAsteroids();
        EndEpisode();
    }
    private void Constraints()
    {  
        if (transform.localPosition.x > mapSr.bounds.extents.x - map.margin || transform.localPosition.x < -1 * (mapSr.bounds.extents.x - map.margin))
        {
            transform.localPosition = new Vector3(transform.localPosition.x * -1,transform.localPosition.y,transform.localPosition.z);
        }
        if (transform.localPosition.y > mapSr.bounds.extents.y - map.margin || transform.localPosition.y < -1 * (mapSr.bounds.extents.y - map.margin))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -1 * transform.localPosition.y, transform.localPosition.z);
        }
    }
}
