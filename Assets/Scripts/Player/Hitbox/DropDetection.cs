using System.Collections.Generic;
using UnityEngine;

public class DropDetection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D rb;
    private Player player;

    public Collider2D triggerCollider;
    public LayerMask contactLayer;
    private ContactFilter2D contactFilter;
    private List<Collider2D> detectedColliders = new List<Collider2D>();

    void Start()
    {
        player = GetComponentInParent<Player>();
        contactFilter.useTriggers = true;
        contactFilter.SetLayerMask(contactLayer);
    }

    // Update is called once per frame
    void Update()
    {
        checkAllColliders();
    }

    public void checkAllColliders()
    {
        detectedColliders.Clear();
        int a = Physics2D.OverlapCollider(triggerCollider, contactFilter, detectedColliders);

        foreach (var collider in detectedColliders)
        {
            
            player.ManaCtrl.AddMana(collider.gameObject.GetComponent<Drop>().GetEnergy());
            // Energy ball will destroy itself after disabled
            Destroy(collider.gameObject);
            break;
        }
    }
}
