using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    private bool myLock = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myLock)
        {
            return;
        }
        if (collision.CompareTag("Player"))
        {
            myLock = true;
            LevelManager.instance.StartTransitionToNextLevel();
        }
    }
}
