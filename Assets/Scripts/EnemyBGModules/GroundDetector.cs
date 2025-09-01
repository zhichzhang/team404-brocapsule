using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private bool showGizmos;
    public bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheckPosition.position, Vector2.down, groundCheckDistance, groundLayerMask);
    }
    private void OnDrawGizmos()
    {   if (!showGizmos) return;
        Gizmos.color = Color.white;
        Gizmos.DrawLine(groundCheckPosition.position, groundCheckPosition.position + Vector3.down * groundCheckDistance);
    }
}
