using System;
using System.Collections.Specialized;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnergyDrop : MonoBehaviour,Drop
{
    public Rigidbody2D rb;
    public SpriteRenderer circle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {

    }
    public int GetEnergy()
    {
        return 1;
    }
}
