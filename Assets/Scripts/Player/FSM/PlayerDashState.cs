using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(ExternalDataManager.instance != null)
        {
            ExternalDataManager.instance.PlayerDodge();
        }
        player.anim.SetBool(animBoolName, true);
        input.isRollBuffered = false;
        player.RollCtrl.Prep();
        player.rb.gravityScale = 0;
    }

    public override void Exit()
    {
        player.rb.gravityScale = player.gravityScale;
        player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocity.y);
        player.anim.SetBool(animBoolName, false);
        base.Exit();
        
    }

    public override bool Update()
    {
        if (base.Update())
        {
            return true;
        }

        player.RollCtrl.Dashing();

        // dash => fall
        if (player.RollCtrl.rollDurationTimer.TimeUp())
        {
            stateMachine.ChangeState(player.fallState);
            return true;
        }
        return false;
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

}
