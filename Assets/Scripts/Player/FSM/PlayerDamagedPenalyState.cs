using Unity.VisualScripting;
using UnityEngine;

public class PlayerDamagedPenalyState : PlayerState
{
    public PlayerDamagedPenalyState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Lock stateMachine, release until timer is up (player will not be able to control)
        player.stateMachine.stateLocked = true;
        //lose health
        //TODO: Mechanism to get How much health to loose;
        player.HealthCtrl.LoseHealth(player.trigger.GetComponent<EnemyCanDoDamage>().HealthLost());

        player.KnockBackCtrl.ApplyKnockback();
        // change player color
        //player.Bleeding.color = Color.red;
    }

    public override void Exit()
    {
        base.Exit();
        // Freeze Player
        player.GroundMoveCtrl.Freeze();
        // change player color
        //player.Bleeding.color = Color.gray;
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override bool Update()
    {
        if (base.Update())
        {
            return true;
        }

        // knockBack => idle
        if (player.KnockBackCtrl.timer.TimeUp())
        {
            player.stateMachine.stateLocked = false;
            stateMachine.ChangeState(player.fallState);
            return true;
        }
        return false;
    }
}
