using System.Collections.Generic;
using UnityEngine;

public class  HitBoxCalculator: MonoBehaviour
{
    public Rigidbody2D rb;
    public Player player;
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

    }
    void LateUpdate()
    {
        if(player.battleInfo == Player.BattleInfo.Peace)
        {
            checkAllColliders();
        }
    }

    public void checkAllColliders()
    {
        detectedColliders.Clear();
        Physics2D.OverlapCollider(triggerCollider, contactFilter, detectedColliders);
        foreach (var collider in detectedColliders)
        {
            if (collider.gameObject.CompareTag("EnemyAttackBox"))
            {
                Debug.Log("Hit by enemy attack box" + collider.gameObject.name);
                player.trigger = collider.gameObject;
                player.battleInfo = Player.BattleInfo.Hit;
                player.trigger.gameObject.GetComponent<EnemyHitBoxBase>().playerDestroy(1);
                break;
            }
        }
    }
    private void OnEnable()
    {
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

}
