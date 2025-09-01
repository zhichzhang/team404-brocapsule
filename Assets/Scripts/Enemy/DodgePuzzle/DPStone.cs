using UnityEngine;

public class DPStone : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    public float wallCheckDistance = 0.5f; // Distance for ground detection
    public LayerMask wallLayer; // LayerMask for the ground

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        CheckGroundCollision(); // Check for ground collision in every frame
    }

    private void CheckGroundCollision()
    {
        // Cast a ray downward to check for the ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, wallCheckDistance, wallLayer);

        if (hit.collider != null)
        {
            Debug.Log("Hit the ground: " + hit.collider.name);
            Destroy(gameObject); // Destroy the object when it hits the ground
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the raycast in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * wallCheckDistance);
    }
}
