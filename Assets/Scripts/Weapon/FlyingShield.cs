using System.Collections;
using UnityEngine;

public class FlyingShield : MonoBehaviour
{
    [Header("Shield Info")]
    public float moveSpeed = 25f;
    public float speedMultiplier = 1.3f;
    public float maxMoveSpeed = 60f;
    public float liveTime = 8f;

    [Header("Collision Check")]
    public Transform leftCheck;
    public Transform rightCheck;
    public float checkDistance = 0.5f;
    public LayerMask collisionLayer;

    [Header("Catch Check")]
    [SerializeField] Transform catchCheck;
    [SerializeField] Vector2 catchBoxSize = new Vector2(10.0f, 1.0f);
    [SerializeField] LayerMask playerLayer;

    private Rigidbody2D rb;
    private int bounceCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(DestroyShieldCoroutine());
    }

    void Update()
    {
        rb.linearVelocity = transform.right * moveSpeed;

        
        if (moveSpeed > 0)
        {
            CheckForBounce(rightCheck, Vector2.right);
        }
        else
        {
            CheckForBounce(leftCheck, Vector2.left);
        }
        CheckForCatch(catchCheck);
    }

    void CheckForBounce(Transform checkPos, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos.position, direction, checkDistance,collisionLayer);

        if (hit.collider != null && (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Ladder")))
        {
            moveSpeed = -moveSpeed * speedMultiplier;
            moveSpeed = Mathf.Clamp(moveSpeed, -maxMoveSpeed, maxMoveSpeed);
            bounceCount++;
        }
    }

    void CheckForCatch(Transform checkPos)
    {
        Collider2D hit = Physics2D.OverlapBox(catchCheck.position, catchBoxSize, 0f, playerLayer);
        if (hit != null && hit.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyShieldCoroutine()
    {
        yield return new WaitForSeconds(liveTime);
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        if (leftCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(leftCheck.position, leftCheck.position + Vector3.left * checkDistance);
        }

        if (rightCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(rightCheck.position, rightCheck.position + Vector3.right * checkDistance);
        }

        if (catchCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(catchCheck.position, catchBoxSize);
        }
    }
}
