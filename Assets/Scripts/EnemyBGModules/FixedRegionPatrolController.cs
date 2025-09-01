using UnityEngine;

public class FixedRegionPatrolController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector2 centerOffset;
    [SerializeField] private Vector2 detectionBoxSize;
    [SerializeField] private bool showGizmo;
    [SerializeField] private float droneMinHeight;

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
            float minHeightY = boxCenter.y - detectionBoxSize.y / 2 + droneMinHeight;

            // Define the points for the minimum height line
            Vector3 leftPoint = new Vector3(boxCenter.x - detectionBoxSize.x / 2, minHeightY, 0);
            Vector3 rightPoint = new Vector3(boxCenter.x + detectionBoxSize.x / 2, minHeightY, 0);

            // Draw the minimum height line
            Gizmos.color = Color.blue; // Use a different color to distinguish it from the box
            Gizmos.DrawLine(leftPoint, rightPoint);
        }

    }
    public (int direction, float time, float verticalSpeed) getRandomLocationInRegion(Vector2 startPoint, float maxTime, float speed)
    {
        float time = Random.Range(maxTime / 2, maxTime);

        Vector2 detectBoxCenter = (Vector2)transform.position + centerOffset;
        float boxLeftX = detectBoxCenter.x - detectionBoxSize.x / 2;
        float boxRightX = detectBoxCenter.x + detectionBoxSize.x / 2;
        float boxBottomY = detectBoxCenter.y - detectionBoxSize.y / 2;
        float boxTopY = detectBoxCenter.y + detectionBoxSize.y / 2;

        int direction = Random.value > 0.5f ? 1 : -1;

        float newX = startPoint.x + direction * speed * time;

        if ((direction == 1 && newX > boxRightX) || (direction == -1 && newX < boxLeftX))
        {
            direction *= -1;
            newX = startPoint.x + direction * speed * time;

            if ((direction == 1 && newX > boxRightX) || (direction == -1 && newX < boxLeftX))
            {
                return (0, 0, 0); // Indicating no viable movement
            }
        }

        float minHeightAboveBoxBase = boxBottomY + droneMinHeight;
        float newY = Random.Range(minHeightAboveBoxBase, boxTopY);
        float verticalSpeed = (newY - startPoint.y) / time;

        return (direction, time, verticalSpeed);
    }

    public (int direction, float time) GetMoveToPlayer(Vector2 startPosition, float speed, float minTime, float maxTime)
    {
        Player player = isPlayerInRange();
        if (player == null)
        {
            //Debug.Log("Player not in range");
            return (0, 0);
        }

        Vector2 playerPosition = player.transform.position;
        float distanceToPlayer = playerPosition.x - startPosition.x;
        int direction = distanceToPlayer > 0 ? 1 : -1;

        float timeToReachPlayer = Mathf.Abs(distanceToPlayer) / speed;
        float clampedTime = Mathf.Clamp(timeToReachPlayer, minTime, maxTime);

        float newXPosition = startPosition.x + direction * speed * clampedTime;
        Vector2 detectBoxCenter = (Vector2)transform.position + centerOffset;
        float boxLeftX = detectBoxCenter.x - detectionBoxSize.x / 2;
        float boxRightX = detectBoxCenter.x + detectionBoxSize.x / 2;

        if (newXPosition < boxLeftX || newXPosition > boxRightX)
        {
            return (direction, 0);
        }

        return (direction, clampedTime);
    }
}
