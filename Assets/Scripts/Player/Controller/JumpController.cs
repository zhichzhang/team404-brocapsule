using UnityEngine;

public class JumpController
{
    public Player player;
    public JumpController(Player player)
    {
        this.player = player;
        player.JumpCounter = 2;
    }

    public void ResetCounter(int x)
    {
        player.JumpCounter = x;
    }

    public void Bump()
    {
        player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, player.JumpInitialSpeed);
        player.JumpCounter--;
    }

}
