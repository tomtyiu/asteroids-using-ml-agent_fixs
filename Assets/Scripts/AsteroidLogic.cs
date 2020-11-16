using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLogic : MonoBehaviour
{

    private void Update()
    {
        Vector2 bounds = new Vector2(MapSetup.dimensions.x / 2 + MapSetup.margin, MapSetup.dimensions.y / 2 + MapSetup.margin); 
        if (transform.localPosition.x > bounds.x || transform.localPosition.x < -bounds.x || transform.localPosition.y > bounds.y || transform.localPosition.y < -bounds.y)
        {
            Destroy(gameObject);
        }
    }
}
