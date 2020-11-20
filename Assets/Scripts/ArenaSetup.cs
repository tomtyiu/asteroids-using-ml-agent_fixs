using UnityEngine;

public class ArenaSetup : MonoBehaviour
{
    public GameObject map;
    public GameObject ship;
    private GameObject asteroidsContainer;
    private GameObject projectileContainer;
    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(map, transform);

        ship = Instantiate(ship, transform);
        asteroidsContainer = new GameObject("Asteroids");
        asteroidsContainer.transform.parent = gameObject.transform;
        asteroidsContainer.transform.localPosition = Vector3.zero;
        projectileContainer = new GameObject("Projectiles");
        projectileContainer.transform.parent = gameObject.transform;
        projectileContainer.transform.localPosition = Vector3.zero;
    }
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.DrawCube(transform.position, new Vector3(MapSetup.dimensions.x + MapSetup.margin, MapSetup.dimensions.y + MapSetup.margin, 0));
        }
        
    }
}
