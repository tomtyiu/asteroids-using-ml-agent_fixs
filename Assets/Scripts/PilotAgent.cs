using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PilotAgent : Agent
{
    private float turnSpeed = 500f;
    private float moveSpeed = 5f;
    private Rigidbody2D rBody;
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }
    public override void OnActionReceived(float[] actions)
    {
        float rotateAxis = actions[0];
        float accelerateAxis = Mathf.Abs(actions[1]);
        transform.Rotate(Vector3.forward, rotateAxis * Time.deltaTime * turnSpeed * -1);
        rBody.AddForce(transform.up * accelerateAxis * moveSpeed);
    }
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
        actionsOut[2] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }
}
