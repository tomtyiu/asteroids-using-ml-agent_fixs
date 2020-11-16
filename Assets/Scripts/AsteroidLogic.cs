using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLogic : MonoBehaviour
{

    private void Update()
    {
        if (transform.localPosition.x > MapSetup.dimensions.x || transform.localPosition.x < -MapSetup.dimensions.x || transform.localPosition.y > MapSetup.dimensions.y || transform.localPosition.y < -MapSetup.dimensions.y)
        {
            Destroy(gameObject);
        }
    }
}
