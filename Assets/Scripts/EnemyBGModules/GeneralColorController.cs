using UnityEngine;

public class GeneralColorController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor(Color color) // set color of all children
    {
        foreach (SpriteRenderer c in GetComponentsInChildren<SpriteRenderer>())
        {
            
                c.color = color;

        }
    }
}
