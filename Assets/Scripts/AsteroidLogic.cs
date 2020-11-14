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
    }
}
