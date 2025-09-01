using UnityEngine;

public class OnFlyableController
{
    Player player;
    public bool OnFlyingPlatform => player.transform.parent != null;

    public OnFlyableController(Player player)
    {
        this.player = player;
    }

    public void Still()
    {
        Vector2 parentSpeed = player.transform.parent.GetComponent<Rigidbody2D>().linearVelocity;
        player.rb.linearVelocity = parentSpeed;
    }
}
