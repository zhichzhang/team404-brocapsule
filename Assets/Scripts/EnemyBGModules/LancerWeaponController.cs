using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LancerWeaponController : EnemyHitBoxBase,EnemyCanDoDamage
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //private int result;
    public LayerMask targetLayer;
    public int damage = 10;    





    public void OnEnable()
    {
        result = 0;
    }

    public override void playerDestroy(int _param)
    {
        result = _param;
        base.playerDestroy(_param);
        
    }
    public void OnDisable()
    {
        //Debug.Log(gameObject.name + "Disabled");
    }

    public int HealthLost()
    {
        return damage;
    }
}
