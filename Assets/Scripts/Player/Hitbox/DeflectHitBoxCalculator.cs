using System;
using System.Collections.Generic;
using UnityEngine;

public class DeflectHitBoxCalculator : MonoBehaviour
{
    public Rigidbody2D rb;
    public Player player;
    public Vector3 offSet;
    public Collider2D triggerCollider;
    public LayerMask contactLayer;
    private ContactFilter2D contactFilter;
    private List<Collider2D> detectedColliders = new List<Collider2D>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    private void Awake()
    {
        player = GetComponentInParent<Player>();
        rb = GetComponent<Rigidbody2D>();
        triggerCollider = GetComponent<Collider2D>();
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
            if (collider.gameObject.CompareTag("EnemyAttackBox"))
            {
                player.trigger = collider.gameObject;
                // aquire vector from player to delfect contact point
                player.vector2mostRecentHit = collider.transform.position - player.transform.position;
                player.vector2mostRecentHit.Normalize();
                player.battleInfo = Player.BattleInfo.Deflect;
                player.trigger.gameObject.GetComponent<EnemyHitBoxBase>().playerDestroy(2);
                break;
            }
        }
    }
    private void OnEnable()
    {
        //if (player.facingDir == 1)
        //{
        //    transform.position = new Vector2(player.transform.position.x + offSet.x, player.transform.position.y + offSet.y);
        //}
        //else
        //{
        //    transform.position = new Vector2(player.transform.position.x - offSet.x, player.transform.position.y + offSet.y);
        //}
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        contactFilter.useTriggers = true;
        contactFilter.SetLayerMask(contactLayer);

    }
}
