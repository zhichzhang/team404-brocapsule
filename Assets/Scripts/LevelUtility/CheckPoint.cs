using Newtonsoft.Json.Bson;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isReached;
    SpriteRenderer sr;
    public bool enableDash = true;
    public bool enableComboAttack = true;
    // string id should follow the following rule: x_y where x is the level index and y is the checkpoint index within the levels
    public string id;

    private void Awake()
    {
        isReached = false;
    }
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        Color color = sr.color;
        color.a = 0.3f; // Set transparency (0 = fully transparent, 1 = fully opaque)
        sr.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isReached)
        {

            Color color = sr.color;
            color.a = 1f; // Set transparency (0 = fully transparent, 1 = fully opaque)
            sr.color = color;

            isReached = true;

            // send google form, clear form if needed
            if(ExternalDataManager.instance != null)
            {
                ExternalDataManager.instance.NewCheckPointReached(transform.position);
            }

            // Send last good position to player
            PlayerInfo.instance.player.LastGoodPosition = transform.position;
            PlayerInfo.instance.player.Health = PlayerInfo.instance.player.MaxHealth;
            PlayerInfo.instance.player.canDash = enableDash;
            PlayerInfo.instance.player.canComboAttack = enableComboAttack;

            // For heatmap analysis
            HeatmapAnalyzer.Instance.isCheckPointCompleted(this);
        }
    }
}
