using System.Collections;
using UnityEngine;

public class KnockBackController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isActive;
    private Rigidbody2D rb;
    private float magnitude;
    private bool enableYDirection;
    public float effectiveTime = 0.1f;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        
        if (isActive)
        {
            Vector2 dir = ((Vector2)transform.position - PlayerInfo.instance.playerPosition).normalized;
            
            rb.linearVelocityX = rb.linearVelocityX + dir.x * magnitude;
        }
    }

    public void KnockBack(float forceMagnitude = 10, bool _enableYDirection = false)
    {
        
        isActive = true;
        magnitude = forceMagnitude;
        enableYDirection = _enableYDirection;
        ResetKnocBackDelay();


    }

    public void ResetKnocBackDelay()
    {
        StopCoroutine(nameof(ResetKnockBackCoroutine));
        StartCoroutine(nameof(ResetKnockBackCoroutine));

    }

    IEnumerator ResetKnockBackCoroutine()
    {
        yield return new WaitForSeconds(effectiveTime);
        isActive = false;
        rb.linearVelocityX = 0;
    }
}
