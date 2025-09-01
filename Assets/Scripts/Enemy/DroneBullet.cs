using UnityEngine;

public class DroneBullet : EnemyHitBoxBase,EnemyCanDoDamage
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed;
    public float lifeTime;
    private Rigidbody2D rb;
    public int damage = 10;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = -speed * gameObject.transform.up;
    }

    private void OnDisable()
    {
        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    public int HealthLost()
    {
        return damage;
    }
}
