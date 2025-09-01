using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    bool waiting;
    // Start is called before the first frame update
    public static TimeManager instance;
 
    private bool isStopped = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    private void Start()
    {
       
    }
    public void SlowTime(float pauseDuration, float scale)
    {   if (waiting)
        {
            return;
        }
        Time.timeScale = scale;
        StartCoroutine(Wait(pauseDuration));
    }
    IEnumerator Wait(float pauseDuration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(pauseDuration);
        Time.timeScale = 1.0f;
        waiting = false;
    }
    
    // Stop or continue time.
    public void ToggleTimeStop() {
        if (isStopped) {
            SetTimeScale(1);
        } else
        {
            SetTimeScale(0);
        }
        isStopped = !isStopped;
    }


    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }


}
