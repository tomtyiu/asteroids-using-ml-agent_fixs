using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLogic : MonoBehaviour
{
    public SpriteRenderer map;
    private void Update()
    {
        if (transform.localPosition.x > map.bounds.extents.x || transform.localPosition.x < -map.bounds.extents.x || transform.localPosition.y > map.bounds.extents.y || transform.localPosition.y < -map.bounds.extents.y)
        {
            Destroy(gameObject);
        }
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
