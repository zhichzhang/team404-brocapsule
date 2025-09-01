using UnityEngine;

public class SpikeDamge : MonoBehaviour, CanDoDamage
{
    public int damage = 2;

    private void Start()
    {
        // sync collider size with parent sprite
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer parentSpriteRenderer = GetComponentInParent<SpriteRenderer>();

        if (boxCollider != null && parentSpriteRenderer != null)
        {
            boxCollider.size = parentSpriteRenderer.size;
        }
    }

    public int GetDamage()
    {
        return damage;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
}


