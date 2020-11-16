using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaSetup : MonoBehaviour
{
    public GameObject map;
    public GameObject ship;
    private GameObject asteroidsContainer;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(map, transform);
        ship = Instantiate(ship, transform);
        asteroidsContainer = new GameObject("Asteroids");
        asteroidsContainer.transform.parent = gameObject.transform;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(MapSetup.dimensions.x, MapSetup.dimensions.y, 0));
    }
}
