using Unity.Behavior;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    public GameObject onDestroyEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyMe()
    {
        //TODO: enemy death +1 event
        if (onDestroyEffect != null)
        {
            Instantiate(onDestroyEffect, transform.position, Quaternion.identity);
        }
        GetComponent<BehaviorGraphAgent>().enabled = false;
        Destroy(gameObject, 0.1f);
    }
}
