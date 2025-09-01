using UnityEngine;

public class DPStoneAttack :  EnemyHitBoxBase, EnemyCanDoDamage
{
    public int damage;

public int HealthLost()
{
    return damage;
}
}
