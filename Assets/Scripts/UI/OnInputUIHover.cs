using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnInputUIHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Animator anim;
    public TMPro.TextMeshProUGUI text;
    public Image image;  
    public string originalText;
    public string newText;
    private bool isHovered = false;
    public float maxHoverTime = 0.5f;
    public float currentHoverTime;

    private void Update()
    {
        if (isHovered)
        {
            currentHoverTime += Time.deltaTime;
            if (currentHoverTime >= maxHoverTime)
            {
                currentHoverTime = maxHoverTime;
            }
        }
        else
        {
            currentHoverTime -= Time.deltaTime;
            if (currentHoverTime <= 0)
            {
                currentHoverTime = 0;
            }
        }

        float ratio = currentHoverTime / maxHoverTime;
        if (ratio > 0.5)
        {
            text.text = newText;
        }
        else
        {
            text.text = originalText;
        }

        anim.Play("Enlarge", 0, ratio);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + "Hovered");
        isHovered = true;
        
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        
    }
}
