using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    private Image fadePanel;
    private TextMeshProUGUI quoteText;
    private CanvasGroup quoteCanvasGroup;
    public float fadeDuration = 1.5f;
    public float displayTime = 3.0f;
    public float textFadeDuration = 1.0f;

    private int currentQuoteIndex = 2;

    private string[] quotes = {
        "\"Only those who will risk going too far can possibly find out how far one can go.\" -T.S. Eliot",
        "Thanks for playing! Live wild, seek wonder, and let your heart chase the wind",
        "\"If you gaze long into an abyss, the abyss gazes also into you\"  -FromAbyss",
        "\"Falling stars, rising hearts.\" -SkyFall",
        "\"Not I, nor anyone else can travel that road for you. You must travel it yourself.\"  -W.W",
        "\"A journey of a thousand miles begins with a single step.\" -Lao Tzu",
        "\"He who jumps into the void owes no explanation to those who stand and watch.\"  -Jean-Luc Godard",
        "\"To explore is to live; to settle is to fade.\"  -Dev Team",
        "\"A river carves stone not by force, but by never stopping.\" -Dev Team",
    };

    public static LevelManager instance;
    private bool isFirstScene = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        FindUIElements();
    }

    void Start()
    {
        if (quoteText != null)
        {
            quoteText.gameObject.SetActive(false);
        }
    }

    void FindUIElements()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("TransitionUI");
        if (canvas == null)
        {
            Debug.LogError("Transition Canvas not found in scene! Make sure it exists.");
            return;
        }

        fadePanel = canvas.transform.Find("FadePanel")?.GetComponent<Image>();
        quoteText = canvas.transform.Find("QuoteText")?.GetComponent<TextMeshProUGUI>();
        if (quoteText != null)
        {
            quoteCanvasGroup = quoteText.GetComponent<CanvasGroup>();
            if (quoteCanvasGroup == null)
            {
                quoteCanvasGroup = quoteText.gameObject.AddComponent<CanvasGroup>();
            }
        }
        else
        {
            Debug.LogError("QuoteText not found in Transition Canvas!");
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    StartTransitionToNextLevel();
        //}
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    StartTransitionToRestartLevel();
        //}
    }

    public void StartTransitionToNextLevel()
    {
        
        if(ExternalDataManager.instance != null)
        {
            ExternalDataManager.instance.OnLevelTransition();
        }

        StartCoroutine(TransitionToNextLevel());
    }

    public void StartTransitionToRestartLevel()
    {
        //comment out since we use Q key to manually send data
        // SendToGoogle.instance.Send();
        // SendToGoogle.instance.ResetAll();
        StartCoroutine(TransitionToRestartLevel());
    }

    IEnumerator TransitionToNextLevel()
    {
        PlayerInput.instance.DisableGamePlayInputs();
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            yield return StartCoroutine(HandleLastLevel());
        }
        else
        {
            yield return StartCoroutine(HandleTransition(isNextLevel: true));
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneIndex);
            asyncLoad.allowSceneActivation = false;
            while (!asyncLoad.isDone)
            {
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }

    IEnumerator TransitionToRestartLevel()
    {
        PlayerInput.instance.DisableGamePlayInputs();
        yield return StartCoroutine(HandleTransition(isNextLevel: false));
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentSceneIndex);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    IEnumerator HandleLastLevel()
    {
        FindUIElements();
        fadePanel.gameObject.SetActive(true);
        quoteText.gameObject.SetActive(false);
        yield return StartCoroutine(Fade(0, 1, fadeDuration));

        quoteText.gameObject.SetActive(true);
        quoteText.text = quotes[1];
        quoteCanvasGroup.alpha = 1f;
    }

    IEnumerator HandleTransition(bool isNextLevel)
    {
        FindUIElements();
        fadePanel.gameObject.SetActive(true);
        quoteText.gameObject.SetActive(false);
        yield return StartCoroutine(Fade(0, 1, fadeDuration));

        //quoteText.text = isNextLevel ? (isFirstScene ? quotes[0] : quotes[Random.Range(2, quotes.Length - 1)]) : quotes[quotes.Length - 1];
        //if (isFirstScene && isNextLevel)
        //{
        //    isFirstScene = false;
        //}

        if (isNextLevel)
        {
            if (isFirstScene)
            {
                quoteText.text = quotes[0];
                isFirstScene = false;
            }
            else
            {
                quoteText.text = quotes[currentQuoteIndex];
                currentQuoteIndex = (currentQuoteIndex + 1) % quotes.Length;
                if (currentQuoteIndex < 2) currentQuoteIndex = 2; 
            }
        }
        else
        {
            quoteText.text = quotes[quotes.Length - 1];
        }

        quoteText.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayTime);

        yield return StartCoroutine(FadeOutText());
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        while (elapsedTime < textFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            quoteCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / textFadeDuration);
            yield return null;
        }
        quoteText.gameObject.SetActive(false);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(WaitForUIElements());
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    IEnumerator WaitForUIElements()
    {
        yield return new WaitForSeconds(0.1f);
        FindUIElements();
        if (fadePanel != null)
        {
            StartCoroutine(Fade(1, 0, fadeDuration));
        }
    }

    IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color color = fadePanel.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            fadePanel.color = color;
            yield return null;
        }
    }
}
