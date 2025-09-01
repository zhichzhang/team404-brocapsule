using System.Collections;
using UnityEngine;

public class OnDisableEffect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject onDisableEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnDisable()
    {
        

        //Destroy(gameObject, 0.1f);
        
        Instantiate(onDisableEffect, transform.position, Quaternion.identity);
        
    }

    public void DestroyMe()
    {
    }
    
    IEnumerator DelayDestoryCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
