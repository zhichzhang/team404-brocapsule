using UnityEngine;

public class SpearDamage : MonoBehaviour,CanDoDamage
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int damage = 70;
    public int GetDamage()
    {
        return damage;
    }   

}
