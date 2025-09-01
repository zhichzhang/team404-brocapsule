using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class DeflectTutorialUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private List<SpriteRenderer> spriteRenderers;
    private Light2D light2d;
    private bool isActive = false;
    public float changeDuration;
    private Dictionary<SpriteRenderer, float> initialAlphas;

    void Start()
    {
        spriteRenderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
        light2d = GetComponentInChildren<Light2D>();
        initialAlphas = new Dictionary<SpriteRenderer, float>();
        foreach (var spriteRenderer in spriteRenderers)
        {
            initialAlphas[spriteRenderer] = spriteRenderer.color.a;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    Show();
        //}
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    Hide();
        //}

        if (isActive)
        {
            foreach (var spriteRenderer in spriteRenderers)
            {
                Color color = spriteRenderer.color;
                color.a = Mathf.MoveTowards(color.a, initialAlphas[spriteRenderer], Time.deltaTime / changeDuration);
                spriteRenderer.color = color;
            }
            if (light2d != null)
            {
                light2d.intensity = Mathf.MoveTowards(light2d.intensity, 2f, Time.deltaTime / changeDuration);
            }
        }
        else
        {
            foreach (var spriteRenderer in spriteRenderers)
            {
                Color color = spriteRenderer.color;
                color.a = Mathf.MoveTowards(color.a, 0f, Time.deltaTime / changeDuration);
                spriteRenderer.color = color;
            }
            if (light2d != null)
            {
                light2d.intensity = Mathf.MoveTowards(light2d.intensity, 0f, Time.deltaTime / changeDuration);
            }
        }
    }

    public void Show()
    {
        isActive = true;
    }
    public void Hide()
    {
        isActive = false;
    }
}
