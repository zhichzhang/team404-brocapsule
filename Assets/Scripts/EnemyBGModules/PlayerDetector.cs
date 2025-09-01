using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask playerAndGroundLayer;    
    [SerializeField] private Vector2 centerOffset;
    [SerializeField] private Vector2 detectionBoxSize;
    [SerializeField] private bool showGizmo;
    public Player isPlayerInRange()
    {
        Vector2 detectBoxCenter = (Vector2)transform.position + centerOffset;
        Vector2 detectBoxC1 = detectBoxCenter + detectionBoxSize / 2;
        Vector2 detectBoxC2 = detectBoxCenter - detectionBoxSize / 2;
        Collider2D[] colliders = Physics2D.OverlapAreaAll(detectBoxC1, detectBoxC2, playerLayer);
        foreach (var hit in colliders)
        {
            Player player = hit.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("Detector see Player");
                Ray2D ray = new Ray2D(transform.position, player.transform.position - transform.position);
                RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, ray.direction, Vector2.Distance(transform.position, player.transform.position), playerAndGroundLayer);
                if (hitInfo.collider == player.GetComponent<Collider2D>())
                    return player;
            }
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = Color.red;
            Vector2 boxCenter = (Vector2)transform.position + centerOffset;
            Gizmos.DrawWireCube(boxCenter, detectionBoxSize);
        }
    }
          

}
