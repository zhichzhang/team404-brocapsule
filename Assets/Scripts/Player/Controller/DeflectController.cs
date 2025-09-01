using UnityEngine;

public class DeflectController
{
    Player player;
    //public Timer timer;

    public DeflectController(Player player)
    {
        this.player = player;
        //timer = new Timer();
        //player.deflectTimer = timer;
        //player.TimerCountDownCtrl.register(timer);
        player.deflectSignal = 0;
        player.deflectCoolDownTimer = new Timer();
        player.TimerCountDownCtrl.register(player.deflectCoolDownTimer);
    }

    public void Deflect()
    {
        player.deflectBox.SetActive(true);
        //timer.Set(player.deflectDuration);
    }

    public void Bump()
    {
        player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, player.deflectJumpSpeed);
        player.JumpCounter = 1;
    }

    public void DefelectOver()
    {
        player.deflectBox.SetActive(false);
    }

}
