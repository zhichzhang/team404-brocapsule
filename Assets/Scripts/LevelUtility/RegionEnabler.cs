using System.Collections;
using UnityEngine;

public class RegionEnabler : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textRelated;
    public float fadeDuration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textRelated.color = new Color(textRelated.color.r, textRelated.color.g, textRelated.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Enable the region
            Color currentColor = textRelated.color;
            StopAllCoroutines(); 
            StartCoroutine(FadeInUICoroutine(1));


        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Disable the region
            if (textRelated == null) return;
            Color currentColor = textRelated.color;
            StopAllCoroutines();
            StartCoroutine(FadeOutUICoroutine(0));

        }
    }

    IEnumerator FadeInUICoroutine(float targetAlpha)
    {
        float time = 0;
        Color currentColor = textRelated.color;
        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(currentColor.a, targetAlpha, time / fadeDuration);
            textRelated.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }
        textRelated.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
    }

    IEnumerator FadeOutUICoroutine(float targetAlpha)
    {
        float time = 0;
        Color currentColor = textRelated.color;
        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(currentColor.a, targetAlpha, time / fadeDuration);
            textRelated.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }
        textRelated.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
    }
}
