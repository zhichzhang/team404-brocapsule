using UnityEngine;
using UnityEngine.Splines;

public class AnimateOnEnable : MonoBehaviour
{
    public SplineAnimate splineAni;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        splineAni.Restart(true);
    }
}
