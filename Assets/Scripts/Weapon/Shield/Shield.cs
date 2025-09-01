using UnityEngine;

public class Shield : PlayerWeapon
{

    [Header("Component")]
    public Rigidbody2D rb;
    public Collider2D col;
    public GameObject skin;
    public Transform spwanPoint;

    public bool isDisappear;


    private void Awake()
    {
        WeaponID = 1;
        UnEquip();
    }

    private void OnEnable()
    {
        transform.position = spwanPoint.position;
        UnEquip();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public override void attack(AttackInfo ai)
    {
        base.attack(ai);
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
        isDisappear = false;
    }


}
