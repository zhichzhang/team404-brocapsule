using UnityEngine;
using UnityEngine.UI;

public class UIInputButtonFadeInOut : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Image image;
    private TMPro.TextMeshProUGUI text;
    private bool isActive = false;
    public float alphaChangeSpeed; // Speed of alpha change
    public float finalAlpha = 1f; // Final alpha value when fully visible
    void Start()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (image == null || text == null)
        {
            
            Debug.LogError("No Image or TextMeshProUGUI component found on this GameObject.");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            Color color = image.color;
            color.a = Mathf.MoveTowards(color.a, finalAlpha, alphaChangeSpeed * Time.deltaTime);
            image.color = color;
            Color textColor = text.color;
            textColor.a = Mathf.MoveTowards(textColor.a, finalAlpha, alphaChangeSpeed * Time.deltaTime);
            text.color = textColor;
        }
        else
        {
            Color color = image.color;
            color.a = Mathf.MoveTowards(color.a, 0f, alphaChangeSpeed * Time.deltaTime);
            image.color = color;
            Color textColor = text.color;
            textColor.a = Mathf.MoveTowards(textColor.a, 0f, alphaChangeSpeed * Time.deltaTime);
            text.color = textColor;
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
