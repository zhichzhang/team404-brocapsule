using UnityEngine;

public class OnDestroyEffect : MonoBehaviour
{
    public GameObject onDestroyEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnDestroy()
    {
        if (onDestroyEffect != null)
        {
            Instantiate(onDestroyEffect, transform.position, Quaternion.identity);
        }
    }
}
