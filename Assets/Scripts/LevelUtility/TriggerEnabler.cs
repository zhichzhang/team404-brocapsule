using UnityEngine;

public class TriggerEnabler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public GameObject ObjectToEnable;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ObjectToEnable.activeSelf) return;
        if (collision.CompareTag("Player"))
        {
            // Enable the game object
            ObjectToEnable.SetActive(true);
        }
    }
}
