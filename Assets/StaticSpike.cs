using System.Collections;
using UnityEngine;

public class StaticSpike : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private SpikeAttackController spikeDamger;
    public float attackPeriod = 1f; // Time in seconds before the spike reactivates
    private Coroutine spikeCoroutine;
    void Start()
    {
        spikeDamger = GetComponentInChildren<SpikeAttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spikeDamger.gameObject.activeSelf)
        {
            if (spikeCoroutine == null)
            {
                spikeCoroutine = StartCoroutine(enableSpike());
            }
        }
    }


    IEnumerator enableSpike()
    {
        yield return new WaitForSeconds(attackPeriod);
        spikeDamger.gameObject.SetActive(true);
        spikeCoroutine = null;
    }
}
