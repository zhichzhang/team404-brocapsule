using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SetColorOnEnable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<SpriteRenderer> sprites;

    public Color normal;
    public Color alert;

    private void OnEnable()
    {
        // Set the color of each sprite renderer to red
        foreach (var sprite in sprites)
        {
            sprite.color = alert;
        }
    }

    private void OnDisable()
    {
        // Reset the color of each sprite renderer to white
        foreach (var sprite in sprites)
        {
            sprite.color = normal;
        }
    }

}
