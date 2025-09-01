using UnityEngine;

public class PlayerFeetController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Player player;
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPlayerParent(Transform parentTransform)
    {
        player.transform.parent = parentTransform;
    }

    public void ResetPlayerParent()
    {
        player.transform.parent = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingPlatform"))
        {
            SetPlayerParent(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingPlatform"))
        {
            ResetPlayerParent();
        }
    }
}
