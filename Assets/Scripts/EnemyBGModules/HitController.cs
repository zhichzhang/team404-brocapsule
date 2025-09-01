using Unity.VisualScripting;
using UnityEngine;

public class HitController : MonoBehaviour,Deflectable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public KnockBackController knockBackController;
    public Transform dfTransform { get; set; }
    private bool isActive;
    private int hitResult;
    private Rigidbody2D rb;
    private Rigidbody2D rbParent;
    private bool isGrabbed;

    public bool grabbable { get; set; }
    public int id => 0;
    void Start()
    {
        knockBackController = GetComponentInParent<KnockBackController>();
        rb = GetComponent<Rigidbody2D>();
        rbParent = GetComponentInParent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        EventManager.StartListening<Deflectable>("PlayerDeflecting", OnDeflect);
        EventManager.StartListening<Deflectable>("PlayerGettingHit", OnSuccess);
        EventManager.StartListening<Deflectable>("PlayerEvading", OnFailure);
        EventManager.StartListening<Deflectable>("PlayerGrabbing", OnGrab);
    }

    private void OnDestory()
    {
        EventManager.StopListening<Deflectable>("PlayerDeflecting", OnDeflect);
        EventManager.StopListening<Deflectable>("PlayerGettingHit", OnSuccess);
        EventManager.StopListening<Deflectable>("PlayerEvading", OnFailure);
        EventManager.StopListening<Deflectable>("PlayerGrabbing", OnGrab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (isEffective)
    //    {
    //        if (other.CompareTag("Player"))
    //        {
    //            int result = other.GetComponent<Player>().OnDamage();
    //            switch (result)
    //            {
    //                case 0:
    //                    // dodged the attack, keep effective
    //                    hitResult = 0;
    //                    break;
    //                case 1:
    //                    // parried the attack, not effective anymore

    //                    isEffective = false;
    //                    hitResult = 1;
    //                    break;
    //                case 2:
    //                    isEffective = false;
    //                    hitResult = 2;
    //                    break;
    //            }
    //        }
    //    }
    //}

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log(isActive);
        if (isActive)
        {
            
                Debug.Log($"{gameObject.name} is trying to damage {other.name}");

                EventManager.TriggerEvent("EnemyAttacking", this);
           
                
            
            

        }
    }

    public int GetHitResult()
    {
        return hitResult;
    }

    public void StartAttackCheck()
    {
        isActive = true;
        hitResult = 0;
        //Debug.Log("StartAttackCheckddddddddddddd");
    }

    public void StopAttackCheck()
    {
        isActive = false;
    }


    private void OnDestroy()
    {
        EventManager.StopListening<Deflectable>("PlayerDeflecting", OnDeflect);
        EventManager.StopListening<Deflectable>("PlayerGettingHit", OnSuccess);
        EventManager.StopListening<Deflectable>("PlayerEvading", OnFailure);
    }

    private void OnDeflect(Deflectable attackingEnemy)
    {
        if (ReferenceEquals(attackingEnemy, this))
        // Check if this enemy is the one attacking and deflected
        // Stay stuned and not attacking for 5s;
        {
            if (isActive)
            {
                Debug.Log($"{name}'s attack was deflected ! Enter stun for 5s");
                isActive = !isActive;
                hitResult = 2;
                if (knockBackController == null)
                {
                    Debug.Log("KnockBackController is not set");
                }
                else
                {
                    knockBackController.KnockBack();
                }
                
            }
        }
    }
    private void OnSuccess(Deflectable df)
    {
        if (ReferenceEquals(df, this))
        // Check if this enemy is the one attacking and deflected
        // Stay stuned and not attacking for 5s;
        {
            //Debug.Log("Lllllllll");
            if (isActive)
            {
                Debug.Log($"{name}'s attack hit player! Good Job");
                //disable current attack
                isActive = !isActive;
                hitResult = 1;

            }
        }
    }
    private void OnFailure(Deflectable df)
    {
        if (ReferenceEquals(df, this))
        // Check if this enemy is the one attacking and deflected
        // Stay stuned and not attacking for 5s;
        {
            if (isActive)
            {
                Debug.Log($"{name}'s attack Evaded by Player, Pitty!");
            }
        }
    }

    private void OnGrab(Deflectable df)
    {
        if (ReferenceEquals(df, this))
        // Check if this enemy is the one attacking and deflected
        // Stay stuned and not attacking for 5s;
        {
            if (grabbable)
            {
                isGrabbed = true;
                UnityEngine.Debug.Log($"{name}'s attack is Grabbed by player, Destroy");
            }
        }
    }
    public void BecomeGrabbable()
    {
        grabbable = true;
    }
    public void BecomeInGrabbale()
    {
        grabbable = false;
    }
    public Transform GetTransform() { return transform; }

    public bool canGrab()
    {
        return grabbable;
    }
    public int getID() { return id; }
    public bool isDrop()
    {
        return false;
    }
    public bool IsGrabbed() {
        return isGrabbed;
    }

    
}
