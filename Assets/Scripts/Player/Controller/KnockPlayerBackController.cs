using UnityEngine;

public class KnockPlayerBackController
{
    public Player player;
    public Timer timer;
    public Vector2 direction;

    public KnockPlayerBackController(Player player)
    {
        this.player = player;
        timer = new Timer();
        player.TimerCountDownCtrl.register(timer);
    }
    public void ApplyKnockback()
    {
        //TODO, parameterize attackForce and make different knockback given different attack force
        //float attackForce = 10f;
        //// Calculate the knockback force
        //Vector2 knockbackForce = attackDirection.normalized * attackForce * player.knockbackForceMultiplier;

        ////Set Speed to 0 before applyforce;
        //player.AirMoveCtrl.Freeze();
        //player.rb.AddForce(knockbackForce, ForceMode2D.Impulse);

        direction = (player.transform.position - player.trigger.transform.position).normalized;
        timer.Set(player.KnockBackDuration);
        player.GoInvincible(player.KnockBackDuration);
        player.rb.linearVelocity = new Vector2(direction.x * player.knockbackForceMultiplier, direction.y * player.knockbackForceMultiplier);
    }
}
