using Unity.AppUI.UI;
using UnityEngine;

public class RollController
{
    public Player player;
    public Timer rollCoolDownTimer;
    public Timer rollDurationTimer;

    public RollController(Player player)
    {
        this.player = player;
        rollCoolDownTimer = new Timer();
        rollDurationTimer = new Timer();
        player.rollCoolDownTimer = rollCoolDownTimer;
        player.rollDurationTimer = rollDurationTimer;
        player.TimerCountDownCtrl.register(rollCoolDownTimer);
        player.TimerCountDownCtrl.register(rollDurationTimer);
    }

    public void Prep()
    {
        rollCoolDownTimer.Set(player.rollCoolDown);
        rollDurationTimer.Set(player.rollDuration);
        player.GoInvincible(player.rollInvincibleDuration);
    }
    public void Rolling()
    {
        player.rb.linearVelocity = new Vector2(player.rollSpeed * player.facingDir, player.rb.linearVelocity.y);
        
    }

    public void Dashing()
    {
        player.rb.linearVelocity = new Vector2(player.rollSpeed * player.facingDir, 0);
        
    }
}
