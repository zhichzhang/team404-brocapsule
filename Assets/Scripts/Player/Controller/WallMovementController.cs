using UnityEngine;

public class WallMovementController
{
    public Player player;
    public Timer wallJumpFreezeTimer;


    public WallMovementController(Player player)
    {
        this.player = player;
        wallJumpFreezeTimer = new Timer();
        player.wallJumpFreezeTimer = wallJumpFreezeTimer;
        player.TimerCountDownCtrl.register(wallJumpFreezeTimer);
    }
    public void Prep()
    {
        player.FlipCtrl.Flip();
        wallJumpFreezeTimer.Set(player.wallJumpFreeze);
    }
    public void Slide()
    {
        player.rb.linearVelocity = new Vector2(0, -player.wallSlideSpeed);
    }

    public void Bump()
    {
        player.rb.linearVelocity = new Vector2(player.facingDir * player.wallJumpSpeedX ,player.wallJumpSpeedY);
    }
}
