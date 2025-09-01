using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static PlayerInfo instance;
    public Player player;
    public Vector2 playerPosition;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
    }
}
