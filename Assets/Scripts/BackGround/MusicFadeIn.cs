using UnityEngine;

public class MusicFadeIn : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeDuration = 3f; 

    void Start()
    {
        audioSource.volume = 0f;
        audioSource.Play();
        StartCoroutine(FadeIn());
    }

    System.Collections.IEnumerator FadeIn()
    {
        float currentTime = 0f;
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, 1f, currentTime / fadeDuration);
            yield return null;
        }
        audioSource.volume = 1f; 
    }
}
