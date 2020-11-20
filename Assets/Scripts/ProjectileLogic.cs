using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    //private PilotAgent ship;
    //private void Start()
    //{
    //    ship = transform.parent.parent.GetComponentInChildren<PilotAgent>();
    //}
    // Update is called once per frame
    void Update()
    {
        Vector2 bounds = new Vector2(MapSetup.dimensions.x / 2 + MapSetup.margin, MapSetup.dimensions.y / 2 + MapSetup.margin);
        if (transform.localPosition.x > bounds.x || transform.localPosition.x < -bounds.x || transform.localPosition.y > bounds.y || transform.localPosition.y < -bounds.y)
        {
            //ship.GiveReward(-0.02f);
            Destroy(gameObject);
        }
    }
}
