using System.Collections.Generic;
using UnityEngine;

public class AnalyticGoogleForm : MonoBehaviour
{
    // new matrix,
    // 1. time spend between each checkpoint
    // 2. death count and death location at each checkpoint
    // 3. checkpoint complete in general (how many checkpoint are completed) (or last check point)

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [SerializeField] private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSeY4A1l1x3K3anho5MYdgZsPemq6980CobLuXJ0fFmSzpvOiQ/formResponse";
    public static AnalyticGoogleForm instance;
    private long _sessionID;
    public CheckPoint lastCheckPoint;
    public float checkPointTimer;
    public List<Vector2> deathLocations = new List<Vector2>();
    private const int MaxDeathLocations = 50;
    // deatch locations
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkPointTimer += Time.deltaTime;
    }

    public void AddDeath()
    {
        // compute player reative position to checkpoint
        if (lastCheckPoint != null)
        {
            Vector2 relativePosition = PlayerInfo.instance.player.transform.position - lastCheckPoint.transform.position;
            deathLocations.Add(relativePosition);
            if (deathLocations.Count > MaxDeathLocations)
            {
                deathLocations.RemoveAt(0);
            }
        }
        else
        {
            Debug.LogWarning("Last checkpoint is not set. Cannot add death location.");
        }
    }

    public void ResetTimer()
    {
        checkPointTimer = 0;
    }

    public void ResetDeath()
    {
        deathLocations.Clear();
    }
}
