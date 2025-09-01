using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RenableMe : MonoBehaviour
{
    public Transform follow;
    public float timeToRenableAfterDisable;

    private void Update()
    {
        if (follow == null)
        {
            return;
        }
        transform.SetPositionAndRotation(follow.position, follow.rotation);
    }

    IEnumerator DisableMeCoroutine()
    {
        yield return new WaitForSeconds(timeToRenableAfterDisable);
        
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(DisableMeCoroutine));
    }
}
