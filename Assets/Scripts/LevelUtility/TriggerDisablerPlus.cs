using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TriggerDisablerPlus : MonoBehaviour
{
    public List<GameObject> objectsRelated;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected virtual bool CertainCondition()
    {
        return true;
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!CertainCondition()) return;
            // Enable the region
            foreach (GameObject obj in objectsRelated)
            {
                if (obj.activeSelf)
                {
                    obj.SetActive(false);
                }
            }
        }
    }


}
