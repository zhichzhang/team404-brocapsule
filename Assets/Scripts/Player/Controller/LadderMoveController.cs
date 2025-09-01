using UnityEngine;

public class LadderMoveController
{
    Player player;

    public LadderMoveController(Player _player)
    {
        player = _player;
        player.ladderRemountCoolDownTimer = new Timer();
        player.TimerCountDownCtrl.register(player.ladderRemountCoolDownTimer);
    }

    public void climbLadder()
    {
        player.rb.gravityScale = 0f;
        player.rb.linearVelocityY = 0;
        player.JumpCtrl.ResetCounter(2);
        player.rb.linearVelocity = new Vector2(0,player.rb.linearVelocity.y);
        player.weapon0.DeactivateWeapon();
    }

    public void leaveLadder()
    {
        player.rb.gravityScale = player.gravityScale;
        player.rb.linearVelocityY = 0;
        
        player.weapon0.ActivateWeapon();
        player.weapon0.Equip();
        
    }


}
