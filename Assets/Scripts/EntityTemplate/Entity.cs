using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb { get; private set; }
    public CinemachineImpulseSource impulseSource;
    [Header("Stats")]
    public int health;

    [Header("PlayerDetection")] // range for enemy to see player, is displayed in gizmo, check OnDrawGizmos()
    public Player player;// need to drag player from inspector
    public SpriteRenderer enemyPrototypeSprite;// need to drag player from inspector
    public SpriteRenderer enemyEye;// need to drag player from inspector
    public float playerDetectionRangeX;
    public float playerDetectionRangeY;
    public bool showDetectionBox;

    [Header("Attack")] // range for enemy to attack player, is displayed in gizmo, check OnDrawGizmos()
    public float attackRangeX;
    public float attackRangeY;
    public bool showAttackBox;
    public float attackCoolDown;
    public bool canAttack;


    [Header("Movement")]
    public bool facingRight = true;
    public int facingDir = 1;
    public float moveSpeed = 4;
    
    [Header("Collision")] // collision detection for ground check and wall check, is displayed in gizmo, check OnDrawGizmos()
    public Transform groundCheck;
    public float grounCheckDistance;
    public LayerMask whatIsGround;
    public Transform wallCheck;
    public float wallCheckDistance;
    public LayerMask whatIsWall;

    #region States
    //public EntityStateMachine stateMachine { get; private set; }
    //public ExampleEntityState idleState { get; private set; } // you still need to write you own state template
    #endregion

    public virtual void Awake()
    {
        //stateMachine = new EntityStateMachine();
        //idleState = new ExampleEntityIdleState(this, stateMachine, "Idle");
    }
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        health = 100;
        canAttack = true;
        //stateMachine.Initialize(idleState);// must be the last line in Start if your idle state enter function uses any pointers declared here.
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //stateMachine.currentState.Update();
    }

    public virtual bool IsGroundDetected()
    {
        bool groundCheckResult = Physics2D.Raycast(groundCheck.position, Vector2.down, grounCheckDistance, whatIsGround);

        return groundCheckResult;
    }
    public virtual bool IsWallDetected()
    {

        bool wallCheckResult = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsWall);

        return wallCheckResult;
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - grounCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        
        if (showDetectionBox)
        {

            Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y), new Vector3(playerDetectionRangeX, playerDetectionRangeY, 0));

        }
        if (showAttackBox)
        {

            Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y), new Vector3(attackRangeX, attackRangeY, 0));

        }


        //Gizmos.DrawWireCube((Vector2)transform.position + attackBoxCenterOffset, new Vector3(attackBoxWidth, attackBoxHeight,  0));
    }

    public virtual void Flip() // use to flip direction
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public virtual void FlipController(float _x) // use to flip direction based on input, usually speed or inputx
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
        else
        {

        }
    }

    public virtual void checkFacingDir() // use to flip direction to face player
    {
        int playerDirection = player.transform.position.x > transform.position.x ? 1 : -1;
        if (playerDirection != facingDir)
        {
            Flip();
        }
        return;
    }

    public (bool inRange, bool inAttackRange) PlayerInRange() // return a tuple where the first one is if player is in range and second one if player in attack range
    {
        float distX = Mathf.Abs(player.transform.position.x - transform.position.x);
        float distY = Mathf.Abs(player.transform.position.y - transform.position.y);



        bool playerInRange = distX <= playerDetectionRangeX / 2 && distY <= playerDetectionRangeY / 2;
        bool playerInAttackRange = distX <= attackRangeX / 2 && distY <= attackRangeY / 2;
        return (playerInRange, playerInAttackRange);
    }

    public void HitPauseAndCameraShake()
    {
        CameraShakeManager.instance.CameraShake(impulseSource);
        TimeManager.instance.SlowTime(0.07f, 0.1f);
    }
    public void SetAttackCoolDown()
    {
        StopCoroutine(nameof(SetCoolDownCoroutine));
        StartCoroutine(nameof(SetCoolDownCoroutine));
    }
    IEnumerator SetCoolDownCoroutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }

    public virtual void OnHitByProjectile()
    {
        // call this when an enemy projectile is defected and land back on enemy
    }

    public virtual void DestroyMe(float delay)
    {
        Destroy(gameObject, delay);
    }

}
