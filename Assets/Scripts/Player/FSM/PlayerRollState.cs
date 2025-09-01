using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerState
{
    public PlayerRollState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.anim.SetBool(animBoolName, true);
        input.isRollBuffered = false;
        player.RollCtrl.Prep();
        if(ExternalDataManager.instance != null)
        {
            ExternalDataManager.instance.PlayerDodge();
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.linearVelocity = new Vector2(0,player.rb.linearVelocity.y);
        player.anim.SetBool(animBoolName, false);

    }

    public override bool Update()
    {
        if (base.Update())
        {
            return true;
        }

        player.RollCtrl.Rolling();

        //roll => idle
        if (player.RollCtrl.rollDurationTimer.TimeUp())
        {
            stateMachine.ChangeState(player.idleState);
            return true;
        }
        return false;
        
    }

}
