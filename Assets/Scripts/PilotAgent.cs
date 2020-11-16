using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PilotAgent : Agent
{
    private MapSetup map;
    private readonly float turnSpeed = 500f;
    private readonly float moveSpeed = 5f;
    private Rigidbody2D rBody;
    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        map = transform.parent.Find("Map(Clone)").GetComponent<MapSetup>();
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
    // Constraints the model to the map. Out of bounds dimension is changed to its negative value.
    private void Constraints()
    {
        // divided by 2, to get the extents.
        Vector2 bounds = new Vector2(MapSetup.dimensions.x / 2, MapSetup.dimensions.y / 2);
        Vector2 ltpos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        if (ltpos.x > bounds.x || ltpos.x < -1 * (bounds.x))
        {
            transform.localPosition = new Vector3(ltpos.x * -1, ltpos.y, transform.localPosition.z);
        }
        if (ltpos.y > bounds.y || ltpos.y < -1 * (bounds.y))
        {
            transform.localPosition = new Vector3(ltpos.x, -1 * ltpos.y, transform.localPosition.z);
        }
    }
}
