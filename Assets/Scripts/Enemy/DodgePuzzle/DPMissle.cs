using UnityEngine;

public class DPMissle : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f; 

    void Start()
    {
        
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
