using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;

//using Cinemachine;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public PlayerInput input;
    //TODO:Discard when implemnt UI
    public enum BattleInfo
    {
        Peace,
        Grab,
        Deflect,
        Doge,
        Attack,
        Hit
        
    }
    [Header("UI")]
    public PlayerEmbeddedUI playerEmbeddedUI;
    // TODO: call spearPopUp.Pop() when player throw spaer upward
    // TODO: call spearPopDown.Pop() when player throw spear downward
    public SpearPop spearPopUp;
    public SpearPop spearPopDown;

    [Header("Info")]
    public SpriteRenderer playerPrototypeSprite;
    public SpriteRenderer Bleeding;
    [HideInInspector]
    public float gravityScale { get; private set; } // a copy of rb.gravityScale for ladder movement
    

    [Header("LevelCollision")]
    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public float groundCheckDistance;
    public Transform wallCheckTop;
    public Transform wallCheckBottom;
    public float wallCheckDistance;
    public LayerMask level;
    public bool ladderCheck;
    public DrpSpearVertical currentInteractingSpear;

    [Header("Input")]
    public bool canDash;
    public bool canComboAttack;


    [Header("GroundMovement")]
    [SerializeField] private Vector2 rawSpeed;
    public float HorizontalSpeedFalling;
    public float HorizontalSpeedGround;
    // acceleration and deceleration
    public float HorizontalAcceleration;
    public float HorizontalDeceleration;
    public int facingDir;
    public float JumpInitialSpeed;
    public int JumpCounter;
    public bool jumpable => JumpCounter > 0;

    [Header("LadderMovement")]
    public float ladderHorizontalSpeed;
    public float ladderVerticalSpeed;
    [Tooltip("Expected duration of the snapping until play x position is the same as ladder x position, smaller value results in faster snap")]
    public float ladderSnapTime;
    public float ladderRemountCoolDown = 0.1f;
    public Timer ladderRemountCoolDownTimer;
    public float ladderCenterDeadZone = 0.01f;
    [HideInInspector]
    public float ladderSnapSpeedX = 0;
    public float ladderSnapSpeedY = 0;

    [Header("WallJump")]
    public float wallSlideSpeed;
    public float wallJumpSpeedX;
    public float wallJumpSpeedY;
    public float wallJumpFreeze;
    public Timer wallJumpFreezeTimer;

    [Header("Deflect")]
    public GameObject deflectBox;
    public float deflectDuration;
    public Timer deflectTimer;
    public float deflectCoolDown;
    public Timer deflectCoolDownTimer;
    public float deflectJumpSpeed;
    public int deflectSignal;

    [Header("Grab")]
    public GameObject grabBox;
    public float grabDuration;
    public Timer grabTimer;


    [Header("Roll")]
    public float rollSpeed;
    public float rollCoolDown;
    public Timer rollCoolDownTimer;
    public float rollDuration;
    public Timer rollDurationTimer;
    public float rollInvincibleDuration;

    [Header("DamagedPenalty")]
    public float knockbackForceMultiplier;
    public float KnockBackDuration;
    public Timer KnockBackTimer;

    [Header("BattleInfo")]
    public BattleInfo battleInfo;
    public GameObject trigger;

    [Header("HitBox")]
    public GameObject InvincibleBox;
    public GameObject HitBox;

    [Header("Weapon")]
    public PlayerWeapon weapon0;
    public PlayerWeapon weapon1;
    public WeaponsDiction weaponDictionary;

    [Header("Stats")]
    public int MaxMana = 2;
    public int Mana;
    public int MaxHealth = 9;
    public int Health;

    [Header("Animation")]
    [SerializeField] private string animState;
    public Animator anim;

    [Header("CameraEffect")]
    public CinemachineImpulseSource impulseSource;
    public Vector2 vector2mostRecentHit;

    [Header("LevelInfo")]
    public Vector3 LastGoodPosition;


    #region Components
    [Header("Components")]
    // public Animator anim;
    public Rigidbody2D rb { get; private set; }
    #endregion

    #region Controllers
    public TimerCountDownController TimerCountDownCtrl { get; private set; }
    public GroundMoveController GroundMoveCtrl { get; private set; }
    public FlipController FlipCtrl { get; private set; }
    public InputBufferController InputBufferCtrl { get; private set; }
    public LevelCollisionController LevelCollisionCtrl { get; private set; }
    public AirMoveController AirMoveCtrl { get; private set; }
    public JumpController JumpCtrl { get; private set; }
    public RollController RollCtrl { get; private set; }
    public WallMovementController WallMovementCtrl { get; private set; }
    public DeflectController DeflectCtrl { get; private set; }
    public GrabController GrabCtrl { get; private set; }
    public KnockPlayerBackController KnockBackCtrl { get; private set; }
    public OnFlyableController OnFlyableCtrl { get; private set; }
    public WeaponController WeaponCtrl { get; private set; }
    public ManaController ManaCtrl { get; private set; }
    public HealthController HealthCtrl { get; private set; }
    public LadderMoveController LadderMoveCtrl { get; private set; }
    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerState idleState { get; private set; }
    public PlayerState moveState { get; private set; }
    public PlayerState fallState { get; private set; }
    public PlayerState jumpState { get; private set; }
    public PlayerState rollState { get; private set; }
    public PlayerState dashState { get; private set; }
    public PlayerState onDamageState { get; private set; }
    public PlayerState deflectState { get; private set; }
    public PlayerState grabState { get; private set; }
    public PlayerState deflectRewardState { get; private set; }
    public PlayerState grabRewardState { get; private set; }
    public PlayerState damagePenaltyState { get; private set; }
    public PlayerState ladderMoveState { get; private set; }
    public PlayerState attackState { get; private set; }
    #endregion

    private void Awake()
    {

        input = GetComponent<PlayerInput>();

        TimerCountDownCtrl = new TimerCountDownController(this);
        GroundMoveCtrl = new GroundMoveController(this);
        FlipCtrl = new FlipController(this);
        InputBufferCtrl = new InputBufferController(this);
        LevelCollisionCtrl = new LevelCollisionController(this);
        AirMoveCtrl = new AirMoveController(this);
        JumpCtrl = new JumpController(this);
        RollCtrl = new RollController(this);
        WallMovementCtrl = new WallMovementController(this);
        DeflectCtrl = new DeflectController(this);
        GrabCtrl = new GrabController(this);
        KnockBackCtrl = new KnockPlayerBackController(this);
        OnFlyableCtrl = new OnFlyableController(this);
        WeaponCtrl = new WeaponController(this);
        ManaCtrl = new ManaController(this);
        HealthCtrl = new HealthController(this);
        LadderMoveCtrl = new LadderMoveController(this);



        stateMachine = new PlayerStateMachine(this);
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        fallState = new PlayerFallState(this, stateMachine, "Fall");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        rollState = new PlayerRollState(this, stateMachine, "Dash");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        deflectState = new PlayerDeflectState(this, stateMachine, "Deflect");
        grabState = new PlayerGrabState(this, stateMachine, "Grab");
        deflectRewardState = new PlayerDeflectRewardState(this, stateMachine, "DeflectReward");
        grabRewardState = new PlayerGrabRewardState(this, stateMachine, "GrabReward");
        damagePenaltyState = new PlayerDamagedPenalyState(this, stateMachine, "DamagePenalty");
        ladderMoveState = new PlayerLadderMoveState(this, stateMachine, "LadderMove");
        attackState = new PlayerAttackState(this, stateMachine, "Skill");

        deflectBox.SetActive(false);
        grabBox.SetActive(false);
        InvincibleBox.SetActive(false);

        battleInfo = BattleInfo.Peace;
        //Mana = MaxMana;
        Health = MaxHealth;

        //input
        canDash = false;
        canComboAttack = false;

    }

    private void Start()
    {
        
        //assign component
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
        weaponDictionary = GetComponentInChildren<WeaponsDiction>();
        impulseSource = GetComponent<CinemachineImpulseSource>();

        input.EnableGamePlayInputs();
        stateMachine.Initialize(fallState);

        //TODO: have some weaponCache
        WeaponCtrl.ActiveWP(0);
        WeaponCtrl.EquipWP(0,0);
        WeaponCtrl.ActiveWP(1);
        WeaponCtrl.EquipWP(1, 1);

        //anim.Play("GroundMove");


    }

    private void Update()
    {
        // set buffer before state update
        InputBufferCtrl.SetBufferOnInput();

        //Timer count down
        TimerCountDownCtrl.Update();

        //battbleTriggered StateChange
        if(battleInfo!= BattleInfo.Peace)
        {
            OnBattle();
        }

        // stateMachine update second; aleast 0 frame on Playerstate.update() is called()
        stateMachine.currentState.Update();


        //Check if swtich weapon is pressed
        WeaponCtrl.checkSwtichPressed();

        //Debug
        rawSpeed = rb.linearVelocity;
        //anim.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        //Debug.Log(LastGoodPosition);



    }

    private void LateUpdate()
    {
        stateMachine.currentState.LateUpdate();
    }

    private void OnDestroy()
    {
        
    }

    void OnGUI()
    {
        // Set the font size
        //TODO:Discard when implemnt UI
        //GUIStyle bigFontStyle = new GUIStyle(GUI.skin.label);
        //bigFontStyle.fontSize = 16;
        //GUI.Label(new Rect(200, 100, 200, 200), "playerState: " + stateMachine.currentState.animBoolName, bigFontStyle);
        //GUI.Label(new Rect(200, 120, 200, 200), "Mana: " + Mana, bigFontStyle);
        //GUI.Label(new Rect(200, 140, 200, 200), "Health: " + Health, bigFontStyle);
        //GUI.Label(new Rect(200, 160, 200, 200), "Jumpable: " + JumpCounter, bigFontStyle);
    }


    private void OnBattle()
    {
        switch (battleInfo)
        {
            case BattleInfo.Grab:
                battleInfo = BattleInfo.Peace;
                stateMachine.ChangeState(grabRewardState);
                break;
            case BattleInfo.Deflect:
                battleInfo = BattleInfo.Peace;
                //stateMachine.ChangeState(deflectRewardState);
                // replace deflect reward logic here
                deflectSignal = 1;

                break;
            case BattleInfo.Doge:
                battleInfo = BattleInfo.Peace;
                ManaCtrl.AddMana(1);
                if(ExternalDataManager.instance != null)
                {
                    ExternalDataManager.instance.PlayerDodgeSuccess();
                }
                break;
            case BattleInfo.Hit:
                battleInfo = BattleInfo.Peace;
                stateMachine.ChangeState(damagePenaltyState);
                break;
            case BattleInfo.Attack:
                //TODO:Implement Attack reward state
                break;
        }
    }

    public void GoInvincible(float Duration)
    {
        StopCoroutine(InvincibleCoroutine(Duration));
        StartCoroutine(InvincibleCoroutine(Duration));
    }

    IEnumerator InvincibleCoroutine(float Duration)
    {
        InvincibleBox.SetActive(true);
        HitBox.SetActive(false);
        yield return new WaitForSeconds(Duration);
        InvincibleBox.SetActive(false);
        HitBox.SetActive(true);
        battleInfo = BattleInfo.Peace;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheckLeft.position, groundCheckLeft.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(groundCheckRight.position, groundCheckRight.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(wallCheckTop.position, wallCheckTop.position + facingDir*Vector3.right * wallCheckDistance);
        Gizmos.DrawLine(wallCheckBottom.position, wallCheckBottom.position + facingDir*Vector3.right * wallCheckDistance);
    }
    

}
