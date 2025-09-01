using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeBlackOut : MonoBehaviour
{
    public static FadeBlackOut Instance { get; private set; }

    private Image fadeImage;
    private float fadeDuration = 1.0f; // Duration of the fade effect in seconds

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        fadeImage = GetComponent<Image>();
    }

    void Update()
    {
        
    }

    public void BlackOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeToBlackAndBack());
    }

    private IEnumerator FadeToBlackAndBack()
    {
        // Set alpha to 1 immediately
        Color finalColor = fadeImage.color;
        finalColor.a = 1;
        fadeImage.color = finalColor;

        // Fade back to transparent
        for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime)
        {
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(1, 0, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Ensure it's fully transparent
        finalColor.a = 0;
        fadeImage.color = finalColor;
    }
}
