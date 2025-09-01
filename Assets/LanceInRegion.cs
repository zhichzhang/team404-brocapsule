using UnityEngine;

public class LanceInRegion : MonoBehaviour
{
    public float areaLength = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool DetectInteractableLadder()
    {
        Vector2 position = transform.position;
        Vector2 size = new Vector2(areaLength, areaLength);
        Collider2D[] colliders = Physics2D.OverlapAreaAll(position - size / 2, position + size / 2);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Interactable") && collider.CompareTag("Ladder"))
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 position = transform.position;
        Vector2 size = new Vector2(areaLength, areaLength);
        Gizmos.DrawWireCube(position, size);
    }
}
