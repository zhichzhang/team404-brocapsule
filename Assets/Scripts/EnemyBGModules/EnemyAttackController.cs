using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Collider2D atkCldr;
    public Collider2D bodyCldr;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartAttack()
    {
        atkCldr.enabled = true;
    }
    public void EndAttack()
    {
        atkCldr.enabled = false;
    }


}
