using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class SpearConfirm : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public enum SpearType
    {
        Vertical,
        Horizontal
    }
    private SpriteRenderer confirmBox;
    private Light2D confirmBoxLight;
    public SpearType spearType;
    private bool confirmed = false;
    public float areaLength = 5f;
    public Vector2 offset = Vector2.zero; // Added offset vector

    void Start()
    {
        confirmBox = GetComponent<SpriteRenderer>();
        confirmBoxLight = GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!confirmed)
        {
            DetectSpear();
        }
        else
        {
            StartCoroutine(ChangeLightIntensity());
        }
    }

    private IEnumerator ChangeLightIntensity()
    {
        float elapsedTime = 0f;
        float duration = 0.2f;

        while (elapsedTime < duration)
        {
            confirmBoxLight.intensity = Mathf.Lerp(0, 1, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        confirmBoxLight.intensity = 1;
        elapsedTime = 0f;
        duration = 0.2f;

        while (elapsedTime < duration)
        {
            confirmBoxLight.intensity = Mathf.Lerp(1, 0, elapsedTime / duration);
            confirmBox.color = new Color(confirmBox.color.r, confirmBox.color.g, confirmBox.color.b, Mathf.Lerp(confirmBox.color.a, 0, elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        confirmBoxLight.intensity = 0;
        confirmBox.color = new Color(confirmBox.color.r, confirmBox.color.g, confirmBox.color.b, 0);

        // Destroy the game object and its children
        Destroy(gameObject);
    }

    public void DetectSpear()
    {
        Vector2 position = (Vector2)transform.position + offset; // Apply offset to position
        Vector2 size = new Vector2(areaLength, areaLength);
        Collider2D[] colliders = Physics2D.OverlapAreaAll(position - size / 2, position + size / 2);

        foreach (var collider in colliders)
        {
            if (spearType == SpearType.Vertical)
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("Interactable") && collider.CompareTag("Ladder"))
                {
                    if (collider.gameObject.GetComponent<DrpSpearVertical>().state == DrpSpearVertical.SpearState.OnGround)
                    {
                        confirmed = true;
                        return;
                    }
                }
            }
            else if (spearType == SpearType.Horizontal)
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("MovingPlatform") && collider.CompareTag("MovingPlatform"))
                {
                    if (collider.gameObject.GetComponent<DraupnirSpear>().state == DraupnirSpear.SpearState.OnWall)
                    {
                        confirmed = true;
                        return;
                    }
                }
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 position = (Vector2)transform.position + offset; // Apply offset to position
        Vector2 size = new Vector2(areaLength, areaLength);
        Gizmos.DrawWireCube(position, size);
    }
}
