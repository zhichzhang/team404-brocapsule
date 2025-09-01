using UnityEngine;

public class TriggerDisabler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    public GameObject ObjectToDisable;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ObjectToDisable.activeSelf) return;
        if (collision.CompareTag("Player"))
        {
            // Enable the game object
            ObjectToDisable.SetActive(false);
        }
    }
}
