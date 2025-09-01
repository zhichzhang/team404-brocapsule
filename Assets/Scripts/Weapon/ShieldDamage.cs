using UnityEngine;

public class ShieldDamage : MonoBehaviour, CanDoDamage
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int damage = 50;
    public int GetDamage()
    {
        return damage;
    }

}
