using UnityEngine;

public class DroneWeaponController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AimAtPosition(Vector2 position)
    {
        Vector2 direction = position - (Vector2)transform.position;
        gameObject.transform.up = -direction;
    }

    public void ResetAimPosition()
    {
        gameObject.transform.rotation = Quaternion.identity;        
    }
    public void Fire()
    {
        //Debug.Log("Fire");   // Fire the weapon
    }
}
