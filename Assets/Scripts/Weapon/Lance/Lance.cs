using Unity.Cinemachine;
using UnityEngine;

public class Lance : PlayerWeapon
{
    [Header("Component")]
    public Rigidbody2D rb;
    public Collider2D col;
    public GameObject skin;
    public GameObject throwableLancePrefabHorizontal;
    public GameObject throwableLancePrefabVerticalUp;
    public GameObject throwableLancePrefabVerticalDown;
    public LayerMask ground;


    [Header("Initialization")]
    public Transform spwanPoint;
    public Transform shootingHPosition;
    public Transform shootingVPosition;

    [Header("Timer")]
    public float timer;

    [Header("Stats")]
    public bool isDisappear;

    #region States
    public LanceStateMachine stateMachine;
    public LanceIdleState idleState;
    public LanceAttackState attackState;
    public LanceDisappearState disappearState;
    #endregion

    private void Awake()
    {
        WeaponID = 0;
        UnEquip();
    }

    void Start()
    {

        isDisappear = false;

        stateMachine = new LanceStateMachine(this);
        idleState = new LanceIdleState(stateMachine,this);
        attackState = new LanceAttackState(stateMachine, this);
        disappearState = new LanceDisappearState(stateMachine, this);

        stateMachine.Initialize(idleState);

    }

    private void OnEnable()
    {
        transform.position = spwanPoint.position;
        UnEquip();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();
        timerUpdate();
    }

    private void timerUpdate()
    {
        timer = Mathf.Max(0,timer-Time.deltaTime);
    }

    public override void attack(AttackInfo ai)
    {
        
        base.attack(ai);
        if (!isDisappear)
        {
            if (player.ManaCtrl.CostMana(1))
            {
                stateMachine.ChangeState(attackState, ai);
                return;
            }
            // TODO: add not enough mana feed back
        }
    }

    public override void skill(AttackInfo ai)
    {
        base.skill(ai);
    }

    public override void ActivateWeapon()
    {
        base.ActivateWeapon();
    }

    public override void DeactivateWeapon()
    {
        base.DeactivateWeapon();
    }

    public override void Equip()
    {
        base.Equip();
        Appear();

    }

    public override void UnEquip()
    {
        base.UnEquip();
        Disappear();
    }

    public void Disappear()
    {
        col.enabled = false;
        skin.SetActive(false);
        isDisappear = true;
    }

    public void Appear()
    {
        col.enabled = true;
        skin.SetActive(true);
        isDisappear = false ;
    }
}
