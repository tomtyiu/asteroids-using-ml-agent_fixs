using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLogic : MonoBehaviour
{
    private SpriteRenderer map;
    private void Start()
    {
        map = GetComponentInParent<SpriteRenderer>();
    }
    private void Update()
    {
        //if (transform.position.x > map.bounds.extents.x || transform.position.x < -map.bounds.extents.x || transform.position.y > map.bounds.extents.y || transform.position.y < -map.bounds.extents.y)
        //{
        //    Destroy(gameObject);
        //}
        //if (transform.position.x > MapSetup.bounds.x)
        //{
        //    transform.position = new Vector3(-MapSetup.bounds.x, transform.position.y, transform.position.z);
        //}
        //if (transform.position.x < -MapSetup.bounds.x)
        //{
        //    transform.position = new Vector3(MapSetup.bounds.x, transform.position.y, transform.position.z);
        //}
        //if (transform.position.y > MapSetup.bounds.y)
        //{
        //    transform.position = new Vector3(transform.position.y, -MapSetup.bounds.y, transform.position.z);
        //}
        //if (transform.position.y < -MapSetup.bounds.y)
        //{
        //    transform.position = new Vector3(-transform.position.y, -MapSetup.bounds.y, transform.position.z);
        //}
    }
}
