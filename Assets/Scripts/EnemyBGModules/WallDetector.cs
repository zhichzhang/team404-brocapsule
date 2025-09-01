using UnityEngine;

public class WallDetector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private Transform wallCheckPosition;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private bool showGizmos;
    public bool IsGrounded()
    {
        return Physics2D.Raycast(wallCheckPosition.position, transform.right, wallCheckDistance, wallLayerMask);
    }
    private void OnDrawGizmos()
    {
        if (!showGizmos) return;
        Gizmos.color = Color.white;
        Gizmos.DrawLine(wallCheckPosition.position, wallCheckPosition.position + transform.right * wallCheckDistance);
        
        
        //Gizmos.DrawLine(groundCheckPosition.position, groundCheckPosition.position + Vector3.down * groundCheckDistance);
    }
}
