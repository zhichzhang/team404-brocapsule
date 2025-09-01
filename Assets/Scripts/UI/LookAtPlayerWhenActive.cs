using System.Collections;
using UnityEngine;

public class LookAtPlayerWhenActive : MonoBehaviour
{
    public bool isActive;
    private SpriteRenderer spriteRenderer;
    public float targetAlpha;
    public float transitionTime;
    private float currentAlpha;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        currentAlpha = 0f;
        isActive = false;
    }

    
    private void Update()
    {
        // Approach target alpha based on whether it is active or not
        float targetAlphaValue = isActive ? targetAlpha : 0f;
        currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlphaValue, (targetAlpha / transitionTime) * Time.deltaTime);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, currentAlpha);

        //if (isActive)
        //{
        //    Transform playerTransform = PlayerInfo.instance.player.transform;
        //    Vector2 direction = playerTransform.position - transform.position;
        //    transform.up = direction;
        //}
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
