using Unity.AppUI.UI;
using UnityEngine;

public class WayPoint2D : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static float smallThresh = 1f;
    private static float largeThresh = 1f;
    public WayPoint2D nextWayPoint;
    public float groundHeight;

    private void Awake()
    {
        float groundDistance = Physics2D.Raycast(transform.position, Vector2.down, 1000, LayerMask.GetMask("Ground")).distance;
        groundHeight = transform.position.y - groundDistance;
        if (groundDistance > largeThresh)
        {
            string message = gameObject.name + ": Waypoint is too high above the ground";
            Debug.LogError(message);
        }
    }
    void Start()
    {
        
        if (nextWayPoint != null && Mathf.Abs(groundHeight - nextWayPoint.groundHeight) > smallThresh)
        {
            string message = gameObject.name + ": Waypoints are not on the same level";
            Debug.LogError(message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * largeThresh);
    }
}
