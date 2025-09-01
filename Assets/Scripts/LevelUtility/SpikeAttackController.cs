using UnityEngine;

/// <summary>
/// This class is used to control spike attack, spike is a tiled sprite, and collider size will be synced with the sprite size.
/// </summary>

public class SpikeAttackController : EnemyHitBoxBase, EnemyCanDoDamage
{
    public int damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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


    public int HealthLost()
    {
        return damage;
    }
}
