using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float lifeTime;
    void Start()
    {
        Destroy(gameObject, lifeTime); 
    }



}
