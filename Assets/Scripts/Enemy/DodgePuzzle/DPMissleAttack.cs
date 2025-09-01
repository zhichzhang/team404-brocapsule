using UnityEngine;

public class DPMissleAttack : EnemyHitBoxBase, EnemyCanDoDamage
{
    public int damage;

    public int HealthLost()
    {
        return damage;
    }
}
